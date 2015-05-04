var isLoading = false;
var favoriteProductListEnd = false;
var browseProductListEnd = false;

//获得收藏夹商品列表
var fpListNextPageNumber = 2;
function getFavoriteProductList(page) {
    if (favoriteProductListEnd) {
        document.getElementById("lastPagePrompt").style.display = "block";
    }
    else {
        isLoading = true;
        document.getElementById("loadPrompt").style.display = "block";
        Ajax.get("/mob/ucenter/ajaxfavoriteproductlist?page=" + page, false, getFavoriteProductListResponse)
    }
}

//处理获得收藏夹商品列表的反馈信息
function getFavoriteProductListResponse(data) {
    try {
        var result = eval("(" + data + ")");
        for (var i = 0; i < result.ProductList.length; i++) {
            var element = document.createElement("li");
            element.id = "favoriteProduct" + result.ProductList[i].pid;
            var innerHTML = "";
            innerHTML += "<a href='/mob/" + result.ProductList[i].pid + "'.html>";
            innerHTML += "<img src='/upload/product/show/thumb60_60/" + result.ProductList[i].showimg + "' /></a>";
            innerHTML += "<div class='description'>";
            innerHTML += "<a href='/mob/" + result.ProductList[i].pid + "'.html>";
            innerHTML += "<span class='title'>" + result.ProductList[i].name + "</span><span class='price'>¥" + result.ProductList[i].shopprice + "</span></a>";
            innerHTML += "<a href='javascript:delFavoriteProduct(" + result.ProductList[i].pid + ")' class='del delFavorite'></a>";
            innerHTML += "</div></li>";
            element.innerHTML = innerHTML;
            document.getElementById("favoriteProductListBlock").appendChild(element);
        }
        document.getElementById("loadPrompt").style.display = "none";
        if (result.PageModel.HasNextPage) {
            fpListNextPageNumber += 1;
        }
        else {
            favoriteProductListEnd = true;
            document.getElementById("lastPagePrompt").style.display = "block";
        }
        isLoading = false;
    }
    catch (ex) {
        alert("加载错误");
    }
}

//删除收藏夹中的商品
function delFavoriteProduct(pid) {
    Ajax.get("/mob/ucenter/delfavoriteproduct?pid=" + pid, false, delFavoriteProductResponse)
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

//获得浏览商品列表
var bpListNextPageNumber = 1;
function getBrowseProductList(page) {
    if (browseProductListEnd) {
        document.getElementById("lastPagePrompt").style.display = "block";
    }
    else {
        isLoading = true;
        document.getElementById("loadPrompt").style.display = "block";
        Ajax.get("/mob/ucenter/ajaxbrowseproductlist?page=" + page, false, getBrowseProductListResponse)
    }
}

//处理获得浏览商品列表的反馈信息
function getBrowseProductListResponse(data) {
    try {
        var result = eval("(" + data + ")");
        for (var i = 0; i < result.ProductList.length; i++) {
            var element = document.createElement("li");
            var innerHTML = "";
            innerHTML += "<a href='/mob/" + result.ProductList[i].Pid + "'.html>";
            innerHTML += "<img src='/upload/product/show/thumb60_60/" + result.ProductList[i].ShowImg + "' /></a>";
            innerHTML += "<div class='description'>";
            innerHTML += "<a href='/mob/" + result.ProductList[i].Pid + "'.html>";
            innerHTML += "<span class='title'>" + result.ProductList[i].Name + "</span><span class='price'>¥" + result.ProductList[i].ShopPrice + "</span></a>";
            innerHTML += "</div></li>";
            element.innerHTML = innerHTML;
            document.getElementById("browseProductListBlock").appendChild(element);
        }
        document.getElementById("loadPrompt").style.display = "none";
        if (result.PageModel.HasNextPage) {
            bpListNextPageNumber += 1;
        }
        else {
            browseProductListEnd = true;
            document.getElementById("lastPagePrompt").style.display = "block";
        }
        isLoading = false;
    }
    catch (ex) {
        alert("加载错误");
    }
}

//删除配送地址
function delShipAddress(saId) {
    Ajax.get("/mob/ucenter/delshipaddress?saId=" + saId, false, delShipAddressResponse)
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
    Ajax.get("/mob/ucenter/setdefaultshipaddress?saId=" + saId, false, function (data) {
        setDefaultShipAddressResponse(data, obj);
    })
}

//处理设置默认配送地址的反馈信息
function setDefaultShipAddressResponse(data, obj) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        var defaultShipAddress = document.getElementById("defaultShipAddress");
        if (defaultShipAddress != undefined) {
            defaultShipAddress.checked = "";
            defaultShipAddress.id = "";
        }
        obj.checked = "checked";
        obj.id = "defaultShipAddress";
    }
    else {
        alert(result.content);
    }
}

//添加配送地址
function addShipAddress() {
    var shipAddressForm = document.forms["shipAddressForm"];

    var consignee = shipAddressForm.elements["consignee"].value;
    var mobile = shipAddressForm.elements["mobile"].value;
    var regionId = getSelectedOption(shipAddressForm.elements["regionId"]).value;
    var address = shipAddressForm.elements["address"].value;

    if (!verifyShipAddress(consignee, mobile, regionId, address)) {
        return;
    }

    Ajax.post("/mob/ucenter/addshipaddress",
            { 'consignee': consignee, 'mobile': mobile, 'regionId': regionId, 'address': address },
            false,
            addShipAddressResponse)
}

//处理添加配送地址的反馈信息
function addShipAddressResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        window.location.href = "/mob/ucenter/shipaddresslist";
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
    var consignee = shipAddressForm.elements["consignee"].value;
    var mobile = shipAddressForm.elements["mobile"].value;
    var regionId = getSelectedOption(shipAddressForm.elements["regionId"]).value;
    var address = shipAddressForm.elements["address"].value;

    if (saId < 1) {
        alert("请选择配送地址");
        return;
    }
    if (!verifyShipAddress(consignee, mobile, regionId, address)) {
        return;
    }

    Ajax.post("/mob/ucenter/editshipaddress?saId=" + saId,
            { 'consignee': consignee, 'mobile': mobile, 'regionId': regionId, 'address': address },
            false,
            editShipAddressResponse)
}

//处理编辑配送地址的反馈信息
function editShipAddressResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        window.location.href = "/mob/ucenter/shipaddresslist";
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

//验证配送地址
function verifyShipAddress(consignee, mobile, regionId, address) {
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