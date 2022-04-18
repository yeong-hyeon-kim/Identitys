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
        List<IdentityRole> GetRolesList();
        List<Users> GetLockList();

        public void CreateUser(string UserCd, string UserNm, string UserDept, string UserEmail, string UserContact = "");
        public void UpdateUser(string UserCd, string UserDept, string UserContact);
        public void DeleteUser(string UserId, string UserCd);
        public void GrantAuthorizationUser(string UserEmail, string RoleName);

        public void DeleteRole(string RoleName);
        public void InsertRole(string RoleId, string RoleNm);
        public void UpdateRole(string RoleId, string RoleNm);

        public void DeleteLock(string LockUserId, bool LockoutEnabled);

        #endregion
    }
}
