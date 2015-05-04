using System;
using System.IO;
using System.Web;

using BrnShop.Core;

namespace BrnShop.Services
{
    public partial class ShopUtils
    {
        private static object _locker = new object();//锁对象

        #region  加密/解密

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptStr">加密字符串</param>
        public static string AESEncrypt(string encryptStr)
        {
            return SecureHelper.AESEncrypt(encryptStr, BSPConfig.ShopConfig.SecretKey);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="decryptStr">解密字符串</param>
        public static string AESDecrypt(string decryptStr)
        {
            return SecureHelper.AESDecrypt(decryptStr, BSPConfig.ShopConfig.SecretKey);
        }

        #endregion

        #region Cookie

        /// <summary>
        /// 获得用户sid
        /// </summary>
        /// <returns></returns>
        public static string GetSidCookie()
        {
            return WebHelper.GetCookie("bspsid");
        }

        /// <summary>
        /// 设置用户sid
        /// </summary>
        public static void SetSidCookie(string sid)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["bspsid"];
            if (cookie == null)
                cookie = new HttpCookie("bspsid");

            cookie.Value = sid;
            cookie.Expires = DateTime.Now.AddDays(15);
            string cookieDomain = BSPConfig.ShopConfig.CookieDomain;
            if (cookieDomain.Length != 0)
                cookie.Domain = cookieDomain;

            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <returns></returns>
        public static int GetUidCookie()
        {
            return TypeHelper.StringToInt(GetBSPCookie("uid"), -1);
        }

        /// <summary>
        /// 设置用户id
        /// </summary>
        public static void SetUidCookie(int uid)
        {
            SetBSPCookie("uid", uid.ToString());
        }

        /// <summary>
        /// 获得cookie密码
        /// </summary>
        /// <returns></returns>
        public static string GetCookiePassword()
        {
            return WebHelper.UrlDecode(GetBSPCookie("password"));
        }

        /// <summary>
        /// 解密cookie密码
        /// </summary>
        /// <param name="cookiePassword">cookie密码</param>
        /// <returns></returns>
        public static string DecryptCookiePassword(string cookiePassword)
        {
            return AESDecrypt(cookiePassword).Trim();
        }

        /// <summary>
        /// 设置cookie密码
        /// </summary>
        public static void SetCookiePassword(string password)
        {
            SetBSPCookie("password", WebHelper.UrlEncode(AESEncrypt(password)));
        }

        /// <summary>
        /// 设置用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="password">密码</param>
        /// <param name="sid">sid</param>
        /// <param name="expires">过期时间</param>
        public static void SetUserCookie(PartUserInfo partUserInfo, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["bsp"];
            if (cookie == null)
                cookie = new HttpCookie("bsp");

            cookie.Values["uid"] = partUserInfo.Uid.ToString();
            cookie.Values["password"] = WebHelper.UrlEncode(AESEncrypt(partUserInfo.Password));
            if (expires > 0)
            {
                cookie.Values["expires"] = expires.ToString();
                cookie.Expires = DateTime.Now.AddDays(expires);
            }
            string cookieDomain = BSPConfig.ShopConfig.CookieDomain;
            if (cookieDomain.Length != 0)
                cookie.Domain = cookieDomain;

            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 获得cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetBSPCookie(string key)
        {
            return WebHelper.GetCookie("bsp", key);
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetBSPCookie(string key, string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["bsp"];
            if (cookie == null)
                cookie = new HttpCookie("bsp");

            cookie[key] = value;

            int expires = TypeHelper.StringToInt(cookie.Values["expires"]);
            if (expires > 0)
                cookie.Expires = DateTime.Now.AddDays(expires);

            string cookieDomain = BSPConfig.ShopConfig.CookieDomain;
            if (cookieDomain.Length != 0)
                cookie.Domain = cookieDomain;

            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 获得访问referer
        /// </summary>
        public static string GetRefererCookie()
        {
            string referer = WebHelper.UrlDecode(WebHelper.GetCookie("referer"));
            if (referer.Length == 0)
                referer = "/";
            return referer;
        }

        /// <summary>
        /// 设置访问referer
        /// </summary>
        public static void SetRefererCookie(string url)
        {
            WebHelper.SetCookie("referer", WebHelper.UrlEncode(url));
        }

        /// <summary>
        /// 获得后台访问referer
        /// </summary>
        public static string GetAdminRefererCookie()
        {
            string adminReferer = WebHelper.UrlDecode(WebHelper.GetCookie("adminreferer"));
            if (adminReferer.Length == 0)
                adminReferer = "/admin/home/shopruninfo";
            return adminReferer;
        }

        /// <summary>
        /// 设置后台访问referer
        /// </summary>
        public static void SetAdminRefererCookie(string url)
        {
            WebHelper.SetCookie("adminreferer", WebHelper.UrlEncode(url));
        }

        #endregion

        #region  上传

        /// <summary>
        /// 保存上传的用户头像
        /// </summary>
        /// <param name="avatar">头像</param>
        /// <returns></returns>
        public static string SaveUploadUserAvatar(HttpPostedFileBase avatar)
        {
            if (avatar == null)
                return "-1";

            ShopConfigInfo shopConfig = BSPConfig.ShopConfig;

            string fileName = avatar.FileName;
            string extension = Path.GetExtension(fileName);
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, shopConfig.UploadImgType))
                return "-2";

            int fileSize = avatar.ContentLength;
            if (fileSize > shopConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/user/");
            string newFileName = string.Format("ua_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);
            string[] sizeList = StringHelper.SplitString(shopConfig.UserAvatarThumbSize);

            string sourceDirPath = dirPath + "source/";
            if (!Directory.Exists(sourceDirPath))
                Directory.CreateDirectory(sourceDirPath);

            string sourcePath = sourceDirPath + newFileName;
            avatar.SaveAs(sourcePath);

            foreach (string size in sizeList)
            {
                string thumbDirPath = string.Format("{0}thumb{1}/", dirPath, size);
                if (!Directory.Exists(thumbDirPath))
                    Directory.CreateDirectory(thumbDirPath);
                string[] widthAndHeight = StringHelper.SplitString(size, "_");
                IOHelper.GenerateThumb(sourcePath,
                                       thumbDirPath + newFileName,
                                       TypeHelper.StringToInt(widthAndHeight[0]),
                                       TypeHelper.StringToInt(widthAndHeight[1]),
                                       "H");
            }
            return newFileName;
        }

        /// <summary>
        /// 保存上传的用户等级头像
        /// </summary>
        /// <param name="avatar">头像</param>
        /// <returns></returns>
        public static string SaveUploadUserRankAvatar(HttpPostedFileBase avatar)
        {
            if (avatar == null)
                return "-1";

            ShopConfigInfo shopConfig = BSPConfig.ShopConfig;

            string fileName = avatar.FileName;
            string extension = Path.GetExtension(fileName);
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, shopConfig.UploadImgType))
                return "-2";

            int fileSize = avatar.ContentLength;
            if (fileSize > shopConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/userrank/");
            string newFileName = string.Format("ura_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);
            string[] sizeList = StringHelper.SplitString(shopConfig.UserRankAvatarThumbSize);

            string sourceDirPath = dirPath + "source/";
            if (!Directory.Exists(sourceDirPath))
                Directory.CreateDirectory(sourceDirPath);

            string sourcePath = sourceDirPath + newFileName;
            avatar.SaveAs(sourcePath);

            foreach (string size in sizeList)
            {
                string thumbDirPath = string.Format("{0}thumb{1}/", dirPath, size);
                if (!Directory.Exists(thumbDirPath))
                    Directory.CreateDirectory(thumbDirPath);
                string[] widthAndHeight = StringHelper.SplitString(size, "_");
                IOHelper.GenerateThumb(sourcePath,
                                       thumbDirPath + newFileName,
                                       TypeHelper.StringToInt(widthAndHeight[0]),
                                       TypeHelper.StringToInt(widthAndHeight[1]),
                                       "H");
            }
            return newFileName;
        }

        /// <summary>
        /// 保存上传的品牌logo
        /// </summary>
        /// <param name="brandLogo">品牌logo</param>
        /// <returns></returns>
        public static string SaveUploadBrandLogo(HttpPostedFileBase brandLogo)
        {
            if (brandLogo == null)
                return "-1";

            ShopConfigInfo shopConfig = BSPConfig.ShopConfig;

            string fileName = brandLogo.FileName;
            string extension = Path.GetExtension(fileName);
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, shopConfig.UploadImgType))
                return "-2";

            int fileSize = brandLogo.ContentLength;
            if (fileSize > shopConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/brand/");
            string newFileName = string.Format("b_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);
            string[] sizeList = StringHelper.SplitString(shopConfig.BrandThumbSize);

            string sourceDirPath = dirPath + "source/";
            if (!Directory.Exists(sourceDirPath))
                Directory.CreateDirectory(sourceDirPath);

            string sourcePath = sourceDirPath + newFileName;
            brandLogo.SaveAs(sourcePath);

            foreach (string size in sizeList)
            {
                string thumbDirPath = string.Format("{0}thumb{1}/", dirPath, size);
                if (!Directory.Exists(thumbDirPath))
                    Directory.CreateDirectory(thumbDirPath);
                string[] widthAndHeight = StringHelper.SplitString(size, "_");
                IOHelper.GenerateThumb(sourcePath,
                                       thumbDirPath + newFileName,
                                       TypeHelper.StringToInt(widthAndHeight[0]),
                                       TypeHelper.StringToInt(widthAndHeight[1]),
                                       "H");
            }
            return newFileName;
        }

        /// <summary>
        /// 保存新闻编辑器中的图片
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns></returns>
        public static string SaveNewsEditorImage(HttpPostedFileBase image)
        {
            if (image == null)
                return "-1";

            ShopConfigInfo shopConfig = BSPConfig.ShopConfig;

            string fileName = image.FileName;
            string extension = Path.GetExtension(fileName);
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, shopConfig.UploadImgType))
                return "-2";

            int fileSize = image.ContentLength;
            if (fileSize > shopConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/news/");
            string newFileName = string.Format("n_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);//生成文件名

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            image.SaveAs(dirPath + newFileName);

            return newFileName;
        }

        /// <summary>
        /// 保存帮助编辑器中的图片
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns></returns>
        public static string SaveHelpEditorImage(HttpPostedFileBase image)
        {
            if (image == null)
                return "-1";

            ShopConfigInfo shopConfig = BSPConfig.ShopConfig;

            string fileName = image.FileName;
            string extension = Path.GetExtension(fileName);
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, shopConfig.UploadImgType))
                return "-2";

            int fileSize = image.ContentLength;
            if (fileSize > shopConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/help/");
            string newFileName = string.Format("h_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);//生成文件名

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            image.SaveAs(dirPath + newFileName);

            return newFileName;
        }

        /// <summary>
        /// 保存商品编辑器中的图片
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns></returns>
        public static string SaveProductEditorImage(HttpPostedFileBase image)
        {
            if (image == null)
                return "-1";

            ShopConfigInfo shopConfig = BSPConfig.ShopConfig;

            string fileName = image.FileName;
            string extension = Path.GetExtension(fileName);
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, shopConfig.UploadImgType))
                return "-2";

            int fileSize = image.ContentLength;
            if (fileSize > shopConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/product/editor/");
            string newFileName = string.Format("pe_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);//生成文件名

            string sourceDirPath = dirPath + "source/";
            if (!Directory.Exists(sourceDirPath))
                Directory.CreateDirectory(sourceDirPath);
            string sourcePath = sourceDirPath + newFileName;
            image.SaveAs(sourcePath);

            string path = dirPath + newFileName;
            if (shopConfig.WatermarkType == 1)//文字水印
            {
                IOHelper.GenerateTextWatermark(sourcePath, path, shopConfig.WatermarkText, shopConfig.WatermarkTextSize, shopConfig.WatermarkTextFont, shopConfig.WatermarkPosition, shopConfig.WatermarkQuality);
            }
            else if (shopConfig.WatermarkType == 2)//图片水印
            {
                string watermarkPath = IOHelper.GetMapPath("/watermarks/" + shopConfig.WatermarkImg);
                IOHelper.GenerateImageWatermark(sourcePath, watermarkPath, path, shopConfig.WatermarkPosition, shopConfig.WatermarkImgOpacity, shopConfig.WatermarkQuality);
            }
            else
            {
                image.SaveAs(path);
            }

            return newFileName;
        }

        /// <summary>
        /// 保存上传的商品图片
        /// </summary>
        /// <param name="productImage">商品图片</param>
        /// <returns></returns>
        public static string SaveUplaodProductImage(HttpPostedFileBase productImage)
        {
            if (productImage == null)
                return "-1";

            ShopConfigInfo shopConfig = BSPConfig.ShopConfig;

            string fileName = productImage.FileName;
            string extension = Path.GetExtension(fileName);
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, shopConfig.UploadImgType))
                return "-2";

            int fileSize = productImage.ContentLength;
            if (fileSize > shopConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/product/show/");
            string name = "ps_" + DateTime.Now.ToString("yyMMddHHmmssfffffff");
            string newFileName = name + extension;
            string[] sizeList = StringHelper.SplitString(shopConfig.ProductShowThumbSize);

            string sourceDirPath = string.Format("{0}source/", dirPath);
            if (!Directory.Exists(sourceDirPath))
                Directory.CreateDirectory(sourceDirPath);
            string sourcePath = sourceDirPath + newFileName;
            productImage.SaveAs(sourcePath);

            if (shopConfig.WatermarkType == 1)//文字水印
            {
                string path = string.Format("{0}{1}_text{2}", sourceDirPath, name, extension);
                IOHelper.GenerateTextWatermark(sourcePath, path, shopConfig.WatermarkText, shopConfig.WatermarkTextSize, shopConfig.WatermarkTextFont, shopConfig.WatermarkPosition, shopConfig.WatermarkQuality);
                sourcePath = path;
            }
            else if (shopConfig.WatermarkType == 2)//图片水印
            {
                string path = string.Format("{0}{1}_img{2}", sourceDirPath, name, extension);
                string watermarkPath = IOHelper.GetMapPath("/watermarks/" + shopConfig.WatermarkImg);
                IOHelper.GenerateImageWatermark(sourcePath, watermarkPath, path, shopConfig.WatermarkPosition, shopConfig.WatermarkImgOpacity, shopConfig.WatermarkQuality);
                sourcePath = path;
            }

            foreach (string size in sizeList)
            {
                string thumbDirPath = string.Format("{0}thumb{1}/", dirPath, size);
                if (!Directory.Exists(thumbDirPath))
                    Directory.CreateDirectory(thumbDirPath);
                string[] widthAndHeight = StringHelper.SplitString(size, "_");
                IOHelper.GenerateThumb(sourcePath,
                                       thumbDirPath + newFileName,
                                       TypeHelper.StringToInt(widthAndHeight[0]),
                                       TypeHelper.StringToInt(widthAndHeight[1]),
                                       "H");
            }
            return newFileName;
        }

        /// <summary>
        /// 保存上传的banner图片
        /// </summary>
        /// <param name="bannerImg">banner图片</param>
        /// <returns></returns>
        public static string SaveUploadBannerImg(HttpPostedFileBase bannerImg)
        {
            if (bannerImg == null)
                return "-1";

            ShopConfigInfo shopConfig = BSPConfig.ShopConfig;

            string fileName = bannerImg.FileName;
            string extension = Path.GetExtension(fileName);
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, shopConfig.UploadImgType))
                return "-2";

            int fileSize = bannerImg.ContentLength;
            if (fileSize > shopConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/banner/");
            string newFileName = string.Format("fr_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);//生成文件名

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            bannerImg.SaveAs(dirPath + newFileName);

            return newFileName;
        }

        /// <summary>
        /// 保存上传的广告主体
        /// </summary>
        /// <param name="advertBody">广告主体</param>
        /// <returns></returns>
        public static string SaveUploadAdvertBody(HttpPostedFileBase advertBody)
        {
            if (advertBody == null)
                return "-1";

            ShopConfigInfo shopConfig = BSPConfig.ShopConfig;

            string fileName = advertBody.FileName;
            string extension = Path.GetExtension(fileName);
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, shopConfig.UploadImgType))
                return "-2";

            int fileSize = advertBody.ContentLength;
            if (fileSize > shopConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/advert/");
            string newFileName = string.Format("ad_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);//生成文件名

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            advertBody.SaveAs(dirPath + newFileName);

            return newFileName;
        }

        /// <summary>
        /// 保存上传的友情链接Logo
        /// </summary>
        /// <param name="friendLinkLogo">友情链接logo</param>
        /// <returns></returns>
        public static string SaveUploadFriendLinkLogo(HttpPostedFileBase friendLinkLogo)
        {
            if (friendLinkLogo == null)
                return "-1";

            ShopConfigInfo shopConfig = BSPConfig.ShopConfig;

            string fileName = friendLinkLogo.FileName;
            string extension = Path.GetExtension(fileName);
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, shopConfig.UploadImgType))
                return "-2";

            int fileSize = friendLinkLogo.ContentLength;
            if (fileSize > shopConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/friendlink/");
            string newFileName = string.Format("fr_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);//生成文件名

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            friendLinkLogo.SaveAs(dirPath + newFileName);

            return newFileName;
        }

        #endregion

        #region  日志

        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="input">输入内容</param>
        public static void WriteLogFile(Exception ex)
        {
            WriteLogFile(string.Format("方法:{0},异常信息:{1}", ex.TargetSite , ex.Message));
        }

        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="input">输入内容</param>
        public static void WriteLogFile(string input)
        {
            lock (_locker)
            {
                FileStream fs = null;
                StreamWriter sw = null;
                try
                {
                    string fileName = IOHelper.GetMapPath("/App_Data/exlogs/") + DateTime.Now.ToString("yyyyMMdd") + ".log";

                    FileInfo fileInfo = new FileInfo(fileName);
                    if (!fileInfo.Directory.Exists)
                    {
                        fileInfo.Directory.Create();
                    }
                    if (!fileInfo.Exists)
                    {
                        fileInfo.Create().Close();
                    }
                    else if (fileInfo.Length > 2048 * 1000)
                    {
                        fileInfo.Delete();
                    }

                    fs = fileInfo.OpenWrite();
                    sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.Write("Log Entry : ");
                    sw.Write("{0}", DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"));
                    sw.Write(Environment.NewLine);
                    sw.Write(input);
                    sw.Write(Environment.NewLine);
                    sw.Write("------------------------------------");
                    sw.Write(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Flush();
                        sw.Close();
                    }
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
            }
        }

        #endregion
    }
}
