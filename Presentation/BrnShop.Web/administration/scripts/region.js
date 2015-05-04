var provinceId = -1; //省id
var cityId = -1; //市id
var countyId = -1; //县或区id

//绑定省列表
function bindProvinceList(provinceSelectObj, selectProvinceId) {
    $.get("/admin/tool/provincelist", function (data) {
        var provinceList = eval("(" + data + ")");
        if (provinceList.length > 0) {
            var optionStr = "<option value='-1'>请选择</option>";
            for (var i = 0; i < provinceList.length; i++) {
                optionStr = optionStr + "<option value='" + provinceList[i].id + "'>" + provinceList[i].name + "</option>";
            }
            provinceSelectObj.html(optionStr);
            if (selectProvinceId == undefined)
                selectProvinceId = -1;
            provinceSelectObj.find("option[value=" + selectProvinceId + "]").attr("selected", true);
        }
        else {
            alert("加载省列表时出错！")
        }
    })
}

//绑定市列表
function bindCityList(provinceId, citySelectObj, selectCityId) {
    $.get("/admin/tool/citylist?provinceId=" + provinceId, function (data) {
        var cityList = eval("(" + data + ")");
        if (cityList.length > 0) {
            var optionStr = "<option value='-1'>请选择</option>";
            for (var i = 0; i < cityList.length; i++) {
                optionStr = optionStr + "<option value='" + cityList[i].id + "'>" + cityList[i].name + "</option>";
            }
            citySelectObj.html(optionStr);
            if (selectCityId == undefined)
                selectCityId = -1;
            citySelectObj.find("option[value=" + selectCityId + "]").attr("selected", true);
        }
        else {
            alert("加载市列表时出错！")
        }
    })
}

//绑定县或区列表
function bindCountyList(cityId, countySelectObj, selectCountyId) {
    $.get("/admin/tool/countylist?cityId=" + cityId, function (data) {
        var countyList = eval("(" + data + ")");
        if (countyList.length > 0) {
            var optionStr = "<option value='-1'>请选择</option>";
            for (var i = 0; i < countyList.length; i++) {
                optionStr = optionStr + "<option value='" + countyList[i].id + "'>" + countyList[i].name + "</option>";
            }
            countySelectObj.html(optionStr);
            if (selectCountyId == undefined)
                selectCountyId = -1;
            countySelectObj.find("option[value=" + selectCountyId + "]").attr("selected", true);
        }
        else {
            alert("加载县或区列表时出错！")
        }
    })
}

$(function () {
    //绑定省列表的改变事件
    $("#provinceSelect").change(function () {
        var selectedProvinceSelect = $(this);
        var selectedProvinceId = selectedProvinceSelect.find('option:selected').val();
        if (selectedProvinceId > 0) {
            selectedProvinceSelect.parent().find(".countySelect").html("<option value='-1'>请选择</option>")
            bindCityList(selectedProvinceId, selectedProvinceSelect.parent().find(".citySelect"));
        }
    })


    //绑定市列表的改变事件
    $("#citySelect").change(function () {
        var selectedCitySelect = $(this);
        var selectedCityId = selectedCitySelect.find('option:selected').val();
        if (selectedCityId > 0) {
            bindCountyList(selectedCityId, selectedCitySelect.parent().find(".countySelect"));
        }
    })

    //绑定省列表
    bindProvinceList($("#provinceSelect"), provinceId);

    if (cityId > 0) {
        bindCityList(provinceId, $("#provinceSelect").parent().find(".citySelect"), cityId);
        bindCountyList(cityId, $("#citySelect").parent().find(".countySelect"), countyId);
    }
})
