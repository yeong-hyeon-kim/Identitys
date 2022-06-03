const defaultURL = window.location.protocol + "//" + window.location.host + "/";
const defaultRequesteURL = defaultURL + "Api/";

function locationAdminPage() {
    location.href = defaultURL + "Admin";
}

function locationHomePage() {
    location.href = defaultURL + "Home";
}

function locationProfilePage() {
    location.href = defaultURL + "Identity/Account/Manage";
}

function locationAPIsPage() {
    location.href = defaultURL + "swagger/";
}

function locationUnAuthorizationPage() {
    location.href = defaultURL + "Admin/UnAuthorizationList";
}

function locationUnRegisteredListPage() {
    location.href = defaultURL + "Admin/UnRegisteredList";
}

function locationAccountRegisterPage() {
    location.href = defaultURL + "Identity/Account/Register";
}

function locationRolesListPage() {
    location.href = defaultURL + "Admin/RolesList";
}

function locationLockListPage() {
    location.href = defaultURL + "Admin/LockList";
}

// Request APIs
const APIList = [{
    "GET": [{ "": "" }],
    "POST": [{ "": "" }],
    "PUT": [
        { "Update User Infomation": "UpdateUserInfomation?" },
        { "Update User Infomation": "UpdateRole?" },
    ],
    "DELETE": [
        { "Remove User": "RemoveUser?" },
        { "Delete Role": "DeleteRole?" }]
}
]

// Index View.
let RequestUpdateUserInfomation = () => {
    let RequestApiURL = "";
    let APIArgument = "UserCd=" + $('#UserCd').val();
    APIArgument += "&UserEmails=" + $('#UserEmails').val();
    APIArgument += "&UserDept=" + $('#UserDept').val();
    APIArgument += "&UserContact=" + $('#UserContact').val();
    APIArgument += "&UserAuthorization=" + $('#AuthorizationName').val();

    RequestApiURL += defaultRequesteURL + APIList["PUT"]["Update User Infomation"] + APIArgument

    console.log(RequestApiURL)
    requestAJAX(RequestApiURL, "PUT");
}

let RequestDeleteUser = () => {
    let RequestApiURL = "";
    let APIArgument = "RemoveUserCd=" + $('#RemoveUserCd').val();
    APIArgument += "&RemoveUserId=" + $('#RemoveUserId').val();

    RequestApiURL += defaultRequesteURL + APIList["DELETE"]["Remove User"] + APIArgument

    console.log(RequestApiURL)
    requestAJAX(RequestApiURL, "DELETE");
}

// Roles List View
let RequestUpdateRole = () => {
    let RequestApiURL = "";
    let APIArgument = "UserCd=" + $('#UserCd').val();
    APIArgument += "&UserDept=" + $('#UserEmails').val();
    APIArgument += "&UserContact=" + $('#UserDept').val();
    APIArgument += "&UserContact=" + $('#UserContact').val();
    APIArgument += "&UserAuthorization=" + $('#UserAuthorization').val();

    RequestApiURL += defaultRequesteURL + APIList["PUT"]["Update Role"] + APIArgument

    console.log(RequestApiURL)
    requestAJAX(RequestApiURL, "PUT");
}

let RequestDeleteRole = () => {
    let RequestApiURL = "";
    let APIArgument = "RemoveRoleId=" + $('#RemoveRoleId').val();

    RequestApiURL += defaultRequesteURL + APIList["DELETE"]["Remove Role"] + APIArgument

    console.log(RequestApiURL)
    requestAJAX(RequestApiURL, "DELETE");
}

function requestAJAX(URL, Method) {
    $.ajax({
        url: URL,
        method: Method,
        dataType: "text",
        success: function (data) {
            console.log(data);
        }
    })
}