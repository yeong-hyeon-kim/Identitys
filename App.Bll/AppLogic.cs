using App.IDAL;
using App.Model;
using Microsoft.AspNetCore.Identity;

namespace App.BLL
{
    public class AppLogic
    {
        private IAppDal _appDal;

        public AppLogic(IAppDal _AppDal)
        {
            _appDal = _AppDal;
        }

        #region 사용자
        public void CreateUser(string UserCd, string UserNm, string UserDept, string UserEmail, string UserContact = "")
        {
            _appDal.CreateUser(UserCd, UserNm, UserDept, UserEmail, UserContact);
        }

        public List<Users> GetUser(string UserEmail)
        {
            return _appDal.GetUser(UserEmail);
        }

        public List<Roles> GetIdentityUsers()
        {
            return _appDal.GetIdentityUsers();
        }

        public List<Users> GetIdentityNullRoleUsers()
        {
            return _appDal.GetIdentityNullRoleUsers();
        }

        public List<Users> GetIdentityNullUsers()
        {
            return _appDal.GetIdentityNullUsers();
        }

        public List<IdentityRole> GetRolesList()
        {
            return _appDal.GetRolesList();
        }
        public List<Users> GetLockList()
        {
            return _appDal.GetLockList();
        }

        public void DeleteLock(string LockUserId, bool LockoutEnabled)
        {
            _appDal.DeleteLock(LockUserId, LockoutEnabled);
        }

        public void GrantAuthorizationUser(string UserEmail, string RoleName)
        {
            _appDal.GrantAuthorizationUser(UserEmail, RoleName);
        }

        public void CreateIdentityUser(string UserId, string Email, string UserPassword, bool EmailConfirmation)
        {
            _appDal.CreateIdentityUser(UserId, Email, UserPassword, EmailConfirmation);
        }

        public void UpdateUser(string UserCd, string UserDept, string UserContact)
        {
            _appDal.UpdateUser(UserCd, UserDept, UserContact);
        }

        public void DeleteUser(string UserId, string UserCd)
        {
            _appDal.DeleteUser(UserId, UserCd);
        }
        
        public void DeleteLocalUser(string UserCd)
        {
            _appDal.DeleteLocalUser(UserCd);
        }

        public void DeleteRole(string RoleId)
        {
            _appDal.DeleteRole(RoleId);
        }

        public void InsertRole(string RoleId, string RoleNm)
        {
            _appDal.InsertRole(RoleId, RoleNm);
        }

        public void UpdateRole(string RoleId, string RoleNm)
        {
            _appDal.UpdateRole(RoleId, RoleNm);
        }
        #endregion
    }
}
