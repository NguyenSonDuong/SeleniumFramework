using AmazonSaveAcc.actionmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework
{
    public class SeleniumFramework
    {
        private ChromeSetting chromeSetting;
        private ChromeAction chromeAction;

        public ChromeSetting ChromeSetting { get => chromeSetting; set => chromeSetting = value; }
        public ChromeAction ChromeAction { get => chromeAction; set => chromeAction = value; }

        public SeleniumFramework(bool isImage = true)
        {
            chromeAction = new ChromeAction();
            chromeAction.Init("", "", "", isImage);
        }
        public SeleniumFramework(String chromeExe,String chromeProfile, bool isImage = true)
        {
            chromeAction = new ChromeAction();
            chromeAction.Init(chromeExe, chromeProfile,"", isImage);
        }
        public SeleniumFramework(String chromeDriver, bool isImage = true)
        {
            chromeAction = new ChromeAction();
            chromeAction.Init("", "", chromeDriver, isImage);
        }
        public SeleniumFramework(String chromeExe, String chromeProfile,String chromeDriver, bool isImage = true)
        {
            chromeAction = new ChromeAction();
            chromeAction.Init(chromeExe, chromeProfile, chromeDriver, isImage);
        }
    }
}
