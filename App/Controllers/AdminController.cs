using App.BLL;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppLogic _AppBll;

        public AdminController(AppLogic AppBll)
        {
            _AppBll = AppBll;
        }

        public IActionResult Index()
        {
            return View(_AppBll.GetIdentityUsers());
        }

        public IActionResult UnAuthorizationList()
        {
            return View(_AppBll.GetIdentityNullRoleUsers());
        }

        public IActionResult UnRegisteredList()
        {
            return View(_AppBll.GetIdentityNullUsers());
        }

        public IActionResult AuthorizationList()
        {
            return View(_AppBll.GetRolesList());
        }

        /// <summary>
        /// 사용자 권한 부여
        /// </summary>
        /// <param name="UserCd">사용자 코드(App.Model.Users.USER_CD)</param>
        /// <param name="UserNm">사용자 명</param>
        /// <param name="UserDept">부서</param>
        /// <param name="UserEmail">이메일</param>
        /// <param name="UserContact">연락처</param>
        /// <param name="AuthorizationName">권한명</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GrantAuthorization(string UserCd, string UserNm, string UserDept, string UserEmail, string UserContact, string AuthorizationName)
        {
            _AppBll.CreateUser(UserCd, UserNm, UserDept, UserEmail, UserContact);
            _AppBll.GrantAuthorizationUser(UserEmail, AuthorizationName);

            return Redirect("~/Admin/Index");
        }

        [HttpPost]
        public IActionResult UpdateUserInfomation(string UserCd, string UserEmails, string UserDept, string UserContact, string UserAuthorization)
        {
            _AppBll.UpdateUser(UserCd, UserDept, UserContact);
            _AppBll.GrantAuthorizationUser(UserEmails, UserAuthorization);

            return Redirect("~/Admin/Index");
        }

        [HttpPost]
        public IActionResult DeleteUser(string UserCd, string UserEmail)
        {
            _AppBll.DeleteUser(UserCd, UserEmail);

            return Redirect("~/Admin/Index");
        }

        [HttpPost]
        public IActionResult DeleteRole(string RoleId)
        {
            _AppBll.DeleteRole(RoleId);

            return Redirect("~/Admin/AuthorizationList");
        }

        [HttpPost]
        public IActionResult InsertRole(string RoleId, string RoleName)
        {
            _AppBll.InsertRole(RoleId, RoleName);

            return Redirect("~/Admin/AuthorizationList");
        }

        [HttpPost]
        public IActionResult UpdateRole(string RoleId, string RoleName)
        {
            _AppBll.UpdateRole(RoleId, RoleName);

            return Redirect("~/Admin/AuthorizationList");
        }
    }
}
