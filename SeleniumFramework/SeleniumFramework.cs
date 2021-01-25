using AmazonSaveAcc.actionmain;
using SeleniumFramework.model;
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
        private List<ActionSelenium> listAction = new List<ActionSelenium>();
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
        public void RunAction()
        {
            foreach(ActionSelenium action in listAction)
            {
                if(action.EnumAction == EnumActionSelenium.CLICK)
                {
                    RunActionClick(action);
                }
                if (action.EnumAction == EnumActionSelenium.SENDKEY)
                {
                    RunActionSendKey(action);
                }
            }
        }
        public void RunActionClick(ActionSelenium actionSelenium)
        {
            if(actionSelenium.BySelenium == EnumActionTypeBySelenium.POSITION)
            {
                if (actionSelenium.Type == EnumActionTypeSelenium.CLASSNAME)
                {
                    chromeAction.ClickClass(actionSelenium.Key, actionSelenium.Position);
                }
                if (actionSelenium.Type == EnumActionTypeSelenium.ID)
                {
                    chromeAction.ClickID(actionSelenium.Key);
                }
                if (actionSelenium.Type == EnumActionTypeSelenium.XPATH)
                {
                    chromeAction.ClickXPath(actionSelenium.Key, actionSelenium.Position);
                }
            }
            else
            {
                chromeAction.ClickXPath(actionSelenium.Key, actionSelenium.SameString, actionSelenium.IsExactly);
            }
            
        }
        public void RunActionSendKey(ActionSelenium actionSelenium)
        {
            if (actionSelenium.Type == EnumActionTypeSelenium.CLASSNAME)
            {
                chromeAction.SendKeyClass(actionSelenium.Key, actionSelenium.Value);
            }
            if (actionSelenium.Type == EnumActionTypeSelenium.ID)
            {
                chromeAction.SendKeyID(actionSelenium.Key, actionSelenium.Value);
            }
            if (actionSelenium.Type == EnumActionTypeSelenium.XPATH)
            {
                chromeAction.SendKeyXPath(actionSelenium.Key, actionSelenium.Value);
            }
        }
    }
}
