using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.settingactiom
{
    public interface IActionSelenium
    {
        void Init();

        #region Check
        bool CheckClass(String classname);
        bool CheckXpath(String xpath);
        bool CheckID(String id);
        bool CheckName(String name);
        #endregion

        #region Click
        void ClickClass(String classname, int location);
        void ClickClass(String classname);
        void ClickClass(String classname,String message);

        void ClickID(String id);

        void ClickXpath(String xpath, int location);
        void ClickXpath(String xpath);
        void ClickXpath(String xpath, String message);

        void ClickName(String name, int location);
        void ClickName(String name);
        void ClickName(String xpath, String message);

        void ClickJS(String js);

        void ClickByJS(String js);
        #endregion

        #region sendkey
        void SendKeyClass(String message, String classname, int location);
        void SendKeyClass(String message, String classname);

        void SendKeyID(String message, String id);

        void SendKeyXpath(String message, String xpath, int location);
        void SendKeyXpath(String message, String xpath);

        void SendKeyName(String message, String name, int location);
        void SendKeyName(String message, String name);

        void SendKeyJS(String message, String js);

        void SendKeyByJS(String message, String js);
        #endregion

        #region writekey
        void WriteKeyClass(String message,String classname, int location, int timeDelay);
        void WriteKeyClass(String message, String classname, int timeDelay);

        void WriteKeyID(String message, String id, int timeDelay);

        void WriteKeyXpath(String message, String xpath, int location, int timeDelay);
        void WriteKeyXpath(String message, String xpath, int timeDelay);

        void WriteKeyName(String message, String name, int location, int timeDelay);
        void WriteKeyName(String message, String name, int timeDelay);

        void WriteKeyJS(String message, String js, int timeDelay);

        void WriteKeyByJS(String message, String js, int timeDelay);
        #endregion

        #region gettext
        String GetTextClass(String classname, int location);
        String GetTextClass(String classname);
        String GetTextClass(String classname, String message);

        String GetTextID(String id);

        String GetTextXpath(String xpath, int location);
        String GetTextXpath(String xpath);
        String GetTextXpath(String xpath, String message);

        String GetTextName(String name, int location);
        String GetTextName(String name);
        String GetTextName(String xpath, String message);

        String GetTextJS(String js);

        String GetTextByJS(String js);
        #endregion

        #region get attr
        String GetAttrClass(String attr, String classname, int location);
        String GetAttrClass(String attr, String classname);
        String GetAttrClass(String attr, String classname, String message);

        String GetAttrID(String attr, String id);

        String GetAttrXpath(String attr,String xpath, int location);
        String GetAttrXpath(String attr, String xpath);
        String GetAttrXpath(String attr, String xpath, String message);

        String GetAttrName(String attr, String name, int location);
        String GetAttrName(String attr, String name);
        String GetAttrName(String attr, String xpath, String message);

        String GetAttrJS(String attr, String js);

        String GetAttrByJS(String attr, String js);
        #endregion

        #region get length
        int GetQuantityClass(String classname);
        int GetQuantityXpath(String xpath);
        int GetQuantityName(String name);
        #endregion
    }
}
