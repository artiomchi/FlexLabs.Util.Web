using FlexLabs.Linq;
using FlexLabs.Util.Mvc.Sample.Models;
using FlexLabs.Util.Mvc.Sample.ViewModels.Home;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FlexLabs.Util.Mvc.Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly static OrderByExpressionCollection<UserSorter, User> _userSorter = new OrderByExpressionCollection<UserSorter, User>
        {
            {  UserSorter.ID, u => u.ID },
            {  UserSorter.Name, u => u.Name },
        };
        IEnumerable<User> GetUsersFromDatabase(UserSorter sortBy, bool sortAsc)
            => Enumerable.Range(1, 10000)
                .Select(i => new User
                {
                    ID = i,
                    Name = $"User {i}",
                })
                .AsQueryable()
                .OrderBy(_userSorter, sortBy, sortAsc);

        public ActionResult Index(IndexModel model)
        {
            var allSortedUsers = GetUsersFromDatabase(model.GetSortBy(), model.GetSortAsc());
            model.SetPageItems(allSortedUsers);
            return View(model);
        }
    }
}