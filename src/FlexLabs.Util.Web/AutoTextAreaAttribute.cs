using System;

namespace FlexLabs.Web
{
    public sealed class AutoTextAreaAttribute : Attribute
    {
        public Int32? Rows { get; set; }
        public Int32? Columns { get; set; }
    }
}
