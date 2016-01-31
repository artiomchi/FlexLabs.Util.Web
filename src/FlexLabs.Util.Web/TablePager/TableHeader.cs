using System;

namespace FlexLabs.Web.TablePager
{
    public class TableHeader : ITableHeader
    {
        public String Title { get; set; }
        public Object Value { get; set; }
        public String ToolTip { get; set; }
        public String CssClass { get; set; }
        public Int32? ColSpan { get; set; }
        public Int32? RowSpan { get; set; }
    }
}
