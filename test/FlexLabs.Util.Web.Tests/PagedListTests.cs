using FlexLabs.Web;
using System.Linq;
using Xunit;

namespace FlexLabs.Util.Web.Tests
{
    public class PagedListTests
    {
        [Fact]
        public void PagedList_Properties()
        {
            int pageNumber = 1, pageSize = 10, pages = 20, totalCount = pageSize * pages;
            var data = Enumerable.Range(1, totalCount);

            var pagedList = data.ToPagedList(pageNumber, pageSize);

            Assert.Equal(pageNumber, pagedList.PageNumber);
            Assert.Equal(pageSize, pagedList.PageSize);
            Assert.Equal(pageSize, pagedList.Count);
            Assert.Equal(totalCount, pagedList.TotalItemCount);
        }

        [Fact]
        public void PagedList_PageCount_Simple()
        {
            int pageNumber = 1, pageSize = 10, pages = 20, totalCount = pageSize * pages;
            var data = Enumerable.Range(1, totalCount);

            var pagedList = data.ToPagedList(pageNumber, pageSize);

            Assert.Equal(totalCount, pagedList.TotalItemCount);
            Assert.Equal(pages, pagedList.PageCount);
        }

        [Fact]
        public void PagedList_PageCount_PartFilled()
        {
            int pageNumber = 1, pageSize = 10, pages = 20, totalCount = pageSize * pages + pageSize / 2;
            var data = Enumerable.Range(1, totalCount);

            var pagedList = data.ToPagedList(pageNumber, pageSize);

            Assert.Equal(totalCount, pagedList.TotalItemCount);
            Assert.Equal(pages + 1, pagedList.PageCount);
        }
    }
}
