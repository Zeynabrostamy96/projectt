using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TapLearn.Core.Convertors;
//using TapLearn.Core.Course;
using TapLearn.Core.DTOs;
using TapLearn.Core.DTOs.Course;
using TapLearn.Core.Generator;
using TapLearn.Core.Security;
using TapLearn.Core.Services.interfaces;
using TapLearn.Layer.Context;
using TapLearn.Layer.Entites.User;
using TapLearn.Layer.Entites.Wallet;

namespace TapLearn.Core.Services
{
    public class UserService : IUserService
    {
        private TopLearnContext _context;
        public UserService(TopLearnContext  contex)
        {
            _context = contex;

        }

      
        public bool ActiveAccount(string activeCode)
        {
            var user = _context.users.SingleOrDefault(u => u.ActiveCode == activeCode);
            if(user==null || user.IsActive)
            {
                return false;
            }
            user.IsActive = true;
            user.ActiveCode = NameGenerater.GenerateUniqCode();
            _context.SaveChanges();
            return true;
        }

        public int adduser(User user)
        {
            _context.users.Add(user);
            _context.SaveChanges();
            return user.Userid;
        }
        public bool IsExisistEmail(string email)
        {
            return _context.users.Any(u => u.Email == email);
        }

        public bool IsExisistUserName(string userName)
        {
            return _context.users.Any(u => u.UserName == userName);
        }

        public User LoginUser(LoginViewModel login)
        {
            string hashcode = PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixedText.FixEmail(login.Email);
            return  _context.users.FirstOrDefault(e => e.Email == email && e.Password == hashcode);
        }

        public User GetuserByEmail(string email)
        {
            return _context.users.FirstOrDefault(c => c.Email == email);
        }

        public User GetuserByActiveCode(string activecode)
        {
          return  _context.users.FirstOrDefault(u => u.ActiveCode == activecode);

        }

       

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public UserPanelViewModel GetUserPanelViewModel(string username)
        {
            User user = GetUserByUserName(username);
            UserPanelViewModel userPanel = new UserPanelViewModel();
            userPanel.UserName = user.UserName;
            userPanel.Email = user.Email;
            userPanel.RegisterDate = user.RegisterDate;
            userPanel.Wallet = AccountBalance(username);
            return userPanel; 
        }

        public User GetUserByUserName(string username)
        {
            return _context.users.SingleOrDefault(c => c.UserName== username);
        }

        public SideBarUserPanelViewModel GetSideBarUserPanelData(string username)
        {
            var user = _context.users.SingleOrDefault(u => u.UserName == username);
            SideBarUserPanelViewModel sideBar = new SideBarUserPanelViewModel();
            sideBar.UserName = user.UserName;
            sideBar.ImageName = user.UserAvatar;
            sideBar.DataRegister = user.RegisterDate;
            return sideBar;
        }

        public EditProfileViewModel GetEditProfile(string username)
        {
            var user = _context.users.SingleOrDefault(c => c.UserName == username);
            EditProfileViewModel profile = new EditProfileViewModel();
            profile.UserName = user.UserName;
            profile.Email = user.Email;
            profile.AvatorName = user.UserAvatar;
            return profile;
        }
        public void EditProfile(string name, EditProfileViewModel profile)
        {
            if(profile.UserAvator != null)
            {
                //metod marbot be image
                string imagepath = "";
                if(profile.AvatorName != "Defult.jpg")
                {
                   
                   imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvator", profile.AvatorName);
                    if (File.Exists(imagepath))
                    {
                        File.Delete(imagepath);
                    }
                }
                
                profile.AvatorName = NameGenerater.GenerateUniqCode() + Path.GetExtension(profile.UserAvator.FileName);
                imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvator", profile.AvatorName);
                using (var stream = new FileStream(imagepath, FileMode.Create))
                {
                    profile.UserAvator.CopyTo(stream);
                }
            }
            User user = GetUserByUserName(name);
            user.UserName = profile.UserName;
            user.Email = profile.Email;
            user.UserAvatar = profile.AvatorName;
            UpdateUser(user);
             
        }

        public bool CompareOldPassword(string Username, string OldPassword)
        {
            string hashcode = PasswordHelper.EncodePasswordMd5(OldPassword);
            return _context.users.Any(u => u.UserName == Username && u.Password == hashcode);
        }

        public void ChangeUserPassword(string username, string password)
        {
           var user = GetUserByUserName(username);
            user.Password = PasswordHelper.EncodePasswordMd5(password);
            UpdateUser(user);
        }

        public int AccountBalance(string username)
        {
            int userid = GetUserIdByUserName(username);
            var deposit = _context.Wallets.Where(w => w.Userid == userid && w.TypeId == 1 && w.IsPay).Select(a => a.Amount).ToList();
            var harvest = _context.Wallets.Where(w => w.Userid == userid && w.TypeId == 2 && w.IsPay).Select(a => a.Amount).ToList();
            return (deposit.Sum() - harvest.Sum());
        }

        public int GetUserIdByUserName(string username)
        {
            return _context.users.SingleOrDefault(u => u.UserName == username).Userid;
        }

        public List<walletViewModel> GetwalletUser(string username)
        {
            int userid = GetUserIdByUserName(username);
            return _context.Wallets.Where(w => w.Userid == userid && w.IsPay).Select(c => new walletViewModel
            {
                Amount = c.Amount,
                Type=c.TypeId,
                Description=c.Description,
                dateTime=c.CreateDate,
            }).ToList() ;
        }

        public int ChargeWallet(string username, int amoute, string Description, bool IsPay = false)
        {
            wallet wallet = new wallet
            {
                Amount = amoute,
                CreateDate = DateTime.Now,
                Description = Description,
                IsPay = IsPay,
                TypeId = 1,
                Userid = GetUserIdByUserName(username)

            };
          return AddWallet(wallet);
        }

        public int AddWallet(wallet wallet)
        {
            _context.Wallets.Add(wallet);
            _context.SaveChanges();
            return wallet.walletid;
        }

        public wallet GetWalletByWalletid(int walletid)
        {
          return  _context.Wallets.Find(walletid);
        }

        public void UpdateWallet(wallet wallet)
        {
            _context.Wallets.Update(wallet);
            _context.SaveChanges();

        }

        public UsersViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.users;


            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }

            //Show Item In Page
            int take = 2;
            int skip = (pageId - 1) * take;
            UsersViewModel list = new UsersViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();
            return list;

        }

        public int AddUserFormAdmin(CreateUserViewModel user)
        {
            User createUserViewModel = new User();
            createUserViewModel.Email = user.Email;
            createUserViewModel.UserName = user.UserName;
            createUserViewModel.Password = PasswordHelper.EncodePasswordMd5(user.Password);
            createUserViewModel.IsActive = true;
            createUserViewModel.RegisterDate = DateTime.Now;
            createUserViewModel.ActiveCode= NameGenerater.GenerateUniqCode();
            #region save avatar
            if (user.UserAvator != null)
            {
                string imagepath = "";
                createUserViewModel.UserAvatar = NameGenerater.GenerateUniqCode() + Path.GetExtension(user.UserAvator.FileName);
                imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvator", createUserViewModel.UserAvatar);
                using (var stream = new FileStream(imagepath, FileMode.Create))
                {
                    user.UserAvator.CopyTo(stream);
                }
            }
            #endregion
            return adduser(createUserViewModel);
        }
        public EditUserViewModel GetEditUserViewModel(int userid)
        {
            return _context.users.Where(u => u.Userid == userid).Select(c => new EditUserViewModel
            {
                userid = c.Userid,
                Email = c.Email,
                UserName = c.UserName,
                AvatarName = c.UserAvatar,
                UserRoles = c.userRoles.Select(r=>r.Roleid).ToList()

            }).SingleOrDefault();
        }

        public void EditUserFromAdmin(EditUserViewModel editUser)
        {
           User user= GetUserByid(editUser.userid);
           user.Email = editUser.Email;
           if (!string.IsNullOrEmpty(editUser.Password))
           {
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);

           }
            
            if (editUser.UserAvator != null)
            {
                if(editUser.AvatarName != "default.jpg")
                {
                    //TODO Delete Image
                    string deletpath = "";

                    deletpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvator", user.UserAvatar);
                    if (File.Exists(deletpath))
                    {
                        File.Delete(deletpath);
                    }


                }

                //Save New Image
                string imagepath = "";


               user.UserAvatar = NameGenerater.GenerateUniqCode() + Path.GetExtension(editUser.UserAvator.FileName);
               imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvator", user.UserAvatar);
               using (var stream = new FileStream(imagepath, FileMode.Create))
               {
                editUser.UserAvator.CopyTo(stream);
               }
              

            }
            _context.users.Update(user);
            _context.SaveChanges();

        }

        public User GetUserByid(int userid)
        {
            return _context.users.Find(userid);
        }

        public UsersViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.users.IgnoreQueryFilters().Where(u => u.IsDelete);
            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }
            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }
            //Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;
            UsersViewModel list = new UsersViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();
            return list;
        }

        public UserPanelViewModel GetUserPanelViewModel(int id)
        {
            User user = GetUserByid(id);
            UserPanelViewModel userPanel = new UserPanelViewModel();
            userPanel.UserName = user.UserName;
            userPanel.Email = user.Email;
            userPanel.RegisterDate = user.RegisterDate;
            userPanel.Wallet = AccountBalance(user.UserName);
            return userPanel;

        }

        public void DeleteUser(int id)
        {
            User user = GetUserByid(id);
            user.IsDelete = true;
            UpdateUser(user);
        }
    }
}
