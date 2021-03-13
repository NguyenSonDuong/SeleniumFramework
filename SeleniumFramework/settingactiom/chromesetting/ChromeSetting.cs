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

namespace AmazonSaveAcc.actionmain
{
    public class ChromeSetting
    {
        private String[] langList = new String[] { "en", "vn" };
        //private String[] langList = new String[] { "af", "ak", "sq", "am", "ar", "hy", "az", "eu", "be", "bn", "bh", "bs", "br", "bg", "km", "ca", "ny", "co", "hr", "cs", "da", "nl", "en", "eo", "et", "ee", "fo", "tl", "fi", "fr", "fy", "gl", "ka", "de", "el", "gn", "gu", "ht", "ha", "iw", "hi", "hu", "is", "ig", "id", "ia", "ga", "it", "ja", "jw", "kn", "kk", "rw", "rn", "kg", "ko", "ku", "ky", "lo", "la", "lv", "ln", "lt", "lg", "mk", "mg", "ms", "ml", "mt", "mi", "mr", "mo", "mn", "ne", "no", "nn", "oc", "or", "om", "ps", "fa", "pl", "pa", "qu", "ro", "rm", "ru", "gd", "sr", "sh", "st", "tn", "sn", "sd", "si", "sk", "sl", "so", "es", "su", "sw", "sv", "tg", "ta", "tt", "te", "th", "ti", "to", "tr", "tk", "tw", "ug", "uk", "ur", "uz", "vi", "cy", "wo", "xh", "yi", "yo", "zu" };
        private static Random random = new Random();
        private ChromeDriver chromeDriver;
        private ChromeOptions chromeOptions;
        private ChromeDriverService chromeDriverService;
        private IJavaScriptExecutor js;
        public int PortNumber = 0;
        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        public ChromeDriver ChromeDriver { get => chromeDriver; set => chromeDriver = value; }
        public ChromeOptions ChromeOptions { get => chromeOptions; set => chromeOptions = value; }
        public ChromeDriverService ChromeDriverService { get => chromeDriverService; set => chromeDriverService = value; }
        public IJavaScriptExecutor Js { get => js; set => js = value; }
        public IntPtr CurrentBrowserHwnd = IntPtr.Zero;
        public int CurrentBrowserPID = -1;
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
        public static void HideAllCommandLine()
        {
            foreach (Process item in Process.GetProcesses())
            {
                Console.WriteLine(item.ProcessName);
            }
            Process[] process = Process.GetProcessesByName("chromedriver");
            foreach (Process item in process)
            {
                ShowWindow(item.Handle, 0);
            }
            Process[] process2 = Process.GetProcessesByName("conhost");
            foreach (Process item in process2)
            {
                ShowWindow(item.Handle, 0);
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

        public IWebElement WaitElement(String text, int timeoutInSeconds, TypeElement typeElement)
        {
            try
            {
                WebDriverWait webDriver = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(timeoutInSeconds));
                IWebElement webElement = null;
                switch (typeElement)
                {
                    case TypeElement.XPATH:
                        webElement = webDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(text)));
                        break;
                    case TypeElement.CLASSNAME:
                        webElement = webDriver.Until(ExpectedConditions.ElementIsVisible(By.ClassName(text)));
                        break;
                    case TypeElement.NAME:
                        webElement = webDriver.Until(ExpectedConditions.ElementIsVisible(By.Name(text)));
                        break;
                    case TypeElement.ID:
                        webElement = webDriver.Until(ExpectedConditions.ElementIsVisible(By.Id(text)));
                        break;
                }
                if (webElement.Displayed)
                    return webElement;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public IWebElement WaitElement(String text, int pos,int timeoutInSeconds, TypeElement typeElement)
        {
            try
            {
                WebDriverWait webDriver = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(timeoutInSeconds));
                IWebElement webElement = null;
                switch (typeElement)
                {
                    case TypeElement.XPATH:
                        webElement = webDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath(text)));
                        break;
                    case TypeElement.CLASSNAME:
                        webElement = webDriver.Until(ExpectedConditions.ElementIsVisible(By.ClassName(text)));
                        break;
                    case TypeElement.NAME:
                        webElement = webDriver.Until(ExpectedConditions.ElementIsVisible(By.Name(text)));
                        break;
                    case TypeElement.ID:
                        webElement = webDriver.Until(ExpectedConditions.ElementIsVisible(By.Id(text)));
                        break;
                }
                if (webElement.Displayed)
                    return webElement;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public void GetHandler()
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName("chrome");
            for (int p = 0; p < processes.Length; p++)
            {
                ManagementObjectSearcher commandLineSearcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + processes[p].Id);
                String commandLine = "";
                foreach (ManagementObject commandLineObject in commandLineSearcher.Get())
                {
                    commandLine += (String)commandLineObject["CommandLine"];
                }

                String script_pid_str = (new Regex("--scriptpid-(.+?) ")).Match(commandLine).Groups[1].Value;

                if (!script_pid_str.Equals("") && Convert.ToInt32(script_pid_str).Equals(System.Diagnostics.Process.GetCurrentProcess().Id))
                {
                    CurrentBrowserPID = processes[p].Id;
                    CurrentBrowserHwnd = processes[p].MainWindowHandle;
                    break;
                }
            }
        }
        // Khơi tạo chrome driver với các cấu hình tiêu chuẩn
        public ChromeDriver BuildChromePortable(String pathEXE = "", String pathProfile = "", String pathChromeDriver = "", bool isHide = false, bool isImage = false)
        {
            chromeDriver = null;
            chromeOptions = null;
            chromeDriverService = null;
            try
            {
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
                        PortNumber = port;
                        chromeOptions = new ChromeOptions();
                        chromeOptions.AcceptInsecureCertificates = true;
                        if (!isImage)
                        {
                            chromeOptions.AddExtension(AppDomain.CurrentDomain.BaseDirectory + "\\blockImage.crx");
                        }
                        if (!String.IsNullOrEmpty(pathEXE))
                            chromeOptions.BinaryLocation = pathEXE;
                        if (!String.IsNullOrEmpty(pathProfile))
                        {
                            int index = pathProfile.LastIndexOf(@"\");
                            string profile = pathProfile.Remove(0, index + 1);
                            chromeOptions.AddArgument($"user-data-dir={pathProfile.Remove(index)}");
                            chromeOptions.AddArgument($"--profile-directory={profile}");

                        }
                        if (isHide)
                        {
                            chromeOptions.AddArguments(new string[]
                            {
                                "headless"
                            });
                        }
                        chromeOptions.AddArgument("user-agent=" + new Faker().Internet.UserAgent());
                        chromeOptions.AddArgument("window-size=1280,800");
                        chromeOptions.AddArgument("--disable-notifications");
                        chromeOptions.AddArgument("--lang=" + langList[random.Next(0, langList.Length)]);
                        chromeOptions.AddArgument($"--remote-debugging-port={port}");
                        chromeOptions.AddExcludedArgument("enable-automation");
                        chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
                        chromeOptions.AddArgument("ignore-certificate-errors");
                        chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
                    }
                }
                catch (Exception)
                {
                    chromeOptions = new ChromeOptions();
                    if (!isImage)
                    {
                        chromeOptions.AddExtension(AppDomain.CurrentDomain.BaseDirectory + "\\blockImage.crx");
                    }
                    if (isHide)
                    {
                        chromeOptions.AddArguments(new string[]
                        {
                            "headless"
                        });
                    }
                    chromeOptions.AcceptInsecureCertificates = true;
                    chromeOptions.AddArgument("user-agent=" + new Faker().Internet.UserAgent());
                    chromeOptions.AddArgument("ignore-certificate-errors");
                    chromeOptions.AddExcludedArgument("enable-automation");
                    chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
                    chromeOptions.AddArgument("--lang=" + langList[random.Next(0, langList.Length)]);
                    chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
                }
                if (chromeDriver == null)
                {
                    if (!String.IsNullOrEmpty(pathChromeDriver))
                        chromeDriver = new ChromeDriver(pathChromeDriver, chromeOptions);
                    else
                        chromeDriver = new ChromeDriver(chromeDriverService, chromeOptions);
                }
                return chromeDriver;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ChromeDriver BuildChromePortable(String pathEXE = "", String pathProfile = "", String pathChromeDriver = "", String proxy = "", bool isHide = false, bool isImage = false)
        {
            chromeDriver = null;
            chromeOptions = null;
            chromeDriverService = null;
            try
            {
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
                        PortNumber = port;
                        chromeOptions = new ChromeOptions();
                        if (!isImage)
                        {
                            chromeOptions.AddExtension(AppDomain.CurrentDomain.BaseDirectory + "\\blockImage.crx");
                        }
                        if (!String.IsNullOrEmpty(proxy))
                        {
                            chromeOptions.AcceptInsecureCertificates = true;
                            chromeOptions.AddArgument("ignore-certificate-errors");
                            if (proxy.Split(':').Length > 2)
                            {
                                chromeOptions.AddArguments(new string[]
                                {
                                    "--proxy-server=" + proxy.Split(':')[2]+":"+proxy.Split(':')[3]+"@"+proxy.Split(':')[0]+":"+proxy.Split(':')[1]
                                }); 
                            }
                            else
                            {

                                chromeOptions.AddArguments(new string[]
                                {
                                "--proxy-server=" + proxy
                                });
                            }
                        }
                        if (!String.IsNullOrEmpty(pathEXE))
                            chromeOptions.BinaryLocation = pathEXE;
                        if (!String.IsNullOrEmpty(pathProfile))
                        {
                            int index = pathProfile.LastIndexOf(@"\");
                            string profile = pathProfile.Remove(0, index + 1);
                            chromeOptions.AddArgument($"user-data-dir={pathProfile.Remove(index)}");
                            Console.WriteLine("Log on: " + pathProfile);
                            chromeOptions.AddArgument($"--profile-directory={profile}");
                        }
                        if (isHide)
                        {
                            chromeOptions.AddArguments(new string[]
                            {
                                "headless"
                            });
                        }
                        chromeOptions.AddArgument("user-agent=" + new Faker().Internet.UserAgent());
                        chromeOptions.AddArgument("window-size=1280,800");
                        chromeOptions.AddArgument("--lang=" + langList[random.Next(0, langList.Length)]);
                        chromeOptions.AddArgument("--disable-notifications");
                        chromeOptions.AddArgument($"--remote-debugging-port={port}");
                        chromeOptions.AddExcludedArgument("enable-automation");
                        chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
                        chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
                    }
                }
                catch (Exception)
                {
                    chromeOptions = new ChromeOptions();
                    if (!isImage)
                    {
                        chromeOptions.AddExtension(AppDomain.CurrentDomain.BaseDirectory + "\\blockImage.crx");
                    }
                    if (!String.IsNullOrEmpty(proxy))
                    {
                        chromeOptions.AcceptInsecureCertificates = true;
                        chromeOptions.AddArgument("ignore-certificate-errors");
                        if (proxy.Split(':').Length > 2)
                        {
                            chromeOptions.AddArguments(new string[]
                            {
                                    "--proxy-server=" + proxy.Split(':')[2]+":"+proxy.Split(':')[3]+"@"+proxy.Split(':')[0]+":"+proxy.Split(':')[1]
                            });
                        }
                        else
                        {

                            chromeOptions.AddArguments(new string[]
                            {
                                "--proxy-server=" + proxy
                            });
                        }
                    }
                    if (isHide)
                    {
                        chromeOptions.AddArguments(new string[]
                        {
                            "headless"
                        });
                    }
                    chromeOptions.AddArgument("user-agent=" + new Faker().Internet.UserAgent());
                    chromeOptions.AddExcludedArgument("enable-automation");
                    chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
                    chromeOptions.AddArgument("--lang=" + langList[random.Next(0, langList.Length)]);
                    chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
                }
                if (chromeDriver == null)
                {
                    if (!String.IsNullOrEmpty(pathChromeDriver))
                        chromeDriver = new ChromeDriver(pathChromeDriver, chromeOptions);
                    else
                        chromeDriver = new ChromeDriver(chromeDriverService, chromeOptions);
                }
                return chromeDriver;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static ChromeSetting Build(String pathEXE, String pathProfile, String pathChromeDriver, bool isImage = true)
        {
            ChromeSetting chromeSetting = new ChromeSetting();
            chromeSetting.BuildChromePortable(pathEXE, pathProfile, pathChromeDriver, isImage);
            return chromeSetting;
        }

        public static ChromeSetting Build(String pathEXE, String pathProfile, String pathChromeDriver, bool isHide = false, bool isImage = true)
        {
            ChromeSetting chromeSetting = new ChromeSetting();
            chromeSetting.BuildChromePortable(pathEXE, pathProfile, pathChromeDriver, isHide, isImage);
            return chromeSetting;
        }
        public static ChromeSetting Build(String pathEXE, String pathProfile, String pathChromeDriver, String proxy = "", bool isImage = true)
        {
            ChromeSetting chromeSetting = new ChromeSetting();
            chromeSetting.BuildChromePortable(pathEXE, pathProfile, pathChromeDriver, proxy, isImage);
            return chromeSetting;
        }
        public static ChromeSetting Build(String pathEXE, String pathProfile, String pathChromeDriver, String proxy = "", bool isHide = false, bool isImage = true)
        {
            ChromeSetting chromeSetting = new ChromeSetting();
            chromeSetting.BuildChromePortable(pathEXE, pathProfile, pathChromeDriver, proxy, isHide, isImage);
            return chromeSetting;
        }
        public void ClickToButtonXpath(String xPath)
        {
            try
            {
                chromeDriver.FindElementByXPath(xPath).Click();
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
