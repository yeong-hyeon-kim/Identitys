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

// Theme

var ThemeMode = "";
var ThemeClassValue = "text-center bg-black";

let ChangeTheme = (e) => {
    if (e) {
        ThemeMode = "Dark";
        ThemeClassValue = "text-center bg-black";

        //_Layout
        $('#ThemeName').text("Dark");
        $("#BooyLayout").attr("style", "background: black;")
        $("#_layout_navbar").attr("class", "navbar navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3 navbar-dark bg-dark")

        $("#Link-Home").attr("class", "nav-link text-white");
        $("#Link-Admin").attr("class", "nav-link text-white");
        $("#Link-API").attr("class", "nav-link text-white");

        //_LoginPartial.cshtml
        $("#Link-Identity").attr("class", "nav-link text-white");
        $("#Link-Register").attr("class", "nav-link text-white");
        $("#Link-Login").attr("class", "nav-link text-white");

        //Index.cshtml
        $("#MainArticle").attr("class", "display-4 text-light")
        $("#SubArticle").attr("class", "text-light")
    } else {
        ThemeMode = "Light";
        ThemeClassValue = "text-center bg-light";

        //_Layout
        $('#ThemeName').text("Light");
        $("#BooyLayout").attr("style", "background: white;")
        $("#_layout_navbar").attr("class", "navbar navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3 navbar bg-light")

        $("#Link-Home").attr("class", "nav-link text-black");
        $("#Link-Admin").attr("class", "nav-link text-black");
        $("#Link-API").attr("class", "nav-link text-black");

        //_LoginPartial.cshtml
        $("#Link-Identity").attr("class", "nav-link text-black");
        $("#Link-Register").attr("class", "nav-link text-black");
        $("#Link-Login").attr("class", "nav-link text-black");

        //Index.cshtml
        $("#MainArticle").attr("class", "display-4 text-dark")
        $("#SubArticle").attr("class", "text-dark")
    }
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