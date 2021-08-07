using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TapLearn.Core.Services.interfaces;
using TapLearn.Layer.Context;
using TapLearn.Layer.Entites.permission;
using TapLearn.Layer.Entites.User;

namespace TapLearn.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private TopLearnContext _context;
        public PermissionService(TopLearnContext context)
        {
            _context = context;  
        }

        public void AddPermissionToRole(int roleid, List<int> permission)
        {
           foreach(var p in permission)
           {
                _context.rolePermissions.Add(new RolePermission
                {
                    PermissionId = p,
                    Roleid=roleid
                }); 

           }
            _context.SaveChanges();
        }

        public int AddRole(Role role)
        {
            _context.roles.Add(role);
            _context.SaveChanges();
            return role.Roleid;
        }

        public void AdduserinRole(int Userid, List<int> Role)
        {
            foreach (int roleid in Role)
            {
                _context.userRoles.Add(new UserRole()
                {
                    Roleid = roleid,
                    Userid = Userid
                });
            }
            _context.SaveChanges();
        }

        public bool checkerPermission(int permissionid, string username)
        {
            int userId = _context.users.SingleOrDefault(u => u.UserName == username).Userid;
            List<int> UsersRoles = _context.userRoles.Where(r => r.Userid == userId).Select(r => r.Roleid).ToList();
            if (!UsersRoles.Any())
                return false;
            List<int> PermissionRolse = _context.rolePermissions.Where(r => r.PermissionId == permissionid).Select(p => p.Roleid).ToList();
            return PermissionRolse.Any(p => UsersRoles.Contains(p));
        }

        public void DeleteRole(Role role)
        {
            role.IsDelete = true;
            UpdateRole(role);
        }

        public void EditRoleUser(int userid, List<int> Roles)
        {
            //Delete All Roles User
            _context.userRoles.Where(u => u.Userid == userid).ToList().ForEach(r=>_context.userRoles.Remove(r));
            //Edit Role
            AdduserinRole(userid,Roles );
        }

        public List<permission> GetPermissions()
        {
            return _context.permissions.ToList();
        }

        public Role GetRoleByid(int roleid)
        {
            return _context.roles.Find(roleid);
        }

        public  List<Role> GetRoles()
        {
            return _context.roles.ToList();
        }

        public List<int> permissionRoles(int roleid)
        {
            return _context.rolePermissions
                .Where(r => r.Roleid == roleid)
                .Select(r => r.PermissionId).ToList();
        }

        public void UpdatePermissionRole(int roleid, List<int> permissions)
        {
            _context.rolePermissions.Where(t => t.Roleid == roleid).ToList()
                .ForEach(p => _context.rolePermissions.Remove(p));
            AddPermissionToRole(roleid, permissions);

        }

        public void UpdateRole(Role role)
        {
            _context.roles.Update(role);
            _context.SaveChanges();

        }
    }
}
