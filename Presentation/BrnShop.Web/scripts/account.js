var returnUrl = "/"; //返回地址
var shadowName = ""; //影子账号名

//展示验证错误
function showVerifyError(verifyErrorList) {
    if (verifyErrorList != undefined && verifyErrorList != null && verifyErrorList.length > 0) {
        var msg = "";
        for (var i = 0; i < verifyErrorList.length; i++) {
            msg += verifyErrorList[i].msg + "\n";
        }
        alert(msg)
    }
}

//用户登录
function login() {
    var loginForm = document.forms["loginForm"];

    var accountName = loginForm.elements[shadowName].value;
    var password = loginForm.elements["password"].value;
    var verifyCode = loginForm.elements["verifyCode"] ? loginForm.elements["verifyCode"].value : undefined;
    var isRemember = loginForm.elements["isRemember"] ? loginForm.elements["isRemember"].checked ? 1 : 0 : 0;

    if (!verifyLogin(accountName, password, verifyCode)) {
        return;
    }

    var parms = new Object();
    parms[shadowName] = accountName;
    parms["password"] = password;
    parms["verifyCode"] = verifyCode;
    parms["isRemember"] = isRemember;
    Ajax.post("/account/login", parms, false, loginResponse)
}

//验证登录
function verifyLogin(accountName, password, verifyCode) {
    if (accountName.length == 0) {
        alert("请输入帐号名");
        return false;
    }
    if (password.length == 0) {
        alert("请输入密码");
        return false;
    }
    if (verifyCode != undefined && verifyCode.length == 0) {
        alert("请输入验证码");
        return false;
    }
    return true;
}

//处理登录的反馈信息
function loginResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        window.location.href = returnUrl;
    }
    else if (result.state == "lockuser") {
        alert(result.content);
    }
    else if (result.state == "error") {
        showVerifyError(result.content);
    }
}

//用户注册
function register() {
    var registerForm = document.forms["registerForm"];

    var accountName = registerForm.elements[shadowName].value;
    var password = registerForm.elements["password"].value;
    var confirmPwd = registerForm.elements["confirmPwd"].value;
    var verifyCode = registerForm.elements["verifyCode"] ? registerForm.elements["verifyCode"].value : undefined;

    if (!verifyRegister(accountName, password, confirmPwd, verifyCode)) {
        return;
    }

    var parms = new Object();
    parms[shadowName] = accountName;
    parms["password"] = password;
    parms["confirmPwd"] = confirmPwd;
    parms["verifyCode"] = verifyCode;
    Ajax.post("/account/register", parms, false, registerResponse)
}

//验证注册
function verifyRegister(accountName, password, confirmPwd, verifyCode) {
    if (accountName.length == 0) {
        alert("请输入帐号名");
        return false;
    }
    if (password.length == 0) {
        alert("请输入密码");
        return false;
    }
    if (password != confirmPwd) {
        alert("两次输入的密码不一样");
        return false;
    }
    if (verifyCode != undefined && verifyCode.length == 0) {
        alert("请输入验证码");
        return false;
    }
    return true;
}

//处理注册的反馈信息
function registerResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        window.location.href = returnUrl;
    }
    else if (result.state == "exception") {
        alert(result.content);
    }
    else if (result.state == "error") {
        showVerifyError(result.content);
    }
}

//找回密码
function findPwd() {
    var findPwdForm = document.forms["findPwdForm"];

    var accountName = findPwdForm.elements[shadowName].value;
    var verifyCode = findPwdForm.elements["verifyCode"].value;

    if (!verifyFindPwd(accountName, verifyCode)) {
        return;
    }

    var parms = new Object();
    parms[shadowName] = accountName;
    parms["verifyCode"] = verifyCode;
    Ajax.post("/account/findpwd", parms, false, findPwdResponse)
}

//验证找回密码
function verifyFindPwd(accountName, verifyCode) {
    if (accountName.length == 0) {
        alert("请输入帐号名");
        return false;
    }
    if (verifyCode != undefined && verifyCode.length == 0) {
        alert("请输入验证码");
        return false;
    }
    return true;
}

//处理找回密码的反馈信息
function findPwdResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        window.location.href = result.content;
    }
    else if (result.state == "nocanfind") {
        alert(result.content);
    }
    else if (result.state == "error") {
        showVerifyError(result.content);
    }
}

//发送找回密码短信
function sendFindPwdMobile(uid) {
    Ajax.get("/account/sendfindpwdmobile?uid=" + uid, false, function (data) {
        var result = eval("(" + data + ")");
        alert(result.content)
    })
}

//验证找回密码短信
function verifyFindPwdMobile(uid, mobileCode) {
    if (mobileCode.length == 0) {
        alert("请输入短信验证码");
        return;
    }
    Ajax.post("/account/verifyfindpwdmobile?uid=" + uid, { 'mobileCode': mobileCode }, false, function (data) {
        var result = eval("(" + data + ")");
        if (result.state == "success") {
            window.location.href = result.content;
        }
        else {
            alert(result.content)
        }
    })
}

//发送找回密码邮件
function sendFindPwdEmail(uid) {
    Ajax.get("/account/sendfindpwdemail?uid=" + uid, false, function (data) {
        var result = eval("(" + data + ")");
        alert(result.content)
    })
}

//重置用户密码
function resetPwd(v) {
    var resetPwdForm = document.forms["resetPwdForm"];

    var password = resetPwdForm.elements["password"].value;
    var confirmPwd = resetPwdForm.elements["confirmPwd"].value;

    if (!verifyResetPwd(password, confirmPwd)) {
        return;
    }

    var parms = new Object();
    parms["password"] = password;
    parms["confirmPwd"] = confirmPwd;
    Ajax.post("/account/resetpwd?v=" + v, parms, false, resetPwdResponse)
}

//验证重置密码
function verifyResetPwd(password, confirmPwd) {
    if (password.length == 0) {
        alert("请输入密码");
        return false;
    }
    if (password != confirmPwd) {
        alert("两次输入的密码不一样");
        return false;
    }
    return true;
}

//处理验证重置密码的反馈信息
function resetPwdResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        alert("密码修改成功,请重新登录");
        window.location.href = result.content;
    }
    else if (result.state == "error") {
        showVerifyError(result.content);
    }
}