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
    public class DeleteRolesModel : PageModel
    {
        
        private IPermissionService _permissionService;
        public DeleteRolesModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [BindProperty]
        public Role roles { get; set; }
        public void OnGet(int id)
        {
            roles = _permissionService.GetRoleByid(id);
        }
        public IActionResult OnPost()
        { 
            _permissionService.DeleteRole(roles);
            return RedirectToPage("Index");
        }
    }
}
