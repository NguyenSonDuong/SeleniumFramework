using AmazonSaveAcc.actionmain;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Renci.SshNet;
using SeleniumFramework.model;
using SeleniumFramework.settingactiom.chromesetting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumFramework.settingactiom.firefoxsetting
{
    public class FirefoxSetting
    {
        
        #region property
        public static String PathToApplication = AppDomain.CurrentDomain.BaseDirectory;
        private bool isClean = true;
        private String windowsSize;
        private String lang;
        private bool disableSavePassword;
        private bool isHide;
        private bool isDisableImage;
        private String proxy;
        private String useragent;
        private String typeProxy = "SSH";
        private String exe;
        private String profile;
        private SshClient sshClient;
        private bool isMobile = false;
        private static Random random = new Random();
        private FirefoxDriver firefoxDriver;
        private FirefoxOptions firefoxOptions;
        private FirefoxDriverService firefoxDriverService;
        private FirefoxProfile firefoxProfile;
        public FirefoxDriver FirefoxDriver { get => firefoxDriver; set => firefoxDriver = value; }
        public FirefoxOptions FirefoxOptions { get => firefoxOptions; set => firefoxOptions = value; }
        public FirefoxDriverService FirefoxDriverService { get => firefoxDriverService; set => firefoxDriverService = value; }
        public string WindowsSize { get => windowsSize; set => windowsSize = value; }
        public string Lang { get => lang; set => lang = value; }
        public bool DisableSavePassword { get => disableSavePassword; set => disableSavePassword = value; }
        public bool IsHide { get => isHide; set => isHide = value; }
        public bool IsDisableImage { get => isDisableImage; set => isDisableImage = value; }
        public string Proxy { get => proxy; set => proxy = value; }
        public string Exe { get => exe; set => exe = value; }
        public string Profile { get => profile; set => profile = value; }
        public string TypeProxy { get => typeProxy; set => typeProxy = value; }
        public bool IsClean { get => isClean; set => isClean = value; }
        public string Useragent { get => useragent; set => useragent = value; }
        public bool IsMobile { get => isMobile; set => isMobile = value; }
        public FirefoxProfile FirefoxProfile { get => firefoxProfile; set => firefoxProfile = value; }
        #endregion

        public FirefoxSetting()
        {

        }

        public FirefoxSetting(FirefoxDriver firefoxDriver)
        {
            this.firefoxDriver = firefoxDriver;
        }
        public FirefoxSetting(FirefoxDriverService firefoxDriverService, FirefoxOptions firefoxOptions)
        {
            this.firefoxOptions = firefoxOptions;
            this.firefoxDriverService = firefoxDriverService;
            this.firefoxDriver = new FirefoxDriver(firefoxDriverService, firefoxOptions);
        }
        public FirefoxDriver BuildFirefox()
        {
            firefoxDriver = null;
            firefoxOptions = null;
            firefoxDriverService = null;
            firefoxProfile = null;
            FirefoxBinary firefoxBinary = null;
            try
            {
                if (firefoxDriverService == null)
                {
                    firefoxDriverService = FirefoxDriverService.CreateDefaultService();
                    firefoxDriverService.HideCommandPromptWindow = true;
                    firefoxDriverService.SuppressInitialDiagnosticInformation = true;
                }
               
                if (firefoxOptions == null)
                {
                    firefoxOptions = new FirefoxOptions();
                    
                    int port = random.Next(3000, 16000);
                    if (firefoxProfile == null)
                    {
                        if (!String.IsNullOrEmpty(profile))
                        {
                            firefoxProfile = new FirefoxProfile(profile);
                            firefoxOptions.Profile = firefoxProfile;
                        }
                    }
                    if (firefoxBinary == null)
                    {
                        if (!String.IsNullOrEmpty(exe))
                        {
                            firefoxBinary = new FirefoxBinary(exe);
                        }
                    }
                    if (isDisableImage)
                    {
                        firefoxOptions.SetPreference("permissions.default.image", 2);
                        firefoxOptions.SetPreference("dom.ipc.plugins.enabled.libflashplayer.so", "false");
                    }
                    if (isHide)
                    {
                        firefoxOptions.AddArgument("--headless");
                    }
                    FakeProxy();
                }
                if (firefoxDriver == null)
                {
                    firefoxDriver = new FirefoxDriver(firefoxDriverService,firefoxOptions);
                }
                return firefoxDriver;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void FakeProxy()
        {
            if (!String.IsNullOrEmpty(proxy))
            {
                if (typeProxy.Equals(ProxyType.PROXY_SSH))
                {
                    if (profile.Split('|').Length >= 3)
                    {
                        String[] proxys = profile.Split('|');
                        sshClient = ActionCustom.SSHCoverSock5(proxys[0], proxys[1], proxys[2]);
                        string proxy5 = "127.0.0.1:" + sshClient.ForwardedPorts;
                        firefoxOptions.SetPreference("network.proxy.type", 1);
                        firefoxOptions.SetPreference("network.proxy.socks", "127.0.0.1");
                        firefoxOptions.SetPreference("network.proxy.socks_port", Int32.Parse(sshClient.ForwardedPorts.ToString()));
                        firefoxOptions.SetPreference("network.proxy.socks_version", 5);
                    }
                    else
                    {
                        throw new Exception("Error: SSH Proxy malformed");
                    }
                }
                else
                if (typeProxy.Equals(ProxyType.PROXY_SOCK_5))
                {
                    if (profile.Split(':').Length >= 2)
                    {
                        String[] proxys = profile.Split(':');
                        firefoxOptions.SetPreference("network.proxy.type", 1);
                        firefoxOptions.SetPreference("network.proxy.socks", proxys[0]);
                        firefoxOptions.SetPreference("network.proxy.socks_port", proxys[1]);
                        firefoxOptions.SetPreference("network.proxy.socks_version", 5);
                    }
                    else
                    {
                        throw new Exception("Error: SOCK5 Proxy malformed");
                    }


                }
                else
                {
                    String[] proxys = profile.Split(':');
                    firefoxOptions.SetPreference("network.proxy.type", 1);
                    firefoxOptions.SetPreference("network.proxy.socks", proxys[0]);
                    firefoxOptions.SetPreference("network.proxy.socks_port", proxys[1]);
                    firefoxOptions.SetPreference("network.proxy.socks_version", 4);
                }

            }
        }
        public void DisconectSSH()
        {
            try
            {
                if (sshClient != null)
                    sshClient.Disconnect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static FirefoxSetting Build()
        {
            FirefoxSetting firefoxSetting = new FirefoxSetting();
            firefoxSetting.BuildFirefox();
            return firefoxSetting;
        }
        public static FirefoxSetting Build(String pathEXE, String pathProfile)
        {
            FirefoxSetting firefoxSetting = new FirefoxSetting();
            firefoxSetting.Profile = pathProfile;
            firefoxSetting.Exe = pathEXE;
            firefoxSetting.BuildFirefox();
            return firefoxSetting;
        }
        public static FirefoxSetting Build(String pathEXE, String pathProfile, String proxy = "", String type = "")
        {
            FirefoxSetting firefoxSetting = FirefoxSetting.Build(pathEXE, pathProfile);
            firefoxSetting.TypeProxy = type;
            firefoxSetting.Proxy = proxy;
            firefoxSetting.BuildFirefox();
            return firefoxSetting;
        }
        public static FirefoxSetting Build(String pathEXE, String pathProfile, bool isHide = false)
        {
            FirefoxSetting firefoxSetting = FirefoxSetting.Build(pathEXE, pathProfile);
            firefoxSetting.IsHide = isHide;
            firefoxSetting.BuildFirefox();
            return firefoxSetting;
        }
        public static FirefoxSetting Build(String pathEXE, String pathProfile, bool isImage = true, bool isHide = false)
        {
            FirefoxSetting firefoxSetting = FirefoxSetting.Build(pathEXE, pathProfile);
            firefoxSetting.IsHide = isHide;
            firefoxSetting.isDisableImage = !isImage;
            firefoxSetting.BuildFirefox();
            return firefoxSetting;
        }
        public static FirefoxSetting Build(String pathEXE, String pathProfile, String proxy = "", String type = "", bool isImage = true, bool isHide = false)
        {
            FirefoxSetting firefoxSetting = FirefoxSetting.Build(pathEXE, pathProfile, proxy, type);
            firefoxSetting.IsHide = isHide;
            firefoxSetting.isDisableImage = !isImage;
            firefoxSetting.BuildFirefox();
            return firefoxSetting;
        }
        public static FirefoxSetting Build(String pathEXE, String pathProfile, String proxy = "", String type = "", bool isImage = true, bool isHide = false, String lang = "en")
        {
            FirefoxSetting firefoxSetting = FirefoxSetting.Build(pathEXE, pathProfile, proxy, type, isImage, isHide, lang);
            firefoxSetting.BuildFirefox();
            return firefoxSetting;
        }
        public void CloseChrome()
        {
            try
            {
                DisconectSSH();
                firefoxDriver.Quit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Click(String element, TypeElement type, int location = -1)
        {
            try
            {
                IWebElement webElement = GetElement(element, type);
                ReadOnlyCollection<IWebElement> webElements = GetElements(element, type);
                if (location >= 0)
                {
                    if (webElements != null || webElements.Count <= 0)
                        webElements[location].Click();
                    else
                        throw new Exception("Element no found");
                }
                else
                {
                    if (webElement != null)
                        webElement.Click();
                    else
                        throw new Exception("Element no found");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IWebElement GetElement(String element, TypeElement type)
        {
            try
            {
                if (firefoxDriver.Title == null)
                    throw new Exception("Chrome be closer");
                IWebElement webElement = null;
                switch (type)
                {
                    case TypeElement.XPATH:
                        webElement = firefoxDriver.FindElementByXPath(element);
                        break;
                    case TypeElement.ID:
                        webElement = firefoxDriver.FindElementById(element);
                        break;
                    case TypeElement.CLASS:
                        webElement = firefoxDriver.FindElementByClassName(element);
                        break;
                    case TypeElement.NAME:
                        webElement = firefoxDriver.FindElementByName(element);
                        break;
                    case TypeElement.JS:
                        webElement = (IWebElement)firefoxDriver.ExecuteScript(element);
                        break;
                }
                return webElement;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ReadOnlyCollection<IWebElement> GetElements(String element, TypeElement type)
        {
            try
            {
                if (firefoxDriver.Title == null)
                    throw new Exception("Chrome be closer");
                ReadOnlyCollection<IWebElement> webElements = null;
                switch (type)
                {
                    case TypeElement.XPATH:
                        webElements = firefoxDriver.FindElementsByXPath(element);
                        break;
                    case TypeElement.CLASS:
                        webElements = firefoxDriver.FindElementsByClassName(element);
                        break;
                    case TypeElement.NAME:
                        webElements = firefoxDriver.FindElementsByName(element);
                        break;
                }
                return webElements;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void Click(String element, TypeElement type, String message, bool isExactly = false, int location = -1)
        {
            try
            {
                IWebElement webElement = GetElement(element, type);
                ReadOnlyCollection<IWebElement> webElements = GetElements(element, type);
                int i = 0;
                if (webElements == null || webElements.Count <= 0)
                {
                    throw new Exception("Element not found");
                }
                foreach (IWebElement element1 in webElements)
                {
                    if ((isExactly ? element1.Text.Trim().Equals(message) : element1.Text.Trim().Contains(message)))
                    {
                        i++;
                        if (i >= location)
                        {
                            element1.Click();
                            return;
                        }
                        if (location < 0)
                        {
                            element1.Click();
                            return;
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
        public void SendKey(String element, TypeElement type, String message = "", int location = -1)
        {
            try
            {
                IWebElement webElement = GetElement(element, type);
                ReadOnlyCollection<IWebElement> webElements = GetElements(element, type);
                if (location >= 0)
                {
                    if (webElements != null || webElements.Count <= 0)
                    {
                        if (isClean)
                            webElements[location].Clear();
                        webElements[location].SendKeys(message);
                    }
                    else
                        throw new Exception("Element no found");
                }
                else
                {
                    if (webElement != null)
                    {
                        if (isClean)
                            webElement.Clear();
                        webElement.SendKeys(message);
                    }
                    else
                        throw new Exception("Element no found");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void WriteKey(String element, TypeElement type, String message = "", int timeDelay = 100, int location = -1)
        {
            try
            {
                IWebElement webElement = GetElement(element, type);
                ReadOnlyCollection<IWebElement> webElements = GetElements(element, type);
                if (location >= 0)
                {
                    if (webElements != null || webElements.Count <= 0)
                    {
                        char[] arrxPath = message.ToCharArray();
                        foreach (var path in arrxPath)
                        {
                            webElements[location].SendKeys(path + "");
                            Thread.Sleep(timeDelay);
                        }
                    }
                    else
                        throw new Exception("Element no found");
                }
                else
                {
                    if (webElement != null)
                    {
                        char[] arrxPath = message.ToCharArray();
                        foreach (var path in arrxPath)
                        {
                            webElement.SendKeys(path + "");
                            Thread.Sleep(timeDelay);
                        }
                    }
                    else
                        throw new Exception("Element no found");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ClickAll(String element, TypeElement type, int delay = 1000, AmazonSaveAcc.actionmain.Action action = null)
        {
            try
            {
                IWebElement webElement = GetElement(element, type);
                ReadOnlyCollection<IWebElement> webElements = GetElements(element, type);
                if (webElement != null)
                {
                    webElement.Click();
                }
                else
                {
                    foreach (IWebElement element1 in webElements)
                    {
                        element1.Click();
                        Thread.Sleep(delay);
                        if (action != null)
                        {
                            action(element1, this);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public String GetText(String element, TypeElement type, int location = -1)
        {
            try
            {
                IWebElement element1 = GetElement(element, type);
                ReadOnlyCollection<IWebElement> elements = GetElements(element, type);
                if (location >= 0)
                {
                    return elements[location].Text.Trim();
                }
                else
                {
                    return element1.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckElement(String element, TypeElement type)
        {
            try
            {
                IWebElement element2 = GetElement(element, type);
                return element2 != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public String GetAttr(String element, TypeElement type, string attr, int location = -1)
        {
            try
            {
                IWebElement element1 = GetElement(element, type);
                ReadOnlyCollection<IWebElement> elements = GetElements(element, type);
                if (location >= 0)
                {
                    return elements[location].GetAttribute(attr).Trim();
                }
                else
                {
                    return element1.GetAttribute(attr).Trim();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GoToUrl(String url)
        {
            try
            {
                firefoxDriver.Navigate().GoToUrl(url);
                WaitLoadJS();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void WaitLoadJS()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(firefoxDriver, TimeSpan.FromSeconds(30));
                wait.Until((x) =>
                {
                    return ((IJavaScriptExecutor)firefoxDriver).ExecuteScript("return document.readyState").Equals("complete");

                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetQuantity(String element, TypeElement type)
        {
            try
            {
                ReadOnlyCollection<IWebElement> elements = GetElements(element, type);
                return elements.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
