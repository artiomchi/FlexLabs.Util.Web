using System;
using System.Collections.Generic;
using System.Text;
using FlexLabs.Web;

namespace FlexLabs.Util.Web.Tests.Models
{
    public class TestEntityModel : TableModel<TestEntitySorter, TestEntity>
    {
        public TestEntityModel(TestEntitySorter defaultSorter, bool defaultAscending, bool pagingEnabled = true) : base(defaultSorter, defaultAscending, pagingEnabled)
        {
        }
    }
}
