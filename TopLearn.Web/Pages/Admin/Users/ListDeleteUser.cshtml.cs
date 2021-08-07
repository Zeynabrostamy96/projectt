using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TapLearn.Core.DTOs;
using TapLearn.Core.Services.interfaces;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class ListDeleteUserModel : PageModel
    {
        private IUserService _userService;
        public ListDeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }
        public   UserPanelViewModel UserPanelViewModel { get; set; }
        public void OnGet(int id)
        {
            ViewData["userid"] = id;
           UserPanelViewModel= _userService.GetUserPanelViewModel(id);

        }
        public IActionResult OnPost(int userid)
        {
            _userService.DeleteUser(userid);
            return RedirectToPage("index");
        }
    }
}
