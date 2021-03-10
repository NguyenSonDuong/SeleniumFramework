using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumFramework;
using SeleniumFramework.model;
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
        private int timeWait = 10;

        private bool isRun = true;

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
        public bool IsRun { get => isRun; set => isRun = value; }
        public int TimeWait { get => timeWait; set => timeWait = value; }

        public void Init(String exe, String profile, String pathChromeDriver, bool isHide = false, bool isImage = true)
        {
            try
            {
                chromeSetting = ChromeSetting.Build(exe, profile, pathChromeDriver, isHide, isImage);
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
        public void Init(String exe, String profile, String pathChromeDriver, String proxy, bool isHide = false, bool isImage = true)
        {
            try
            {
                chromeSetting = ChromeSetting.Build(exe, profile, pathChromeDriver, proxy, isHide, isImage);
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
                IWebElement webElement = chromeSetting.WaitElement(className, TimeWait, TypeElement.CLASSNAME);
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
                IWebElement webElement = chromeSetting.WaitElement(className, TimeWait, TypeElement.CLASSNAME);
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
        public void ClickXPath(String xPath)
        {
            try
            {
                IWebElement webElement = chromeSetting.WaitElement(xPath, TimeWait, TypeElement.XPATH);
                chromeSetting.ChromeDriver.FindElementByXPath(xPath).Click();

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
                IWebElement webElement = chromeSetting.WaitElement(xPath, TimeWait, TypeElement.XPATH);
                chromeSetting.ChromeDriver.FindElementsByXPath(xPath)[pos].Click();
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
                IWebElement webElement = chromeSetting.WaitElement(xPath, TimeWait, TypeElement.XPATH);
                foreach (IWebElement item in chromeSetting.ChromeDriver.FindElementsByXPath(xPath))
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
                IWebElement webElement = chromeSetting.WaitElement(xPath, TimeWait, TypeElement.XPATH);
                int qua = 0;
                foreach (IWebElement item in chromeSetting.ChromeDriver.FindElementsByXPath(xPath))
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
                IWebElement webElement = chromeSetting.WaitElement(xPath, TimeWait, TypeElement.XPATH);
                int qua = 0;
                foreach (IWebElement item in chromeSetting.ChromeDriver.FindElementsByXPath(xPath))
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
                IWebElement webElement = chromeSetting.WaitElement(xPath, TimeWait, TypeElement.XPATH);
                return chromeSetting.ChromeDriver.FindElementsByXPath(xPath)[pos].Text;
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
                IWebElement webElement = chromeSetting.WaitElement(name, TimeWait, TypeElement.NAME);
                chromeSetting.ChromeDriver.FindElementsByName(name)[pos].Clear();
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
                IWebElement webElement = chromeSetting.WaitElement(xPath, TimeWait, TypeElement.XPATH);
                chromeSetting.ChromeDriver.FindElementsByXPath(xPath)[pos].Clear();
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
                IWebElement webElement = chromeSetting.WaitElement(id, TimeWait, TypeElement.ID);
                chromeSetting.ChromeDriver.FindElementById(id).Clear();
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
                IWebElement webElement = chromeSetting.WaitElement(xPath, TimeWait, TypeElement.XPATH);
                foreach (IWebElement item in chromeSetting.ChromeDriver.FindElementsByXPath(xPath))
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
                IWebElement webElement = chromeSetting.WaitElement(xPath, TimeWait, TypeElement.XPATH);
                foreach (IWebElement item in chromeSetting.ChromeDriver.FindElementsByXPath(xPath))
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

        public void ClickID(String id)
        {
            try
            {
                IWebElement webElement = chromeSetting.WaitElement(id, TimeWait, TypeElement.ID);
                chromeSetting.ChromeDriver.FindElementById(id).Click();

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
                        chromeSetting.Js.ExecuteScript($"window.scrollTo(0,{i});");
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
                        chromeSetting.Js.ExecuteScript($"window.scrollTo(0,{i});");
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
                IWebElement webElement = chromeSetting.WaitElement(className, TimeWait, TypeElement.CLASSNAME);
                chromeSetting.ChromeDriver.FindElementByClassName(className).Clear();
                Thread.Sleep(100);
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
                IWebElement webElement = chromeSetting.WaitElement(Name, TimeWait, TypeElement.NAME);
                chromeSetting.ChromeDriver.FindElementByName(Name).Clear();
                Thread.Sleep(100);
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
                Thread.Sleep(100);
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
        public void SendKeyXPath(String xPath, String mess, int step)
        {
            try
            {
                IWebElement webElement = chromeSetting.WaitElement(xPath, TimeWait, TypeElement.XPATH);
                chromeSetting.ChromeDriver.FindElementByXPath(xPath).Clear();
                Thread.Sleep(100);
                for (int i = 0; i < mess.Length; i++)
                {
                    chromeSetting.ChromeDriver.FindElementByXPath(xPath).SendKeys(mess[i] + "");
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
                IWebElement webElement = chromeSetting.WaitElement(id, TimeWait, TypeElement.ID);
                chromeSetting.ChromeDriver.FindElementById(id).Click();
                Thread.Sleep(100);
                chromeSetting.ChromeDriver.FindElementById(id).Clear();
                Thread.Sleep(100);
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
                IWebElement webElement = chromeSetting.WaitElement(className, TimeWait, TypeElement.CLASSNAME);
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
                IWebElement webElement = chromeSetting.WaitElement(name, TimeWait, TypeElement.NAME);
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
        public void SendKeyXPath(String xPath, String[] mess, int step = 300)
        {
            try
            {
                IWebElement webElement = chromeSetting.WaitElement(xPath, TimeWait, TypeElement.XPATH);
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
                ReadOnlyCollection<IWebElement> list = chromeSetting.ChromeDriver.FindElementsByXPath(xPath);
                foreach (IWebElement item in list)
                {
                    return chromeSetting.ChromeDriver.FindElementByXPath(xPath).Text.Trim().Equals(mess);
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
                ReadOnlyCollection<IWebElement> list = chromeSetting.ChromeDriver.FindElementsByXPath(xPath);
                foreach (IWebElement item in list)
                {
                    if (isExactly)
                        return chromeSetting.ChromeDriver.FindElementByXPath(xPath).Text.Trim().Equals(mess);
                    else
                        return chromeSetting.ChromeDriver.FindElementByXPath(xPath).Text.Trim().Equals(mess);
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
                    return chromeSetting.ChromeDriver.FindElementsByXPath(xPath)[pos].Text.Trim().Equals(mess);
                else
                    return chromeSetting.ChromeDriver.FindElementsByXPath(xPath)[pos].Text.Trim().Equals(mess);
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
        public bool CheckID(String id)
        {
            try
            {
                chromeSetting.ChromeDriver.FindElementById(id);
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
        public String GetTextElement(String xPath)
        {
            try
            {
                IWebElement webElement = chromeSetting.WaitElement(xPath, TimeWait, TypeElement.XPATH);
                return chromeSetting.ChromeDriver.FindElementByXPath(xPath).Text;
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
                IWebElement webElement = chromeSetting.WaitElement(xPath, TimeWait, TypeElement.XPATH);
                return chromeSetting.ChromeDriver.FindElementsByXPath(xPath)[pos].Text;
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
                ((IJavaScriptExecutor)chromeSetting.ChromeDriver).ExecuteScript("window.open();");
                chromeSetting.ChromeDriver.SwitchTo().Window(chromeSetting.ChromeDriver.WindowHandles.Last());
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
                chromeSetting.ChromeDriver.Close();
                chromeSetting.ChromeDriver.SwitchTo().Window(chromeSetting.ChromeDriver.WindowHandles.First());
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
        public void MovieTo(IWebElement webElement)
        {
            try
            {
                Actions actions = new Actions(chromeSetting.ChromeDriver);
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
                chromeSetting.ChromeDriver.FindElementByXPath(xPath).Clear();

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
        public void SelectOptionValue(String xPath, String value, bool isName = false)
        {
            try
            {

                IWebElement select = chromeSetting.ChromeDriver.FindElementByXPath(xPath);
                if (isName)
                    select = chromeSetting.ChromeDriver.FindElementByName(xPath);
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

                IWebElement select = chromeSetting.ChromeDriver.FindElementByXPath(xPath);
                if (isName)
                    select = chromeSetting.ChromeDriver.FindElementByName(xPath);
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

                IWebElement select = chromeSetting.ChromeDriver.FindElementByXPath(xPath);
                if (isName)
                    select = chromeSetting.ChromeDriver.FindElementByName(xPath);
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
                    return chromeSetting.ChromeDriver.FindElementsByXPath(xPath).Count;
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
