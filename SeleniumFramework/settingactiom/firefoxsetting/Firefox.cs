using AmazonSaveAcc.actionmain;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumFramework.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleniumFramework.settingactiom.firefoxsetting
{
    public class Firefox :IActionSelenium
    {
        private FirefoxSetting firefoxSettingCustion;
        private event ErrorHandle errorEvent;
        private event ProcessHandle processEvent;
        private event SuccessHandle successEvent;
        private int timeWait = 10;
        private List<StartActionDriver> listAction;
        public List<StartActionDriver> ListAction { get => listAction; set => listAction = value; }

        public FirefoxSetting FirefoxSettingCustion { get => firefoxSettingCustion; set => firefoxSettingCustion = value; }

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

        public void SetCloseWhenFormClosing(Form form)
        {
            form.FormClosing += (obj, send) =>
            {
                firefoxSettingCustion.CloseChrome();
            };
        }
        public Thread StartThread(StartActionDriver startActionChrome, int delay = 1000)
        {
            return ActionCustom.MakeThread(() =>
            {
                try
                {
                    firefoxSettingCustion.BuildFirefox();
                    if (startActionChrome != null)
                    {
                        startActionChrome(firefoxSettingCustion, this);
                    }
                    foreach (StartActionDriver startAction in ListAction)
                    {
                        startAction();
                        Thread.Sleep(delay);
                    }
                    firefoxSettingCustion.CloseChrome();

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
        public Thread StartThread()
        {
            return ActionCustom.MakeThread(() =>
            {
                try
                {
                    firefoxSettingCustion.BuildFirefox();
                    foreach (StartActionDriver startAction in ListAction)
                    {
                        startAction();
                    }
                    firefoxSettingCustion.CloseChrome();
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
                firefoxSettingCustion.BuildFirefox();
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
                    firefoxSettingCustion = FirefoxSetting.Build();
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
                firefoxSettingCustion.GoToUrl(url);
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
                firefoxSettingCustion.Click(classname, TypeElement.CLASS, location);
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
                firefoxSettingCustion.Click(classname, TypeElement.CLASS);
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
                firefoxSettingCustion.Click(classname, TypeElement.CLASS, message);
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
                firefoxSettingCustion.Click(id, TypeElement.ID);
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
                firefoxSettingCustion.Click(xpath, TypeElement.XPATH, location);
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
                firefoxSettingCustion.Click(xpath, TypeElement.XPATH);
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
                firefoxSettingCustion.Click(xpath, TypeElement.XPATH, message);
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
                firefoxSettingCustion.Click(name, TypeElement.NAME, location);
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
                firefoxSettingCustion.Click(name, TypeElement.NAME);
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
                firefoxSettingCustion.Click(name, TypeElement.NAME, message);
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
                firefoxSettingCustion.Click(js, TypeElement.JS);
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
                firefoxSettingCustion.Click(js, TypeElement.JS2);
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
                firefoxSettingCustion.SendKey(classname, TypeElement.CLASS, message, location);
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
                firefoxSettingCustion.SendKey(classname, TypeElement.CLASS, message);
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
                firefoxSettingCustion.SendKey(id, TypeElement.ID, message);
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
                firefoxSettingCustion.SendKey(xpath, TypeElement.XPATH, message, location);
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
                firefoxSettingCustion.SendKey(xpath, TypeElement.XPATH, message);
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
                firefoxSettingCustion.SendKey(name, TypeElement.NAME, message, location);
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
                firefoxSettingCustion.SendKey(name, TypeElement.NAME, message);
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
                firefoxSettingCustion.SendKey(js, TypeElement.JS, message);
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
                firefoxSettingCustion.SendKey(js, TypeElement.JS2);
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
                firefoxSettingCustion.WriteKey(classname, TypeElement.CLASS, message, timeDelay, location);
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
                firefoxSettingCustion.WriteKey(classname, TypeElement.CLASS, message, timeDelay);
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
                firefoxSettingCustion.WriteKey(id, TypeElement.ID, message, timeDelay);
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
                firefoxSettingCustion.WriteKey(xpath, TypeElement.XPATH, message, timeDelay, location);
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
                firefoxSettingCustion.WriteKey(xpath, TypeElement.CLASS, message, timeDelay);
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
                firefoxSettingCustion.WriteKey(name, TypeElement.NAME, message, timeDelay, location);
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
                firefoxSettingCustion.WriteKey(name, TypeElement.NAME, message, timeDelay);
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
                firefoxSettingCustion.WriteKey(js, TypeElement.JS, message, timeDelay);
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
                return firefoxSettingCustion.GetText(classname, TypeElement.CLASS, location);
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
                return firefoxSettingCustion.GetText(classname, TypeElement.CLASS);
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
                return firefoxSettingCustion.GetText(id, TypeElement.ID);
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
                return firefoxSettingCustion.GetText(xpath, TypeElement.XPATH, location);
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
                return firefoxSettingCustion.GetText(xpath, TypeElement.XPATH);
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
                return firefoxSettingCustion.GetText(name, TypeElement.NAME, location);
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
                return firefoxSettingCustion.GetText(name, TypeElement.NAME);
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
                return firefoxSettingCustion.GetText(js, TypeElement.JS);
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
                return firefoxSettingCustion.GetQuantity(classname, TypeElement.CLASS);
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
                return firefoxSettingCustion.GetQuantity(xpath, TypeElement.XPATH);
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
                return firefoxSettingCustion.GetQuantity(name, TypeElement.NAME);
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
                firefoxSettingCustion.CheckElement(classname, TypeElement.CLASS);
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
                firefoxSettingCustion.CheckElement(xpath, TypeElement.XPATH);
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
                firefoxSettingCustion.CheckElement(id, TypeElement.ID);
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
                firefoxSettingCustion.CheckElement(name, TypeElement.NAME);
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
                firefoxSettingCustion.GetAttr(xpath, TypeElement.XPATH, attr, location);
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
                firefoxSettingCustion.GetAttr(xpath, TypeElement.XPATH, attr);
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
                firefoxSettingCustion.GetAttr(name, TypeElement.NAME, attr, location);
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
                firefoxSettingCustion.GetAttr(name, TypeElement.NAME, attr);
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
                firefoxSettingCustion.GetAttr(classname, TypeElement.CLASS, attr, location);

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
                firefoxSettingCustion.GetAttr(classname, TypeElement.CLASS, attr);
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
                firefoxSettingCustion.GetAttr(id, TypeElement.ID, attr);
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