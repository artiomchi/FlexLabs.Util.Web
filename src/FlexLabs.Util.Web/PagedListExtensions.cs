using System.Collections.Generic;
using System.Linq;

namespace FlexLabs.Web
{
    public static class PagedListExtensions
    {
        /// <summary>
        /// Extract a single page worth of results from the dataset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
            => ToPagedList(source.AsQueryable(), pageNumber, pageSize);

        /// <summary>
        /// Extract a single page worth of results from the dataset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalResults = source.Count();
            var pageResults = pageNumber > 1
                ? source.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                : source.Take(pageSize);

            return new PagedList<T>(pageNumber, pageSize, pageResults, totalResults);
        }
    }
}
