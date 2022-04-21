var defaultURL = window.location.protocol + "//" + window.location.host + "/";
var defaultRequesteURL = defaultURL + "Api/";

(function () {
    requestAuthorizationList();
}());

function UpdateUserInfo(data) {
    let RequestURL = defaultRequesteURL + "UpdateUserInfomation?UserCd"

    $.ajax({
        url: RequestURL,
        type: "UPDATE",
        dataType: 'text',
        success: function (data) {
            window.location.reload();
        },
        error: function (request, msg, error) {
            console.log(request);
            console.log(msg);
            console.log(error);
        }
    })
}

function requestRemoveUser(data) {
    let RequestURL = defaultRequesteURL + "RemoveUser?UserId=" + data.split("##")[0] + "&UserCd=" + data.split("##")[1]

    $.ajax({
        url: RequestURL,
        type: "DELETE",
        dataType: 'text',
        success: function (data) {
            window.location.reload();
        },
        error: function (request, msg, error) {
            console.log(request);
            console.log(msg);
            console.log(error);
        }
    })
}

// 역할 리스트 요청
function requestAuthorizationList() {
    let RequestURL = defaultRequesteURL + "GetRolesList"
    $.getJSON(RequestURL, getAuthorizationList);
}

// 역할 리스트 표시
function getAuthorizationList(data) {
    let html = "";
    $("#AuthorizationName option").empty();

    html += "<option disabled selected hidden>권한을 선택하세요.</option>"

    for (var i = 0; i < Object.keys(data).length; i++) {
        html += "<option>" + data[i]["Name"] + "</option>"
    }

    $("#AuthorizationName").append(html);
}