﻿using App.BLL;
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
        [HttpGet]
        public string GetUserInfo(string UserEmail)
        {
            string JsonSerialize = JsonConvert.SerializeObject(_AppBll.GetUser(UserEmail));
            return JsonSerialize;
        }

        [HttpGet]
        public string GetIdentityNullRoleUsers()
        {
            string JsonSerialize = JsonConvert.SerializeObject(_AppBll.GetIdentityNullRoleUsers());
            return JsonSerialize;
        }

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
        #endregion

    }
}
