using System;
using System.Web;

namespace FlexLabs.Web.TablePager
{
    public class CustomTableHeader : ITableHeader
    {
        public Func<Object, IHtmlString> Content { get; set; }
        public String CssClass { get; set; }
        public Int32? ColSpan { get; set; }
        public Int32? RowSpan { get; set; }
    }
}
