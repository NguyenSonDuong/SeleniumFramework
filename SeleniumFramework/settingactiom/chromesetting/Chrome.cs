using AmazonSaveAcc.actionmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.settingactiom.chromesetting
{
    public class Chrome : IActionSelenium
    {
        private ChromeSetting chromeSetting;

        private event ErrorHandle errorEvent;
        private event ProcessHandle processEvent;
        private event SuccessHandle successEvent;


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

        public Chrome()
        {

        }

        public void Init()
        {
            try
            {
                chromeSetting = ChromeSetting.Build();
            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        

        public void ClickClass(string classname, int location)
        {
            try
            {

            }catch(Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickClass(string classname)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickClass(string classname, string message)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickID(string id)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickXpath(string xpath, int location)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickXpath(string xpath)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickXpath(string xpath, string message)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickName(string name, int location)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickName(string name)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickName(string xpath, string message)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickJS(string js)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void ClickByJS(string js)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyClass(string message, string classname, int location)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyClass(string message, string classname)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyID(string message, string id)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyXpath(string message, string xpath, int location)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyXpath(string message, string xpath)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyName(string message, string name, int location)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyName(string message, string name)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyJS(string message, string js)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void SendKeyByJS(string message, string js)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyClass(string message, string classname, int location, int timeDelay)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyClass(string message, string classname, int timeDelay)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyID(string message, string id, int timeDelay)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyXpath(string message, string xpath, int location, int timeDelay)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyXpath(string message, string xpath, int timeDelay)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyName(string message, string name, int location, int timeDelay)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyName(string message, string name, int timeDelay)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyJS(string message, string js, int timeDelay)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public void WriteKeyByJS(string message, string js, int timeDelay)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
        }

        public string GetTextClass(string classname, int location)
        {
            try
            {

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

        public string GetTextClass(string classname)
        {
            try
            {

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

        public string GetTextClass(string classname, string message)
        {
            try
            {

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

        public string GetTextID(string id)
        {
            try
            {

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

        public string GetTextXpath(string xpath, int location)
        {
            try
            {

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

        public string GetTextXpath(string xpath)
        {
            try
            {

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

        public string GetTextXpath(string xpath, string message)
        {
            try
            {

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

        public string GetTextName(string name, int location)
        {
            try
            {

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

        public string GetTextName(string name)
        {
            try
            {

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

        public string GetTextName(string xpath, string message)
        {
            try
            {

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

        public string GetTextJS(string js)
        {
            try
            {

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

        public string GetTextByJS(string js)
        {
            try
            {

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


        public int GetQuantityClass(string classname)
        {
            try
            {

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

        public int GetQuantityXpath(string xpath)
        {
            try
            {

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

        public int GetQuantityName(string name)
        {
            try
            {

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

        public bool CheckClass(string classname)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return false;
        }

        public bool CheckXpath(string xpath)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return false;
        }

        public bool CheckID(string id)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return false;
        }

        public bool CheckName(string name)
        {
            try
            {

            }
            catch (Exception ex)
            {
                if (errorEvent != null)
                    errorEvent(ex, this, 100);
                else
                    throw ex;
            }
            return false;
        }

        public string GetAttrXpath(string attr, string xpath, int location)
        {
            try
            {

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

        public string GetAttrXpath(string attr, string xpath)
        {
            try
            {

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

        public string GetAttrXpath(string attr, string xpath, string message)
        {
            try
            {

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

        public string GetAttrName(string attr, string name, int location)
        {
            try
            {

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

        public string GetAttrName(string attr, string name)
        {
            try
            {

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

        public string GetAttrName(string attr, string xpath, string message)
        {
            try
            {

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

        public string GetAttrJS(string attr, string js)
        {
            try
            {

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

        public string GetAttrByJS(string attr, string js)
        {
            try
            {

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

        public string GetAttrClass(string attr, string classname, int location)
        {
            try
            {

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

        public string GetAttrClass(string attr, string classname)
        {
            try
            {

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

        public string GetAttrClass(string attr, string classname, string message)
        {
            try
            {

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

        public string GetAttrID(string attr, string id)
        {
            try
            {

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
    }
}
