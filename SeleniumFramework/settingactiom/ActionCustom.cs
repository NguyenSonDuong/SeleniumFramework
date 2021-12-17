﻿using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmazonSaveAcc.actionmain
{
    public delegate void RunInvoker();
    public delegate void ActionRunHandle();
    public class ActionCustom
    {
        public static String PATH_SAVE_LOG = AppDomain.CurrentDomain.BaseDirectory + "\\log.ini";
        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool turnon);
        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        public static void Invoker(Control control, RunInvoker runInvoker)
        {
            control.Invoke(new MethodInvoker(runInvoker));
        }
        public static void RemoveAllEvent(Object obj, String field)
        {
            FieldInfo f1 = typeof(Object).GetField(field,
           BindingFlags.Static | BindingFlags.NonPublic);

            object obj2 = f1.GetValue(obj);
            PropertyInfo pi = obj.GetType().GetProperty("Events",
                BindingFlags.NonPublic | BindingFlags.Instance);
            EventHandlerList list = (EventHandlerList)pi.GetValue(obj, null);
            list.RemoveHandler(obj2, list[obj2]);
        }

        public static void ZipFile(String[] listPath, String pathOutput)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var file = archive.CreateEntry("background.js");
                    using (var entryStream = file.Open())
                    {
                        using (var streamWriter = new StreamWriter(entryStream))
                        {
                            streamWriter.Write(listPath[0]);
                        }
                    }

                    file = archive.CreateEntry("manifest.json");
                    using (var entryStream = file.Open())
                    {
                        using (var streamWriter = new StreamWriter(entryStream))
                        {
                            streamWriter.Write(listPath[1]);
                        }
                    }
                }
                using (var fileStream = new FileStream(pathOutput, FileMode.Create))
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.CopyTo(fileStream);
                }
            }
        }
        public static void AddLogToRicText(RichTextBox richText, String mess, Color color)
        {
            Invoker(richText, () =>
            {
                richText.SelectionColor = color;
                richText.AppendText(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " :" + mess + "\n");
            });
        }
        public static void SaveObject(String path, Object item, char split)
        {
            PropertyInfo[] propertyInfo = item.GetType().GetProperties();
            File.WriteAllText(path, "");
            foreach (PropertyInfo i in propertyInfo)
            {
                File.AppendAllText(path, i.Name + split + i.GetValue(item) + "\n");
            }
        }
        public static void SetValueObject(String path, Object item, char split)
        {
            List<String> list = ReadListToFile(path);
            foreach (String it in list)
            {
                String[] value = it.Split(split);
                if (value.Length < 2)
                {
                    continue;
                }
                PropertyInfo property = item.GetType().GetProperty(value[0]);
                switch (Type.GetTypeCode(property.PropertyType))
                {
                    case TypeCode.Boolean:
                        property.SetValue(item, bool.Parse(value[1]), null);
                        break;
                    case TypeCode.Int32:
                        property.SetValue(item, Int32.Parse(value[1]), null);
                        break;
                    case TypeCode.Int16:
                        property.SetValue(item, Int16.Parse(value[1]), null);
                        break;
                    case TypeCode.Int64:
                        property.SetValue(item, Int64.Parse(value[1]), null);
                        break;
                    case TypeCode.Double:
                        property.SetValue(item, Double.Parse(value[1]), null);
                        break;
                    case TypeCode.String:
                        property.SetValue(item, value[1] + "", null);
                        break;
                    case TypeCode.Object:
                        property.SetValue(item, null, null);
                        break;
                    default:
                        property.SetValue(item, null, null);
                        break;

                }

            }
        }
        public static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name));
            //Copy(file.FullName,Path.Combine(target.FullName, file.Name));
        }
        public static void Copy(String pathSource, String pathEnd)
        {
            using (var inputFile = new FileStream(
                pathSource,
                FileMode.Open, FileAccess.Read,
                FileShare.ReadWrite))
            {
                using (var outputFile = new FileStream(pathEnd, FileMode.Create,
                FileAccess.ReadWrite))
                {
                    var buffer = new byte[0x10000];
                    int bytes;
                    while ((bytes = inputFile.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        outputFile.Write(buffer, 0, bytes);
                    }
                }
            }
        }

        public static void WriteListToFile(String path, String[] listObject, String split = "\n", bool isAppend = false)
        {
            try
            {
                if (!isAppend)
                    File.WriteAllText(path, "");
                foreach (String item in listObject)
                {
                    if (!String.IsNullOrEmpty(item))
                        File.AppendAllText(path, item.ToString() + split);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void WriteListToFile(String path, object[] listObject, String split = "\n", bool isAppend = false)
        {
            try
            {
                if (!isAppend)
                    File.WriteAllText(path, "");
                foreach (object item in listObject)
                {
                    File.AppendAllText(path, item.ToString() + split);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<String> ReadListToFile(String path, char split1 = '\n')
        {
            try
            {
                List<String> listout = new List<string>();
                String[] listObject = File.ReadAllText(path).Split(split1);
                foreach (String item in listObject)
                {
                    if (!String.IsNullOrEmpty(item))
                        listout.Add(item);
                }
                return listout;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void AddLogToFile(String mess, ErrorHandle errorHandle)
        {
            try
            {
                File.AppendAllText(PATH_SAVE_LOG, DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + mess + "\n");
            }
            catch (Exception ex)
            {
                errorHandle(ex, "", 900);
            }
        }

        public static Thread MakeThread(ThreadStart runInvoker, bool isBackground = true)
        {
            Thread thread = new Thread(runInvoker);
            thread.IsBackground = isBackground;
            thread.Start();
            return thread;
        }


        public static SshClient SSHCoverSock5(string host, string username, string password)
        {
            SshClient _client = new SshClient(host, username, password);
            _client.Connect();
            Random random = new Random();
            uint port = (uint)random.Next(5000, 50000);
            
            ForwardedPortDynamic portD = new ForwardedPortDynamic("127.0.0.1", port);
            _client.AddForwardedPort(portD);
            if (_client.IsConnected)
            {
                if (portD.IsStarted)
                {
                    portD.Stop();
                }
                try
                {
                    portD.Start();
                    return _client;
                }
                catch
                {
                    return null;
                }
            }
            return _client;
        }
    }
}
