namespace FlexLabs.Mvc.Html
{
    public class TableHeader : ITableHeader
    {
        public string Title { get; set; }
        public object Value { get; set; }
        public string ToolTip { get; set; }
        public string CssClass { get; set; }
        public int? ColSpan { get; set; }
        public int? RowSpan { get; set; }
        public bool IsRendered { get; set; } = true;
    }
}
