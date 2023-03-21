using App.Model;
using Microsoft.AspNetCore.Identity;

namespace App.IDAL
{
    public interface IAppDal
    {
        #region 사용자

        List<Users> GetUser(string UserEmail);
        List<Roles> GetIdentityUsers();
        List<Users> GetIdentityNullRoleUsers();
        List<Users> GetIdentityNullUsers();
        List<Users> GetLockList();

        public void CreateIdentityUser(string UserId, string Email, string UserPassword, bool EmailConfirmation);
        public void CreateUser(string UserCd, string UserNm, string UserDept, string UserEmail, string UserContact = "");
        public void UpdateUser(string UserCd, string UserDept, string UserContact);
        public void DeleteUser(string UserId, string UserCd);
        public void DeleteLocalUser(string UserCd);
        public void LockingUser(string UserId, DateTime Date);
        public void UnLockingUser(string LockUserId, bool LockoutEnabled);

        public void SetEmailConfirm(string UserEmail, bool IsEmailConfirm);

        #endregion

        #region 역할(권한)
        List<IdentityRole> GetRolesList();

        public void GrantAuthorizationUser(string UserEmail, string RoleName);
        public void DeleteRole(string RoleName);
        public void CreateRole(string RoleId, string RoleNm);
        public void UpdateRole(string RoleId, string RoleNm);

        #endregion
    }
}
