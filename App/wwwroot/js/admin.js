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

// 역할 리스트 요청
function requestAuthorizationList() {
    let RequestURL = defaultRequesteURL + "GetRolesList"
    $.getJSON(RequestURL, getAuthorizationList);
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

// 사용자 정보 변경 다이얼로그
var ApproveModal = document.getElementById('UpdateUserInfoModal')
ApproveModal.addEventListener('show.bs.modal', function (event) {
    var modalTitle = ApproveModal.querySelector('.modal-title')
    modalTitle.textContent = '사용자 정보'

    var button = event.relatedTarget;
    var recipient = button.getAttribute('data-bs-whatever')

    $("#UserCd").val($("#USER_CD_" + recipient).text());
    $("#UserEmails").val($("#USER_EMAILS_" + recipient).text());
    $("#UserDept").val($("#USER_DEPT_" + recipient).text());
    $("#UserContact").val($("#USER_CONTACT_" + recipient).text());
    $("#UserAuthorization").val($("#ROLE_NM_" + recipient).text());
})

// 사용자 제거 확인 다이얼로그
let RemoveUserModal = document.getElementById('RemoveConfirmModal')
RemoveUserModal.addEventListener('show.bs.modal', function (event) {
    let modalTitle = ApproveModal.querySelector('.modal-title')
    modalTitle.textContent = '알림'

    let button = event.relatedTarget;
    let recipient = button.getAttribute('data-bs-whatever')
    recipient = $("#USER_ID_" + recipient).text() + "##" + recipient;

    console.log($("#USER_ID").text());
    console.log($("#USER_EMAIL").text());

    $("#RemoveUserCd").val($("#USER_ID").text());
    $("#RemoveUserId").val($("#USER_EMAIL").text());

    if (recipient !== "##null") {
        let UserCd = recipient.split("##")[0];
        let UserId = $("#USER_EMAILS_" + recipient.split("##")[1]).text();
        $("#RemoveUserCd").val(UserCd);
        $("#RemoveUserId").val(UserId);
    }

    $("#RemoveUser").attr("onclick", "requestRemoveUser('" + recipient + "')");
})