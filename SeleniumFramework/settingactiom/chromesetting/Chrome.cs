using AmazonSaveAcc.actionmain;
using SeleniumFramework.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleniumFramework.settingactiom.chromesetting
{
    public class Chrome : IActionSelenium
    {
        private ChromeSetting chromeSetting;
        private bool isCleanInput = true;
        private event ErrorHandle errorEvent;
        private event ProcessHandle processEvent;
        private event SuccessHandle successEvent;
        private bool isStop = false;
        private List<StartActionDriver> listAction;

        public event ErrorHandle ErrorEvent
        {
            add { this.errorEvent += value; }
            remove { this.errorEvent -= value; }
        }
        public event ProcessHandle ProcessEvent
        {
            add { this.processEvent += value; }
            remove { this.processEvent -= value; }
        }
        public event SuccessHandle SuccessEvent
        {
            add { this.successEvent += value; }
            remove { this.successEvent -= value; }
        }


        public ChromeSetting ChromeSetting { get => chromeSetting; set => chromeSetting = value; }
        public bool IsCleanInput { get => isCleanInput; set => isCleanInput = value; }
        public List<StartActionDriver> ListAction { get => listAction; set => listAction = value; }
        public bool IsStop { get => isStop; set => isStop = value; }

        public Chrome()
        {
            chromeSetting = new ChromeSetting();
            ListAction = new List<StartActionDriver>();
        }

        public void SetCloseWhenFormClosing(Form form)
        {
            form.FormClosing += (obj, send) =>
            {
                chromeSetting.CloseChrome();
            };
        }
        public Thread StartThread(StartActionDriver startActionChrome, int delay = 1000)
        {
            return ActionCustom.MakeThread(() =>
            {
                try
                {
                    chromeSetting.BuildChrome();
                    if (startActionChrome != null)
                    {
                        startActionChrome(chromeSetting, this);
                    }
                    foreach (StartActionDriver startAction in ListAction)
                    {
                        startAction();
                        Thread.Sleep(delay);
                    }
                    if (isStop)
                        chromeSetting.CloseChrome();

                }
                catch (Exception ex)
                {
                    if (errorEvent != null)
                        errorEvent(ex, this, 100);
                    else
                        throw ex;
                }
            });
        }
        public void ExcuteJS(String js)
        {
            try
            {
                chromeSetting.ChromeDriver.ExecuteScript(js);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void StartNoThread()
        {
            try
            {
                chromeSetting.BuildChrome();
                foreach (StartActionDriver startAction in ListAction)
                {
                    startAction();
                }
                if (isStop)
                    chromeSetting.CloseChrome();
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public Thread StartThread()
        {
            return ActionCustom.MakeThread(() =>
            {
                try
                {
                    chromeSetting.BuildChrome();
                    foreach (StartActionDriver startAction in ListAction)
                    {
                        startAction();
                    }
                    if (isStop)
                        chromeSetting.CloseChrome();
                }
                catch (Exception ex)
                {
                    if (errorEvent != null)
                        errorEvent(ex, this, 100);
                    else
                        throw ex;
                }
            });
        }
        public void Start()
        {
            try
            {
                chromeSetting.BuildChrome();
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void Init()
        {
            ActionCustom.MakeThread(() =>
            {
                try
                {
                    chromeSetting = ChromeSetting.Build();
                }
                catch (Exception ex)
                {
                    if (errorEvent != null)
                        errorEvent(ex, this, 100);
                    else
                        throw ex;
                }
            });

        }


        public void GoToUrl(String url)
        {
            try
            {
                chromeSetting.GoToUrl(url);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void ClickClass(string classname, int location)
        {
            try
            {
                chromeSetting.Click(classname, TypeElement.CLASS, location);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickClass(string classname)
        {
            try
            {
                chromeSetting.Click(classname, TypeElement.CLASS);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickClass(string classname, string message)
        {
            try
            {
                chromeSetting.Click(classname, TypeElement.CLASS, message);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickID(string id)
        {
            try
            {
                chromeSetting.Click(id, TypeElement.ID);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickXpath(string xpath, int location)
        {
            try
            {
                chromeSetting.Click(xpath, TypeElement.XPATH, location);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickXpath(string xpath)
        {
            try
            {
                chromeSetting.Click(xpath, TypeElement.XPATH);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickXpath(string xpath, string message)
        {
            try
            {
                chromeSetting.Click(xpath, TypeElement.XPATH, message);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickName(string name, int location)
        {
            try
            {
                chromeSetting.Click(name, TypeElement.NAME, location);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickName(string name)
        {
            try
            {
                chromeSetting.Click(name, TypeElement.NAME);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickName(string name, string message)
        {
            try
            {
                chromeSetting.Click(name, TypeElement.NAME, message);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickJS(string js)
        {
            try
            {
                chromeSetting.Click(js, TypeElement.JS);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickByJS(string js)
        {
            try
            {
                chromeSetting.Click(js, TypeElement.JS2);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyClass(string message, string classname, int location)
        {
            try
            {
                chromeSetting.SendKey(classname, TypeElement.CLASS, message, location);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyClass(string message, string classname)
        {
            try
            {
                chromeSetting.SendKey(classname, TypeElement.CLASS, message);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyID(string message, string id)
        {
            try
            {
                chromeSetting.SendKey(id, TypeElement.ID, message);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyXpath(string message, string xpath, int location)
        {
            try
            {
                chromeSetting.SendKey(xpath, TypeElement.XPATH, message, location);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyXpath(string message, string xpath)
        {
            try
            {
                chromeSetting.SendKey(xpath, TypeElement.XPATH, message);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyName(string message, string name, int location)
        {
            try
            {
                chromeSetting.SendKey(name, TypeElement.NAME, message, location);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyName(string message, string name)
        {
            try
            {
                chromeSetting.SendKey(name, TypeElement.NAME, message);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyJS(string message, string js)
        {
            try
            {
                chromeSetting.SendKey(js, TypeElement.JS, message);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyByJS(string message, string js)
        {
            try
            {
                chromeSetting.SendKey(js, TypeElement.JS2);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyClass(string message, string classname, int location, int timeDelay)
        {
            try
            {
                chromeSetting.WriteKey(classname, TypeElement.CLASS, message, timeDelay, location);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyClass(string message, string classname, int timeDelay)
        {
            try
            {
                chromeSetting.WriteKey(classname, TypeElement.CLASS, message, timeDelay);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyID(string message, string id, int timeDelay)
        {
            try
            {
                chromeSetting.WriteKey(id, TypeElement.ID, message, timeDelay);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyXpath(string message, string xpath, int location, int timeDelay)
        {
            try
            {
                chromeSetting.WriteKey(xpath, TypeElement.XPATH, message, timeDelay, location);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyXpath(string message, string xpath, int timeDelay)
        {
            try
            {
                chromeSetting.WriteKey(xpath, TypeElement.CLASS, message, timeDelay);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyName(string message, string name, int location, int timeDelay)
        {
            try
            {
                chromeSetting.WriteKey(name, TypeElement.NAME, message, timeDelay, location);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyName(string message, string name, int timeDelay)
        {
            try
            {
                chromeSetting.WriteKey(name, TypeElement.NAME, message, timeDelay);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyJS(string message, string js, int timeDelay)
        {
            try
            {
                chromeSetting.WriteKey(js, TypeElement.JS, message, timeDelay);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyByJS(string message, string js, int timeDelay)
        {
            try
            {
                throw new Exception("Not support");
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public string GetTextClass(string classname, int location)
        {
            try
            {
                return chromeSetting.GetText(classname, TypeElement.CLASS, location);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";
        }

        public string GetTextClass(string classname)
        {
            try
            {
                return chromeSetting.GetText(classname, TypeElement.CLASS);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";
        }


        public string GetTextID(string id)
        {
            try
            {
                return chromeSetting.GetText(id, TypeElement.ID);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";
        }

        public string GetTextXpath(string xpath, int location)
        {
            try
            {
                return chromeSetting.GetText(xpath, TypeElement.XPATH, location);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";
        }

        public string GetTextXpath(string xpath)
        {
            try
            {
                return chromeSetting.GetText(xpath, TypeElement.XPATH);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";

        }


        public string GetTextName(string name, int location)
        {
            try
            {
                return chromeSetting.GetText(name, TypeElement.NAME, location);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";

        }

        public string GetTextName(string name)
        {
            try
            {
                return chromeSetting.GetText(name, TypeElement.NAME);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";

        }


        public string GetTextJS(string js)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";

        }

        public string GetTextByJS(string js)
        {
            try
            {
                return chromeSetting.GetText(js, TypeElement.JS);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";

        }


        public int GetQuantityClass(string classname)
        {
            try
            {
                return chromeSetting.GetQuantity(classname, TypeElement.CLASS);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return -1;
        }

        public int GetQuantityXpath(string xpath)
        {
            try
            {
                return chromeSetting.GetQuantity(xpath, TypeElement.XPATH);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return -1;
        }

        public int GetQuantityName(string name)
        {
            try
            {
                return chromeSetting.GetQuantity(name, TypeElement.NAME);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return -1;
        }

        public bool CheckClass(string classname)
        {
            try
            {
                chromeSetting.CheckElement(classname, TypeElement.CLASS);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return false;
        }

        public bool CheckXpath(string xpath)
        {
            try
            {
                chromeSetting.CheckElement(xpath, TypeElement.XPATH);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return false;
        }

        public bool CheckID(string id)
        {
            try
            {
                chromeSetting.CheckElement(id, TypeElement.ID);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return false;
        }

        public bool CheckName(string name)
        {
            try
            {
                chromeSetting.CheckElement(name, TypeElement.NAME);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return false;
        }

        public string GetAttrXpath(string attr, string xpath, int location)
        {
            try
            {
                chromeSetting.GetAttr(xpath, TypeElement.XPATH, attr, location);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";
        }

        public string GetAttrXpath(string attr, string xpath)
        {
            try
            {
                chromeSetting.GetAttr(xpath, TypeElement.XPATH, attr);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";
        }


        public string GetAttrName(string attr, string name, int location)
        {
            try
            {
                chromeSetting.GetAttr(name, TypeElement.NAME, attr, location);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";
        }

        public string GetAttrName(string attr, string name)
        {
            try
            {
                chromeSetting.GetAttr(name, TypeElement.NAME, attr);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";
        }


        public string GetAttrClass(string attr, string classname, int location)
        {
            try
            {
                chromeSetting.GetAttr(classname, TypeElement.CLASS, attr, location);

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";
        }

        public string GetAttrClass(string attr, string classname)
        {
            try
            {
                chromeSetting.GetAttr(classname, TypeElement.CLASS, attr);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";
        }

        public string GetAttrID(string attr, string id)
        {
            try
            {
                chromeSetting.GetAttr(id, TypeElement.ID, attr);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return "";
        }

    }
}
