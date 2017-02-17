using System.Collections;
using System.Collections.Generic;

namespace FlexLabs.Web
{
    public interface IPagedList
    {
        int Count { get; }
        int PageNumber { get; }
        int PageSize { get; }
        int PageCount { get; }
        int TotalItemCount { get; }
    }

    public interface IPagedList<T> : IPagedList, IEnumerable<T>, IEnumerable
    {
        T this[int index] { get; }
    }
}
