using System;
using System.Linq;

namespace FlexLabs.Web.TablePager
{
    public class PagedListData
    {
        public PagedListData(int pageNumber, int pageSize, int pageCount, int totalCount)
        {
            var range = TableModel.DefaultPageRange;
            PageNumber = pageNumber;
            PageCount = pageCount;
            PageRange = Enumerable.Range(Math.Max(1, Math.Min(pageNumber - range / 2 + 1, pageCount - range + 1)), range)
                .TakeWhile(i => i <= pageCount).ToArray();
        }

        public bool CanSeeFirstPage() { return PageRange.Contains(1); }
        public bool CanSeeLastPage() { return PageRange.Contains(PageCount); }

        public int[] PageRange { get; private set; }
        public int PageCount { get; private set; }
        public int PageNumber { get; private set; }
    }
}
