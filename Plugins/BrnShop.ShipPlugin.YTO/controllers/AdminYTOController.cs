using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.ShipPlugin.YTO;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台圆通快递控制器类
    /// </summary>
    public class AdminYTOController : BaseAdminController
    {
        /// <summary>
        /// 配送规则列表
        /// </summary>
        [ChildActionOnly]
        public ActionResult ShipRuleList()
        {
            ShipRuleListModel model = new ShipRuleListModel();
            model.ShipRuleList = PluginUtils.GetShipRuleList();
            return View("~/plugins/BrnShop.ShipPlugin.YTO/views/adminyto/shiprulelist.cshtml", model);
        }

        /// <summary>
        /// 添加配送规则
        /// </summary>
        [HttpGet]
        [ChildActionOnly]
        public ActionResult AddShipRule()
        {
            ShipRuleModel model = new ShipRuleModel();
            Load(-1, -1, -1);
            return View("~/plugins/BrnShop.ShipPlugin.YTO/views/adminyto/addshiprule.cshtml", model);
        }

        /// <summary>
        /// 添加配送规则
        /// </summary>
        [HttpPost]
        public ActionResult AddShipRule(ShipRuleModel model)
        {
            if (ModelState.IsValid)
            {
                ShipRuleInfo shipRuleInfo = new ShipRuleInfo();
                shipRuleInfo.Name = model.Name;
                shipRuleInfo.Type = model.Type;
                shipRuleInfo.ExtCode1 = model.ExtCode1;
                if (model.Type == 0)
                    shipRuleInfo.ExtCode2 = model.ExtCode2;
                shipRuleInfo.FreeMoney = model.FreeMoney;
                shipRuleInfo.CODPayFee = model.CODPayFee;

                int regionId = 0;
                if (model.ProvinceId > 0)
                    regionId = model.ProvinceId;
                if (model.CityId > 0)
                    regionId = model.CityId;
                if (model.CountyId > 0)
                    regionId = model.CountyId;
                RegionInfo regionInfo = Regions.GetRegionById(regionId);
                if (regionInfo == null)
                {
                    shipRuleInfo.RegionId = 0;
                    shipRuleInfo.RegionTitle = "全国";
                }
                else
                {
                    if (regionInfo.Layer == 1)
                    {
                        shipRuleInfo.RegionId = regionId;
                        shipRuleInfo.RegionTitle = regionInfo.Name;
                    }
                    else if (regionInfo.Layer == 2)
                    {
                        shipRuleInfo.RegionId = regionId;
                        shipRuleInfo.RegionTitle = regionInfo.ProvinceName + regionInfo.Name;
                    }
                    else if (regionInfo.Layer == 3)
                    {
                        shipRuleInfo.RegionId = regionId;
                        shipRuleInfo.RegionTitle = regionInfo.ProvinceName + regionInfo.CityName + regionInfo.Name;
                    }
                }

                List<ShipRuleInfo> shipRuleList = PluginUtils.GetShipRuleList();
                shipRuleList.Add(shipRuleInfo);
                PluginUtils.SaveShipRuleList(shipRuleList);

                AddAdminOperateLog("添加圆通快递配送规则");
                return PromptView(Url.Action("config", "plugin", new { configController = "AdminYTO", configAction = "ShipRuleList" }), "配送规则添加成功");
            }

            return PromptView(Url.Action("config", "plugin", new { configController = "AdminYTO", configAction = "AddShipRule" }), "内容有误，请重写填写");
        }

        /// <summary>
        /// 编辑配送规则
        /// </summary>
        [HttpGet]
        [ChildActionOnly]
        public ActionResult EditShipRule(string shipRuleName = "")
        {
            ShipRuleInfo shipRuleInfo = PluginUtils.GetShipRuleList().Find(x => x.Name == shipRuleName);
            if (shipRuleInfo == null)
                return PromptView(Url.Action("config", "plugin", new { configController = "AdminYTO", configAction = "ShipRuleList" }), "配送规则不存在");

            ShipRuleModel model = new ShipRuleModel();
            model.Name = shipRuleInfo.Name;

            RegionInfo regionInfo = Regions.GetRegionById(shipRuleInfo.RegionId);
            if (regionInfo == null)
            {
                model.ProvinceId = -1;
                model.CityId = -1;
                model.CountyId = -1;
            }
            else
            {
                if (regionInfo.Layer == 1)
                {
                    model.ProvinceId = regionInfo.ProvinceId;
                    model.CityId = -1;
                    model.CountyId = -1;
                }
                else if (regionInfo.Layer == 2)
                {
                    model.ProvinceId = regionInfo.ProvinceId;
                    model.CityId = regionInfo.CityId;
                    model.CountyId = -1;
                }
                else if (regionInfo.Layer == 3)
                {
                    model.ProvinceId = regionInfo.ProvinceId;
                    model.CityId = regionInfo.CityId;
                    model.CountyId = regionInfo.RegionId;
                }
            }

            model.Type = shipRuleInfo.Type;
            model.ExtCode1 = shipRuleInfo.ExtCode1;
            model.ExtCode2 = shipRuleInfo.ExtCode2;
            model.FreeMoney = shipRuleInfo.FreeMoney;
            model.CODPayFee = shipRuleInfo.CODPayFee;

            Load(model.ProvinceId, model.CityId, model.CountyId);

            return View("~/plugins/BrnShop.ShipPlugin.YTO/views/adminyto/editshiprule.cshtml", model);
        }

        /// <summary>
        /// 编辑配送规则
        /// </summary>
        [HttpPost]
        public ActionResult EditShipRule(ShipRuleModel model)
        {
            ShipRuleInfo shipRuleInfo = PluginUtils.GetShipRuleList().Find(x => x.Name == model.Name);
            if (shipRuleInfo == null)
                return PromptView(Url.Action("config", "plugin", new { configController = "AdminYTO", configAction = "ShipRuleList" }), "配送规则不存在");

            if (ModelState.IsValid)
            {
                shipRuleInfo.Type = model.Type;
                shipRuleInfo.ExtCode1 = model.ExtCode1;
                if (model.Type == 0)
                    shipRuleInfo.ExtCode2 = model.ExtCode2;
                else
                    shipRuleInfo.ExtCode2 = 0;
                shipRuleInfo.FreeMoney = model.FreeMoney;
                shipRuleInfo.CODPayFee = model.CODPayFee;

                int regionId = 0;
                if (model.ProvinceId > 0)
                    regionId = model.ProvinceId;
                if (model.CityId > 0)
                    regionId = model.CityId;
                if (model.CountyId > 0)
                    regionId = model.CountyId;
                RegionInfo regionInfo = Regions.GetRegionById(regionId);
                if (regionInfo == null)
                {
                    shipRuleInfo.RegionId = 0;
                    shipRuleInfo.RegionTitle = "全国";
                }
                else
                {
                    if (regionInfo.Layer == 1)
                    {
                        shipRuleInfo.RegionId = regionId;
                        shipRuleInfo.RegionTitle = regionInfo.Name;
                    }
                    else if (regionInfo.Layer == 2)
                    {
                        shipRuleInfo.RegionId = regionId;
                        shipRuleInfo.RegionTitle = regionInfo.ProvinceName + regionInfo.Name;
                    }
                    else if (regionInfo.Layer == 3)
                    {
                        shipRuleInfo.RegionId = regionId;
                        shipRuleInfo.RegionTitle = regionInfo.ProvinceName + regionInfo.CityName + regionInfo.Name;
                    }
                }

                List<ShipRuleInfo> shipRuleList = PluginUtils.GetShipRuleList();
                PluginUtils.SaveShipRuleList(shipRuleList);

                AddAdminOperateLog("编辑圆通快递配送规则");
                return PromptView(Url.Action("config", "plugin", new { configController = "AdminYTO", configAction = "ShipRuleList" }), "配送规则编辑成功");
            }

            return PromptView(Url.Action("config", "plugin", new { configController = "AdminYTO", configAction = "EditShipRule", shipRuleName = shipRuleInfo.Name }), "内容有误，请重写填写");
        }

        private void Load(int provinceId, int cityId, int countyId)
        {
            ViewData["provinceId"] = provinceId;
            ViewData["cityId"] = cityId;
            ViewData["countyId"] = countyId;
        }

        /// <summary>
        /// 删除配送规则
        /// </summary>
        public ActionResult DelShipRule(string shipRuleName = "")
        {
            List<ShipRuleInfo> shipRuleList = PluginUtils.GetShipRuleList();
            foreach (ShipRuleInfo shipRuleInfo in shipRuleList)
            {
                if (shipRuleInfo.Name == shipRuleName)
                {
                    shipRuleList.Remove(shipRuleInfo);
                    break;
                }
            }

            PluginUtils.SaveShipRuleList(shipRuleList);
            AddAdminOperateLog("删除圆通快递配送规则");
            return PromptView(Url.Action("config", "plugin", new { configController = "AdminYTO", configAction = "ShipRuleList" }), "配送规则删除成功");
        }
    }
}
