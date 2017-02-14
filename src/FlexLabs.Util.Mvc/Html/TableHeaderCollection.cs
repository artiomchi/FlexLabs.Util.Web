using System;
using System.Collections;
using System.Collections.Generic;

namespace FlexLabs.Util.Mvc.Html
{
    public class TableHeaderCollection : ICollection<ITableHeader>
    {
        private readonly IList<ITableHeader> _headers = new List<ITableHeader>();

        public int Count => _headers.Count;
        public bool IsReadOnly => false;

        public void Add(ITableHeader item)
        {
            _headers.Add(item);
        }

        public void Add(Boolean isRendered, String title, Object value = null, String tooltip = null)
        {
            _headers.Add(new TableHeader { Title = title, Value = value, ToolTip = tooltip, IsRendered = isRendered });
        }

        public void Add(String title, Object value = null, String tooltip = null)
        {
            _headers.Add(new TableHeader { Title = title, Value = value, ToolTip = tooltip });
        }

        public void Clear() => _headers.Clear();
        public bool Contains(ITableHeader item) => _headers.Contains(item);
        public void CopyTo(ITableHeader[] array, int arrayIndex) => _headers.CopyTo(array, arrayIndex);
        public IEnumerator<ITableHeader> GetEnumerator() => _headers.GetEnumerator();
        public bool Remove(ITableHeader item) => _headers.Remove(item);
        IEnumerator IEnumerable.GetEnumerator() => _headers.GetEnumerator();
    }
}
