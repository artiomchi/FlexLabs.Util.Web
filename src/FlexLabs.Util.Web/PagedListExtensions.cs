using System.Collections.Generic;
using System.Linq;

namespace FlexLabs.Web
{
    public static class PagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var totalResults = source.Count();
            var pageResults = pageNumber > 1
                ? source.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                : source.Take(pageSize);

            return new PagedList<T>(pageNumber, pageSize, pageResults, totalResults);
        }
    }
}
