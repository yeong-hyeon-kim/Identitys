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