using AmazonSaveAcc.actionmain;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumFramework.settingactiom.firefoxsetting
{
    public class FireFoxAction
    {
        private FirefoxSetting firefoxOption;
        private event ErrorHandle errorEvent;
        private event ProcessHandle processEvent;
        private event SuccessHandle successEvent;
        private int timeWait = 10;

        public FirefoxSetting FirefoxOption { get => firefoxOption; set => firefoxOption = value; }

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

        public void Init(String exe, String profile, String pathChromeDriver, bool isHide = false)
        {
            try
            {
                firefoxOption = FirefoxSetting.Build(exe, profile, pathChromeDriver, isHide);
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
                firefoxOption.FirefoxDriver.Navigate().GoToUrl(url);
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
                IWebElement webElement = firefoxOption.FirefoxDriver.FindElementByClassName(className);
                webElement.Click();
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
                IWebElement webElement = firefoxOption.FirefoxDriver.FindElementsByClassName(className)[pos];
                webElement.Click();
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void ClickXPath(String xPath)
        {
            try
            {
                IWebElement webElement = firefoxOption.FirefoxDriver.FindElementByXPath(xPath);
                webElement.Click();
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void ClickXPathNo(String xPath)
        {
            try
            {
                firefoxOption.FirefoxDriver.FindElementByXPath(xPath).Click();
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void ClickXPath(string xPath, int pos)
        {
            try
            {
                firefoxOption.FirefoxDriver.FindElementsByXPath(xPath)[pos].Click();
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void ClickXPathNo(string xPath, int pos)
        {
            try
            {
                firefoxOption.FirefoxDriver.FindElementsByXPath(xPath)[pos].Click();
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void ClickXPath(String xPath, String stringSame, bool isExactly)
        {
            try
            {
                foreach (IWebElement item in firefoxOption.FirefoxDriver.FindElementsByXPath(xPath))
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
        public void ClickXPath(String xPath, String stringSame, bool isExactly, int quatity)
        {
            try
            {
                int qua = 0;
                foreach (IWebElement item in firefoxOption.FirefoxDriver.FindElementsByXPath(xPath))
                {
                    if (qua >= quatity)
                    {
                        break;
                    }
                    if (isExactly)
                    {
                        if (item.Text.Trim().Equals(stringSame))
                        {
                            item.Click();
                            qua++;
                        }
                    }
                    else
                    {
                        if (item.Text.Trim().Contains(stringSame))
                        {
                            item.Click();
                            qua++;
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
        public void ClickXPathPos(String xPath, String stringSame, bool isExactly, int pos)
        {
            try
            {
                int qua = 0;
                foreach (IWebElement item in firefoxOption.FirefoxDriver.FindElementsByXPath(xPath))
                {

                    if (isExactly)
                    {
                        if (item.Text.Trim().Equals(stringSame))
                        {
                            if (qua == pos)
                            {
                                item.Click();
                            }
                            qua++;
                        }
                    }
                    else
                    {
                        if (item.Text.Trim().Contains(stringSame))
                        {
                            if (qua == pos)
                            {
                                item.Click();
                            }
                            qua++;
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
        public String GetText(String xPath, int pos)
        {
            try
            {
                return firefoxOption.FirefoxDriver.FindElementsByXPath(xPath)[pos].Text;
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
        public void CleanAllName(String name, int pos = 0)
        {
            try
            {
                firefoxOption.FirefoxDriver.FindElementsByName(name)[pos].Clear();
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void CleanAllXPath(String xPath, int pos = 0)
        {
            try
            {
                firefoxOption.FirefoxDriver.FindElementsByXPath(xPath)[pos].Clear();
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void CleanAllID(String id, int pos = 0)
        {
            try
            {
                firefoxOption.FirefoxDriver.FindElementById(id).Clear();
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public List<String> GetAllText(String xPath, ProcessHandle processHandle = null)
        {
            List<String> listResult = new List<string>();
            try
            {
                foreach (IWebElement item in firefoxOption.FirefoxDriver.FindElementsByXPath(xPath))
                {
                    try
                    {
                        if (processHandle != null)
                            processHandle(item, this);
                        listResult.Add(item.Text);
                    }
                    catch (Exception ex)
                    {
                        if (errorEvent != null)
                            errorEvent(ex, this, 100);
                    }

                }
                return listResult;
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return listResult;
        }
        public List<String> GetAllAttr(String xPath, String attr, ProcessHandle processHandle = null)
        {
            List<String> listResult = new List<string>();
            try
            {
                foreach (IWebElement item in firefoxOption.FirefoxDriver.FindElementsByXPath(xPath))
                {
                    try
                    {
                        if (processHandle != null)
                            processHandle(item, this);
                        listResult.Add(item.GetAttribute(attr));
                    }
                    catch (Exception ex)
                    {
                        if (errorEvent != null)
                            errorEvent(ex, this, 100);
                    }
                }
                return listResult;
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return listResult;
        }
        public void GetAllAttr(String xPath, String attr, int start, ProcessHandle processHandle = null)
        {
            List<String> listResult = new List<string>();
            try
            {
                ReadOnlyCollection<IWebElement> listElement = firefoxOption.FirefoxDriver.FindElementsByXPath(xPath);
                if (start >= listElement.Count)
                    return;
                for (int i = start; i < listElement.Count; i++)
                {
                    if (i >= listElement.Count)
                        return;
                    try
                    {
                        if (processHandle != null)
                            processHandle(listElement[i], this);
                    }
                    catch (Exception ex)
                    {
                        if (errorEvent != null)
                            errorEvent(ex, this, 100);
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return;
        }

        public void ClickID(String id)
        {
            try
            {
                firefoxOption.FirefoxDriver.FindElementById(id).Click();
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
                    try
                    {
                        processEvent("Vị trí:" + i, this);
                        firefoxOption.FirefoxDriver.ExecuteScript($"window.scrollTo(0,{i});");
                        if (actionRunHandle != null)
                            actionRunHandle();
                        Thread.Sleep(delay);
                    }
                    catch (Exception ex)
                    {
                        if (errorEvent != null)
                            errorEvent(ex, this, 100);
                        else
                            throw ex;
                    }

                }
            }
            else
            {
                for (int i = from; i >= to; i -= jump)
                {
                    try
                    {
                        processEvent("Vị trí:" + i, this);
                        firefoxOption.FirefoxDriver.ExecuteScript($"window.scrollTo(0,{i});");
                        if (actionRunHandle != null)
                            actionRunHandle();
                        Thread.Sleep(delay);
                    }
                    catch (Exception ex)
                    {
                        if (errorEvent != null)
                            errorEvent(ex, this, 100);
                        else
                            throw ex;
                    }

                }
            }
        }
        public void SendKeyClass(String className, String mess)
        {
            try
            {
                firefoxOption.FirefoxDriver.FindElementByClassName(className).Clear();
                Thread.Sleep(100);
                firefoxOption.FirefoxDriver.FindElementByClassName(className).SendKeys(mess);
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
                firefoxOption.FirefoxDriver.FindElementByName(Name).Clear();
                Thread.Sleep(100);
                firefoxOption.FirefoxDriver.FindElementByName(Name).SendKeys(mess);
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


                firefoxOption.FirefoxDriver.FindElementByXPath(xPath).Clear();
                Thread.Sleep(100);
                firefoxOption.FirefoxDriver.FindElementByXPath(xPath).SendKeys(mess);

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyXPath(String xPath, int pos, String mess)
        {
            try
            {


                firefoxOption.FirefoxDriver.FindElementsByXPath(xPath)[pos].Clear();
                Thread.Sleep(100);
                firefoxOption.FirefoxDriver.FindElementsByXPath(xPath)[pos].SendKeys(mess);

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void SendKeyXPath(String xPath, String mess, int step)
        {
            try
            {
                firefoxOption.FirefoxDriver.FindElementByXPath(xPath).Clear();
                Thread.Sleep(100);
                for (int i = 0; i < mess.Length; i++)
                {
                    firefoxOption.FirefoxDriver.FindElementByXPath(xPath).SendKeys(mess[i] + "");
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
        public void SendKeyID(String id, String mess)
        {
            try
            {
                firefoxOption.FirefoxDriver.FindElementById(id).Click();
                Thread.Sleep(100);
                firefoxOption.FirefoxDriver.FindElementById(id).Clear();
                Thread.Sleep(100);
                firefoxOption.FirefoxDriver.FindElementById(id).SendKeys(mess);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void SendKeyIDNo(String id, String mess)
        {
            try
            {


                Thread.Sleep(100);
                firefoxOption.FirefoxDriver.FindElementById(id).SendKeys(mess);
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
                ReadOnlyCollection<IWebElement> webs = firefoxOption.FirefoxDriver.FindElementsByClassName(className);
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
                ReadOnlyCollection<IWebElement> webs = firefoxOption.FirefoxDriver.FindElementsByName(name);
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
        public void SendKeyXPath(String xPath, String[] mess, int step = 300)
        {
            try
            {
                ReadOnlyCollection<IWebElement> webs = firefoxOption.FirefoxDriver.FindElementsByXPath(xPath);
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
                    Thread.Sleep(step);
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
                return firefoxOption.FirefoxDriver.FindElementByClassName(className).Text.Equals(mess);
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
                return firefoxOption.FirefoxDriver.FindElementByClassName(Name).Text.Equals(mess);
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
                ReadOnlyCollection<IWebElement> list = firefoxOption.FirefoxDriver.FindElementsByXPath(xPath);
                foreach (IWebElement item in list)
                {
                    return firefoxOption.FirefoxDriver.FindElementByXPath(xPath).Text.Trim().Equals(mess);
                }

            }
            catch (Exception ex)
            {

            }
            return false;
        }//https://www.pinterest.cl/AshleyLive97/_created/
        public bool CheckXPath(String xPath, int pos)
        {
            try
            {
                ReadOnlyCollection<IWebElement> list = firefoxOption.FirefoxDriver.FindElementsByXPath(xPath);
                try
                {
                    IWebElement web = list[pos];
                    return true;
                }
                catch (IndexOutOfRangeException ex)
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public bool CheckXPath(String xPath, String mess, bool isExactly)
        {
            try
            {
                ReadOnlyCollection<IWebElement> list = firefoxOption.FirefoxDriver.FindElementsByXPath(xPath);
                foreach (IWebElement item in list)
                {
                    if (isExactly)
                        return firefoxOption.FirefoxDriver.FindElementByXPath(xPath).Text.Trim().Equals(mess);
                    else
                        return firefoxOption.FirefoxDriver.FindElementByXPath(xPath).Text.Trim().Equals(mess);
                }

            }
            catch (Exception ex)
            {
            }
            return false;
        }
        public bool CheckXPath(String xPath, String mess, bool isExactly, int pos)
        {
            try
            {
                if (isExactly)
                    return firefoxOption.FirefoxDriver.FindElementsByXPath(xPath)[pos].Text.Trim().Equals(mess);
                else
                    return firefoxOption.FirefoxDriver.FindElementsByXPath(xPath)[pos].Text.Trim().Equals(mess);
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
                firefoxOption.FirefoxDriver.FindElementByXPath(xPath);
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
                return firefoxOption.FirefoxDriver.FindElementById(id).Text.Equals(mess);
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool CheckID(String id)
        {
            try
            {
                firefoxOption.FirefoxDriver.FindElementById(id);
                return true;
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

                return firefoxOption.FirefoxDriver.FindElementByXPath(xPath).Location;

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
                return firefoxOption.FirefoxDriver.FindElementsByXPath(xPath)[pos].Location;
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
                return firefoxOption.FirefoxDriver.FindElementsByClassName(className)[pos].Location;
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
                        firefoxOption.FirefoxDriver.ExecuteScript($"window.scrollTo(0,{i});");
                        Thread.Sleep(10);
                    }
                }
                else
                {
                    for (int i = from; i >= to; i -= 20)
                    {
                        firefoxOption.FirefoxDriver.ExecuteScript($"window.scrollTo(0,{i});");
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
        public String GetTextElement(String xPath)
        {
            try
            {
                return firefoxOption.FirefoxDriver.FindElementByXPath(xPath).Text;
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
        public String GetTextElement(String xPath, int pos)
        {
            try
            {
                return firefoxOption.FirefoxDriver.FindElementsByXPath(xPath)[pos].Text;
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
        public void CreateNewTap(String url)
        {
            try
            {
                ((IJavaScriptExecutor)firefoxOption.FirefoxDriver).ExecuteScript("window.open();");
                firefoxOption.FirefoxDriver.SwitchTo().Window(firefoxOption.FirefoxDriver.WindowHandles.Last());
                GoToURL(url);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void CloseThisTab()
        {
            try
            {
                firefoxOption.FirefoxDriver.Close();
                firefoxOption.FirefoxDriver.SwitchTo().Window(firefoxOption.FirefoxDriver.WindowHandles.First());
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
                firefoxOption.FirefoxDriver.ExecuteScript("window.history.go(-1)");
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }

        }
        public int GetScrollHeight()
        {
            try
            {

                return Int32.Parse(firefoxOption.FirefoxDriver.ExecuteScript("return document.body.scrollHeight;") + "");
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
                int pos = Int32.Parse(firefoxOption.FirefoxDriver.ExecuteScript($"return window.scrollY;") + "");
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
                        firefoxOption.FirefoxDriver.ExecuteScript($"document.getElementsByClassName('{name}')[{pos}].click();");
                        break;
                    case 2:
                        firefoxOption.FirefoxDriver.ExecuteScript($"document.getElementById('{name}').click();");
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
                Actions actions = new Actions(firefoxOption.FirefoxDriver);
                actions.MoveToElement(firefoxOption.FirefoxDriver.FindElementsByClassName(classname)[pos]).Build().Perform();

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void MovieTo(IWebElement webElement)
        {
            try
            {
                Actions actions = new Actions(firefoxOption.FirefoxDriver);
                actions.MoveToElement(webElement).Build().Perform();
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void CleanXpath(String xPath, int pos = 0)
        {
            try
            {
                firefoxOption.FirefoxDriver.FindElementByXPath(xPath).Clear();
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

                Actions actions = new Actions(firefoxOption.FirefoxDriver);
                actions.MoveToElement(firefoxOption.FirefoxDriver.FindElementsByXPath(xPath)[pos]).Build().Perform();
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

                return firefoxOption.FirefoxDriver.Url;
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
        public void SelectOptionValue(String xPath, String value, bool isName = false)
        {
            try
            {

                IWebElement select = firefoxOption.FirefoxDriver.FindElementByXPath(xPath);
                if (isName)
                    select = firefoxOption.FirefoxDriver.FindElementByName(xPath);
                SelectElement selectElement = new SelectElement(select);
                selectElement.SelectByValue(value);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void SelectOptionText(String xPath, String value, bool isName = false)
        {
            try
            {

                IWebElement select = firefoxOption.FirefoxDriver.FindElementByXPath(xPath);
                if (isName)
                    select = firefoxOption.FirefoxDriver.FindElementByName(xPath);
                SelectElement selectElement = new SelectElement(select);
                selectElement.SelectByText(value);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public void SelectOptionIndex(String xPath, int index, bool isName = false)
        {
            try
            {

                IWebElement select = firefoxOption.FirefoxDriver.FindElementByXPath(xPath);
                if (isName)
                    select = firefoxOption.FirefoxDriver.FindElementByName(xPath);
                SelectElement selectElement = new SelectElement(select);
                selectElement.SelectByIndex(index);
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }
        public int GetLengthElementXPath(String xPath)
        {
            try
            {

                return firefoxOption.FirefoxDriver.FindElementsByXPath(xPath).Count;
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

                return firefoxOption.FirefoxDriver.FindElementsByClassName(className).Count;
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
        public void WaittingShowElement(String xPath, int step = 200)
        {
            while (!CheckXPath(xPath))
            {
                Thread.Sleep(200);
            }
        }
        public int GetQuatityElement(String xPath)
        {
            try
            {
                if (CheckXPath(xPath))
                {
                    return firefoxOption.FirefoxDriver.FindElementsByXPath(xPath).Count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                return 0;
            }
        }
    }
}