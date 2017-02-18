using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexLabs.Linq;
using FlexLabs.Util.AspNetCore.Sample.Models;
using FlexLabs.Util.AspNetCore.Sample.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace FlexLabs.Util.AspNetCore.Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly static OrderByExpressionCollection<UserSorter, User> _userSorter = new OrderByExpressionCollection<UserSorter, User>
        {
            {  UserSorter.ID, u => u.ID },
            {  UserSorter.Name, u => u.Name },
        };
        IEnumerable<User> GetUsersFromDatabase(UserSorter sortBy, bool sortAsc)
            => Enumerable.Range(1, 10_000)
                .Select(i => new User
                {
                    ID = i,
                    Name = $"User {i}",
                })
                .AsQueryable()
                .OrderBy(_userSorter, sortBy, sortAsc);

        public IActionResult Index(IndexModel model)
        {
            var allSortedUsers = GetUsersFromDatabase(model.GetSortBy(), model.GetSortAsc());
            model.SetPageItems(allSortedUsers);
            return View(model);
        }
    }
}
