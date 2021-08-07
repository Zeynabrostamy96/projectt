using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TapLearn.Core.Services.interfaces;
using TapLearn.Core.DTOs;
using TapLearn.Core.Security;

namespace TopLearn.Web.Pages.Admin.Users
{
    //[PermissionCheckerAttibute(1)]
    public class IndexModel : PageModel
    {
        private IUserService _userService;
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }
        public UsersViewModel UsersViewModel { get; set; }
        public void OnGet(int pageId = 1 , string filterEmail="", string UserName="")
        {
            UsersViewModel = _userService.GetUsers(pageId, filterEmail, UserName );
        }
    }
}
