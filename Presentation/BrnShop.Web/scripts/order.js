//获得配送地址列表
function getShipAddressList() {
    Ajax.get("/ucenter/ajaxshipaddresslist", false, getShipAddressListResponse);
}

//处理获得配送地址列表的反馈信息
function getShipAddressListResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        var shipAddressList = "<ul class='orderList'>";
        for (var i = 0; i < result.content.count; i++) {
            shipAddressList += "<li><label><b><input type='radio' class='radio' name='shipAddressItem' value='" + result.content.list[i].saId + "' onclick=\"selectShipAddress(" + result.content.list[i].saId + ",'" + result.content.list[i].user + "','" + result.content.list[i].address + "')\" />" + result.content.list[i].user + "</b><i>" + result.content.list[i].address + "</i></label></li>";
        }
        shipAddressList += "<li id='newAdress'><label><input type='radio' class='radio' name='shipAddressItem' onclick='openAddShipAddressBlock()' />使用新地址</label></li></ul>";
        document.getElementById("shipAddressShowBlock").style.display = "none";
        document.getElementById("shipAddressListBlock").style.display = "";
        document.getElementById("shipAddressListBlock").innerHTML = shipAddressList;
    }
    else {
        alert(result.content);
    }
}

//选择配送地址
function selectShipAddress(saId, user, address) {
    document.getElementById("saId").value = saId;
    document.getElementById("shipAddressListBlock").style.display = "none";
    document.getElementById("addShipAddressBlock").style.display = "none";
    document.getElementById("shipAddressShowBlock").style.display = "";
    document.getElementById("shipAddressShowBlock").innerHTML = "<p>" + user + "</p><p>" + address + "</p>";

    calculateShipFeeOfAddress();
}

//打开添加配送地址块
function openAddShipAddressBlock() {
    document.getElementById("addShipAddressBlock").style.display = "";
}

//添加配送地址
function addShipAddress() {
    var addShipAddressForm = document.forms["addShipAddressForm"];

    var alias = addShipAddressForm.elements["alias"].value;
    var consignee = addShipAddressForm.elements["consignee"].value;
    var mobile = addShipAddressForm.elements["mobile"].value;
    var phone = addShipAddressForm.elements["phone"].value;
    var email = addShipAddressForm.elements["email"].value;
    var zipcode = addShipAddressForm.elements["zipcode"].value;
    var regionId = getSelectedOption(addShipAddressForm.elements["regionId"]).value;
    var address = addShipAddressForm.elements["address"].value;
    var isDefault = addShipAddressForm.elements["isDefault"] == undefined ? 0 : addShipAddressForm.elements["isDefault"].checked ? 1 : 0;
    isDefault = 1;

    if (!verifyAddShipAddress(alias, consignee, mobile, phone, regionId, address)) {
        return;
    }

    Ajax.post("/ucenter/addshipaddress",
            { 'alias': alias, 'consignee': consignee, 'mobile': mobile, 'phone': phone, 'email': email, 'zipcode': zipcode, 'regionId': regionId, 'address': address, 'isDefault': isDefault },
            false,
            addShipAddressResponse)
}

//验证添加的收货地址
function verifyAddShipAddress(alias, consignee, mobile, phone, regionId, address) {
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

//处理添加配送地址的反馈信息
function addShipAddressResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        var addShipAddressForm = document.forms["addShipAddressForm"];

        var html = "<p>";
        html += addShipAddressForm.elements["consignee"].value + "&nbsp;&nbsp;";
        if (addShipAddressForm.elements["mobile"].value.length > 0) {
            html += addShipAddressForm.elements["mobile"].value + "&nbsp;&nbsp;";
        }
        else {
            html += addShipAddressForm.elements["phone"].value + "&nbsp;&nbsp;";
        }

        html += "</p><p>";

        html += getSelectedOption(addShipAddressForm.elements["provinceId"]).text + "&nbsp;&nbsp;";
        html += getSelectedOption(addShipAddressForm.elements["cityId"]).text + "&nbsp;&nbsp;";
        html += getSelectedOption(addShipAddressForm.elements["regionId"]).text + "&nbsp;&nbsp;";

        html += addShipAddressForm.elements["address"].value + "&nbsp;&nbsp;";
        html += "</p>";

        document.getElementById("shipAddressShowBlock").style.display = "";
        document.getElementById("shipAddressShowBlock").innerHTML = html;

        document.getElementById("saId").value = result.content;
        document.getElementById("shipAddressListBlock").style.display = "none";
        document.getElementById("addShipAddressBlock").style.display = "none";

        addShipAddressForm.elements["alias"].value = "";
        addShipAddressForm.elements["consignee"].value = "";
        addShipAddressForm.elements["mobile"].value = "";
        addShipAddressForm.elements["phone"].value = "";
        addShipAddressForm.elements["email"].value = "";
        addShipAddressForm.elements["zipcode"].value = "";
        addShipAddressForm.elements["address"].value = "";

        calculateShipFeeOfAddress();
    }
    else {
        var msg = "";
        for (var i = 0; i < result.content.length; i++) {
            msg += result.content[i].msg + "\n";
        }
        alert(msg)
    }
}

//计算地址的配送费用
function calculateShipFeeOfAddress() {
    var saId = document.getElementById("saId").value;
    var shipName = document.getElementById("shipName").value;
    var selectedCartItemKeyList = document.getElementById("selectedCartItemKeyList").value
    Ajax.get("/order/changeshipaddress?saId=" + saId + "&shipName=" + shipName + "&selectedCartItemKeyList=" + selectedCartItemKeyList, false, function (data) {
        var result = eval("(" + data + ")");
        if (result.state == "success") {
            var oldShipFee = document.getElementById("shipFee").value;
            document.getElementById("shipFee").value = result.content;
            var changeMoney = parseFloat(result.content) - parseFloat(oldShipFee);
            var oldOrderAmount = document.getElementById("orderAmount").value;
            var newOrderAmount = parseFloat(oldOrderAmount) + parseFloat(changeMoney);
            document.getElementById("orderAmount").value = newOrderAmount;
            if (document.getElementById("shipFeeShowBlock") != undefined) {
                document.getElementById("shipFeeShowBlock").innerHTML = result.content;
            }
            if (document.getElementById("orderAmountShowBlock1") != undefined) {
                document.getElementById("orderAmountShowBlock1").innerHTML = newOrderAmount;
            }
            if (document.getElementById("orderAmountShowBlock2") != undefined) {
                document.getElementById("orderAmountShowBlock2").innerHTML = newOrderAmount;
            }
        }
        else if (result.state == "emptyship" || result.state == "faraddress") {
            alert(result.content);
            getShipPluginList();
        }
        else {
            alert(result.content);
        }
    });
}

//展示配送插件列表
function showShipPluginList() {
    document.getElementById("shipPluginShowBlock").style.display = "none";
    document.getElementById("shipPluginListBlock").style.display = "";
}

//选择配送方式
function selectShipPlugin(shipSystemName, shipFriendName) {
    Ajax.get("/order/selectshipplugin?shipName=" + shipSystemName + "&payName=" + document.getElementById("payName").value + "&saId=" + document.getElementById("saId").value + "&selectedCartItemKeyList=" + document.getElementById("selectedCartItemKeyList").value, false, function (data) {
        selectShipPluginResponse(data, shipSystemName, shipFriendName);
    });
}

//处理选择配送方式的反馈信息
function selectShipPluginResponse(data, shipSystemName, shipFriendName) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        document.getElementById("shipName").value = shipSystemName;
        document.getElementById("shipPluginShowBlock").style.display = "";
        document.getElementById("shipPluginShowBlock").innerHTML = shipFriendName;
        document.getElementById("shipPluginListBlock").style.display = "none";


        var oldShipFee = document.getElementById("shipFee").value;
        document.getElementById("shipFee").value = result.content;
        var changeMoney = parseFloat(result.content) - parseFloat(oldShipFee);
        var oldOrderAmount = document.getElementById("orderAmount").value;
        var newOrderAmount = parseFloat(oldOrderAmount) + parseFloat(changeMoney);
        document.getElementById("orderAmount").value = newOrderAmount;
        if (document.getElementById("shipFeeShowBlock") != undefined) {
            document.getElementById("shipFeeShowBlock").innerHTML = result.content;
        }
        if (document.getElementById("orderAmountShowBlock1") != undefined) {
            document.getElementById("orderAmountShowBlock1").innerHTML = newOrderAmount;
        }
        if (document.getElementById("orderAmountShowBlock2") != undefined) {
            document.getElementById("orderAmountShowBlock2").innerHTML = newOrderAmount;
        }
    }
    else {
        alert(result.content);
    }
}

//展示支付插件列表
function showPayPluginList() {
    document.getElementById("payPluginShowBlock").style.display = "none";
    document.getElementById("payPluginListBlock").style.display = "";
}

//选择支付方式
function selectPayPlugin(paySystemName, payFriendName) {
    Ajax.get("/order/selectpayplugin?payName=" + paySystemName + "&shipName=" + document.getElementById("shipName").value + "&saId=" + document.getElementById("saId").value + "&selectedCartItemKeyList=" + document.getElementById("selectedCartItemKeyList").value, false, function (data) {
        selectPayPluginResponse(data, paySystemName, payFriendName);
    });
}

//处理选择支付方式的反馈信息
function selectPayPluginResponse(data, paySystemName, payFriendName) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        document.getElementById("payName").value = paySystemName;
        document.getElementById("payPluginShowBlock").style.display = "";
        document.getElementById("payPluginShowBlock").innerHTML = payFriendName;
        document.getElementById("payPluginListBlock").style.display = "none";


        var oldPayFee = document.getElementById("payFee").value;
        document.getElementById("payFee").value = result.content;
        var changeMoney = parseFloat(result.content) - parseFloat(oldPayFee);
        var oldOrderAmount = document.getElementById("orderAmount").value;
        var newOrderAmount = parseFloat(oldOrderAmount) + parseFloat(changeMoney);
        document.getElementById("orderAmount").value = newOrderAmount;
        if (document.getElementById("payFeeShowBlock") != undefined) {
            document.getElementById("payFeeShowBlock").innerHTML = result.content;
        }
        if (document.getElementById("orderAmountShowBlock1") != undefined) {
            document.getElementById("orderAmountShowBlock1").innerHTML = newOrderAmount;
        }
        if (document.getElementById("orderAmountShowBlock2") != undefined) {
            document.getElementById("orderAmountShowBlock2").innerHTML = newOrderAmount;
        }
    }
    else {
        alert(result.content);
    }
}

//验证支付积分
function verifyPayCredit(hasPayCreditCount, maxUsePayCreditCount) {
    var obj = document.getElementById("payCreditCount");
    var usePayCreditCount = obj.value;
    if (isNaN(usePayCreditCount)) {
        obj.value = 0;
        alert("请输入数字");
    }
    else if (usePayCreditCount > hasPayCreditCount) {
        obj.value = hasPayCreditCount;
        alert("积分不足");
    }
    else if (usePayCreditCount > maxUsePayCreditCount) {
        obj.value = maxUsePayCreditCount;
        alert("最多只能使用" + maxUsePayCreditCount + "个");
    }
}

//获得有效的优惠劵列表
function getValidCouponList() {
    Ajax.get("/order/getvalidcouponlist?selectedCartItemKeyList=" + document.getElementById("selectedCartItemKeyList").value, false, getValidCouponListResponse);
}

//处理获得有效的优惠劵列表的反馈信息
function getValidCouponListResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        if (result.content.length < 1) {
            document.getElementById("validCouponList").innerHTML = "<p>此订单暂无可用的优惠券</p>";
        }
        else {
            var itemList = "<p class='chooseYH'>";
            for (var i = 0; i < result.content.length; i++) {
                itemList += "<label><input type='checkbox' name='couponId' value='" + result.content[i].couponId + "' useMode='" + result.content[i].usemode + "' onclick='checkCouponUseMode(this)'/>" + result.content[i].name + "</label>";
            }
            itemList += "</p>";
            document.getElementById("validCouponList").innerHTML = itemList;
        }
        document.getElementById("validCouponCount").innerHTML = result.content.length;
    }
    else {
        alert(result.content);
    }
}

//检查优惠劵的使用模式
function checkCouponUseMode(obj) {
    if (!obj.checked) {
        return;
    }
    var useMode = obj.getAttribute("useMode");
    if (useMode == "0") {
        return;
    }
    var checkboxList = document.getElementById("validCouponList").getElementsByTagName("input");
    for (var i = 0; i < checkboxList.length; i++) {
        checkboxList[i].checked = false;
    }
    obj.checked = true;
}

//验证优惠劵编号
function verifyCouponSN(couponSN) {
    if (couponSN == undefined || couponSN == null || couponSN.length == 0) {
        alert("请输入优惠劵编号");
    }
    else if (couponSN.length != 16) {
        alert("优惠劵编号不正确");
    }
    else {
        Ajax.get("/order/verifycouponsn?couponSN=" + couponSN, false, verifyCouponSNResponse);
    }
}

//处理验证优惠劵编号的反馈信息
function verifyCouponSNResponse(data) {
    var result = eval("(" + data + ")");
    alert(result.content);
}

//提交订单
function submitOrder() {
    var selectedCartItemKeyList = document.getElementById("selectedCartItemKeyList").value
    var saId = document.getElementById("saId").value;
    var payName = document.getElementById("payName").value;
    var shipName = document.getElementById("shipName").value;
    var payCreditCount = document.getElementById("payCreditCount") ? document.getElementById("payCreditCount").value : 0;

    var couponIdList = "";
    var couponIdCheckboxList = document.getElementById("validCouponList").getElementsByTagName("input");
    for (var i = 0; i < couponIdCheckboxList.length; i++) {
        if (couponIdCheckboxList[i].checked == true) {
            couponIdList += couponIdCheckboxList[i].value + ",";
        }
    }
    if (couponIdCheckboxList.length > 0)
        couponIdList = couponIdList.substring(0, couponIdCheckboxList.length - 1)

    var couponSN = document.getElementById("couponSN") ? document.getElementById("couponSN").value : "";
    var fullCut = document.getElementById("fullCut") ? document.getElementById("fullCut").value : 0;
    var bestTime = document.getElementById("bestTime") ? document.getElementById("bestTime").value : "";
    var buyerRemark = document.getElementById("buyerRemark") ? document.getElementById("buyerRemark").value : "";
    var verifyCode = document.getElementById("verifyCode") ? document.getElementById("verifyCode").value : "";

    if (!verifySubmitOrder(saId, payName, shipName, buyerRemark)) {
        return;
    }

    Ajax.post("/order/submitorder",
            { 'selectedCartItemKeyList': selectedCartItemKeyList, 'saId': saId, 'payName': payName, 'shipName': shipName, 'payCreditCount': payCreditCount, 'couponIdList': couponIdList, 'couponSNList': couponSN, 'fullCut': fullCut, 'bestTime': bestTime, 'buyerRemark': buyerRemark, 'verifyCode': verifyCode },
            false,
            submitOrderResponse)
}

//验证提交订单
function verifySubmitOrder(saId, payName, shipName, buyerRemark) {
    if (saId < 1) {
        alert("请填写收货人信息");
        return false;
    }
    if (payName.length < 1) {
        alert("配送方式不能为空");
        return false;
    }
    if (shipName.length < 1) {
        alert("支付方式不能为空");
        return false;
    }
    if (buyerRemark.length > 125) {
        alert("最多只能输入125个字");
        return false;
    }
    return true;
}

//处理提交订单的反馈信息
function submitOrderResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state != "success") {
        alert(result.content);
    }
    else {
        window.location.href = result.content;
    }
}