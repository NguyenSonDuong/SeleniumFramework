using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmazonSaveAcc.actionmain
{
    public delegate void RunInvoker();
    public class ActionCustom
    {
        public static String PATH_SAVE_LOG = "log.ini";
        public static void Invoker(Control control, RunInvoker runInvoker)
        {
            control.Invoke(new MethodInvoker(runInvoker));
        }

        public static void AddLogToRicText(RichTextBox richText,String mess,Color color)
        {
            Invoker(richText, () =>
            {
                richText.SelectionColor = color;
                richText.AppendText(DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString() + " :" + mess +"\n");
            });
        }
        public static void WriteListToFile(String path, Object[] listObject, String split = "\n")
        {
            try
            {
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
