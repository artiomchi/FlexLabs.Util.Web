using FlexLabs.Web.TablePager;
using System;
using System.Linq;
using System.Web;
using Xunit;

namespace FlexLabs.Util.Web.Tests.TablePager
{
    public class TableHeaderCollectionTests
    {
        [Fact]
        public void TableHeaderCollection_InlineInit()
        {
            Func<Object, IHtmlString> customContent = x => new HtmlString("hello");

            var headers = new TableHeaderCollection
            {
                { "Header 1" },
                { "Header 2", 123 },
                { new TableHeader { Title = "Header 3", CssClass = "css" } },
                { new CustomTableHeader { Content = customContent } },
            }.ToList();

            Assert.Equal(4, headers.Count);

            Assert.IsType<TableHeader>(headers[0]);
            Assert.Equal("Header 1", (headers[0] as TableHeader).Title);

            Assert.IsType<TableHeader>(headers[1]);
            Assert.Equal("Header 2", (headers[1] as TableHeader).Title);
            Assert.Equal(123, (headers[1] as TableHeader).Value);

            Assert.IsType<TableHeader>(headers[2]);
            Assert.Equal("Header 3", (headers[2] as TableHeader).Title);
            Assert.Equal("css", (headers[2] as TableHeader).CssClass);

            Assert.IsType<CustomTableHeader>(headers[3]);
            Assert.Equal(customContent, (headers[3] as CustomTableHeader).Content);
        }
    }
}
