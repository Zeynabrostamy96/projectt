using System;
using System.Collections.Generic;
using System.Text;
using TapLearn.Core.DTOs;
using TapLearn.Core.DTOs.Course;
using TapLearn.Layer.Entites.User;
using TapLearn.Layer.Entites.Wallet;

namespace TapLearn.Core.Services.interfaces
{
    public interface IUserService
    {
         #region Account
        bool IsExisistUserName(string userName);
        bool IsExisistEmail(string email);
        int adduser(User user );
        User LoginUser(LoginViewModel login);
        bool ActiveAccount(string activeCode);
        User GetuserByEmail(string email);
        User GetuserByActiveCode(string activecode);
        void UpdateUser(User user);
        User GetUserByUserName(string username);
        int GetUserIdByUserName(string username);
        User GetUserByid(int userid);
        #endregion
         #region UserPanel
        UserPanelViewModel GetUserPanelViewModel(string username);
        UserPanelViewModel GetUserPanelViewModel(int id);
        SideBarUserPanelViewModel GetSideBarUserPanelData(string username);
        void EditProfile(string name, EditProfileViewModel profile);
        EditProfileViewModel GetEditProfile(string username);
        bool CompareOldPassword(string Username, string OldPassword);
        void ChangeUserPassword(string username, string password);
        #endregion
         #region Wallet
         int AccountBalance(string username);
         List<walletViewModel> GetwalletUser(string username);
         int ChargeWallet(string username, int amoute,string Description,bool IsPay=false);
         int AddWallet(wallet wallet);
         wallet GetWalletByWalletid(int walletid);
         void UpdateWallet(wallet wallet);
         #endregion
         #region adminPanel
         UsersViewModel GetUsers(int pageId = 1 , string filterEmail="", string filterUserName = "");
         UsersViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");
         int AddUserFormAdmin(CreateUserViewModel user);
         EditUserViewModel GetEditUserViewModel(int userid);
         void EditUserFromAdmin(EditUserViewModel editUser);
         void DeleteUser(int id);
        
        #endregion
    }
}
