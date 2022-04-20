﻿using App.DAL.CONTEXT;
using App.IDAL;
using App.Model;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace App.DAL
{
    public class AppDal : IAppDal
    {
        private AppDbContext _context;
        private ApplicationDbContext _identity_context;

        public AppDal()
        {
            _context = new AppDbContext();
            _identity_context = new ApplicationDbContext();
        }

        #region 사용자
        /// <summary>
        /// 사용자 조회
        /// </summary>
        /// <param name="UserEmail">사용자 이메일</param>
        /// <returns></returns>
        public List<Users> GetUser(string UserEmail)
        {
            List<Users> UserList = new List<Users>();

            using (var db = new AppDbContext())
            {
                try
                {
                    return UserList = db.USERS.Where(x => x.USER_EMAIL == UserEmail).ToList();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return UserList;
                }
            }
        }

        /// <summary>
        /// 사용자 등록
        /// </summary>
        /// <param name="UserCd">사용자 코드</param>
        /// <param name="UserNm">이름</param>
        /// <param name="UserDept">부서</param>
        /// <param name="UserEmail">이메일</param>
        /// <param name="UserContact">연락처</param>
        public void CreateUser(string UserCd, string UserNm, string UserDept, string UserEmail, string UserContact = "")
        {
            using (var db = _context)
            {
                try
                {
                    // 사용자 코드를 지정하지 않으면 새로운 GUID 값으로 사용자 코드 등록
                    if (string.IsNullOrEmpty(UserCd))
                    {
                        Guid guid = Guid.NewGuid();
                        UserCd = guid.ToString();
                    }

                    // 기존 사용자가 없을 경우에만 등록
                    if (GetUser(UserEmail).Count == 0)
                    {
                        db.Add(new Users { USER_CD = UserCd, USER_NM = UserNm, USER_DEPT = UserDept, USER_EMAIL = UserEmail, USER_CONTACT = UserContact });
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// 사용자 정보 업데이트
        /// </summary>
        /// <param name="UserCd">사용자 코드</param>
        /// <param name="UserDept">부서</param>
        /// <param name="UserContact">연락처</param>
        public void UpdateUser(string UserCd, string UserDept, string UserContact)
        {
            string UserIdentity = string.Empty;

            // User PhoneNumber
            using (var db = _context)
            {
                try
                {
                    var Model = db.USERS.First(x => x.USER_CD.Equals(UserCd));
                    Model.USER_DEPT = UserDept;
                    Model.USER_CONTACT = UserContact;
                    UserIdentity = Model.USER_EMAIL;

                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            // Identity PhoneNumber
            using (var db = _identity_context)
            {
                try
                {
                    if (!string.IsNullOrEmpty(UserIdentity))
                    {
                        var Model = db.Users.First(x => x.Email.Equals(UserIdentity));
                        Model.PhoneNumber = UserContact;

                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// 사용자 제거
        /// </summary>
        /// <param name="UserId">사용자 ID(AspNetUsers.Id)</param>
        /// <param name="UserEmail">사용자 CD</param>
        public void DeleteUser(string UserId, string UserEmail)
        {
            try
            {
                // Remove Identity User 
                using (var db = _identity_context)
                {
                    var Model = db.Users.First(x => x.Id.Equals(UserId));

                    if (Model != null)
                    {
                        db.Users.Remove(Model);
                        db.SaveChanges();
                    }
                }

                // Remove User
                using (var db = _context)
                {
                    var Model = db.USERS.First(x => x.USER_EMAIL.Equals(UserEmail));

                    if (Model != null)
                    {
                        db.USERS.Remove(Model);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 사용자 권한 및 정보 조회
        /// </summary>
        /// <returns></returns>
        public List<Roles> GetIdentityUsers()
        {
            List<Roles> UserToRole = new List<Roles>();

            try
            {
                List<Users> LocalUser;
                List<IdentityUser> Users;

                List<Roles> UserRoleToRole;
                List<Roles> IdentityUserToRole = new List<Roles>();
                //List<Roles> UserToRole = new List<Roles>();

                using (var db = _context)
                {
                    LocalUser = db.USERS.ToList();
                }

                using (var db = _identity_context)
                {
                    // Join IdentiUserRoles To IdentityRoles
                    Users = db.Users.ToList();
                    UserRoleToRole = db.UserRoles.Join(db.Roles, c => c.RoleId, d => d.Id, (c, d) =>
                        new Roles
                        {
                            USER_ID = c.UserId,
                            ROLE_ID = c.RoleId,
                            ROLE_NM = d.Name,
                        }).ToList();
                }

                // Join IdentiUsers To (Join IdentiUserRoles To IdentityRoles)
                var JoinIdentityUserToRole = from User in Users
                                             join Role in UserRoleToRole
                                             on User.Id equals Role.USER_ID
                                             select new Roles { USER_CD = User.Id, USER_ID = User.UserName, USER_NM = "", USER_DEPT = "", USER_EMAIL = "", USER_CONTACT = "", ROLE_ID = Role.ROLE_ID, ROLE_NM = Role.ROLE_NM };

                IdentityUserToRole.AddRange(JoinIdentityUserToRole);

                // Join Users To (Join IdentiUserRoles To IdentityRoles)
                var JoinUserToRole = from LocalUsers in LocalUser
                                     join RoleUsers in IdentityUserToRole
                                     on LocalUsers.USER_EMAIL equals RoleUsers.USER_ID
                                     select new Roles { USER_CD = LocalUsers.USER_CD, USER_ID = RoleUsers.USER_CD, USER_NM = LocalUsers.USER_NM, USER_DEPT = LocalUsers.USER_DEPT, USER_EMAIL = LocalUsers.USER_EMAIL, USER_CONTACT = LocalUsers.USER_CONTACT, ROLE_ID = RoleUsers.ROLE_ID, ROLE_NM = RoleUsers.ROLE_NM };

                UserToRole.AddRange(JoinUserToRole.OrderBy(x => x.USER_CD));

                return UserToRole;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return UserToRole;
            }
        }

        /// <summary>
        /// 미등록 사용자 조회
        /// </summary>
        /// <returns></returns>
        public List<Users> GetIdentityNullUsers()
        {
            List<Users> UserList = new List<Users>();

            try
            {
                List<IdentityUser> IdentityUser;
                List<IdentityUserRole<string>> IdentityUserRoles;

                using (var db = _context)
                {
                    UserList = db.USERS.ToList();
                }

                using (var db = _identity_context)
                {
                    IdentityUser = db.Users.ToList();
                    IdentityUserRoles = db.UserRoles.ToList();
                }

                var list = from identityUser in IdentityUser
                           join IdentityUserRolesList in IdentityUserRoles
                           on identityUser.Id equals IdentityUserRolesList.UserId
                           select new Users
                           {
                               USER_EMAIL = identityUser.Email,
                           };

                UserList = UserList.Where(x => !list.Any(y => y.USER_EMAIL == x.USER_EMAIL)).ToList();

                return UserList;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return UserList;
            }
        }

        /// <summary>
        /// 미승인(권한 X) 사용자 리스트 조회
        /// </summary>
        /// <returns></returns>
        public List<Users> GetIdentityNullRoleUsers()
        {
            List<Users> ResultUserList = new List<Users>();

            try
            {
                List<Users> UserList;
                List<IdentityUser> IdentityUser;
                List<IdentityUserRole<string>> IdentityUserRoles;

                // 리스트 초기화
                using (var db = _context)
                {
                    UserList = db.USERS.ToList();
                }

                using (var db = _identity_context)
                {
                    IdentityUser = db.Users.ToList();
                    IdentityUserRoles = db.UserRoles.ToList();
                }

                // 권한이 지정된 리스트 조회
                var listA = from identityUser in IdentityUser
                            join IdentityUserRolesList in IdentityUserRoles
                            on identityUser.Id equals IdentityUserRolesList.UserId
                            select new Users
                            {
                                USER_CD = identityUser.Id,
                                USER_EMAIL = identityUser.Email
                            };

                // 권한이 지정되어 있지 않은 리스트 조회
                IdentityUser = IdentityUser.Where(x => !listA.Any(y => y.USER_CD == x.Id)).ToList();

                // Users 형식으로 형변환
                var listB = from IdentityUser IdentityList in IdentityUser
                            select new Users
                            {
                                USER_CD = IdentityList.Id,
                                USER_EMAIL = IdentityList.UserName
                            };

                ResultUserList.AddRange(listB);

                return ResultUserList;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return ResultUserList;
            }
        }

        /// <summary>
        /// 사용자 제거
        /// </summary>
        /// <param name="UserEmail">사용자 이메일</param>
        public void DeleteIdentityUser(string UserEmail)
        {
            using (var db = _identity_context)
            {
                try
                {
                    var list = db.Users.FirstOrDefault(x => x.UserName == UserEmail);

                    if (list != null)
                    {
                        db.Users.Remove(list);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// 사용자 권한 부여
        /// </summary>
        /// <param name="UserEmail">사용자 이메일</param>
        /// <param name="RoleName">권한(역할)명칭</param>
        public void GrantAuthorizationUser(string UserEmail, string RoleName)
        {
            try
            {
                // RoleName을 지정하지 않으면 역할 등록을 하지 않음.
                if (string.IsNullOrEmpty(RoleName))
                {
                    IdentityUserRole<string> Model = new IdentityUserRole<string>();

                    using (var db = new ApplicationDbContext())
                    {
                        var listUser = db.Users.FirstOrDefault(x => x.UserName.Equals(UserEmail));
                        var listRole = db.Roles.FirstOrDefault(x => x.Name.Equals(RoleName));

                        var ApprovaledRole = GetUserRoles(UserEmail);

                        // 기존 Role 제거
                        if (ApprovaledRole.Count > 0)
                        {
                            foreach (var item in ApprovaledRole)
                            {
                                RevokeAuthorizationUser(UserEmail, item);
                            }
                        }

                        if (listUser != null && listRole != null)
                        {
                            Model.UserId = listUser.Id;
                            Model.RoleId = listRole.Id;

                            db.UserRoles.Add(Model);
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 사용자에게 부여된 권한명 조회
        /// </summary>
        /// <param name="UserEmail">사용자 이메일</param>
        /// <returns>List<string> RoleNameList</returns>
        public List<string> GetUserRoles(string UserEmail)
        {
            List<string> RoleIdList = new List<string>();
            List<string> RoleNameList = new List<string>();

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    // AspNetUsers.UserName To AspNetUsers.Id 
                    var UsersId = db.Users.FirstOrDefault(x => x.UserName.Equals(UserEmail));
                    string? UserCd = UsersId.Id;

                    // 부여된 Role Id
                    if (!string.IsNullOrEmpty(UserCd))
                    {
                        var listUser = db.UserRoles.Where(x => x.UserId.Equals(UserCd)).ToList();

                        foreach (var item in listUser)
                        {
                            RoleIdList.Add(item.RoleId);
                        }

                        // Get RoleName
                        foreach (string item in RoleIdList)
                        {
                            string RoleName = db.Roles.FirstOrDefault(x => x.Id.Equals(item)).Name.ToString();
                            RoleNameList.Add(RoleName);
                        }
                    }
                }

                return RoleNameList;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return RoleNameList;
            }
        }

        /// <summary>
        /// 사용자 권한 취소
        /// </summary>
        /// <param name="UserEmail"></param>
        /// <param name="RoleName"></param>
        public void RevokeAuthorizationUser(string UserEmail, string RoleName)
        {
            try
            {
                IdentityUserRole<string> Model = new IdentityUserRole<string>();

                using (var db = _identity_context)
                {
                    var listUser = db.Users.FirstOrDefault(x => x.UserName.Equals(UserEmail));
                    var listRole = db.Roles.FirstOrDefault(x => x.Name.Equals(RoleName));

                    if (listUser != null && listRole != null)
                    {
                        Model.UserId = listUser.Id;
                        Model.RoleId = listRole.Id;
                    }

                    db.UserRoles.Remove(Model);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 특정 사용자에 부여된 역할 리스트
        /// </summary>
        /// <param name="UserEmail">사용자 이메일</param>
        /// <param name="RoleName">역할 이름</param>
        public void OverlapAuthorization(string UserEmail, string RoleName)
        {
            try
            {
                IdentityUserRole<string> Model = new IdentityUserRole<string>();

                using (var db = _identity_context)
                {
                    var listUser = db.Users.FirstOrDefault(x => x.UserName.Equals(UserEmail));
                    var listUserRole = db.UserRoles.Where(x => x.UserId.Equals(listUser.Id)).ToList();
                    var listRole = db.Roles.FirstOrDefault(x => x.Name.Equals(RoleName));

                    listUserRole = listUserRole.Where(x => !x.RoleId.Equals(listRole.Id)).ToList();

                    Model.UserId = listUser.Id;
                    Model.RoleId = listUserRole.First().RoleId;

                    db.UserRoles.Remove(Model);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 계정 잠금 해제
        /// </summary>
        /// <param name="LockUserId">사용자 ID</param>
        /// <param name="LockoutEnabled">잠금 사용 여부</param>
        public void DeleteLock(string LockUserId, bool LockoutEnabled)
        {
            string UserEmail = string.Empty;

            using (var db = _context)
            {
                try
                {
                    var Model = db.USERS.First(x => x.USER_CD.Equals(LockUserId));

                    if (Model != null)
                    {
                        UserEmail = Model.USER_EMAIL;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            using (var db = _identity_context)
            {
                try
                {
                    if (!string.IsNullOrEmpty(UserEmail))
                    {
                        var Model = db.Users.First(x => x.Email.Equals(UserEmail));

                        // Lcck Date > null
                        Model.LockoutEnd = null;
                        // 체크 시 Lock 사용 안함. 
                        Model.LockoutEnabled = !LockoutEnabled;

                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// 계정 잠금 리스트
        /// </summary>
        /// <returns></returns>
        public List<Users> GetLockList()
        {
            // Identity Lock List
            dynamic? LockList = null;
            List<Users> LockUserList = new List<Users>();

            using (var db = _identity_context)
            {
                try
                {
                    LockList = db.Users.Where(x => x.LockoutEnabled.Equals(true) && x.LockoutEnd > DateTimeOffset.Now).ToList();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            using (var db = _context)
            {
                try
                {
                    if (LockList != null)
                    {
                        foreach (var item in LockList)
                        {
                            string UserEmail = item.Email.ToString();
                            LockUserList = db.USERS.Where(x => x.USER_EMAIL.Equals(UserEmail)).ToList();

                            return LockUserList;
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            return LockUserList;
        }

        #endregion

        #region 역할
        /// <summary>
        /// 역할(권한) 리스트 조회
        /// </summary>
        /// <returns></returns>
        public List<IdentityRole> GetRolesList()
        {
            List<IdentityRole> roles = new List<IdentityRole>();

            using (var db = _identity_context)
            {
                try
                {
                    roles = db.Roles.ToList();
                    return roles;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return roles;
                }
            }
        }

        /// <summary>
        /// 역할 업데이트
        /// </summary>
        /// <param name="RoleId">Identity Role Id</param>
        /// <param name="RoleNm">Identity Role Name</param>
        public void UpdateRole(string RoleId, string RoleNm)
        {
            using (var db = _identity_context)
            {
                try
                {
                    var Model = db.Roles.First(x => x.Id.Equals(RoleId));

                    Model.Name = RoleNm;
                    Model.NormalizedName = RoleNm;

                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// 역할 추가
        /// </summary>
        /// <param name="RoleId">Role Id</param>
        /// <param name="RoleNm"></param>
        public void InsertRole(string RoleId, string RoleNm)
        {
            using (var db = _identity_context)
            {
                try
                {
                    IdentityRole Roles = new IdentityRole();

                    if (string.IsNullOrEmpty(RoleId))
                    {
                        Guid guid = Guid.NewGuid();
                        Roles.Id = guid.ToString();
                    }
                    else
                    {
                        Roles.Id = RoleId;
                    }

                    Roles.Name = RoleNm;
                    Roles.NormalizedName = RoleNm.ToUpper();

                    db.Roles.Add(Roles);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// 권한(역할) 제거
        /// </summary>
        /// <param name="RoleId"></param>
        public void DeleteRole(string RoleId)
        {
            try
            {
                using (var db = _identity_context)
                {
                    var Model = db.Roles.First(x => x.Id.Equals(RoleId));

                    if (Model != null)
                    {
                        db.Roles.Remove(Model);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        #endregion
    }
}