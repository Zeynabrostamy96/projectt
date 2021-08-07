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
    public class EditUserModel : PageModel
    {
        private IPermissionService _permissionService { get; set; }
        private IUserService _userService { get; set; }
        public EditUserModel(IUserService userService,IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [BindProperty]
        public EditUserViewModel EditUserViewModel { get; set; }
        public void OnGet(int id)
        {
            EditUserViewModel = _userService.GetEditUserViewModel(id);
            ViewData["Roles"] = _permissionService.GetRoles();

        }
        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
                return Page();
            _userService.EditUserFromAdmin(EditUserViewModel);
            _permissionService.EditRoleUser(EditUserViewModel.userid, SelectedRoles);

            return RedirectToPage("Index");

        }

    }
}

