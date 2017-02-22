using FlexLabs.Util.AspNetCore.Sample.Models;
using FlexLabs.Web;

namespace FlexLabs.Util.AspNetCore.Sample.ViewModels.Home
{
    public class IndexModel : TableModel<UserSorter, User>
    {
        public IndexModel()
            : base(UserSorter.ID, true)
        { }
    }
}
