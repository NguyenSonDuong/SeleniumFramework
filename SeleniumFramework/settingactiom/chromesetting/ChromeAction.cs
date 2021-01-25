using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AmazonSaveAcc.actionmain
{
    public delegate void ActionRunHandle();
    public class ChromeAction
    {
        private ChromeSetting chromeSetting;

        private event ErrorHandle errorEvent;
        private event ProcessHandle processEvent;
        private event SuccessHandle successEvent;

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

        public void Init(String exe, String profile, String pathChromeDriver, bool isImage = true)
        {
            try
            {
                chromeSetting = ChromeSetting.Build(exe, profile, pathChromeDriver,isImage);
                chromeSetting.Js = chromeSetting.ChromeDriver;
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }

        }
        public void GoToURL(String url)
        {
            try
            {
                chromeSetting.ChromeDriver.Navigate().GoToUrl(url);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void ClickClass(String className)
        {
            try
            {
                chromeSetting.ChromeDriver.FindElementByClassName(className).Click();

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void ClickClass(String className, int pos)
        {
            try
            {
                chromeSetting.ChromeDriver.FindElementsByClassName(className)[pos].Click();

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void ClickXPath(String className)
        {
            try
            {
                chromeSetting.ChromeDriver.FindElementByXPath(className).Click();

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void ClickXPath(String className, int pos)
        {
            try
            {
                chromeSetting.ChromeDriver.FindElementsByXPath(className)[pos].Click();

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void ClickXPath(String className, String stringSame, bool isExactly)
        {
            try
            {
                foreach(IWebElement item in chromeSetting.ChromeDriver.FindElementsByXPath(className))
                {
                    if (isExactly)
                    {
                        if (item.Text.Trim().Equals(stringSame))
                        {
                            item.Click();
                            return;
                        }
                    }
                    else
                    {
                        if (item.Text.Trim().Contains(stringSame))
                        {
                            item.Click();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void ClickID(String className)
        {
            try
            {
                chromeSetting.ChromeDriver.FindElementById(className).Click();

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void ScollMovieAction(int from, int to, int jump = 20, int delay = 10, ActionRunHandle actionRunHandle = null)
        {
            if (to >= from)
            {
                for (int i = from; i < to; i += jump)
                {
                    processEvent("Vị trí:" + i, this);
                    chromeSetting.Js.ExecuteScript($"window.scrollTo(0,{i});");
                    if (actionRunHandle != null)
                        actionRunHandle();
                    Thread.Sleep(delay);
                }
            }
            else
            {
                for (int i = from; i >= to; i -= jump)
                {
                    processEvent("Vị trí:" + i, this);
                    chromeSetting.Js.ExecuteScript($"window.scrollTo(0,{i});");
                    if (actionRunHandle != null)
                        actionRunHandle();
                    Thread.Sleep(delay);
                }
            }
        }
        public void SendKeyClass(String className, String mess)
        {
            try
            {
                chromeSetting.ChromeDriver.FindElementByClassName(className).Clear();
                chromeSetting.ChromeDriver.FindElementByClassName(className).SendKeys(mess);

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void SendKeyName(String Name, String mess)
        {
            try
            {
                chromeSetting.ChromeDriver.FindElementByName(Name).Clear();
                chromeSetting.ChromeDriver.FindElementByName(Name).SendKeys(mess);

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void SendKeyXPath(String xPath, String mess)
        {
            try
            {
                chromeSetting.ChromeDriver.FindElementByXPath(xPath).Clear();
                chromeSetting.ChromeDriver.FindElementByXPath(xPath).SendKeys(mess);

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void SendKeyID(String id, String mess)
        {
            try
            {
                chromeSetting.ChromeDriver.FindElementById(id).Clear();
                chromeSetting.ChromeDriver.FindElementById(id).SendKeys(mess);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void SendKeyClass(String className, String[] mess)
        {
            try
            {
                ReadOnlyCollection<IWebElement> webs = chromeSetting.ChromeDriver.FindElementsByClassName(className);
                int i = 0;
                foreach (IWebElement item in webs)
                {
                    if (String.IsNullOrEmpty(mess[i]))
                    {
                        i++;
                        continue;
                    }
                    item.Clear();
                    item.SendKeys(mess[i]);
                    i++;
                }
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }

        }
        public void SendKeyName(String name, String[] mess)
        {
            try
            {
                ReadOnlyCollection<IWebElement> webs = chromeSetting.ChromeDriver.FindElementsByName(name);
                int i = 0;
                foreach (IWebElement item in webs)
                {
                    if (String.IsNullOrEmpty(mess[i]))
                    {
                        i++;
                        continue;
                    }
                    item.Clear();
                    item.SendKeys(mess[i]);
                    i++;
                }
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }

        }
        public void SendKeyXPath(String xPath, String[] mess)
        {
            try
            {
                ReadOnlyCollection<IWebElement> webs = chromeSetting.ChromeDriver.FindElementsByXPath(xPath);
                int i = 0;
                foreach (IWebElement item in webs)
                {
                    if (String.IsNullOrEmpty(mess[i]))
                    {
                        i++;
                        continue;
                    }
                    item.Clear();
                    item.SendKeys(mess[i]);
                    i++;
                }
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }


        }
        public bool CheckClass(String className, String mess)
        {
            try
            {
                return chromeSetting.ChromeDriver.FindElementByClassName(className).Text.Equals(mess);
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool CheckName(String Name, String mess)
        {
            try
            {
                return chromeSetting.ChromeDriver.FindElementByClassName(Name).Text.Equals(mess);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CheckXPath(String xPath, String mess)
        {
            try
            {
                return chromeSetting.ChromeDriver.FindElementByXPath(xPath).Text.Equals(mess);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CheckXPath(String xPath)
        {
            try
            {
                chromeSetting.ChromeDriver.FindElementByXPath(xPath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CheckID(String id, String mess)
        {
            try
            {
                return chromeSetting.ChromeDriver.FindElementById(id).Text.Equals(mess);
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public Point GetPositionOfElement(String xPath)
        {
            try
            {
                return chromeSetting.ChromeDriver.FindElementByXPath(xPath).Location;

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
            }
            return new Point();
        }
        public Point GetPositionOfElement(String xPath, int pos)
        {
            try
            {
                return chromeSetting.ChromeDriver.FindElementsByXPath(xPath)[pos].Location;
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
            }
            return new Point();
        }
        public Point GetPositionOfElementClass(String className, int pos)
        {
            try
            {
                return chromeSetting.ChromeDriver.FindElementsByClassName(className)[pos].Location;
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
            }
            return new Point();

        }
        public void ScollMovie(int from, int to)
        {
            try
            {
                if (to >= from)
                {
                    for (int i = from; i < to; i += 20)
                    {
                        chromeSetting.Js.ExecuteScript($"window.scrollTo(0,{i});");
                        Thread.Sleep(10);
                    }
                }
                else
                {
                    for (int i = from; i >= to; i -= 20)
                    {
                        chromeSetting.Js.ExecuteScript($"window.scrollTo(0,{i});");
                        Thread.Sleep(10);
                    }
                }
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }


        }
        public void Presious()
        {
            try
            {
                chromeSetting.ChromeDriver.ExecuteScript("window.history.go(-1)");
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }

        }
        public int getScrollHeight()
        {
            try
            {

                return Int32.Parse(chromeSetting.Js.ExecuteScript("return document.body.scrollHeight;") + "");
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
        public int GetScollPosition()
        {
            try
            {
                int pos = Int32.Parse(chromeSetting.Js.ExecuteScript($"return window.scrollY;") + "");
                Thread.Sleep(100);
                return pos;

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
        public void ClickByJS(String name, int pos, int type)
        {
            try
            {
                switch (type)
                {
                    case 1:
                        chromeSetting.ChromeDriver.ExecuteScript($"document.getElementsByClassName('{name}')[{pos}].click();");
                        break;
                    case 2:
                        chromeSetting.ChromeDriver.ExecuteScript($"document.getElementById('{name}').click();");
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void MovieTo(String classname, int pos)
        {
            try
            {
                Actions actions = new Actions(chromeSetting.ChromeDriver);
                actions.MoveToElement(chromeSetting.ChromeDriver.FindElementsByClassName(classname)[pos]).Build().Perform();

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void MovieToXPath(String xPath, int pos)
        {
            try
            {

                Actions actions = new Actions(chromeSetting.ChromeDriver);
                actions.MoveToElement(chromeSetting.ChromeDriver.FindElementsByXPath(xPath)[pos]).Build().Perform();
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public String GetURL()
        {
            try
            {

                return chromeSetting.ChromeDriver.Url;
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
        public int GetLengthElementXPath(String xPath)
        {
            try
            {

                return chromeSetting.ChromeDriver.FindElementsByXPath(xPath).Count;
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
        public int GetLengthElementClass(String className)
        {
            try
            {

                return chromeSetting.ChromeDriver.FindElementsByClassName(className).Count;
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
    }
}
