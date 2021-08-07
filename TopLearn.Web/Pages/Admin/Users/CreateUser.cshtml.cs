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
    public class CreateUserModel : PageModel
    {
        private IPermissionService _permissionService { get; set; }
        private IUserService _userService { get; set; }
        public CreateUserModel(IPermissionService permissionService, IUserService userService)
        {
            _permissionService = permissionService;
            _userService = userService;
        }
        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetRoles();

        }
        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
                return Page();

            //add user
            int userid = _userService.AddUserFormAdmin(CreateUserViewModel);
            //add role
            _permissionService.AdduserinRole(userid,SelectedRoles );

            return Redirect("/Admin/Users");
        }
    }
}
