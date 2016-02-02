using System;
using System.Collections;
using System.Collections.Generic;

namespace FlexLabs.Web.TablePager
{
    public class TableHeaderCollection : ICollection<ITableHeader>
    {
        private readonly IList<ITableHeader> _headers = new List<ITableHeader>();

        public int Count { get { return _headers.Count; } }

        public bool IsReadOnly { get { return false; } }

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

        public void Clear()
        {
            _headers.Clear();
        }

        public bool Contains(ITableHeader item)
        {
            return _headers.Contains(item);
        }

        public void CopyTo(ITableHeader[] array, int arrayIndex)
        {
            _headers.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ITableHeader> GetEnumerator()
        {
            return _headers.GetEnumerator();
        }

        public bool Remove(ITableHeader item)
        {
            return _headers.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _headers.GetEnumerator();
        }
    }
}
