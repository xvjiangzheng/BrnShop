using System;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台用户控制器类
    /// </summary>
    public partial class UserController : BaseAdminController
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        public ActionResult List(string userName, string email, string mobile, int userRid = 0, int adminGid = 0,
                                 int pageNumber = 1, int pageSize = 15)
        {
            string condition = AdminUsers.AdminGetUserListCondition(userName, email, mobile, userRid, adminGid);
            string sort = AdminUsers.AdminGetUserListSort("", "");

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminUsers.AdminGetUserCount(condition));

            UserListModel model = new UserListModel()
            {
                UserList = AdminUsers.AdminGetUserList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel,
                UserName = userName,
                Email = email,
                Mobile = mobile,
                UserRid = userRid,
                AdminGid = adminGid
            };
            List<SelectListItem> userRankList = new List<SelectListItem>();
            userRankList.Add(new SelectListItem() { Text = "全部等级", Value = "0" });
            foreach (UserRankInfo info in AdminUserRanks.GetUserRankList())
            {
                userRankList.Add(new SelectListItem() { Text = info.Title, Value = info.UserRid.ToString() });
            }
            ViewData["userRankList"] = userRankList;

            List<SelectListItem> adminGroupList = new List<SelectListItem>();
            adminGroupList.Add(new SelectListItem() { Text = "全部组", Value = "0" });
            foreach (AdminGroupInfo info in AdminGroups.GetAdminGroupList())
            {
                adminGroupList.Add(new SelectListItem() { Text = info.Title, Value = info.AdminGid.ToString() });
            }
            ViewData["adminGroupList"] = adminGroupList;

            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&userName={3}&email={4}&mobile={5}&userRid={6}&adminGid={7}",
                                                          Url.Action("list"), pageModel.PageNumber, pageModel.PageSize,
                                                          userName, email, mobile, userRid, adminGid));
            return View(model);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        [HttpGet]
        public ActionResult Add()
        {
            UserModel model = new UserModel();
            Load(model.RegionId);
            return View(model);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        [HttpPost]
        public ActionResult Add(UserModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password))
                ModelState.AddModelError("Password", "密码不能为空");

            if (AdminUsers.IsExistUserName(model.UserName))
                ModelState.AddModelError("UserName", "名称已经存在");

            if (AdminUsers.IsExistEmail(model.Email))
                ModelState.AddModelError("Email", "email已经存在");

            if (AdminUsers.IsExistMobile(model.Mobile))
                ModelState.AddModelError("Mobile", "手机号已经存在");

            if (ModelState.IsValid)
            {
                string salt = Users.GenerateUserSalt();
                string nickName;
                if (string.IsNullOrWhiteSpace(model.NickName))
                    nickName = "bsp" + Randoms.CreateRandomValue(7);
                else
                    nickName = model.NickName;

                UserInfo userInfo = new UserInfo()
                {
                    UserName = model.UserName,
                    Email = model.Email == null ? "" : model.Email,
                    Mobile = model.Mobile == null ? "" : model.Mobile,
                    Salt = salt,
                    Password = Users.CreateUserPassword(model.Password, salt),
                    UserRid = model.UserRid,
                    AdminGid = model.AdminGid,
                    NickName = WebHelper.HtmlEncode(nickName),
                    Avatar = model.Avatar == null ? "" : WebHelper.HtmlEncode(model.Avatar),
                    PayCredits = model.PayCredits,
                    RankCredits = AdminUserRanks.GetUserRankById(model.UserRid).CreditsLower,
                    VerifyEmail = 1,
                    VerifyMobile = 1,
                    LiftBanTime = UserRanks.IsBanUserRank(model.UserRid) ? DateTime.Now.AddDays(WorkContext.UserRankInfo.LimitDays) : new DateTime(1900, 1, 1),
                    LastVisitTime = DateTime.Now,
                    LastVisitIP = WorkContext.IP,
                    LastVisitRgId = WorkContext.RegionId,
                    RegisterTime = DateTime.Now,
                    RegisterIP = WorkContext.IP,
                    RegisterRgId = WorkContext.RegionId,
                    Gender = model.Gender,
                    RealName = model.RealName == null ? "" : WebHelper.HtmlEncode(model.RealName),
                    Bday = model.Bday ?? new DateTime(1970, 1, 1),
                    IdCard = model.IdCard == null ? "" : model.IdCard,
                    RegionId = model.RegionId,
                    Address = model.Address == null ? "" : WebHelper.HtmlEncode(model.Address),
                    Bio = model.Bio == null ? "" : WebHelper.HtmlEncode(model.Bio)
                };

                AdminUsers.CreateUser(userInfo);
                AddAdminOperateLog("添加用户", "添加用户,用户为:" + model.UserName);
                return PromptView("用户添加成功");
            }
            Load(model.RegionId);

            return View(model);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int uid = -1)
        {
            UserInfo userInfo = AdminUsers.GetUserById(uid);
            if (userInfo == null)
                return PromptView("用户不存在");

            UserModel model = new UserModel();
            model.UserName = userInfo.UserName;
            model.Email = userInfo.Email;
            model.Mobile = userInfo.Mobile;
            model.UserRid = userInfo.UserRid;
            model.AdminGid = userInfo.AdminGid;
            model.NickName = userInfo.NickName;
            model.Avatar = userInfo.Avatar;
            model.PayCredits = userInfo.PayCredits;
            model.Gender = userInfo.Gender;
            model.RealName = userInfo.RealName;
            model.Bday = userInfo.Bday;
            model.IdCard = userInfo.IdCard;
            model.RegionId = userInfo.RegionId;
            model.Address = userInfo.Address;
            model.Bio = userInfo.Bio;

            Load(model.RegionId);

            return View(model);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        [HttpPost]
        public ActionResult Edit(UserModel model, int uid = -1)
        {
            UserInfo userInfo = AdminUsers.GetUserById(uid);
            if (userInfo == null)
                return PromptView("用户不存在");

            int uid2 = AdminUsers.GetUidByUserName(model.UserName);
            if (uid2 > 0 && uid2 != uid)
                ModelState.AddModelError("UserName", "用户名已经存在");

            int uid3 = AdminUsers.GetUidByEmail(model.Email);
            if (uid3 > 0 && uid3 != uid)
                ModelState.AddModelError("Email", "邮箱已经存在");

            int uid4 = AdminUsers.GetUidByMobile(model.Mobile);
            if (uid4 > 0 && uid4 != uid)
                ModelState.AddModelError("Mobile", "手机号已经存在");

            if (ModelState.IsValid)
            {
                string nickName;
                if (string.IsNullOrWhiteSpace(model.NickName))
                    nickName = userInfo.NickName;
                else
                    nickName = model.NickName;

                userInfo.UserName = model.UserName;
                userInfo.Email = model.Email == null ? "" : model.Email;
                userInfo.Mobile = model.Mobile == null ? "" : model.Mobile;
                if (!string.IsNullOrWhiteSpace(model.Password))
                    userInfo.Password = Users.CreateUserPassword(model.Password, userInfo.Salt);
                userInfo.UserRid = model.UserRid;
                userInfo.AdminGid = model.AdminGid;
                userInfo.NickName = WebHelper.HtmlEncode(nickName);
                userInfo.Avatar = model.Avatar == null ? "" : WebHelper.HtmlEncode(model.Avatar);
                userInfo.PayCredits = model.PayCredits;
                userInfo.RankCredits = userInfo.UserRid == model.UserRid ? userInfo.RankCredits : AdminUserRanks.GetUserRankById(model.UserRid).CreditsLower;
                userInfo.LiftBanTime = UserRanks.IsBanUserRank(model.UserRid) ? DateTime.Now.AddDays(WorkContext.UserRankInfo.LimitDays) : new DateTime(1900, 1, 1);
                userInfo.Gender = model.Gender;
                userInfo.RealName = model.RealName == null ? "" : WebHelper.HtmlEncode(model.RealName);
                userInfo.Bday = model.Bday ?? new DateTime(1970, 1, 1);
                userInfo.IdCard = model.IdCard == null ? "" : model.IdCard;
                userInfo.RegionId = model.RegionId;
                userInfo.Address = model.Address == null ? "" : WebHelper.HtmlEncode(model.Address);
                userInfo.Bio = model.Bio == null ? "" : WebHelper.HtmlEncode(model.Bio);

                AdminUsers.UpdateUser(userInfo);
                AddAdminOperateLog("修改用户", "修改用户,用户ID为:" + uid);
                return PromptView("用户修改成功");
            }

            Load(model.RegionId);

            return View(model);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        public ViewResult Del(int[] uidList)
        {
            AdminUsers.DeleteUserById(uidList);
            AddAdminOperateLog("删除用户", "删除用户,用户ID为:" + CommonHelper.IntArrayToString(uidList));
            return PromptView("用户删除成功");
        }

        private void Load(int regionId)
        {
            List<SelectListItem> userRankList = new List<SelectListItem>();
            userRankList.Add(new SelectListItem() { Text = "选择会员等级", Value = "0" });
            foreach (UserRankInfo info in AdminUserRanks.GetUserRankList())
            {
                userRankList.Add(new SelectListItem() { Text = info.Title, Value = info.UserRid.ToString() });
            }
            ViewData["userRankList"] = userRankList;


            List<SelectListItem> adminGroupList = new List<SelectListItem>();
            adminGroupList.Add(new SelectListItem() { Text = "选择管理员组", Value = "0" });
            foreach (AdminGroupInfo info in AdminGroups.GetAdminGroupList())
            {
                adminGroupList.Add(new SelectListItem() { Text = info.Title, Value = info.AdminGid.ToString() });
            }
            ViewData["adminGroupList"] = adminGroupList;

            RegionInfo regionInfo = Regions.GetRegionById(regionId);
            if (regionInfo != null)
            {
                ViewData["provinceId"] = regionInfo.ProvinceId;
                ViewData["cityId"] = regionInfo.CityId;
                ViewData["countyId"] = regionInfo.RegionId;
            }
            else
            {
                ViewData["provinceId"] = -1;
                ViewData["cityId"] = -1;
                ViewData["countyId"] = -1;
            }

            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
        }
    }
}
