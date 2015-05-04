//取消订单
function cancelOrder(oid, cancelReason) {
    Ajax.post("/ucenter/cancelorder", { 'oid': oid, 'cancelReason': cancelReason }, false, cancelOrderResponse);
}

//处理取消订单的反馈信息
function cancelOrderResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        document.getElementById("orderState" + result.content).innerHTML = "取消";
        removeNode(document.getElementById("payOrderBut" + result.content));
        removeNode(document.getElementById("cancelOrderBut" + result.content));
        alert("取消成功");
    }
    else {
        alert(result.content);
    }
}

//打开评价商品层
function openReviewProductBlock(recordId) {
    var reviewProductFrom = document.forms["reviewProductFrom"];
    reviewProductFrom.elements["recordId"].value = recordId;
    document.getElementById("reviewProductBlock").style.display = "";
}

//评价商品
function reviewProduct() {
    var reviewProductFrom = document.forms["reviewProductFrom"];

    var oid = reviewProductFrom.elements["oid"].value;
    var recordId = reviewProductFrom.elements["recordId"].value;
    var star = getSelectedRadio(reviewProductFrom.elements["star"]).value;
    var message = reviewProductFrom.elements["message"].value;

    if (!verifyReviewProduct(recordId, star, message)) {
        return;
    }
    Ajax.post("/ucenter/reviewproduct?oid=" + oid + "&recordId=" + recordId, { 'star': star, 'message': message }, false, reviewProductResponse);
}

//验证评价商品
function verifyReviewProduct(recordId, star, message) {
    if (recordId < 1) {
        alert("请选择商品");
        return false;
    }
    if (star < 1 || star > 5) {
        alert("请选择正确的星星");
        return false;
    }
    if (message.length == 0) {
        alert("请输入评价内容");
        return false;
    }
    if (message.length > 100) {
        alert("评价内容最多输入100个字");
        return false;
    }
    return true;
}

//处理评价商品的反馈信息
function reviewProductResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        var reviewProductFrom = document.forms["reviewProductFrom"];
        reviewProductFrom.elements["recordId"].value = 0;
        reviewProductFrom.elements["message"].value = "";

        document.getElementById("reviewProductBlock").style.display = "none";

        document.getElementById("reviewState" + result.content).innerHTML = "已评价";
        document.getElementById("reviewOperate" + result.content).innerHTML = "";

        alert("评价成功");
    }
    else {
        alert(result.content);
    }
}