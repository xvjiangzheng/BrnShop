using System;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台工具控制器类
    /// </summary>
    public partial class ToolController : Controller
    {
        private string ip = "";//ip地址
        private ShopConfigInfo shopConfigInfo = BSPConfig.ShopConfig;//商城配置信息
        private PartUserInfo partUserInfo = null;//用户信息

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            ip = WebHelper.GetIP();
            //当用户ip不在允许的后台访问ip列表时
            if (!string.IsNullOrEmpty(shopConfigInfo.AdminAllowAccessIP) && !ValidateHelper.InIPList(ip, shopConfigInfo.AdminAllowAccessIP))
            {
                filterContext.Result = HttpNotFound();
                return;
            }
            //当用户IP被禁止时
            if (BannedIPs.CheckIP(ip))
            {
                filterContext.Result = HttpNotFound();
                return;
            }

            //获得用户id
            int uid = ShopUtils.GetUidCookie();
            if (uid < 1)
                uid = WebHelper.GetRequestInt("uid");
            if (uid < 1)//当用户为游客时
            {
                //创建游客
                partUserInfo = Users.CreatePartGuest();
            }
            else//当用户为会员时
            {
                //获得保存在cookie中的密码
                string encryptPwd = ShopUtils.GetCookiePassword();
                if (string.IsNullOrWhiteSpace(encryptPwd))
                    encryptPwd = WebHelper.GetRequestString("password");
                //防止用户密码被篡改为危险字符
                if (encryptPwd.Length == 0 || !SecureHelper.IsBase64String(encryptPwd))
                {
                    //创建游客
                    partUserInfo = Users.CreatePartGuest();
                    ShopUtils.SetUidCookie(-1);
                    ShopUtils.SetCookiePassword("");
                }
                else
                {
                    partUserInfo = Users.GetPartUserByUidAndPwd(uid, ShopUtils.DecryptCookiePassword(encryptPwd));
                    if (partUserInfo == null)
                    {
                        partUserInfo = Users.CreatePartGuest();
                        ShopUtils.SetUidCookie(-1);
                        ShopUtils.SetCookiePassword("");
                    }
                }
            }

            //当用户等级是禁止访问等级时
            if (partUserInfo.UserRid == 1)
            {
                filterContext.Result = HttpNotFound();
                return;
            }

            //如果当前用户没有登录
            if (partUserInfo.Uid < 1)
            {
                filterContext.Result = HttpNotFound();
                return;
            }

            //如果当前用户不是管理员
            if (partUserInfo.AdminGid == 1)
            {
                filterContext.Result = HttpNotFound();
                return;
            }
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <returns></returns>
        public ActionResult Upload()
        {
            string operation = WebHelper.GetQueryString("operation");

            if (operation == "ueconfig")
            {
                StringBuilder imageAllowFiles = new StringBuilder("[");
                foreach (string imgType in StringHelper.SplitString(shopConfigInfo.UploadImgType))
                {
                    imageAllowFiles.AppendFormat("\"{0}\",", imgType);
                }
                if (imageAllowFiles.Length > 1)
                    imageAllowFiles.Remove(imageAllowFiles.Length - 1, 1);
                imageAllowFiles.Append("]");

                string imageUrlPrefix = string.IsNullOrEmpty(shopConfigInfo.UploadServer) ? "/" : shopConfigInfo.UploadServer;

                return Content(string.Format("{0}\"imageActionName\": \"uploadimage\", \"imageFieldName\": \"upfile\", \"imageMaxSize\": {1},\"imageAllowFiles\": {2}, \"imageCompressEnable\": true, \"imageCompressBorder\": 1600, \"imageInsertAlign\": \"none\", \"imageUrlPrefix\": \"{3}\", \"imagePathFormat\": \"\", \"imageManagerActionName\": \"listimage\",\"imageManagerListPath\": \"upload/image\",\"imageManagerListSize\": 20, \"imageManagerUrlPrefix\": \"/ueditor/net/\",\"imageManagerInsertAlign\": \"none\", \"imageManagerAllowFiles\": [\".png\", \".jpg\", \".jpeg\", \".gif\", \".bmp\"]{4}", "{", shopConfigInfo.UploadImgSize, imageAllowFiles, imageUrlPrefix, "}"));
            }
            if (operation == "uploadproductimage")//上传商品图片
            {
                HttpPostedFileBase file = Request.Files[0];
                string result = ShopUtils.SaveUplaodProductImage(file);
                return Content(result);
            }
            if (operation == "uploadproducteditorimage")//上传商品编辑器中图片
            {
                HttpPostedFileBase file = Request.Files[0];
                string result = ShopUtils.SaveProductEditorImage(file);
                return Content(string.Format("{2}'url':'upload/product/editor/{0}','state':'{1}'{3}", result, GetUEState(result), "{", "}"));
            }
            if (operation == "uploadadvertbody")//上传广告主体
            {
                HttpPostedFileBase file = Request.Files[0];
                string result = ShopUtils.SaveUploadAdvertBody(file);
                return Content(result);
            }
            if (operation == "uploadbannerimg")//上传banner图片
            {
                HttpPostedFileBase file = Request.Files[0];
                string result = ShopUtils.SaveUploadBannerImg(file);
                return Content(result);
            }
            if (operation == "uploadnewseditorimage")//上传新闻编辑器中的图片
            {
                HttpPostedFileBase file = Request.Files[0];
                string result = ShopUtils.SaveNewsEditorImage(file);
                return Content(string.Format("{2}'url':'upload/news/{0}','state':'{1}'{3}", result, GetUEState(result), "{", "}"));
            }
            if (operation == "uploadbrandlogo")//上传品牌logo
            {
                HttpPostedFileBase file = Request.Files[0];
                string result = ShopUtils.SaveUploadBrandLogo(file);
                return Content(result);
            }
            if (operation == "uploadhelpeditorimage")//上传帮助编辑器中的图片
            {
                HttpPostedFileBase file = Request.Files[0];
                string result = ShopUtils.SaveHelpEditorImage(file);
                return Content(string.Format("{2}'url':'upload/help/{0}','state':'{1}'{3}", result, GetUEState(result), "{", "}"));
            }
            if (operation == "uploadfriendlinklogo")//上传友情链接logo
            {
                HttpPostedFileBase file = Request.Files[0];
                string result = ShopUtils.SaveUploadFriendLinkLogo(file);
                return Content(result);
            }
            if (operation == "uploaduserrankavatar")//上传用户等级头像
            {
                HttpPostedFileBase file = Request.Files[0];
                string result = ShopUtils.SaveUploadUserRankAvatar(file);
                return Content(result);
            }
            return HttpNotFound();
        }

        /// <summary>
        /// 省列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ProvinceList()
        {
            List<RegionInfo> regionList = Regions.GetProvinceList();

            StringBuilder sb = new StringBuilder();

            sb.Append("[");

            foreach (RegionInfo info in regionList)
            {
                sb.AppendFormat("{0}\"id\":\"{1}\",\"name\":\"{2}\"{3},", "{", info.RegionId, info.Name, "}");
            }

            if (regionList.Count > 0)
                sb.Remove(sb.Length - 1, 1);

            sb.Append("]");

            return Content(sb.ToString());
        }

        /// <summary>
        /// 市列表
        /// </summary>
        /// <param name="provinceId">省id</param>
        /// <returns></returns>
        public ActionResult CityList(int provinceId = -1)
        {
            List<RegionInfo> regionList = Regions.GetCityList(provinceId);

            StringBuilder sb = new StringBuilder();

            sb.Append("[");

            foreach (RegionInfo info in regionList)
            {
                sb.AppendFormat("{0}\"id\":\"{1}\",\"name\":\"{2}\"{3},", "{", info.RegionId, info.Name, "}");
            }

            if (regionList.Count > 0)
                sb.Remove(sb.Length - 1, 1);

            sb.Append("]");

            return Content(sb.ToString());
        }

        /// <summary>
        /// 县或区列表
        /// </summary>
        /// <param name="cityId">市id</param>
        /// <returns></returns>
        public ActionResult CountyList(int cityId = -1)
        {
            List<RegionInfo> regionList = Regions.GetCountyList(cityId);

            StringBuilder sb = new StringBuilder();

            sb.Append("[");

            foreach (RegionInfo info in regionList)
            {
                sb.AppendFormat("{0}\"id\":\"{1}\",\"name\":\"{2}\"{3},", "{", info.RegionId, info.Name, "}");
            }

            if (regionList.Count > 0)
                sb.Remove(sb.Length - 1, 1);

            sb.Append("]");

            return Content(sb.ToString());
        }

        /// <summary>
        /// 获得ueditor状态
        /// </summary>
        /// <param name="result">上传结果</param>
        /// <returns></returns>
        private string GetUEState(string result)
        {
            if (result == "-1")
            {
                return "上传图片不能为空";
            }
            else if (result == "-2")
            {
                return "不允许的图片类型";
            }
            else if (result == "-3")
            {
                return "图片大小超出网站限制";
            }
            else
            {
                return "SUCCESS";
            }
        }

    }
}
