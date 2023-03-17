using App.BLL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using App.Attributes;

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

#if !DEBUG
    [ApiKey]
#endif

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
        [Route($"/{API_VERSION}/identity/user")]
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
        [Route($"/{API_VERSION}/identity/personal")]
        [HttpGet]
        public IActionResult GetUserInfo(string UserEmail)
        {
            return Ok(JsonConvert.SerializeObject(_AppBll.GetUser(UserEmail)));
        }

        [HttpGet]
        [Route($"/{API_VERSION}/identity/user")]
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
        [Route($"/{API_VERSION}/identity/user")]
        [HttpDelete]
        public IActionResult RemoveUser(string UserId, string UserCd)
        {
            _AppBll.DeleteUser(UserId, UserCd);

            return Ok();
        }

        /// <summary>
        /// 사용자(User) 수정
        /// </summary>
        /// <param name="UserCd"></param>
        /// <param name="UserEmails"></param>
        /// <param name="UserDept"></param>
        /// <param name="UserContact"></param>
        /// <param name="UserAuthorization"></param>
        /// <returns></returns>
        [Route($"/{API_VERSION}/identity/user")]
        [HttpPut]
        public IActionResult UpdateUserInfomation(string UserCd, string UserEmails, string UserDept, string UserContact, string UserAuthorization)
        {
            _AppBll.UpdateUser(UserCd, UserDept, UserContact);
            _AppBll.GrantAuthorizationUser(UserEmails, UserAuthorization);

            return Redirect("~/Admin/Index");
        }

        /// <summary>
        /// 역할(Role)이 없는 사용자(User) 조회
        /// </summary>
        /// <returns></returns>
        [Route($"/{API_VERSION}/identity/role/null")]
        [HttpGet]
        public IActionResult GetIdentityNullRoleUsers()
        {
            return Ok(JsonConvert.SerializeObject(_AppBll.GetIdentityNullRoleUsers()));
        }

        /// <summary>
        /// 미등록 사용자(User) 조회
        /// </summary>
        /// <returns></returns>
        [Route($"/{API_VERSION}/identity/user/null")]
        [HttpGet]
        public IActionResult GetIdentityNullUsers()
        {
            return Ok(JsonConvert.SerializeObject(_AppBll.GetIdentityNullUsers()));
        }

#endregion

#region 역할(Role)

        /// <summary>
        /// 역할(Role) 조회
        /// </summary>
        /// <returns></returns>
        [Route($"/{API_VERSION}/identity/role")]
        [HttpGet]
        public IActionResult GetRolesList()
        {
            return Ok(JsonConvert.SerializeObject(_AppBll.GetRolesList()));
        }

        /// <summary>
        /// 역할(Role) 등록
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        [Route($"/{API_VERSION}/identity/role")]
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
        /// <param name="RoleName">역할 명</param>
        /// <returns></returns>
        [Route($"/{API_VERSION}/identity/role")]
        [HttpPut]
        public IActionResult UpdateRole(string RoleId, string RoleName)
        {
            _AppBll.UpdateRole(RoleId, RoleName);

            return Redirect("~/Admin/RolesList");
        }

        /// <summary>
        /// 역할(Role) 제거
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [Route($"/{API_VERSION}/identity/role")]
        [HttpDelete]
        public IActionResult DeleteRole(string RoleId)
        {
            _AppBll.DeleteRole(RoleId);

            return Ok();
        }
#endregion
    }
}
