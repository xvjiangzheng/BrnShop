var uid = -1; //用户id
var isGuestSC = 0; //是否允许游客使用购物车(0代表不可以，1代表可以)

//返回
function pageBack() {
    var a = window.location.href;
    if (/#top/.test(a)) {
        window.history.go(-2);
        window.location.load(window.location.href)
    } else {
        window.history.back();
        window.location.load(window.location.href)
    }
}

//导航栏显示和隐藏
function navSH() {
    var navObj = document.getElementById("nav");
    if (navObj.style.display == "none") {
        navObj.style.display = "block";
    }
    else {
        navObj.style.display = "none";
    }
}

//搜索
function search(keyword) {
    if (keyword == undefined || keyword == null || keyword.length < 1) {
        alert("请输入关键词");
    }
    else {
        window.location.href = "/mob/catalog/search?keyword=" + encodeURIComponent(keyword);
    }
}

//获得分类商品列表
var cpListNextPageNumber = 2;
function getCategoryProductList(cateId, brandId, filterPrice, filterAttr, onlyStock, sortColumn, sortDirection, page) {
    document.getElementById("loadBut").style.display = "none";
    document.getElementById("loadPrompt").style.display = "block";
    Ajax.get("/mob/catalog/ajaxcategory?cateId=" + cateId + "&brandId=" + brandId + "&filterPrice=" + filterPrice + "&filterAttr=" + filterAttr + "&onlyStock=" + onlyStock + "&sortColumn=" + sortColumn + "&sortDirection=" + sortDirection + "&page=" + page, false, function (data) {
        getCategoryProductListResponse(data);
    })
}

//处理获得分类商品列表的反馈信息
function getCategoryProductListResponse(data) {
    try {
        var result = eval("(" + data + ")");
        var element = document.createElement("div");
        element.className = "proItme";
        var innerHTML = "";
        for (var i = 0; i < result.ProductList.length; i++) {
            var reviewLayer = 100;
            var goodStars = result.ProductList[i].Star1 + result.ProductList[i].Star2 + result.ProductList[i].Star3;
            var allStars = goodStars + result.ProductList[i].Star4 + result.ProductList[i].Star5;
            if (allStars != 0) {
                reviewLayer = goodStars * 100 / allStars;
            }

            innerHTML += "<a href='/mob/" + result.ProductList[i].Pid + ".html'>";
            innerHTML += "<img src='/upload/product/show/thumb100_100/" + result.ProductList[i].ShowImg + "' width='100' height='100' class='img' />";
            innerHTML += "<span class='proDt'>";
            innerHTML += "<strong class='proDD DD1'>" + result.ProductList[i].Name + "</strong>";
            innerHTML += "<b class='proDD DD3'>￥" + result.ProductList[i].ShopPrice + "</b>";
            innerHTML += "<p class='proDD DD4'>" + result.ProductList[i].ReviewCount + " 人评价，" + reviewLayer + "%好评</p>";
            innerHTML += "</span></a>";
        }
        element.innerHTML = innerHTML;
        document.getElementById("categoryProductListBlock").appendChild(element);
        if (result.PageModel.HasNextPage) {
            document.getElementById("loadBut").style.display = "block";
            document.getElementById("loadPrompt").style.display = "none";
            cpListNextPageNumber += 1;
        }
        else {
            document.getElementById("loadBut").style.display = "none";
            document.getElementById("loadPrompt").style.display = "none";
            document.getElementById("lastPagePrompt").style.display = "block";
        }
    }
    catch (ex) {
        alert("加载错误");
    }
}

//获得搜索商品列表
var spListNextPageNumber = 2;
function getSearchProductList(keyword, cateId, brandId, filterPrice, filterAttr, onlyStock, sortColumn, sortDirection, page) {
    document.getElementById("loadBut").style.display = "none";
    document.getElementById("loadPrompt").style.display = "block";
    Ajax.get("/mob/catalog/ajaxsearch?keyword=" + encodeURIComponent(keyword) + "&cateId=" + cateId + "&brandId=" + brandId + "&filterPrice=" + filterPrice + "&filterAttr=" + filterAttr + "&onlyStock=" + onlyStock + "&sortColumn=" + sortColumn + "&sortDirection=" + sortDirection + "&page=" + page, false, function (data) {
        getSearchProductListResponse(data);
    })
}

//处理获得搜索商品列表的反馈信息
function getSearchProductListResponse(data) {
    try {
        var result = eval("(" + data + ")");
        var element = document.createElement("div");
        element.className = "proItme";
        var innerHTML = "";
        for (var i = 0; i < result.ProductList.length; i++) {
            var reviewLayer = 100;
            var goodStars = result.ProductList[i].Star1 + result.ProductList[i].Star2 + result.ProductList[i].Star3;
            var allStars = goodStars + result.ProductList[i].Star4 + result.ProductList[i].Star5;
            if (allStars != 0) {
                reviewLayer = goodStars * 100 / allStars;
            }

            innerHTML += "<a href='/mob/" + result.ProductList[i].Pid + ".html'>";
            innerHTML += "<img src='/upload/product/show/thumb100_100/" + result.ProductList[i].ShowImg + "' width='100' height='100' class='img' />";
            innerHTML += "<span class='proDt'>";
            innerHTML += "<strong class='proDD DD1'>" + result.ProductList[i].Name + "</strong>";
            innerHTML += "<b class='proDD DD3'>￥" + result.ProductList[i].ShopPrice + "</b>";
            innerHTML += "<p class='proDD DD4'>" + result.ProductList[i].ReviewCount + " 人评价，" + reviewLayer + "%好评</p>";
            innerHTML += "</span></a>";
        }
        element.innerHTML = innerHTML;
        document.getElementById("searchProductListBlock").appendChild(element);
        if (result.PageModel.HasNextPage) {
            document.getElementById("loadBut").style.display = "block";
            document.getElementById("loadPrompt").style.display = "none";
            spListNextPageNumber += 1;
        }
        else {
            document.getElementById("loadBut").style.display = "none";
            document.getElementById("loadPrompt").style.display = "none";
            document.getElementById("lastPagePrompt").style.display = "block";
        }
    }
    catch (ex) {
        alert("加载错误");
    }
}