//删除收藏夹中的商品
function delFavoriteProduct(pid) {
    Ajax.get("/ucenter/delfavoriteproduct?pid=" + pid, false, delFavoriteProductResponse)
}

//处理删除收藏夹中的商品的反馈信息
function delFavoriteProductResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        removeNode(document.getElementById("favoriteProduct" + result.content));
        alert("删除成功");
    }
    else {
        alert(result.content);
    }
}

//打开添加配送地址层
function openAddShipAddressBlock() {
    var shipAddressForm = document.forms["shipAddressForm"];

    shipAddressForm.elements["saId"].value = "";
    shipAddressForm.elements["alias"].value = "";
    shipAddressForm.elements["consignee"].value = "";
    shipAddressForm.elements["mobile"].value = "";
    shipAddressForm.elements["phone"].value = "";
    shipAddressForm.elements["email"].value = "";
    shipAddressForm.elements["zipcode"].value = "";
    shipAddressForm.elements["address"].value = "";
    shipAddressForm.elements["isDefault"].checked = true;

    document.getElementById("editShipAddressBut").style.display = "none";
    document.getElementById("addShipAddressBut").style.display = "";
    document.getElementById("shipAddressBlock").style.display = "";
}

//打开编辑配送地址层
function openEditShipAddressBlock(saId) {
    Ajax.get("/ucenter/shipaddressinfo?saId=" + saId, false, openEditShipAddressBlockResponse)
}

//处理打开编辑配送地址层的反馈信息
function openEditShipAddressBlockResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {

        var shipAddressForm = document.forms["shipAddressForm"];

        var info = result.content;
        shipAddressForm.elements["saId"].value = info.saId;
        shipAddressForm.elements["alias"].value = info.alias;
        shipAddressForm.elements["consignee"].value = info.consignee;
        shipAddressForm.elements["mobile"].value = info.mobile;
        shipAddressForm.elements["phone"].value = info.phone;
        shipAddressForm.elements["email"].value = info.email;
        shipAddressForm.elements["zipcode"].value = info.zipCode;
        shipAddressForm.elements["address"].value = info.address;

        if (info.isDefault == 1) {
            shipAddressForm.elements["isDefault"].checked = true;
        }
        else {
            shipAddressForm.elements["isDefault"].checked = false;
        }

        setSelectedOptions(document.getElementById("provinceId"), info.provinceId);
        bindCityList(info.provinceId, document.getElementById("cityId"), info.cityId);
        bindCountyList(info.cityId, document.getElementById("regionId"), info.countyId);

        document.getElementById("addShipAddressBut").style.display = "none";
        document.getElementById("editShipAddressBut").style.display = "";
        document.getElementById("shipAddressBlock").style.display = "";
    }
    else {
        alert(result.content)
    }
}

//关闭配送地址层
function closeShipAddressBlock() {

    var shipAddressForm = document.forms["shipAddressForm"];

    shipAddressForm.elements["saId"].value = "";
    shipAddressForm.elements["alias"].value = "";
    shipAddressForm.elements["consignee"].value = "";
    shipAddressForm.elements["mobile"].value = "";
    shipAddressForm.elements["phone"].value = "";
    shipAddressForm.elements["email"].value = "";
    shipAddressForm.elements["zipcode"].value = "";
    shipAddressForm.elements["address"].value = "";
    shipAddressForm.elements["isDefault"].checked = true;

    document.getElementById("addShipAddressBut").style.display = "none";
    document.getElementById("editShipAddressBut").style.display = "none";
    document.getElementById("shipAddressBlock").style.display = "none";
}

//验证配送地址
function verifyShipAddress(alias, consignee, mobile, phone, regionId, address) {
    if (alias == "") {
        alert("请填写昵称");
        return false;
    }
    if (consignee == "") {
        alert("请填写收货人");
        return false;
    }
    if (mobile == "" && phone == "") {
        alert("手机号和固定电话必须填写一项");
        return false;
    }
    if (parseInt(regionId) < 1) {
        alert("请选择区域");
        return false;
    }
    if (address == "") {
        alert("请填写详细地址");
        return false;
    }
    return true;
}

//添加配送地址
function addShipAddress() {
    var shipAddressForm = document.forms["shipAddressForm"];

    var alias = shipAddressForm.elements["alias"].value;
    var consignee = shipAddressForm.elements["consignee"].value;
    var mobile = shipAddressForm.elements["mobile"].value;
    var phone = shipAddressForm.elements["phone"].value;
    var email = shipAddressForm.elements["email"].value;
    var zipcode = shipAddressForm.elements["zipcode"].value;
    var regionId = getSelectedOption(shipAddressForm.elements["regionId"]).value;
    var address = shipAddressForm.elements["address"].value;
    var isDefault = shipAddressForm.elements["isDefault"].checked == true ? 1 : 0;

    if (!verifyShipAddress(alias, consignee, mobile, phone, regionId, address)) {
        return;
    }

    Ajax.post("/ucenter/addshipaddress",
            { 'alias': alias, 'consignee': consignee, 'mobile': mobile, 'phone': phone, 'email': email, 'zipcode': zipcode, 'regionId': regionId, 'address': address, 'isDefault': isDefault },
            false,
            addShipAddressResponse)
}

//处理添加配送地址的反馈信息
function addShipAddressResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        closeShipAddressBlock();
        window.location.href = "/ucenter/shipaddresslist";
    }
    else if (result.state == "full") {
        alert("配送地址的数量已经达到系统所允许的最大值")
    }
    else if (result.state == "error") {
        var msg = "";
        for (var i = 0; i < result.content.length; i++) {
            msg += result.content[i].msg + "\n";
        }
        alert(msg)
    }
}

//编辑配送地址
function editShipAddress() {
    var shipAddressForm = document.forms["shipAddressForm"];

    var saId = shipAddressForm.elements["saId"].value;
    var alias = shipAddressForm.elements["alias"].value;
    var consignee = shipAddressForm.elements["consignee"].value;
    var mobile = shipAddressForm.elements["mobile"].value;
    var phone = shipAddressForm.elements["phone"].value;
    var email = shipAddressForm.elements["email"].value;
    var zipcode = shipAddressForm.elements["zipcode"].value;
    var regionId = getSelectedOption(shipAddressForm.elements["regionId"]).value;
    var address = shipAddressForm.elements["address"].value;
    var isDefault = shipAddressForm.elements["isDefault"].checked == true ? 1 : 0;

    if (saId < 1) {
        alert("请选择配送地址");
        return;
    }
    if (!verifyShipAddress(alias, consignee, mobile, phone, regionId, address)) {
        return;
    }

    Ajax.post("/ucenter/editshipaddress?saId=" + saId,
            { 'alias': alias, 'consignee': consignee, 'mobile': mobile, 'phone': phone, 'email': email, 'zipcode': zipcode, 'regionId': regionId, 'address': address, 'isDefault': isDefault },
            false,
            editShipAddressResponse)
}

//处理编辑配送地址的反馈信息
function editShipAddressResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        closeShipAddressBlock();
        window.location.href = "/ucenter/shipaddresslist";
    }
    else if (result.state == "noexist") {
        alert("配送地址不存在");
    }
    else if (result.state == "error") {
        var msg = "";
        for (var i = 0; i < result.content.length; i++) {
            msg += result.content[i].msg + "\n";
        }
        alert(msg)
    }
}

//删除配送地址
function delShipAddress(saId) {
    Ajax.get("/ucenter/delshipaddress?saId=" + saId, false, delShipAddressResponse)
}

//处理删除配送地址的反馈信息
function delShipAddressResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        removeNode(document.getElementById("shipAddress" + result.content));
        alert("删除成功");
    }
    else {
        alert(result.content);
    }
}

//设置默认配送地址
function setDefaultShipAddress(saId, obj) {
    Ajax.get("/ucenter/setdefaultshipaddress?saId=" + saId, false, function (data) {
        setDefaultShipAddressResponse(data, obj);
    })
}

//处理设置默认配送地址的反馈信息
function setDefaultShipAddressResponse(data, obj) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        var defaultShipAddress = document.getElementById("defaultShipAddress");
        if (defaultShipAddress != undefined) {
            defaultShipAddress.style.display = "";
            defaultShipAddress.id = "";
        }
        obj.style.display = "none";
        obj.id = "defaultShipAddress";
    }
    else {
        alert(result.content);
    }
}

//编辑用户
function editUser() {
    var userInfoForm = document.forms["userInfoForm"];

    var userName = userInfoForm.elements["userName"] ? userInfoForm.elements["userName"].value : "";
    var nickName = userInfoForm.elements["nickName"].value;
    var realName = userInfoForm.elements["realName"].value;
    var avatar = userInfoForm.elements["avatar"] ? userInfoForm.elements["avatar"].value : "";
    var gender = getSelectedRadio(userInfoForm.elements["gender"]).value;
    var idCard = userInfoForm.elements["idCard"].value;
    var bday = userInfoForm.elements["bday"].value;
    var regionId = getSelectedOption(userInfoForm.elements["regionId"]).value;
    var address = userInfoForm.elements["address"].value;
    var bio = userInfoForm.elements["bio"].value;

    if (!verifyEditUser(userName, nickName, realName, address, bio)) {
        return;
    }

    Ajax.post("/ucenter/edituser",
            { 'userName': userName, 'nickName': nickName, 'realName': realName, 'avatar': avatar, 'gender': gender, 'idCard': idCard, 'bday': bday, 'regionId': regionId, 'address': address, 'bio': bio },
            false,
            editUserResponse)
}

//验证编辑用户
function verifyEditUser(userName, nickName, realName, address, bio) {
    if (userName != undefined) {
        if (userName.length > 10) {
            alert("用户名长度不能大于10");
            return false;
        }
    }
    if (nickName.length > 10) {
        alert("昵称长度不能大于10");
        return false;
    }
    if (realName.length > 5) {
        alert("真实姓名长度不能大于10");
        return false;
    }
    if (address.length > 75) {
        alert("详细地址长度不能大于75");
        return false;
    }
    if (bio.length > 150) {
        alert("简介长度不能大于150");
        return false;
    }
    return true;
}

//处理编辑用户的反馈信息
function editUserResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        var userInfoForm = document.forms["userInfoForm"];
        var userNameObj = userInfoForm.elements["userName"];
        if (userNameObj != undefined) {
            userNameObj.disabled = disabled;
        }
        alert(result.content);
    }
    else if (result.state == "error") {
        var msg = "";
        for (var i = 0; i < result.content.length; i++) {
            msg += result.content[i].msg + "\n";
        }
        alert(msg)
    }
}