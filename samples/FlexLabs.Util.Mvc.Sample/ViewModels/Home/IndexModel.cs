using FlexLabs.Util.Mvc.Sample.Models;
using FlexLabs.Web;

namespace FlexLabs.Util.Mvc.Sample.ViewModels.Home
{
    public class IndexModel : TableModel<UserSorter, User>
    {
        public IndexModel()
            : base(UserSorter.ID, true)
        { }
    }
}
