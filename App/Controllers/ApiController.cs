using App.BLL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App.Controllers
{
    /* REST API */

    /* HTTP METHOD */
    // 메서드 자체 의미로 행위를 포함합니다.
    // POST   - 생성
    // GET    - 조회
    // PUT    - 수정
    // DELETE - 삭제

    /* URL Design */
    // 계층 관계는 슬래시 구분자(/) 사용하기
    // URL 경로 구성은 소문자 사용하기
    // URL 경로 마지막은 슬래시(/) 사용하지 않기
    // URL 경로가 길어질 경우 하이픈(-)으로 높이기

    /* HTTP 상태 코드(Status Code) */
    // 200 : 정상
    // 401 : 인증되지 않은 요청입니다.
    // 404 : 클라이언트가 요청한 리소스가 없습니다.
    // 405 : 요청한 리소스에서 사용 불가능한 Method 입니다.
    // 500 : 서버 문제

    //#if !DEBUG
    //    [ApiKey]
    //#endif
    [Route("/v1/[action]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        const string API_VERSION = "v1";
        private readonly AppLogic _AppBll;

        public ApiController(AppLogic AppBll)
        {
            _AppBll = AppBll;
        }

        #region 사용자(User)
        /// <summary>
        /// 사용자 생성
        /// </summary>
        /// <param name="UserId">사용자 ID</param>
        /// <param name="UserPassword">사용자 PW</param>
        /// <param name="EmailConfirmation">이메일 검증 여부</param>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "사용자(User)"), Route($"/{API_VERSION}/identity/user")]
        [HttpPost]
        public IActionResult CreateUser(string UserId, string UserPassword, bool EmailConfirmation)
        {
            // Identity User
            _AppBll.CreateIdentityUser(UserId, UserId, UserPassword, EmailConfirmation);
            return Ok();
        }

        /// <summary>
        /// 사용자(User) 조회
        /// </summary>
        /// <param name="UserEmail">사용자 이메일</param>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "사용자(User)"), Route($"/{API_VERSION}/identity/user/info")]
        [HttpGet]
        public IActionResult GetUserInfo(string UserEmail)
        {
            return Ok(JsonConvert.SerializeObject(_AppBll.GetUser(UserEmail)));
        }

        /// <summary>
        /// 사용자(User) 목록 조회
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiExplorerSettings(GroupName = "사용자(User)"), Route($"/{API_VERSION}/identity/user")]
        public IActionResult GetUserList()
        {
            return Ok(JsonConvert.SerializeObject(_AppBll.GetIdentityUsers()).Replace("null", "\"\""));
        }

        /// <summary>
        /// 사용자(User) 제거
        /// </summary>
        /// <param name="UserId">사용자 ID</param>
        /// <param name="UserCd">사용자 CD</param>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "사용자(User)"), Route($"/{API_VERSION}/identity/user")]
        [HttpDelete]
        public IActionResult RemoveUser(string UserId, string UserCd)
        {
            _AppBll.DeleteUser(UserId, UserCd);

            return Ok();
        }

        /// <summary>
        /// 로컬 사용자 제거
        /// </summary>
        /// <param name="UserCd">사용자 CD</param>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "사용자(User)"), Route($"/{API_VERSION}/identity/user/local")]
        [HttpDelete]
        public IActionResult RemoveLocalUser(string UserCd)
        {
            _AppBll.DeleteLocalUser(UserCd);

            return Ok();
        }

        /// <summary>
        /// 사용자(User) 수정
        /// </summary>
        /// <param name="UserCd">사용자 CD</param>
        /// <param name="UserEmails">이메일</param>
        /// <param name="UserDept">부서</param>
        /// <param name="UserContact">연락처</param>
        /// <param name="UserAuthorization">역할</param>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "사용자(User)"), Route($"/{API_VERSION}/identity/user")]
        [HttpPut]
        public IActionResult UpdateUserInfomation(string UserCd, string UserEmails, string UserDept, string UserContact, string UserAuthorization)
        {
            _AppBll.UpdateUser(UserCd, UserDept, UserContact);
            _AppBll.GrantAuthorizationUser(UserEmails, UserAuthorization);

            return Redirect("~/Admin/Index");
        }

        /// <summary>
        /// 사용자(User) 비밀번호 초기화
        /// </summary>
        /// <param name="UserId">사용자 ID</param>
        /// <param name="UserPw">교체 PW</param>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "사용자(User)"), Route($"/{API_VERSION}/identity/user/password")]
        [HttpPut]
        public IActionResult ResetUserPassword(string UserId, string UserPw)
        {
            _AppBll.UpdateIdentityUserPassword(UserId, UserPw);

            return Ok();
        }

        /// <summary>
        /// 역할(Role)이 없는 사용자(User) 조회
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "사용자(User)"), Route($"/{API_VERSION}/identity/user/role/null")]
        [HttpGet]
        public IActionResult GetIdentityNullRoleUsers()
        {
            return Ok(JsonConvert.SerializeObject(_AppBll.GetIdentityNullRoleUsers()));
        }

        /// <summary>
        /// 미등록 사용자(User) 조회
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "사용자(User)"), Route($"/{API_VERSION}/identity/user/null")]
        [HttpGet]
        public IActionResult GetIdentityNullUsers()
        {
            return Ok(JsonConvert.SerializeObject(_AppBll.GetIdentityNullUsers()));
        }

        /// <summary>
        /// 계정이 잠긴 사용자 조회
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "사용자(User)"), Route($"/{API_VERSION}/identity/user/lock")]
        [HttpGet]
        public IActionResult GetLockedUser()
        {
            return Ok(JsonConvert.SerializeObject(_AppBll.GetLockList()));
        }

        /// <summary>
        /// 사용자 잠금 설정
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "사용자(User)"), Route($"/{API_VERSION}/identity/user/lock")]
        [HttpPut]
        public IActionResult LockingUser(string UserId, DateTime Date)
        {
            _AppBll.LockingUser(UserId, Date);
            return Ok();
        }

        /// <summary>
        /// 사용자 이메일 확인
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "사용자(User)"), Route($"/{API_VERSION}/identity/email/confirm")]
        [HttpPut]
        public IActionResult SetEmailConfirm(string UserEmail, bool IsConfirm)
        {
            _AppBll.SetEmailConfirm(UserEmail, IsConfirm);
            return Ok();
        }

        #endregion

        #region 역할(Role)

        /// <summary>
        /// 역할(Role) 조회
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "역할(Role)"), Route($"/{API_VERSION}/identity/role")]
        [HttpGet]
        public IActionResult GetRolesList()
        {
            return Ok(JsonConvert.SerializeObject(_AppBll.GetRolesList()));
        }

        /// <summary>
        /// 역할(Role) 등록
        /// </summary>
        /// <param name="RoleId">역할 ID</param>
        /// <param name="RoleName">역할 이름</param>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "역할(Role)"), Route($"/{API_VERSION}/identity/role")]
        [HttpPost]
        public IActionResult InsertRole(string RoleId, string RoleName)
        {
            _AppBll.InsertRole(RoleId, RoleName);

            return Redirect("~/Admin/RolesList");
        }

        /// <summary>
        /// 역할(Role) 수정
        /// </summary>
        /// <param name="RoleId">역할 ID</param>
        /// <param name="RoleName">역할 이름</param>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "역할(Role)"), Route($"/{API_VERSION}/identity/role")]
        [HttpPut]
        public IActionResult UpdateRole(string RoleId, string RoleName)
        {
            _AppBll.UpdateRole(RoleId, RoleName);

            return Redirect("~/Admin/RolesList");
        }

        /// <summary>
        /// 역할(Role) 제거
        /// </summary>
        /// <param name="RoleId">역할 ID</param>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "역할(Role)"), Route($"/{API_VERSION}/identity/role")]
        [HttpDelete]
        public IActionResult DeleteRole(string RoleId)
        {
            _AppBll.DeleteRole(RoleId);

            return Ok();
        }
        #endregion
    }
}