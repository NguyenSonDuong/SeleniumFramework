using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Management;
using System.Text.RegularExpressions;
using Bogus;
using SeleniumFramework.model;
using OpenQA.Selenium.Support.UI;
using System.IO;
using SeleniumFramework.settingactiom.chromesetting;
using Renci.SshNet;
using OpenQA.Selenium.Remote;

namespace AmazonSaveAcc.actionmain
{
    public class ChromeSetting
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
        private static Random random = new Random();

        private ChromeDriver chromeDriver;
        private ChromeOptions chromeOptions;
        private ChromeDriverService chromeDriverService;

        public ChromeDriver ChromeDriver { get => chromeDriver; set => chromeDriver = value; }
        public ChromeOptions ChromeOptions { get => chromeOptions; set => chromeOptions = value; }
        public ChromeDriverService ChromeDriverService { get => chromeDriverService; set => chromeDriverService = value; }
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
        #endregion

        public ChromeSetting()
        {

        }

        public static void CloseAllChrome()
        {
            Process[] process = Process.GetProcessesByName("chromedriver");
            foreach (Process item in process)
            {
                try
                {
                    item.Kill();
                }
                catch
                {

                }
            }
            Process[] process2 = Process.GetProcessesByName("chrome");
            foreach (Process item in process2)
            {
                try
                {
                    item.Kill();
                }
                catch (Exception ex)
                {

                }

            }
        }

        public ChromeSetting(ChromeDriver chromeDriver)
        {
            this.chromeDriver = chromeDriver;
        }
        public ChromeSetting(ChromeDriverService chromeDriverService, ChromeOptions chromeOptions)
        {
            this.chromeDriver = new ChromeDriver(chromeDriverService, chromeOptions);
            this.chromeOptions = chromeOptions;
            this.chromeDriverService = chromeDriverService;
        }

        // Khơi tạo chrome driver với các cấu hình tiêu chuẩn
        public ChromeDriver BuildChromeMobile()
        {
            chromeDriver = null;
            chromeOptions = null;
            chromeDriverService = null;
            try
            {
                if (chromeDriverService == null)
                {
                    chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = true;
                    chromeDriverService.SuppressInitialDiagnosticInformation = true;
                }
                if (chromeOptions == null)
                {
                    int port = random.Next(3000, 16000);
                    chromeOptions = new ChromeOptions();
                    
                    chromeOptions.AcceptInsecureCertificates = true;
                    if (isDisableImage)
                    {
                        chromeOptions.AddExtension(AppDomain.CurrentDomain.BaseDirectory + "\\blockImage.crx");
                    }
                    if (!String.IsNullOrEmpty(exe))
                        chromeOptions.BinaryLocation = exe;
                    if (!String.IsNullOrEmpty(profile))
                    {
                        int index = profile.LastIndexOf(@"\");
                        string profile2 = profile.Remove(0, index + 1);
                        chromeOptions.AddArgument($"user-data-dir={profile.Remove(index)}");
                        chromeOptions.AddArgument($"--profile-directory={profile2}");
                    }
                    if (isHide)
                    {
                        chromeOptions.AddArguments(new string[]
                        {
                                "headless"
                        });
                    }
                    FakeProxy();
                    ChromeMobileEmulationDeviceSettings CMEDS = new ChromeMobileEmulationDeviceSettings();
                    CMEDS.Width = Int32.Parse(windowsSize.Split(',')[0]);
                    CMEDS.Height = Int32.Parse(windowsSize.Split(',')[1]);
                    CMEDS.PixelRatio = 3.0;
                    CMEDS.EnableTouchEvents = true;
                    CMEDS.UserAgent = useragent;
                    chromeOptions.EnableMobileEmulation(CMEDS);
                    
                }
                if (chromeDriver == null)
                {
                    chromeDriver = new ChromeDriver(chromeDriverService, chromeOptions);
                }
                return chromeDriver;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ChromeDriver BuildChrome()
        {
            chromeDriver = null;
            chromeOptions = null;
            chromeDriverService = null;
            try
            {
                if (chromeDriverService == null)
                {
                    chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = true;
                    chromeDriverService.SuppressInitialDiagnosticInformation = true;
                }
                if (chromeOptions == null)
                {
                    int port = random.Next(3000, 16000);
                    chromeOptions = new ChromeOptions();
                    chromeOptions.AcceptInsecureCertificates = true;
                    if (isDisableImage)
                    {
                        chromeOptions.AddExtension(AppDomain.CurrentDomain.BaseDirectory + "\\blockImage.crx");
                    }
                    if (!String.IsNullOrEmpty(exe))
                        chromeOptions.BinaryLocation = exe;
                    if (!String.IsNullOrEmpty(profile))
                    {
                        int index = profile.LastIndexOf(@"\");
                        string profile2 = profile.Remove(0, index + 1);
                        chromeOptions.AddArgument($"user-data-dir={profile.Remove(index)}");
                        chromeOptions.AddArgument($"--profile-directory={profile2}");
                    }
                    if (isHide)
                    {
                        chromeOptions.AddArguments(new string[]
                        {
                                "headless"
                        });
                    }
                    chromeOptions = new ChromeOptions();
                    chromeOptions.AcceptInsecureCertificates = true;
                    if (!String.IsNullOrEmpty(windowsSize))
                        chromeOptions.AddArgument("--window-size=" + windowsSize);
                    if (!String.IsNullOrEmpty(lang))
                        chromeOptions.AddArgument("--lang=" + lang);
                    chromeOptions.AddArgument($"--remote-debugging-port={port}");
                    chromeOptions.AddArgument($"--user-agent={Useragent}");
                    chromeOptions.AddExcludedArgument("enable-automation");
                    chromeOptions.AddArgument("--disable-notifications");
                    chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
                    chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
                    chromeOptions.AddArgument("ignore-certificate-errors");
                    chromeOptions.AddUserProfilePreference("credentials_enable_service", !disableSavePassword);
                    chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", !disableSavePassword);
                    FakeProxy();
                }
                if (chromeDriver == null)
                {
                    chromeDriver = new ChromeDriver(chromeDriverService, chromeOptions);
                }
                return chromeDriver;
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
                        chromeOptions.AddArguments(new string[]
                        {
                                    "--proxy-server=" + proxy5
                        });
                    }
                    else
                    {
                        throw new Exception("Error: SSH Proxy malformed");
                    }
                }
                else
                if (typeProxy.Equals(ProxyType.PROXY_SOCK_5))
                {
                    if (profile.Split(':').Length >= 4)
                    {
                        String[] proxys = profile.Split(':');
                        String fileName = PathToApplication + "\\" + proxys[0] + proxys[1] + ".zip";
                        if (File.Exists(fileName))
                        {
                            chromeOptions.AddExtension(fileName);
                        }
                        else
                        {
                            String background = File.ReadAllText(PathToApplication + "\\background.js");
                            String manifest = File.ReadAllText(PathToApplication + "\\manifest.json");
                            background = background.Replace("%HOST%", proxys[0]);
                            background = background.Replace("%PORT%", proxys[1]);
                            background = background.Replace("%USERNAME%", proxys[2]);
                            background = background.Replace("%PASSWORD%", proxys[3]);
                            Console.WriteLine(background);
                            try
                            {
                                ActionCustom.ZipFile(new string[] { background, manifest }, fileName);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            chromeOptions.AddExtension(fileName);
                        }
                    }
                    else
                    {
                        throw new Exception("Error: SOCK5 Proxy malformed");
                    }


                }
                else
                {
                    chromeOptions.AddArguments(new string[]   //sock4 va http
                    {
                            "--proxy-server=" + proxy
                    });
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

        public static ChromeSetting Build()
        {
            ChromeSetting chromeSetting = new ChromeSetting();
            chromeSetting.BuildChrome();
            return chromeSetting;
        }
        public static ChromeSetting Build(String pathEXE, String pathProfile)
        {
            ChromeSetting chromeSetting = new ChromeSetting();
            chromeSetting.Profile = pathProfile;
            chromeSetting.Exe = pathEXE;
            chromeSetting.BuildChrome();
            return chromeSetting;
        }
        public static ChromeSetting Build(String pathEXE, String pathProfile, String proxy = "", String type = "")
        {
            ChromeSetting chromeSetting = ChromeSetting.Build(pathEXE, pathProfile);
            chromeSetting.TypeProxy = type;
            chromeSetting.Proxy = proxy;
            chromeSetting.BuildChrome();
            return chromeSetting;
        }
        public static ChromeSetting Build(String pathEXE, String pathProfile, bool isHide = false)
        {
            ChromeSetting chromeSetting = ChromeSetting.Build(pathEXE, pathProfile);
            chromeSetting.IsHide = isHide;
            chromeSetting.BuildChrome();
            return chromeSetting;
        }
        public static ChromeSetting Build(String pathEXE, String pathProfile, bool isImage = true, bool isHide = false)
        {
            ChromeSetting chromeSetting = ChromeSetting.Build(pathEXE, pathProfile);
            chromeSetting.IsHide = isHide;
            chromeSetting.isDisableImage = !isImage;
            chromeSetting.BuildChrome();
            return chromeSetting;
        }
        public static ChromeSetting Build(String pathEXE, String pathProfile, String proxy = "", String type = "", bool isImage = true, bool isHide = false)
        {
            ChromeSetting chromeSetting = ChromeSetting.Build(pathEXE, pathProfile, proxy, type);
            chromeSetting.IsHide = isHide;
            chromeSetting.isDisableImage = !isImage;
            chromeSetting.BuildChrome();
            return chromeSetting;
        }
        public static ChromeSetting Build(String pathEXE, String pathProfile, String proxy = "", String type = "", bool isImage = true, bool isHide = false, String lang = "en")
        {
            ChromeSetting chromeSetting = ChromeSetting.Build(pathEXE, pathProfile, proxy, type, isImage, isHide, lang);
            chromeSetting.BuildChrome();
            return chromeSetting;
        }

        public void CloseChrome()
        {
            try
            {
                DisconectSSH();
                chromeDriver.Quit();
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
                IWebElement webElement = null;
                ReadOnlyCollection<IWebElement> webElements = null;
                switch (type)
                {
                    case TypeElement.XPATH:
                        webElement = chromeDriver.FindElementByXPath(element);
                        webElements = chromeDriver.FindElementsByXPath(element);
                        break;
                    case TypeElement.ID:
                        webElement = chromeDriver.FindElementById(element);
                        break;
                    case TypeElement.CLASS:
                        webElement = chromeDriver.FindElementByClassName(element);
                        webElements = chromeDriver.FindElementsByClassName(element);
                        break;
                    case TypeElement.NAME:
                        webElement = chromeDriver.FindElementByName(element);
                        webElements = chromeDriver.FindElementsByName(element);
                        break;
                    case TypeElement.JS:
                        webElement = (IWebElement)chromeDriver.ExecuteScript(element);
                        break;
                    case TypeElement.JS2:
                        chromeDriver.ExecuteScript(element);
                        return;
                }
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
                if (chromeDriver.Title == null)
                    throw new Exception("Chrome be closer");
                IWebElement webElement = null;
                switch (type)
                {
                    case TypeElement.XPATH:
                        webElement = chromeDriver.FindElementByXPath(element);
                        break;
                    case TypeElement.ID:
                        webElement = chromeDriver.FindElementById(element);
                        break;
                    case TypeElement.CLASS:
                        webElement = chromeDriver.FindElementByClassName(element);
                        break;
                    case TypeElement.NAME:
                        webElement = chromeDriver.FindElementByName(element);
                        break;
                    case TypeElement.JS:
                        webElement = (IWebElement)chromeDriver.ExecuteScript(element);
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
                if (chromeDriver.Title == null)
                    throw new Exception("Chrome be closer");
                ReadOnlyCollection<IWebElement> webElements = null;
                switch (type)
                {
                    case TypeElement.XPATH:
                        webElements = chromeDriver.FindElementsByXPath(element);
                        break;
                    case TypeElement.CLASS:
                        webElements = chromeDriver.FindElementsByClassName(element);
                        break;
                    case TypeElement.NAME:
                        webElements = chromeDriver.FindElementsByName(element);
                        break;
                    case TypeElement.JS:
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
        public void ClickAll(String element, TypeElement type, int delay = 1000, Action action = null)
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
                chromeDriver.Navigate().GoToUrl(url);
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
                WebDriverWait wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(30));
                wait.Until((x) =>
                {
                    return ((IJavaScriptExecutor)chromeDriver).ExecuteScript("return document.readyState").Equals("complete");

                });
            }
            catch(Exception ex)
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
