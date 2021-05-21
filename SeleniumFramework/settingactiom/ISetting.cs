using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.settingactiom
{
    interface ISetting
    {
        IWebDriver BuildChromePortable(String pathEXE = "", String pathProfile = "", String pathChromeDriver = "", bool isHide = false, bool isImage = false);
        IWebDriver BuildChromePortable(String pathEXE = "", String pathProfile = "", String pathChromeDriver = "", String proxy = "", bool isHide = false, bool isImage = false);
    }
}
