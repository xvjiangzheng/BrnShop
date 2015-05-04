var productPageType = 0; //0为添加商品页面,1为编辑商品页面,2为商品列表页面,3为添加sku页面
var pid = 0; //商品id
var productAttributeList = null; //商品属性列表
var productSKUItemList = null; //商品sku项列表
var batchOnSaleMessage = "您确定要上架全部吗？";
var batchOutSaleMessage = "您确定要下架全部吗？";
var batchRecycleMessage = "您确定要删除全部吗？";
var batchRestoreMessage = "您确定要恢复全部吗？";

//小数取整
function modFoat(v) {
    var max = parseInt(v) + 1;
    if (max - v < 1) {
        return max;
    }
    return v;
}

//构建普通属性选择表格
function buildCommonAttrSelectTable(result) {
    if (result.length < 1) {
        return "";
    }

    var tableHtml = "";
    var tempInputAttrValueId = 0;
    for (var i = 0; i < result.length; i++) {
        tableHtml += "<tr><td width='80px;' align='right'>";
        tableHtml += result[i].name;
        tableHtml += "：</td><td>";
        tableHtml += "<input name='AttrIdList' type='checkbox' style='display:none;' value='" + result[i].attrId + "'/>";
        for (var j = 0; j < result[i].attrValueList.length; j++) {
            tableHtml += "<input name='AttrValueIdList' type='checkbox' onclick='selectCommonAttrValue(this)' isInput='" + result[i].attrValueList[j].isInput + "' value='" + result[i].attrValueList[j].attrValueId + "' />" + result[i].attrValueList[j].attrValue + "&nbsp;&nbsp;";
            if (result[i].attrValueList[j].isInput == 1) {
                tempInputAttrValueId = result[i].attrValueList[j].attrValueId;
            }
        }
        tableHtml += "<input name='AttrInputValueList' type='text' attrId='" + result[i].attrId + "' attrValueId='" + tempInputAttrValueId + "' class='inputattrvalue input' size='20' style='display:none;' value=''/>";
    }
    tableHtml += "</td></tr>";
    return tableHtml;
}

//选择普通属性值
function selectCommonAttrValue(obj) {
    var attrValueObj = $(obj);
    var isInput = attrValueObj.attr("isInput");

    if (attrValueObj.attr("checked") == true) {
        attrValueObj.parent().find("input[type=checkbox]").attr("checked", false);
        attrValueObj.parent().find("input[name=AttrIdList]").attr("checked", true);
        attrValueObj.attr("checked", true);
        if (isInput == "1") {//手动输入时
            attrValueObj.parent().find("input[type=text]").show();
        }
        else {//选择此属性时
            attrValueObj.parent().find("input[type=text]").val("").hide();
        }
    }
    else {
        attrValueObj.parent().find("input[type=checkbox]").attr("checked", false);
        attrValueObj.parent().find("input[type=text]").val("").hide();
    }

    //页面为编辑商品页面时
    if (productPageType == 1) {

        var changeProductAttributeUrl = "";
        if (attrValueObj.attr("checked") == false) {
            changeProductAttributeUrl = "/admin/product/delproductattribute?pid=" + pid + "&attrId=" + attrValueObj.parent().find("input[name=AttrIdList]").val() + "&t=" + new Date();
        }
        else {
            if (isInput == "0") {
                changeProductAttributeUrl = "/admin/product/updateproductattribute?type=0" + "&pid=" + pid + "&attrId=" + attrValueObj.parent().find("input[name=AttrIdList]").val() + "&attrValueId=" + attrValueObj.val() + "&t=" + new Date();
            }
        }
        if (changeProductAttributeUrl != "") {
            $.jBox.tip("正在更新...", 'loading');
            $.get(changeProductAttributeUrl, function (data, textStatus) {
                if (data != "0") {
                    $.jBox.tip('更新成功！', 'success');
                } else {
                    $.jBox.error('更新失败，请联系管理员！', '更新失败');
                }
            });
        }

    }
}

//构建SKU属性选择表格
function buildSKUAttrSelectTable(result) {
    if (result.length < 1) {
        return "";
    }

    var tableHtml = "";
    var tempInputAttrValueId = 0;
    for (var i = 0; i < result.length; i++) {
        tableHtml += "<tr name='attrTr'><td width='80px;' align='right'>" + result[i].name + "：</td><td><p>";
        for (var j = 0; j < result[i].attrValueList.length; j++) {
            if (result[i].attrValueList[j].isInput == "0") {
                tableHtml += "<label><input type='checkbox' name='attrValue' attrId='" + result[i].attrId + "' attrValue='" + result[i].attrValueList[j].attrValue + "' onclick='selectSKUAttrValue()' value='" + result[i].attrValueList[j].attrValueId + "' />" + result[i].attrValueList[j].attrValue + "</label>" + "&nbsp;&nbsp;";
            }
            else {
                tempInputAttrValueId = result[i].attrValueList[j].attrValueId;
            }
        }
        tableHtml += "手动输入<input type='text' name='inputValue' attrId='" + result[i].attrId + "' attrValueId='" + tempInputAttrValueId + "' class='input' size='20' onblur='selectSKUAttrValue()' value=''/>";
        tableHtml += "<br /></p></td></tr>";
    }
    return tableHtml;
}

//选择sku属性值
function selectSKUAttrValue() {

    //初始化商品sku项列表
    productSKUItemList = new Array();

    var attrTrList = $("#skuAttributeTable tr[name=attrTr]");
    for (var i = 0; i < attrTrList.length; i++) {
        var items = new Array();

        //复选框
        var checkedAttrValueList = $(attrTrList[i]).find("input[name=attrValue]:checked");
        for (var j = 0; j < checkedAttrValueList.length; j++) {
            var checkedAttrValueObj = $(checkedAttrValueList[j]);
            var item = {
                'attrId': checkedAttrValueObj.attr("attrId"),
                'attrValueId': checkedAttrValueObj.val(),
                'attrValue': checkedAttrValueObj.attr("attrValue"),
                'inputValue': ''
            };
            items.push(item);
        }

        //手动输入
        var inputValueObj = $(attrTrList[i]).find("input[name=inputValue]");
        var inputValue = inputValueObj.val();
        if (inputValue != "") {
            var item = {
                'attrId': inputValueObj.attr("attrId"),
                'attrValueId': inputValueObj.attr("attrValueId"),
                'attrValue': inputValue,
                'inputValue': inputValue
            };
            items.push(item);
        }

        if (items.length > 0)
            productSKUItemList.push(items);
    }

    //计算sku的总数
    var skuCount = 0;
    if (productSKUItemList.length > 0) {
        skuCount = 1;
        for (var i = 0; i < productSKUItemList.length; i++) {
            skuCount = skuCount * productSKUItemList[i].length;
        }
    }

    //构建sku列表
    var skuListHtml = "";
    for (var i = 1; i <= skuCount; i++) {
        skuListHtml += "<tr name='skuTr'><td width='76px' align='right'>SKU：</td><td><table style='border-bottom:1px solid #d1d1d1;'><tr>";
        for (var j = 0; j < productSKUItemList.length; j++) {
            var index = 1;

            if (j == 0) {
                index = modFoat(i / (skuCount / productSKUItemList[0].length)) - 1;
            }
            else if (j == productSKUItemList.length - 1) {
                index = i % productSKUItemList[j].length;
                if (index == 0) {
                    index = productSKUItemList[j].length - 1;
                }
                else {
                    index = index - 1;
                }
            }
            else {
                var itemRepeatCount = 1;
                for (var x = j; x < productSKUItemList.length; x++) {
                    itemRepeatCount = itemRepeatCount * productSKUItemList[x].length;
                }
                var itemGroupCount = i % itemRepeatCount;
                if (itemGroupCount == 0) {
                    index = productSKUItemList[j].length - 1;
                }
                else {
                    var lineCount = itemRepeatCount / productSKUItemList[j].length;
                    index = modFoat(itemGroupCount / lineCount) - 1;
                }
            }

            skuListHtml += "<td width='88px'>" + productSKUItemList[j][index].attrValue + "<input type='hidden' name='AttrIdList' value='" + productSKUItemList[j][index].attrId + "'/><input type='hidden' name='AttrValueIdList' value='" + productSKUItemList[j][index].attrValueId + "'/><input type='hidden' name='AttrInputValueList' value='" + productSKUItemList[j][index].inputValue + "'/></td>";
        }
        skuListHtml += "</tr></table></td></tr>";
    }
    $("#skuAttributeTable tr[name=skuTr]").remove();
    $("#addSkuBut").before(skuListHtml);
}

//重写分类弹出层中的setSelectedCategory方法
function setSelectedCategory(selectedCateId, selectedCateName) {
    $(openCategorySelectLayerBut).parent().find(".CateId").val(selectedCateId);
    $(openCategorySelectLayerBut).val(selectedCateName).parent().find(".CategoryName").val(selectedCateName);
    $.jBox.close('categorySelectLayer');

    if (productPageType == 0 || productPageType == 3) {
        $("#commonAttributeTable,#skuAttributeTable").find("tr:not('.keepTr')").remove();

        $.get("/admin/category/aandvjsonlist?cateId=" + selectedCateId + "&time=" + new Date(), function (data) {
            var result = eval("(" + data + ")");

            if (productPageType == 0) {//添加商品页面时
                $("#commonAttributeTable").prepend(buildCommonAttrSelectTable(result));
            }
            else if (productPageType == 3) {//添加sku页面时
                $("#addSkuBut").before(buildSKUAttrSelectTable(result));
            }
        });
    }
}

$(function () {

    //提交按钮
    $(".submit").click(function () {
        $("form:first").submit();
        return false;
    })

    //选项卡
    $(".addTag li").click(function () {
        $(".addTag li").removeClass("hot");
        $(this).addClass("hot");
        $(".addTable").hide().eq($(this).index()).show(0);

    })

    //当页面不是列表且选择了分类时
    var cateId = parseInt($("#CateId").val());
    if (productPageType != 2 && cateId > 0) {

        //当页面为添加商品页面或编辑商品页面时
        if (productPageType == 0 || productPageType == 1) {

            //设置商品属性
            $("#commonAttributeTable").find("tr:not('.keepTr')").remove();
            $.get("/admin/category/aandvjsonlist?cateId=" + cateId + "&time=" + new Date(), function (data) {

                var result = eval("(" + data + ")");
                $("#commonAttributeTable").prepend(buildCommonAttrSelectTable(result));
                //选中对应属性
                if (productAttributeList != undefined && productAttributeList != null) {
                    for (var i = 0; i < productAttributeList.length; i++) {
                        $("#commonAttributeTable").find("input[name=AttrIdList][value=" + productAttributeList[i].attrId + "]").attr("checked", true);
                        $("#commonAttributeTable").find("input[name=AttrValueIdList][value=" + productAttributeList[i].attrValueId + "]").attr("checked", true);
                        if (productAttributeList[i].inputValue != "") {
                            $("#commonAttributeTable").find("input[name=AttrInputValueList][attrId=" + productAttributeList[i].attrId + "]").show().val(productAttributeList[i].inputValue);
                        }
                    }
                }

                var oldInputAttrValue = "";
                $(".inputattrvalue").live("focus", function () {
                    oldInputAttrValue = $(this).val();
                });

                $(".inputattrvalue").live("blur", function () {
                    var inputAttrValueObj = $(this);
                    if (inputAttrValueObj.val() != oldInputAttrValue) {
                        $.jBox.tip("正在更新...", 'loading');
                        $.get("/admin/product/updateproductattribute?type=1" + "&pid=" + pid + "&attrId=" + inputAttrValueObj.parent().find("input[name=AttrIdList]").val() + "&attrValueId=" + inputAttrValueObj.attr("attrValueId") + "&inputValue=" + encodeURIComponent(inputAttrValueObj.val()) + "&t=" + new Date(), function (data, textStatus) {
                            if (data != "0") {
                                $.jBox.tip('更新成功！', 'success');
                            } else {
                                inputAttrValueObj.val(oldInputAttrValue);
                                $.jBox.error('更新失败，请联系管理员！', '更新失败');
                            }
                        });
                    }
                });

            });
        }
        else if (productPageType == 3) {//当页面为添加sku页面时

            $("#skuAttributeTable").find("tr:not('.keepTr')").remove();
            $.get("/admin/category/aandvjsonlist?cateId=" + cateId + "&time=" + new Date(), function (data) {

                var result = eval("(" + data + ")");
                $("#addSkuBut").before(buildSKUAttrSelectTable(result));

                //选中对应属性
                if (productSKUItemList != undefined && productSKUItemList != null && productSKUItemList.length > 0) {
                    for (var i = 0; i < productSKUItemList.length; i++) {
                        $("#skuAttributeTable").find("input[name=attrValue][type=checkbox][value=" + productSKUItemList[i].attrValueId + "]").attr("checked", true);
                        $("#skuAttributeTable").find("input[name=inputValue][attrId=" + productSKUItemList[i].attrId + "]").val(productSKUItemList[i].inputValue);
                    }

                    selectSKUAttrValue();
                }
            })

        }

    }
    else if (productPageType == 2) {//当页面为列表时
        //本店售价
        var oldShopPriceInputValue = 0;
        $(".shoppriceinput").focus(function () {
            var shopPriceInputObj = $(this);
            oldShopPriceInputValue = shopPriceInputObj.val();
            shopPriceInputObj.val("");
            shopPriceInputObj.attr("class", "selectedsortinput");
        });
        $(".shoppriceinput").blur(function () {
            var shopPriceInputObj = $(this);
            if (shopPriceInputObj.val() == "") {
                shopPriceInputObj.val(oldShopPriceInputValue)
            }
            else {
                var reg = /^\d+(\.\d{1,2})?$/;
                if (!reg.test(shopPriceInputObj.val())) {
                    shopPriceInputObj.val(oldShopPriceInputValue).attr("class", "selectedsortinput");
                    alert("只能输入正数且最多两位小数！")
                    return;
                }
                else {
                    if (oldShopPriceInputValue != shopPriceInputObj.val()) {
                        $.jBox.tip("正在更新...", 'loading');
                        $.get(shopPriceInputObj.attr("url") + "&shopPrice=" + shopPriceInputObj.val() + "&t=" + new Date(), function (data, textStatus) {
                            if (data != "0") {
                                $.jBox.tip('更新成功！', 'success');
                            } else {
                                shopPriceInputObj.val(oldShopPriceInputValue);
                                $.jBox.error('更新失败，请联系管理员！', '更新失败');
                            }
                        });
                    }
                }
            }
            shopPriceInputObj.attr("class", "unselectedsortinput");
        });

        //库存
        var oldStockInputValue = 0;
        $(".stockinput").focus(function () {
            var stockInputObj = $(this);
            oldStockInputValue = stockInputObj.val();
            stockInputObj.val("");
            stockInputObj.attr("class", "selectedsortinput");
        });
        $(".stockinput").blur(function () {
            var stockInputObj = $(this);
            if (stockInputObj.val() == "") {
                stockInputObj.val(oldStockInputValue)
            }
            else {
                var reg = /^\d+$/;
                if (!reg.test(stockInputObj.val())) {
                    stockInputObj.val(oldStockInputValue).attr("class", "selectedsortinput");
                    alert("只能输入数字！")
                    return;
                }
                else {
                    if (oldStockInputValue != stockInputObj.val()) {
                        $.jBox.tip("正在更新...", 'loading');
                        $.get(stockInputObj.attr("url") + "&stockNumber=" + stockInputObj.val() + "&t=" + new Date(), function (data, textStatus) {
                            if (data != "0") {
                                $.jBox.tip('更新成功！', 'success');
                            } else {
                                stockInputObj.val(oldStockInputValue);
                                $.jBox.error('更新失败，请联系管理员！', '更新失败');
                            }
                        });
                    }
                }
            }
            stockInputObj.attr("class", "unselectedsortinput");
        });

        /*精品*/
        $(".isbestspan").click(function () {
            var isBestSpanObj = $(this);
            var isBest = isBestSpanObj.attr("isbest");
            var alertMessage = "";
            var isBestUrl = "";
            if (isBest == "0") {
                alertMessage = "您确认要设置此商品为精品吗？";
                isBestUrl = isBestSpanObj.attr("url") + "&state=1" + "&t=" + new Date();
            }
            else if (isBest == "1") {
                alertMessage = "您确认要取消此商品为精品吗？";
                isBestUrl = isBestSpanObj.attr("url") + "&state=0" + "&t=" + new Date();
            }
            $.jBox.confirm(alertMessage, "提示", function (v, h, f) {
                if (v == 'ok') {
                    $.jBox.tip("正在设置...", 'loading');
                    $.get(isBestUrl, function (data, textStatus) {
                        if (data != "0") {
                            if (isBest == "0") {
                                isBestSpanObj.html("是").attr("isbest", "1");
                            }
                            else if (isBest == "1") {
                                isBestSpanObj.html("否").attr("isbest", "0");
                            }
                            $.jBox.tip('设置成功！', 'success');
                        } else {
                            $.jBox.error('设置失败，请联系管理员！', '设置失败');
                        }
                    });
                }
                else if (v == 'cancel') {
                    // 取消
                }

                return true; //close
            });
        });

        /*热销*/
        $(".ishotspan").click(function () {
            var isHotSpanObj = $(this);
            var isHot = isHotSpanObj.attr("ishot");
            var alertMessage = "";
            var isHotUrl = "";
            if (isHot == "0") {
                alertMessage = "您确认要设置此商品为热销吗？";
                isHotUrl = isHotSpanObj.attr("url") + "&state=1" + "&t=" + new Date();
            }
            else if (isHot == "1") {
                alertMessage = "您确认要取消此商品为热销吗？";
                isHotUrl = isHotSpanObj.attr("url") + "&state=0" + "&t=" + new Date();
            }
            $.jBox.confirm(alertMessage, "提示", function (v, h, f) {
                if (v == 'ok') {
                    $.jBox.tip("正在设置...", 'loading');
                    $.get(isHotUrl, function (data, textStatus) {
                        if (data != "0") {
                            if (isHot == "0") {
                                isHotSpanObj.html("是").attr("ishot", "1");
                            }
                            else if (isHot == "1") {
                                isHotSpanObj.html("否").attr("ishot", "0");
                            }
                            $.jBox.tip('设置成功！', 'success');
                        } else {
                            $.jBox.error('设置失败，请联系管理员！', '设置失败');
                        }
                    });
                }
                else if (v == 'cancel') {
                    // 取消
                }

                return true; //close
            });
        });

        /*新品*/
        $(".isnewspan").click(function () {
            var isNewSpanObj = $(this);
            var isNew = isNewSpanObj.attr("isnew");
            var alertMessage = "";
            var isNewUrl = "";
            if (isNew == "0") {
                alertMessage = "您确认要设置此商品为新品吗？";
                isNewUrl = isNewSpanObj.attr("url") + "&state=1" + "&t=" + new Date();
            }
            else if (isNew == "1") {
                alertMessage = "您确认要取消此商品为新品吗？";
                isNewUrl = isNewSpanObj.attr("url") + "&state=0" + "&t=" + new Date();
            }
            $.jBox.confirm(alertMessage, "提示", function (v, h, f) {
                if (v == 'ok') {
                    $.jBox.tip("正在设置...", 'loading');
                    $.get(isNewUrl, function (data, textStatus) {
                        if (data != "0") {
                            if (isNew == "0") {
                                isNewSpanObj.html("是").attr("isnew", "1");
                            }
                            else if (isNew == "1") {
                                isNewSpanObj.html("否").attr("isnew", "0");
                            }
                            $.jBox.tip('设置成功！', 'success');
                        } else {
                            $.jBox.error('设置失败，请联系管理员！', '设置失败');
                        }
                    });
                }
                else if (v == 'cancel') {
                    // 取消
                }

                return true; //close
            });
        });

        /*下架*/
        $(".outsaletag").click(function () {
            var outSaleTagObj = $(this);
            var outSaleUrl = outSaleTagObj.attr("url") + "&t=" + new Date();
            $.jBox.confirm("您确认要下架此商品吗？", "提示", function (v, h, f) {
                if (v == 'ok') {
                    $.jBox.tip("正在设置...", 'loading');
                    $.get(outSaleUrl, function (data, textStatus) {
                        if (data != "0") {
                            outSaleTagObj.parents("tr").remove();
                            $.jBox.tip('下架成功！', 'success');
                        } else {
                            $.jBox.error('下架失败，请联系管理员！', '下架失败');
                        }
                    });
                }
                else if (v == 'cancel') {
                    // 取消
                }

                return true; //close
            });

            return false;
        });

        /*上架*/
        $(".onsaletag").click(function () {
            var onSaleTagObj = $(this);
            var onSaleUrl = onSaleTagObj.attr("url") + "&t=" + new Date();
            $.jBox.confirm("您确认要上架此商品吗？", "提示", function (v, h, f) {
                if (v == 'ok') {
                    $.jBox.tip("正在上架...", 'loading');
                    $.get(onSaleUrl, function (data, textStatus) {
                        if (data != "0") {
                            onSaleTagObj.parents("tr").remove();
                            $.jBox.tip('上架成功！', 'success');
                        } else {
                            $.jBox.error('上架失败，请联系管理员！', '上架失败');
                        }
                    });
                }
                else if (v == 'cancel') {
                    // 取消
                }

                return true; //close
            });

            return false;
        });

        //批量下架
        $(".batchOutSale").click(function () {
            if ($("input[type='checkbox'][selectItem='true']:checked").length > 0) {
                if (confirm(batchOutSaleMessage)) {
                    doPostBack($(this).attr("outSaleUrl"));
                }
            }
            else {
                alert("没有选中任何一项!");
            }
        })

        //批量上架
        $(".batchOnSale").click(function () {
            if ($("input[type='checkbox'][selectItem='true']:checked").length > 0) {
                if (confirm(batchOnSaleMessage)) {
                    doPostBack($(this).attr("onSaleUrl"));
                }
            }
            else {
                alert("没有选中任何一项!");
            }
        })

        //批量回收
        $(".batchRecycle").click(function () {
            if ($("input[type='checkbox'][selectItem='true']:checked").length > 0) {
                if (confirm(batchRecycleMessage)) {
                    doPostBack($(this).attr("recycleUrl"));
                }
            }
            else {
                alert("没有选中任何一项!");
            }
        })

        //批量恢复
        $(".batchRestore").click(function () {
            if ($("input[type='checkbox'][selectItem='true']:checked").length > 0) {
                if (confirm(batchRestoreMessage)) {
                    doPostBack($(this).attr("restoreUrl"));
                }
            }
            else {
                alert("没有选中任何一项!");
            }
        })

    }
});
