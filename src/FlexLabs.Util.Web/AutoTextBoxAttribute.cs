using System;

namespace FlexLabs.Web
{
    public sealed class AutoTextBoxAttribute : Attribute
    {
        /// <summary>
        /// A string that is used to format the input
        /// </summary>
        public String Format { get; set; }
        public String Type { get; set; }
    }
}
