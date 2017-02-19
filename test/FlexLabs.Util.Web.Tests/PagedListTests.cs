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

        [Theory]
        [InlineData(200, 20)]
        [InlineData(201, 21)]
        [InlineData(205, 21)]
        [InlineData(209, 21)]
        [InlineData(210, 21)]
        [InlineData(211, 22)]
        public void PagedList_PageCount(int totalCount, int expectingPages)
        {
            int pageNumber = 1, pageSize = 10;
            var data = Enumerable.Range(1, totalCount);

            var pagedList = data.ToPagedList(pageNumber, pageSize);

            Assert.Equal(totalCount, pagedList.TotalItemCount);
            Assert.Equal(expectingPages, pagedList.PageCount);
        }
    }
}
