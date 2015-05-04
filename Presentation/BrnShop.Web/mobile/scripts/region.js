var provinceId = -1; //省id
var cityId = -1; //市id
var countyId = -1; //县或区id

//绑定省列表
function bindProvinceList(provinceSelectObj, selectProvinceId) {
    Ajax.get("/mob/tool/provincelist", true, function (data) {
        var provinceList = eval("(" + data + ")");
        if (provinceList.content.length > 0) {
            removeAllOptions(provinceSelectObj);
            provinceSelectObj.options[0] = new Option("请选择", "-1");
            for (var i = 0; i < provinceList.content.length; i++) {
                provinceSelectObj.options[i + 1] = new Option(provinceList.content[i].name, provinceList.content[i].id);
            }
            if (selectProvinceId == undefined)
                selectProvinceId = -1;
            setSelectedOptions(provinceSelectObj, selectProvinceId);
        }
        else {
            alert("加载省列表时出错！")
        }
    })
}

//绑定市列表
function bindCityList(provinceId, citySelectObj, selectCityId) {
    Ajax.get("/mob/tool/citylist?provinceId=" + provinceId, true, function (data) {
        var cityList = eval("(" + data + ")");
        if (cityList.content.length > 0) {
            removeAllOptions(citySelectObj);
            citySelectObj.options[0] = new Option("请选择", "-1");
            for (var i = 0; i < cityList.content.length; i++) {
                citySelectObj.options[i + 1] = new Option(cityList.content[i].name, cityList.content[i].id);
            }
            if (selectCityId == undefined)
                selectCityId = -1;
            setSelectedOptions(citySelectObj, selectCityId);
        }
        else {
            alert("加载市列表时出错！")
        }
    })
}

//绑定县或区列表
function bindCountyList(cityId, countySelectObj, selectCountyId) {
    Ajax.get("/mob/tool/countylist?cityId=" + cityId, true, function (data) {
        var countyList = eval("(" + data + ")");
        if (countyList.content.length > 0) {
            removeAllOptions(countySelectObj);
            countySelectObj.options[0] = new Option("请选择", "-1");
            for (var i = 0; i < countyList.content.length; i++) {
                countySelectObj.options[i + 1] = new Option(countyList.content[i].name, countyList.content[i].id);
            }
            if (selectCountyId == undefined)
                selectCountyId = -1;
            setSelectedOptions(countySelectObj, selectCountyId);
        }
        else {
            alert("加载县或区列表时出错！")
        }
    })
}
