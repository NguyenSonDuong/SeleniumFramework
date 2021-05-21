using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.settingactiom.firefoxsetting
{
    public class FirefoxSetting
    {
        private FirefoxDriver firefoxDriver;
        private FirefoxOptions firefoxOptions;
        private FirefoxDriverService firefoxDriverService;

        public FirefoxDriver FirefoxDriver { get => firefoxDriver; set => firefoxDriver = value; }
        public FirefoxOptions FirefoxOptions { get => firefoxOptions; set => firefoxOptions = value; }
        public FirefoxDriverService FirefoxDriverService { get => firefoxDriverService; set => firefoxDriverService = value; }

        public FirefoxDriver BuildFirefoxPortable(String pathEXE = "", String pathProfile = "", String pathDriver = "", bool isHide = false)
        {
            FirefoxDriver = null;
            FirefoxOptions = null;
            FirefoxDriverService = null;
            try
            {
                if(FirefoxDriverService == null)
                {
                    FirefoxDriverService = FirefoxDriverService.CreateDefaultService();
                    FirefoxDriverService.HideCommandPromptWindow = true;
                    FirefoxDriverService.SuppressInitialDiagnosticInformation = true;
                }
                if(FirefoxOptions == null)
                {
                    FirefoxOptions = new FirefoxOptions();
                    if (!String.IsNullOrEmpty(pathEXE))
                        FirefoxOptions.BrowserExecutableLocation = pathEXE;
                    if (!String.IsNullOrEmpty(pathProfile))
                    {
                        FirefoxProfile firefoxProfile = new FirefoxProfile(pathProfile);
                        FirefoxOptions.Profile = firefoxProfile;
                    }
                    if (isHide)
                        FirefoxOptions.AddArgument("--headless");
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            if (FirefoxDriver == null)
            {
                if (!String.IsNullOrEmpty(pathDriver))
                    FirefoxDriver = new FirefoxDriver(pathDriver, FirefoxOptions);
                else
                    FirefoxDriver = new FirefoxDriver(FirefoxDriverService, FirefoxOptions);
            }
            return FirefoxDriver;
        }

        public static FirefoxSetting Build(String pathEXE, String pathProfile, String pathDriver, bool isHide = false)
        {
            FirefoxSetting chromeSetting = new FirefoxSetting();
            chromeSetting.BuildFirefoxPortable(pathEXE, pathProfile, pathDriver, isHide);
            return chromeSetting;
        }

        public static void CloseAllFirefox()
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
    }
}
