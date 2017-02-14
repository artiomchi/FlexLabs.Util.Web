using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FlexLabs.Util.Web
{
    internal class PagedList<T> : IPagedList<T>
    {
        private readonly IList<T> _pageResults;

        internal PagedList(int pageNumber, int pageSize, IEnumerable<T> pageResults, int totalItemCount)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            PageCount = Convert.ToInt32(Math.Ceiling((decimal)totalItemCount / pageSize));
            _pageResults = pageResults.ToList();
        }

        public int PageNumber { get; }
        public int PageSize { get; }
        public int PageCount { get; }
        public int TotalItemCount { get; }
        public int Count => _pageResults.Count;
        public T this[int index] => _pageResults[index];
        public IEnumerator<T> GetEnumerator() => _pageResults.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _pageResults.GetEnumerator();
    }
}
