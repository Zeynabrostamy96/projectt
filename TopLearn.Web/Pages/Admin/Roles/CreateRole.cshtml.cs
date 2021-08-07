using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TapLearn.Layer.Entites.User;
using TapLearn.Core.Services.interfaces;
using TapLearn.Core.Security;

namespace TopLearn.Web.Pages.Admin.Roles
{
    [PermissionCheckerAttibute(6)]
    public class CreateRoleModel : PageModel
    {
        private IPermissionService _permissionService;
        public CreateRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [BindProperty]
        public Role Roles { get; set; }
        public void OnGet()
        {
            ViewData["permissions"] = _permissionService.GetPermissions();

        }
        public IActionResult OnPost(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
                return Page();
           int roleid= _permissionService.AddRole(Roles);
            //todo add permission
            _permissionService.AddPermissionToRole(roleid, SelectedPermission);
            return RedirectToPage("Index");
        }
    }
}
