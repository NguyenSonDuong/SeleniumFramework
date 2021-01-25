using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.model
{
    public class ActionSelenium
    {
        private String key;
        private int position;
        private String sameString;
        private bool isExactly;
        private String value;
        private EnumActionTypeBySelenium bySelenium;
        private EnumActionTypeSelenium type;
        private EnumActionSelenium enumAction;

        public int Position { get => position; set => position = value; }
        public string SameString { get => sameString; set => sameString = value; }
        public bool IsExactly { get => isExactly; set => isExactly = value; }
        public string Value { get => value; set => this.value = value; }
        public string Key { get => key; set => key = value; }
        internal EnumActionSelenium EnumAction { get => enumAction; set => enumAction = value; }
        internal EnumActionTypeSelenium Type { get => type; set => type = value; }
        internal EnumActionTypeBySelenium BySelenium { get => bySelenium; set => bySelenium = value; }
    }
}
