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

        public void CreateIdentityUser(string UserId, string Email, string UserPassword, bool EmailConfirmation);
        public void CreateUser(string UserCd, string UserNm, string UserDept, string UserEmail, string UserContact = "");
        public void UpdateUser(string UserCd, string UserDept, string UserContact);
        public void DeleteUser(string UserId, string UserCd);

        #endregion

        #region 역할(권한)
        List<IdentityRole> GetRolesList();
        public void GrantAuthorizationUser(string UserEmail, string RoleName);
        public void DeleteRole(string RoleName);
        public void InsertRole(string RoleId, string RoleNm);
        public void UpdateRole(string RoleId, string RoleNm);

        #endregion

        List<Users> GetLockList();
        public void DeleteLock(string LockUserId, bool LockoutEnabled);

    }
}
