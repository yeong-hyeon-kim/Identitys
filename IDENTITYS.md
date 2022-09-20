# Identity Schema

## AspNetUsers

|필드(Field)|내용(Content)|
|-|-|
|AspNetUsers |테이블의 기본키|
|UserName |사용자명(기본값 : 이메일)|
|NormalizedUserName| 정규화된 사용자명|
|Email |이메일 주소|
|NormalizedEmail |정규화된 이메일 주소 정규화|
|EmailConfirmed |이메일 인증 여부|
|PasswordHash |패스워드 해시 변환 값|
|SecurityStamp |사용자 자격 증명이 변경될 때마다 변경해야 하는 임의 값(암호 변경, 로그인 제거)|
|ConcurrencyStamp |사용자가 저장소에 유지될 때마다 변경해야 하는 임의 값|
|PhoneNumber |휴대전화 번호|
|PhoneNumberConfirmed |휴대전호 번호 인증 여부|
|TwoFactorEnabled |사용자에 대해 2단계 인증이 사용되는지 여부를 나타내는 값|
|LockoutEnd |계정 잠금 유효기간|
|LockoutEnabled |사용자에 대해 계정 잠금이 사용되는지 여부를 나타내는 값|
|AccessFailedCount |현재 사용자에 대해 실패한 로그인 시도 횟수|

## AspNetRoles

|필드(Field)|내용(Content)|예시(Example)|
|-|-|-|
|Id|AspNetRoles 테이블의 기본키|
|Name|역할(권한)명|개발팀, 인사팀, 영업팀|
|NormalizedName|정규화된 역할(권한)명|

## AspNetUserRoles

|필드(Field)|내용(Content)|
|-|-|
|UserId| 사용자 Id |
|RoleId|역할(권한)명|

## AspNetUserClaims

|필드(Field)|내용(Content)|예시(Example)|
|-|-|-|
|Id|RoleClaims 기본키|
|RoleId| 역할(권한) Id|
|ClaimType| 역할 그룹명|개발팀 포지션|
|ClaimValue| 역할 그룹에 속한 요소|프론트 엔드, 벡 엔드|
