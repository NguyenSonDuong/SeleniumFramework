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

namespace AmazonSaveAcc.actionmain
{
    public class ChromeSetting
    {
        #region property
        public static String PathToApplication = AppDomain.CurrentDomain.BaseDirectory;
        private String windowsSize;
        private String lang;
        private bool disableSavePassword;
        private bool isHide;
        private bool isDisableImage;
        private String proxy;
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
                        chromeOptions.AddArgument("window-size=" + windowsSize);
                    if (!String.IsNullOrEmpty(lang))
                        chromeOptions.AddArgument("--lang=" + lang);
                    chromeOptions.AddArgument($"--remote-debugging-port={port}");
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

        public void Click(String element, TypeElement type, string message = "", int location =-1)
        {
            try
            {
                IWebElement webElement = null ;
                ReadOnlyCollection<IWebElement> webElements = null;
                switch (type)
                {
                    case TypeElement.XPATH:
                        webElement = chromeDriver.FindElementByXPath(element);
                        webElements = chromeDriver.FindElementsByXPath(element);
                        break;
                    case TypeElement.ID:
                        webElement = chromeDriver.FindElementsByClassName(element)[location];
                        break;
                    case TypeElement.CLASS:
                        break;
                    case TypeElement.NAME:
                        break;
                    case TypeElement.JS:
                        break;
                }
                webElement.Click();
            }
            catch (Exception ex)
            {

            }


        }

    }
}
