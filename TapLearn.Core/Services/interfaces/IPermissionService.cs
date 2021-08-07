using System;
using System.Collections.Generic;
using System.Text;
using TapLearn.Layer.Entites.permission;
using TapLearn.Layer.Entites.User;

namespace TapLearn.Core.Services.interfaces
{
    public  interface IPermissionService
    {
        #region Roles
        List<Role> GetRoles();
        void AdduserinRole(int Userid,List<int> Role);
        void EditRoleUser(int userid, List<int> Roles);
        int AddRole(Role role);
        Role GetRoleByid(int roleid);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        #endregion
        #region Permission
        List<permission> GetPermissions();
        void AddPermissionToRole(int roleid, List<int> permission);
        List<int> permissionRoles(int roleid);
        void UpdatePermissionRole(int roleid,List<int>permissions);
        bool checkerPermission(int permissionid, string username);
        #endregion
    }
}
