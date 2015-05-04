//获得配送地址列表
function getShipAddressList() {
    Ajax.get("/mob/ucenter/ajaxshipaddresslist", false, getShipAddressListResponse);
}

//处理获得配送地址列表的反馈信息
function getShipAddressListResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        var shipAddressList = "";
        for (var i = 0; i < result.content.count; i++) {
            shipAddressList += "<div class='bgBlock'></div><div class='adressI'><p>" + result.content.list[i].user + "</p><p class='f14'>" + result.content.list[i].address + "</p><div class='chooseAD'><input type='checkbox' class='radio' name='shipAddressItem' value='" + result.content.list[i].saId + "' onclick=\"selectShipAddress(" + result.content.list[i].saId + ",'" + result.content.list[i].user + "','" + result.content.list[i].address + "')\" />送到这里去</div></div>";
        }
        shipAddressList += "<a href='javascript:openAddShipAddressBlock()' class='addAddress'>+添加收货地址</a>";
        document.getElementById("mainBlock").style.display = "none";
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
    document.getElementById("shipAddressShowBlock").innerHTML = "<p>" + user + "</p><p>" + address + "</p>";
    document.getElementById("mainBlock").style.display = "";

    calculateShipFeeOfAddress();
}

//打开添加配送地址块
function openAddShipAddressBlock() {
    document.getElementById("addShipAddressBlock").style.display = "";
}

//添加配送地址
function addShipAddress() {
    var addShipAddressForm = document.forms["addShipAddressForm"];

    var consignee = addShipAddressForm.elements["consignee"].value;
    var mobile = addShipAddressForm.elements["mobile"].value;
    var regionId = getSelectedOption(addShipAddressForm.elements["regionId"]).value;
    var address = addShipAddressForm.elements["address"].value;

    if (!verifyAddShipAddress(consignee, mobile, regionId, address)) {
        return;
    }

    Ajax.post("/mob/ucenter/addshipaddress",
            { 'consignee': consignee, 'mobile': mobile, 'regionId': regionId, 'address': address, 'isDefault': 1 },
            false,
            addShipAddressResponse)
}

//验证添加的收货地址
function verifyAddShipAddress(consignee, mobile, regionId, address) {
    if (consignee == "") {
        alert("请填写收货人");
        return false;
    }
    if (mobile == "") {
        alert("请填写手机号");
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
        html += addShipAddressForm.elements["mobile"].value + "&nbsp;&nbsp;";
        html += "</p><p>";
        html += getSelectedOption(addShipAddressForm.elements["provinceId"]).text + "&nbsp;&nbsp;";
        html += getSelectedOption(addShipAddressForm.elements["cityId"]).text + "&nbsp;&nbsp;";
        html += getSelectedOption(addShipAddressForm.elements["regionId"]).text + "&nbsp;&nbsp;";
        html += addShipAddressForm.elements["address"].value + "&nbsp;&nbsp;";
        html += "</p>";

        document.getElementById("saId").value = result.content;
        document.getElementById("shipAddressListBlock").style.display = "none";
        document.getElementById("addShipAddressBlock").style.display = "none";

        addShipAddressForm.elements["consignee"].value = "";
        addShipAddressForm.elements["mobile"].value = "";
        addShipAddressForm.elements["address"].value = "";

        document.getElementById("shipAddressShowBlock").innerHTML = html;
        document.getElementById("mainBlock").style.display = "";

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
    Ajax.get("/mob/order/changeshipaddress?saId=" + saId + "&shipName=" + shipName + "&selectedCartItemKeyList=" + selectedCartItemKeyList, false, function (data) {
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
            if (document.getElementById("orderAmountShowBlock") != undefined) {
                document.getElementById("orderAmountShowBlock").innerHTML = newOrderAmount;
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
    document.getElementById("mainBlock").style.display = "none";
    document.getElementById("shipPluginListBlock").style.display = "";
}

//选择配送方式
function selectShipPlugin(shipSystemName, shipFriendName) {
    Ajax.get("/mob/order/selectshipplugin?shipName=" + shipSystemName + "&payName=" + document.getElementById("payName").value + "&saId=" + document.getElementById("saId").value + "&selectedCartItemKeyList=" + document.getElementById("selectedCartItemKeyList").value, false, function (data) {
        selectShipPluginResponse(data, shipSystemName, shipFriendName);
    });
}

//处理选择配送方式的反馈信息
function selectShipPluginResponse(data, shipSystemName, shipFriendName) {
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
        if (document.getElementById("orderAmountShowBlock") != undefined) {
            document.getElementById("orderAmountShowBlock").innerHTML = newOrderAmount;
        }

        document.getElementById("shipName").value = shipSystemName;
        document.getElementById("shipPluginShowBlock").innerHTML = shipFriendName;
        document.getElementById("shipPluginListBlock").style.display = "none";
        document.getElementById("mainBlock").style.display = "";
    }
    else {
        alert(result.content);
    }
}

//展示支付插件列表
function showPayPluginList() {
    document.getElementById("mainBlock").style.display = "none";
    document.getElementById("payPluginListBlock").style.display = "";
}

//选择支付方式
function selectPayPlugin(paySystemName, payFriendName) {
    Ajax.get("/mob/order/selectpayplugin?payName=" + paySystemName + "&shipName=" + document.getElementById("shipName").value + "&saId=" + document.getElementById("saId").value + "&selectedCartItemKeyList=" + document.getElementById("selectedCartItemKeyList").value, false, function (data) {
        selectPayPluginResponse(data, paySystemName, payFriendName);
    });
}

//处理选择支付方式的反馈信息
function selectPayPluginResponse(data, paySystemName, payFriendName) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        var oldPayFee = document.getElementById("payFee").value;
        document.getElementById("payFee").value = result.content;
        var changeMoney = parseFloat(result.content) - parseFloat(oldPayFee);
        var oldOrderAmount = document.getElementById("orderAmount").value;
        var newOrderAmount = parseFloat(oldOrderAmount) + parseFloat(changeMoney);
        document.getElementById("orderAmount").value = newOrderAmount;
        if (document.getElementById("payFeeShowBlock") != undefined) {
            document.getElementById("payFeeShowBlock").innerHTML = result.content;
        }
        if (document.getElementById("orderAmountShowBlock") != undefined) {
            document.getElementById("orderAmountShowBlock").innerHTML = newOrderAmount;
        }

        document.getElementById("payName").value = paySystemName;
        document.getElementById("payPluginShowBlock").innerHTML = payFriendName;
        document.getElementById("payPluginListBlock").style.display = "none";
        document.getElementById("mainBlock").style.display = "";
    }
    else {
        alert(result.content);
    }
}

//获得有效的优惠劵列表
function getValidCouponList() {
    Ajax.get("/mob/order/getvalidcouponlist?selectedCartItemKeyList=" + document.getElementById("selectedCartItemKeyList").value, false, getValidCouponListResponse);
}

//处理获得有效的优惠劵列表的反馈信息
function getValidCouponListResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        if (result.content.length < 1) {
            document.getElementById("validCouponList").innerHTML = "<div class=\"allCell\">此订单暂无可用的优惠券</div>";
        }
        else {
            var itemList = "";
            for (var i = 0; i < result.content.length; i++) {
                itemList += "<div class=\"allCell\"><span class=\"radio\" checked='false' value='" + result.content[i].couponId + "' useMode='" + result.content[i].useMode + "' onclick='checkCouponUseMode(this)' text='" + result.content[i].name + "'></span>" + result.content[i].name + "</div>";
            }
            document.getElementById("validCouponList").innerHTML = itemList;
        }
        document.getElementById("mainBlock").style.display = "none";
        document.getElementById("validCouponListBlcok").style.display = "";
    }
    else {
        alert(result.content);
    }
}

//检查优惠劵的使用模式
function checkCouponUseMode(obj) {
    if (obj.getAttribute("checked") == "true") {
        obj.setAttribute("checked", "false");
        obj.className = "radio";
    }
    else {
        var useMode = obj.getAttribute("useMode");
        if (useMode == "1") {
            var checkboxList = document.getElementById("validCouponList").getElementsByTagName("span");
            for (var i = 0; i < checkboxList.length; i++) {
                checkboxList[i].setAttribute("checked", "false");
                checkboxList[i].className = "radio";
            }
        }
        obj.setAttribute("checked", "true");
        obj.className = "radio checked";
    }
}

//确认选择的优惠劵
function confirmSelectedCoupon() {
    var couponList = "";
    var couponIdCheckboxList = document.getElementById("validCouponList").getElementsByTagName("span");
    for (var i = 0; i < couponIdCheckboxList.length; i++) {
        if (couponIdCheckboxList[i].getAttribute("checked") == "true") {
            couponList += "<div class='sell'><i>惠</i>" + couponIdCheckboxList[i].getAttribute("text") + "</div>";
        }
    }
    document.getElementById("selectCouponList").innerHTML = couponList;
    document.getElementById("mainBlock").style.display = "";
    document.getElementById("validCouponListBlcok").style.display = "none";
}

//提交订单
function submitOrder() {
    var selectedCartItemKeyList = document.getElementById("selectedCartItemKeyList").value
    var saId = document.getElementById("saId").value;
    var payName = document.getElementById("payName").value;
    var shipName = document.getElementById("shipName").value;
    var payCreditCount = document.getElementById("payCreditCount") && document.getElementById("payCreditCount").checked == "checked" ? document.getElementById("payCreditCount").value : 0;

    var couponIdList = "";
    var couponIdCheckboxList = document.getElementById("validCouponList").getElementsByTagName("span");
    for (var i = 0; i < couponIdCheckboxList.length; i++) {
        if (couponIdCheckboxList[i].getAttribute("checked") == "true") {
            couponIdList += couponIdCheckboxList[i].getAttribute("value") + ",";
        }
    }
    if (couponIdCheckboxList.length > 0)
        couponIdList = couponIdList.substring(0, couponIdCheckboxList.length - 1)

    var fullCut = document.getElementById("fullCut") ? document.getElementById("fullCut").value : 0;

    if (!verifySubmitOrder(saId, payName, shipName)) {
        return;
    }

    Ajax.post("/mob/order/submitorder",
            { 'selectedCartItemKeyList': selectedCartItemKeyList, 'saId': saId, 'payName': payName, 'shipName': shipName, 'payCreditCount': payCreditCount, 'couponIdList': couponIdList, 'fullCut': fullCut },
            false,
            submitOrderResponse)
}

//验证提交订单
function verifySubmitOrder(saId, payName, shipName) {
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