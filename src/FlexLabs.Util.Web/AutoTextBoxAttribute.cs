using System;

namespace FlexLabs.Web
{
    public sealed class AutoTextBoxAttribute : Attribute
    {
        /// <summary>
        /// A string that is used to format the input
        /// </summary>
        public string Format { get; set; }
        public string Type { get; set; }
    }
}
