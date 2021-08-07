using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TapLearn.Core.Security;
using TapLearn.Core.Services.interfaces;
using TapLearn.Layer.Entites.User;

namespace TopLearn.Web.Pages.Admin.Roles
{
    [PermissionCheckerAttibute(6)]
    public class EditRoleModel : PageModel
    {
        private IPermissionService _permissionService;
        public EditRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [BindProperty]
        public Role roles { get; set; }
        public void OnGet(int id)
        {
            roles= _permissionService.GetRoleByid(id);
            ViewData["permissions"] = _permissionService.GetPermissions();
            ViewData["SelectedPermission"] = _permissionService.permissionRoles(id);
        }
        public IActionResult OnPost(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
                return Page();
            _permissionService.UpdateRole(roles);
            _permissionService.UpdatePermissionRole(roles.Roleid, SelectedPermission);
            return RedirectToPage("index");
        }
    }
}
