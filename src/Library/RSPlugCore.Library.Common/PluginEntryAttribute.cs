using System;

namespace RSPlugCore.Library.Common
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class PluginEntryAttribute : System.Attribute
    {
        public virtual string Name
        {
            get;
            set;
        }
    }
}
