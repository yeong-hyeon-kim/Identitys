using App.BLL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ALIMS.Controllers
{
    [ApiExplorerSettings(GroupName = "APIS")]
    // 접두사 "V"로 버전을 지정하고 지속적인 버전 관리를 합니다..
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

        #region 사용자
        /// <summary>
        /// 사용자 정보
        /// </summary>
        /// <param name="UserEmail">사용자 이메일</param>
        /// <returns></returns>
        [Route($"/{API_VERSION}/identity/user")]
        // URI 경로의 마지막에는 슬래시(/)를 사용하지 않습니다.
        // 구분자로 밑줄(_) 대신 하이픈(-)을 사용합니다.
        [HttpGet]
        public string GetUserInfo(string UserEmail)
        {
            string JsonSerialize = JsonConvert.SerializeObject(_AppBll.GetUser(UserEmail));
            return JsonSerialize;
        }

        /// <summary>
        /// 역할(권한)이 없는 사용자 조회
        /// </summary>
        /// <returns></returns>
        [Route($"/{API_VERSION}/identity/role/null")]
        [HttpGet]
        public string GetIdentityNullRoleUsers()
        {
            string JsonSerialize = JsonConvert.SerializeObject(_AppBll.GetIdentityNullRoleUsers());
            return JsonSerialize;
        }

        /// <summary>
        /// 미등록 사용자 조회
        /// </summary>
        /// <returns></returns>
        [Route($"/{API_VERSION}/identity/user/null")]
        [HttpGet]
        public string GetIdentityNullUsers()
        {
            string JsonSerialize = JsonConvert.SerializeObject(_AppBll.GetIdentityNullUsers());
            return JsonSerialize;
        }

        /// <summary>
        /// 사용자 권한 리스트
        /// </summary>
        /// <returns></returns>
        [Route($"/{API_VERSION}/identity/role")]
        [HttpGet]
        public string GetRolesList()
        {
            string JsonSerialize = JsonConvert.SerializeObject(_AppBll.GetRolesList());
            return JsonSerialize;
        }

        /// <summary>
        /// 권한 수정
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
        /// 사용자 제거
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
        /// 사용자 정보 수정
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
        /// 역할(권한) 등록
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

        #endregion
    }
}
