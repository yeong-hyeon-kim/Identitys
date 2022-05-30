using App.BLL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace ALIMS.Controllers
{
    [ApiExplorerSettings(GroupName = "APIS")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
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
        [HttpPut]
        public IActionResult UpdateUserInfomation(string UserCd, string UserEmails, string UserDept, string UserContact, string UserAuthorization)
        {
            _AppBll.UpdateUser(UserCd, UserDept, UserContact);
            _AppBll.GrantAuthorizationUser(UserEmails, UserAuthorization);

            return Redirect("~/Admin/Index");
        }

        #endregion
    }
}
