using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TapLearn.Core.DTOs;
using TapLearn.Core.Security;
using TapLearn.Core.Services.interfaces;

namespace TopLearn.Web.Pages.Admin.Users
{
    [PermissionCheckerAttibute(2)]
    public class DeleteUserModel : PageModel
    {
        private IUserService _userService;
        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }
        public UsersViewModel UsersViewModel { get; set; }
        public void OnGet(int pageId = 1, string filterEmail = "", string UserName = "")
        {
            UsersViewModel = _userService.GetDeleteUsers(pageId, filterEmail, UserName);
        }
    }
}
