//增加商品数量
function addProuctCount() {
    var buyCountInput = document.getElementById("buyCount");
    var buyCount = buyCountInput.value;
    if (!isInt(buyCount)) {
        alert('请输入数字');
        return false;
    }
    buyCountInput.value = parseInt(buyCount) + 1;
}

//减少商品数量
function cutProductCount() {
    var buyCountInput = document.getElementById("buyCount");
    var buyCount = buyCountInput.value;
    if (!isInt(buyCount)) {
        alert('请输入数字');
        return false;
    }
    var count = parseInt(buyCount);
    if (count > 1) {
        buyCountInput.value = count - 1;
    }
}

//添加商品到收藏夹
function addToFavorite(pid) {
    if (pid < 1) {
        alert("请选择商品");
    }
    else if (uid < 1) {
        alert("请先登录");
    }
    else {
        Ajax.get("/mob/ucenter/addproducttofavorite?pid=" + pid, false, addProductToFavoriteResponse)
    }
}

//处理添加商品到收藏夹的反馈信息
function addToFavoriteResponse(data) {
    var result = eval("(" + data + ")");
    alert(result.content);
}

//添加商品到购物车
function addProductToCart(pid, buyCount, type) {
    if (pid < 1) {
        alert("请选择商品");
    }
    else if (isGuestSC == 0 && uid < 1) {
        alert("请先登录");
    }
    else if (buyCount < 1) {
        alert("请填写购买数量");
    }
    else {
        Ajax.get("/mob/cart/addproduct?pid=" + pid + "&buyCount=" + buyCount, false, function (data) {
            addProductToCartResponse(type, data);
        });
    }
}

//处理添加商品到购物车的反馈信息
function addProductToCartResponse(type, data) {
    var result = eval("(" + data + ")");
    if(result.state == "success") {
        if (type == 0) {
            window.location.href = "/mob/cart/index";
        }
        else {
            document.getElementById("addResult1").style.display = "block";
            document.getElementById("addResult2").style.display = "block";
        }
    }
    else{
        alert(result.content);
    }
}

//添加套装到购物车
function addSuitToCart(pmId, buyCount, type) {
    if (pmId < 1) {
        alert("请选择套装");
    }
    else if (isGuestSC == 0 && uid < 1) {
        alert("请先登录");
    }
    else if (buyCount < 1) {
        alert("请填写购买数量");
    }
    else {
        Ajax.get("/mob/cart/addsuit?pmId=" + pmId + "&buyCount=" + buyCount, false, function (data) {
            addSuitToCartResponse(type, data);
        });
    }
}

//处理添加套装到购物车的反馈信息
function addSuitToCartResponse(type, data) {
    var result = eval("(" + data + ")");
    if (result.state != "stockout") {
        if (type == 0) {
            window.location.href = "/mob/cart/index";
        }
        else {
            document.getElementById("addResult1").style.display = "block";
            document.getElementById("addResult2").style.display = "block";
        }
    }
    else {
        alert("商品库存不足");
    }
}

//获得商品评价列表
function getProductReviewList(pid, reviewType, page) {
    Ajax.get("/mob/catalog/ajaxproductreviewlist?pid=" + pid + "&reviewType=" + reviewType + "&page=" + page, false, getProductReviewListResponse)
}

//处理获得商品评价的反馈信息
function getProductReviewListResponse(data) {
    document.getElementById("productReviewList").innerHTML = data;
}

//获得商品咨询列表
function getProductConsultList(pid, consultTypeId, consultMessage, page) {
    Ajax.get("/mob/catalog/ajaxproductconsultlist?pid=" + pid + "&consultTypeId=" + consultTypeId + "&consultMessage=" + encodeURIComponent(consultMessage) + "&page=" + page, false, getProductConsultListResponse)
}

//处理获得商品咨询的反馈信息
function getProductConsultListResponse(data) {
    document.getElementById("productConsultList").innerHTML = data;
}