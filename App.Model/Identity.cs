using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Model
{
    /// <summary>
    /// 사용자
    /// </summary>
    [Table("USERS")]
    public class Users
    {
        /// <summary>
        /// 사용자 코드
        /// </summary>
        [Key] public string USER_CD { get; set; }

        /// <summary>
        /// 사용자 명
        /// </summary>
        [Required] public string USER_NM { get; set; }

        /// <summary>
        /// 사용자 부서
        /// </summary>
        public string? USER_DEPT { get; set; }

        /// <summary>
        /// 사용자 이메일
        /// </summary>
        [Required] public string USER_EMAIL { get; set; }

        /// <summary>
        /// 사용자 연락처
        /// </summary>
        public string? USER_CONTACT { get; set; }

        /// <summary>
        /// 비고
        /// </summary>
        public string? REMARK { get; set; }
    }

    /// <summary>
    /// 역할
    /// </summary>
    public class Roles
    {
        /// <summary>
        /// 사용자 코드
        /// </summary>
        [Key] public string USER_CD { get; set; }

        /// <summary>
        /// 사용자 고유 ID
        /// </summary>
        [Required] public string USER_ID { get; set; }

        /// <summary>
        /// 사용자 이름
        /// </summary>
        [Required] public string USER_NM { get; set; }

        /// <summary>
        /// 사용자 부서
        /// </summary>
        [Required] public string USER_DEPT { get; set; }

        /// <summary>
        /// 사용자 이메일
        /// </summary>
        [Required] public string USER_EMAIL { get; set; }

        /// <summary>
        /// 사용자 연락처
        /// </summary>
        public string? USER_CONTACT { get; set; }

        /// <summary>
        /// 권한 ID
        /// </summary>
        public string ROLE_ID { get; set; }

        /// <summary>
        /// 권한 이름
        /// </summary>
        public string ROLE_NM { get; set; }

        /// <summary>
        /// 비고
        /// </summary>
        public string? REMARK { get; set; }
    }
}