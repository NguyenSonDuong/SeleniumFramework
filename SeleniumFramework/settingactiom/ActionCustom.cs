using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
    public class ActionCustom
    {
        public static String PATH_SAVE_LOG = AppDomain.CurrentDomain.BaseDirectory+ "\\log.ini";
        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool turnon);

        
        public static void Invoker(Control control, RunInvoker runInvoker)
        {
            control.Invoke(new MethodInvoker(runInvoker));
        }
        public static void RemoveAllEvent(Object obj,String field)
        {
            FieldInfo f1 = typeof(Object).GetField(field,
           BindingFlags.Static | BindingFlags.NonPublic);

            object obj2 = f1.GetValue(obj);
            PropertyInfo pi = obj.GetType().GetProperty("Events",
                BindingFlags.NonPublic | BindingFlags.Instance);

            EventHandlerList list = (EventHandlerList)pi.GetValue(obj, null);
            list.RemoveHandler(obj2, list[obj2]);
        }
        public static void AddLogToRicText(RichTextBox richText,String mess,Color color)
        {
            Invoker(richText, () =>
            {
                richText.SelectionColor = color;
                richText.AppendText(DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString() + " :" + mess +"\n");
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
            foreach(String it in list)
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
                        property.SetValue(item, value[1]+"", null);
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
        public static void Copy(String pathSource,String pathEnd)
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
        
        public static void WriteListToFile(String path, Object[] listObject, String split = "\n", bool isAppend = false)
        {
            try
            {
                if(!isAppend)
                    File.WriteAllText(path, "");
                foreach (Object item in listObject)
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
                File.AppendAllText(PATH_SAVE_LOG, DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + mess +"\n");
            }catch(Exception ex)
            {
                errorHandle(ex, "", 900);
            }
        }

        public static Thread MakeThread(ThreadStart runInvoker)
        {
            Thread thread = new Thread(runInvoker);
            thread.IsBackground = true;
            thread.Start();
            return thread;
        }
    }
}
