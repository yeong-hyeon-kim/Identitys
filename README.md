
## π νλ‘μ νΈ κ°μ(Introduce Project)

### ASP .Net Core Identitys

* `ASP.NET Core Identity Entity Framework Core`λ₯Ό νμ©ν μ¬μ©μ κ΄λ¦¬`(User Management)` ννλ¦Ώ μλλ€.
* κΈ°λ³Έμ μΌλ‘ μ κ³΅λλ `Identity Migration`μ νμ©νμ¬ μ¬μ©μ κ΄λ¦¬ κΈ°λ₯μ νμ₯ν©λλ€.
* μλ¬ΈμΌλ‘ μμ±λ `Identity` ν¬νλ¦Ώμ νκΈν ν©λλ€.

![ν](./Snapshot/Home.PNG)

## π·οΈ κΈ°λ₯(Function)

1. μ¬μ©μ`(Identity Users)` κ΄λ¦¬
   1. [μ¬μ©μ λ±λ‘(Registration User.)](#μ¬μ©μ-λ±λ‘)
   2. [μ¬μ©μ μ‘°ν(Select Users.)](#μ¬μ©μ-κ΄λ¦¬)
   3. μ¬μ©μ μλ°μ΄νΈ(Update User information.)
   4. μ¬μ©μ μ κ±°(Delete User)]
   5. μ¬μ©μ μ κΈ ν΄μ (Remove Account Lock.)
2. μ­ν `(Identity Roles)` κ΄λ¦¬
   1. κΆν λ±λ‘(Registration Roles.)
   2. κΆν μ‘°ν(Select Roles.)  
   3. κΆν μλ°μ΄νΈ(Update Role.)
   4. κΆν μ κ±°(Delete Role.)

### μΈλΆ κΈ°λ₯(Function Detail)

#### μ¬μ©μ λ±λ‘

   1. μ¬μ©μλ₯Ό λ±λ‘ν©λλ€.

#### μ­ν (κΆν) μ‘°ν

1. μ­ν (κΆν) λͺ©λ‘μ νμν©λλ€.
2. νΉμ  μ­ν (κΆν)μ λͺμΉ­μ μμ ν©λλ€.
3. νΉμ  μ­ν (κΆν)μ μ­μ ν©λλ€.

![μ­ν (κΆν) μ‘°ν](./Snapshot/Roles.PNG)
#### μ¬μ©μ κ΄λ¦¬

1. μ¬μ©μλ³ μ­ν (κΆν) λͺ©λ‘μ νμν©λλ€.
2. μ¬μ©μλ³ μ­ν (κΆν) λ° μ λ³΄λ₯Ό μμ ν©λλ€.
3. μ¬μ©μλ₯Ό μ­μ ν©λλ€.

![μ¬μ©μ κ΄λ¦¬](./Snapshot/Users.PNG)

#### λ―ΈμΉμΈ μ¬μ©μ

1. `Identity` μ¬μ©μ λ±λ‘μ λμ΄μμΌλ κΆνμ΄ λΆμ¬λμ§ μμ μ¬μ©μ λͺ©λ‘μ νμν©λλ€.
2. λ―ΈμΉμΈ μ¬μ©μλ₯Ό μ­μ ν©λλ€.
3. μ­ν (κΆν)μ λΆμ¬νμ¬ μΉμΈν©λλ€.

![λ―ΈμΉμΈ μ¬μ©μ](./Snapshot/Authorization.PNG)

#### λ―Έλ±λ‘ μ¬μ©μ

1. `Users` μ¬μ©μ λ±λ‘μ λμ΄μμΌλ `Identity` μ¬μ©μ λ±λ‘μ λμ΄μμ§ μμ μ¬μ©μ λͺ©λ‘μ νμν©λλ€.

#### μ¬μ©μ μ κΈ ν΄μ 

1. ID λ° PW μ€λ₯λ‘ κ³μ μ΄ μ κΈ μνμΌ κ²½μ° ν΄μ ν©λλ€.

![μ¬μ©μ μ κΈ ν΄μ ](./Snapshot/AccountLock.PNG)

## π» κ°λ° νκ²½(Develop Environment)

* β `OS` : ![Windows](https://img.shields.io/badge/Windows-0078D6?style=flat-square&logo=Windows&logoColor=white)
  * π Version : ` 10 Pro `, `11 Pro 21H2`
* β `Framework` : `.NET Core`, `Entity Framework Core`
  * π Version : `.NET 6`
* β `Language` : ![C#](https://img.shields.io/badge/CSharp-239120?style=flat-square&logo=CSharp&logoColor=white)
* β `Database` : `SQL Server`
  * π Version : `2019`
* β `IDE` : `Visual Studio`
  * π Version : `2022`

<hr>

### π§ͺ νμ€νΈ(Test)

#### Swagger

* URL : <https://{Domain}:{Port}/swagger/index.html>
![SwaggerAPIs](./Snapshot/SwaggerAPIs.PNG)

## π λΉκ³ (Remark)

### 1. λ°μ΄ν°λ² μ΄μ€ μ°κ²°[Database Connection]

#### Ref:appsettings.json

*`User Database`
> "APP.DB": "Server=`Server IP`, `Port`; Database=APP.DB; User Id=`Login User ID`; Password=`Login User PW`;"

*`Identity Database`
> "APP.INDENTITY": "Server=`Server IP`, `Port`; Database=APP.Identity; User Id=`Login User ID`; Password=`Login User PW`;"

### 2. λ°μ΄ν°λ² μ΄μ€ μλ°μ΄νΈ(κ΅¬μ‘° λκΈ°ν) [Database Update(Sync Structure)]

#### Package Manage Console

* `User Database`

> "update-database -Context `AppDbContext`"

* `Identity Database`

> "update-database -Context `ApplicationDbContext`"

## π Identity Schema

### AspNetUsers

|νλ(Field)|λ΄μ©(Content)|
|-|-|
|AspNetUsers |νμ΄λΈμ κΈ°λ³Έν€|
|UserName |μ¬μ©μλͺ(κΈ°λ³Έκ° : μ΄λ©μΌ)|
|NormalizedUserName| μ κ·νλ μ¬μ©μλͺ|
|Email |μ΄λ©μΌ μ£Όμ|
|NormalizedEmail |μ κ·νλ μ΄λ©μΌ μ£Όμ μ κ·ν|
|EmailConfirmed |μ΄λ©μΌ μΈμ¦ μ¬λΆ|
|PasswordHash |ν¨μ€μλ ν΄μ λ³ν κ°|
|SecurityStamp |μ¬μ©μ μκ²© μ¦λͺμ΄ λ³κ²½λ  λλ§λ€ λ³κ²½ν΄μΌ νλ μμ κ°(μνΈ λ³κ²½, λ‘κ·ΈμΈ μ κ±°)|
|ConcurrencyStamp |μ¬μ©μκ° μ μ₯μμ μ μ§λ  λλ§λ€ λ³κ²½ν΄μΌ νλ μμ κ°|
|PhoneNumber |ν΄λμ ν λ²νΈ|
|PhoneNumberConfirmed |ν΄λμ νΈ λ²νΈ μΈμ¦ μ¬λΆ|
|TwoFactorEnabled |μ¬μ©μμ λν΄ 2λ¨κ³ μΈμ¦μ΄ μ¬μ©λλμ§ μ¬λΆλ₯Ό λνλ΄λ κ°|
|LockoutEnd |κ³μ  μ κΈ μ ν¨κΈ°κ°|
|LockoutEnabled |μ¬μ©μμ λν΄ κ³μ  μ κΈμ΄ μ¬μ©λλμ§ μ¬λΆλ₯Ό λνλ΄λ κ°|
|AccessFailedCount |νμ¬ μ¬μ©μμ λν΄ μ€ν¨ν λ‘κ·ΈμΈ μλ νμ|

### AspNetRoles

|νλ(Field)|λ΄μ©(Content)|μμ|
|-|-|-|
|Id|AspNetRoles νμ΄λΈμ κΈ°λ³Έν€|
|Name|μ­ν (κΆν)λͺ|κ°λ°ν, μΈμ¬ν, μμν|
|NormalizedName|μ κ·νλ μ­ν (κΆν)λͺ|

### AspNetUserRoles

|νλ(Field)|λ΄μ©(Content)|
|-|-|
|UserId| μ¬μ©μ Id |
|RoleId|μ­ν (κΆν)λͺ|

### AspNetUserClaims

|νλ(Field)|λ΄μ©(Content)|μμ|
|-|-|-|
|Id|RoleClaims κΈ°λ³Έν€|
|RoleId| μ­ν (κΆν) Id|
|ClaimType| μ­ν  κ·Έλ£Ήλͺ|κ°λ°ν ν¬μ§μ|
|ClaimValue| μ­ν  κ·Έλ£Ήμ μν μμ|νλ‘ νΈ μλ, λ²‘ μλ|
