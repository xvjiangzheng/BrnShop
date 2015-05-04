using System;
using System.Web;
using System.Data;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台商品控制器类
    /// </summary>
    public partial class ProductController : BaseAdminController
    {
        /// <summary>
        /// 在售商品列表
        /// </summary>
        /// <param name="productName">商品名称</param>
        /// <param name="categoryName">分类名称</param>
        /// <param name="brandName">品牌名称</param>
        /// <param name="sortOptions">排序</param>
        /// <param name="cateId">分类id</param>
        /// <param name="brandId">品牌id</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">每页数</param>
        /// <returns></returns>
        public ActionResult OnSaleProductList(string productName, string categoryName, string brandName, string sortColumn, string sortDirection, int cateId = -1, int brandId = -1, int pageNumber = 1, int pageSize = 15)
        {
            string condition = AdminProducts.AdminGetProductListCondition(productName, cateId, brandId, (int)ProductState.OnSale);
            string sort = AdminProducts.AdminGetProductListSort(sortColumn, sortDirection);

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminProducts.AdminGetProductCount(condition));

            ProductListModel model = new ProductListModel()
            {
                ProductList = AdminProducts.AdminGetProductList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                ProductName = productName,
                CateId = cateId,
                BrandId = brandId,
                CategoryName = string.IsNullOrWhiteSpace(categoryName) ? "全部分类" : categoryName,
                BrandName = string.IsNullOrWhiteSpace(brandName) ? "全部品牌" : brandName
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&ProductName={5}&cateId={6}&brandId={7}&categoryName={8}&brandName={9}",
                                                          Url.Action("onsaleproductlist"),
                                                          pageModel.PageNumber, pageModel.PageSize,
                                                          sortColumn, sortDirection,
                                                          productName,
                                                          cateId, brandId,
                                                          categoryName, brandName));
            return View(model);
        }

        /// <summary>
        /// 下架商品列表
        /// </summary>
        /// <param name="productName">商品名称</param>
        /// <param name="categoryName">分类名称</param>
        /// <param name="brandName">品牌名称</param>
        /// <param name="sortOptions">排序</param>
        /// <param name="cateId">分类id</param>
        /// <param name="brandId">品牌id</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">每页数</param>
        /// <returns></returns>
        public ActionResult OutSaleProductList(string productName, string categoryName, string brandName, string sortColumn, string sortDirection, int cateId = -1, int brandId = -1, int pageNumber = 1, int pageSize = 15)
        {
            string condition = AdminProducts.AdminGetProductListCondition(productName, cateId, brandId, (int)ProductState.OutSale);
            string sort = AdminProducts.AdminGetProductListSort(sortColumn, sortDirection);

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminProducts.AdminGetProductCount(condition));

            ProductListModel model = new ProductListModel()
            {
                ProductList = AdminProducts.AdminGetProductList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                ProductName = productName,
                CateId = cateId,
                BrandId = brandId,
                CategoryName = string.IsNullOrWhiteSpace(categoryName) ? "全部分类" : categoryName,
                BrandName = string.IsNullOrWhiteSpace(brandName) ? "全部品牌" : brandName
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&ProductName={5}&cateId={6}&brandId={7}&categoryName={8}&brandName={9}",
                                                          Url.Action("outsaleproductlist"),
                                                          pageModel.PageNumber, pageModel.PageSize,
                                                          sortColumn, sortDirection,
                                                          productName,
                                                          cateId, brandId,
                                                          categoryName, brandName));
            return View(model);
        }

        /// <summary>
        /// 回收站商品列表
        /// </summary>
        /// <param name="productName">商品名称</param>
        /// <param name="categoryName">分类名称</param>
        /// <param name="brandName">品牌名称</param>
        /// <param name="sortOptions">排序</param>
        /// <param name="cateId">分类id</param>
        /// <param name="brandId">品牌id</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">每页数</param>
        /// <returns></returns>
        public ActionResult RecycleBinProductList(string productName, string categoryName, string brandName, string sortColumn, string sortDirection, int cateId = -1, int brandId = -1, int pageNumber = 1, int pageSize = 15)
        {
            string condition = AdminProducts.AdminGetProductListCondition(productName, cateId, brandId, (int)ProductState.RecycleBin);
            string sort = AdminProducts.AdminGetProductListSort(sortColumn, sortDirection);

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminProducts.AdminGetProductCount(condition));

            ProductListModel model = new ProductListModel()
            {
                ProductList = AdminProducts.AdminGetProductList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                ProductName = productName,
                CateId = cateId,
                BrandId = brandId,
                CategoryName = string.IsNullOrWhiteSpace(categoryName) ? "全部分类" : categoryName,
                BrandName = string.IsNullOrWhiteSpace(brandName) ? "全部品牌" : brandName
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&ProductName={5}&cateId={6}&brandId={7}&categoryName={8}&brandName={9}",
                                                           Url.Action("recyclebinproductlist"),
                                                           pageModel.PageNumber, pageModel.PageSize,
                                                           sortColumn, sortDirection,
                                                           productName,
                                                           cateId, brandId,
                                                           categoryName, brandName));
            return View(model);
        }

        /// <summary>
        /// 商品选择列表
        /// </summary>
        /// <param name="productName">商品名称</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="cateId">分类id</param>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        public ActionResult ProductSelectList(string productName, int pageNumber = 1, int pageSize = 12, int cateId = -1, int brandId = -1)
        {
            string condition = AdminProducts.AdminGetProductListCondition(productName, cateId, brandId, (int)ProductState.OnSale);
            string sort = AdminProducts.AdminGetProductListSort("", "");

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminProducts.AdminGetProductCount(condition));

            DataTable dt = AdminProducts.AdminGetProductList(pageModel.PageSize, pageModel.PageNumber, condition, sort);
            StringBuilder result = new System.Text.StringBuilder("{");
            result.AppendFormat("\"totalPages\":\"{0}\",\"pageNumber\":\"{1}\",\"items\":[", pageModel.TotalPages, pageModel.PageNumber);
            foreach (DataRow row in dt.Rows)
                result.AppendFormat("{0}\"id\":\"{1}\",\"name\":\"{2}\"{3},", "{", row["pid"], row["name"].ToString().Trim(), "}");

            if (dt.Rows.Count > 0)
                result.Remove(result.Length - 1, 1);

            result.Append("]}");
            return Content(result.ToString());
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        [HttpGet]
        public ActionResult AddProduct()
        {
            AddProductModel model = new AddProductModel();

            string backUrl = ShopUtils.GetAdminRefererCookie();
            if (backUrl.Length == 0 || backUrl == "/admin/home/shopruninfo")
            {
                backUrl = Url.Action("onsaleproductlist");
                ShopUtils.SetAdminRefererCookie(backUrl);
            }
            ViewData["referer"] = backUrl;
            return View(model);
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        [HttpPost]
        public ActionResult AddProduct(AddProductModel model)
        {
            if (AdminProducts.AdminGetProductIdByName(model.ProductName) > 0)
                ModelState.AddModelError("ProductName", "名称已经存在");

            if (ModelState.IsValid)
            {
                ProductInfo productInfo = new ProductInfo()
                {
                    PSN = model.PSN ?? "",
                    CateId = model.CateId,
                    BrandId = model.BrandId,
                    SKUGid = 0,
                    Name = model.ProductName,
                    ShopPrice = model.ShopPrice,
                    MarketPrice = model.MarketPrice,
                    CostPrice = model.CostPrice,
                    State = model.State,
                    IsBest = model.IsBest == true ? 1 : 0,
                    IsHot = model.IsHot == true ? 1 : 0,
                    IsNew = model.IsNew == true ? 1 : 0,
                    DisplayOrder = model.DisplayOrder,
                    Weight = model.Weight,
                    ShowImg = "",
                    Description = model.Description ?? "",
                    AddTime = DateTime.Now,
                };

                //属性处理
                List<ProductAttributeInfo> productAttributeList = new List<ProductAttributeInfo>();
                if (model.AttrValueIdList != null && model.AttrValueIdList.Length > 0)
                {
                    for (int i = 0; i < model.AttrValueIdList.Length; i++)
                    {
                        int attrId = model.AttrIdList[i];//属性id
                        int attrValueId = model.AttrValueIdList[i];//属性值id
                        string inputValue = model.AttrInputValueList[i];//属性输入值
                        if (attrId > 0 && attrValueId > 0)
                        {
                            productAttributeList.Add(new ProductAttributeInfo
                            {
                                AttrId = attrId,
                                AttrValueId = attrValueId,
                                InputValue = inputValue ?? ""
                            });
                        }
                    }
                }

                AdminProducts.AddProduct(productInfo, model.StockNumber, model.StockLimit, productAttributeList);
                AddAdminOperateLog("添加普通商品", "添加普通商品,商品为:" + model.ProductName);

                string backUrl = null;
                if (productInfo.State == (int)ProductState.OnSale)
                    backUrl = Url.Action("onsaleproductlist");
                else
                    backUrl = Url.Action("outsaleproductlist");
                return PromptView(backUrl, "普通商品添加成功");
            }
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑商品
        /// </summary>
        [HttpGet]
        public ActionResult EditProduct(int pid = -1)
        {
            ProductInfo productInfo = AdminProducts.AdminGetProductById(pid);
            if (productInfo == null)
                return PromptView("商品不存在");

            EditProductModel model = new EditProductModel();

            model.PSN = productInfo.PSN;
            model.BrandId = productInfo.BrandId;
            model.ProductName = productInfo.Name;
            model.ShopPrice = productInfo.ShopPrice;
            model.MarketPrice = productInfo.MarketPrice;
            model.CostPrice = productInfo.CostPrice;
            model.State = productInfo.State;
            model.IsBest = productInfo.IsBest == 1 ? true : false;
            model.IsHot = productInfo.IsHot == 1 ? true : false;
            model.IsNew = productInfo.IsNew == 1 ? true : false;
            model.DisplayOrder = productInfo.DisplayOrder;
            model.Weight = productInfo.Weight;
            model.Description = productInfo.Description;

            model.BrandName = AdminBrands.GetBrandById(productInfo.BrandId).Name;


            //库存信息
            ProductStockInfo productStockInfo = AdminProducts.GetProductStockByPid(pid);
            model.StockNumber = productStockInfo.Number;
            model.StockLimit = productStockInfo.Limit;

            //商品属性列表
            List<ProductAttributeInfo> productAttributeList = Products.GetProductAttributeList(pid);

            //商品sku项列表
            DataTable productSKUItemList = AdminProducts.GetProductSKUItemList(productInfo.Pid);

            ViewData["pid"] = productInfo.Pid;
            ViewData["cateId"] = productInfo.CateId;
            ViewData["categoryName"] = AdminCategories.GetCategoryById(productInfo.CateId).Name;
            ViewData["productAttributeList"] = productAttributeList;
            ViewData["productSKUItemList"] = productSKUItemList;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 编辑商品
        /// </summary>
        [HttpPost]
        public ActionResult EditProduct(EditProductModel model, int pid = -1)
        {
            ProductInfo productInfo = AdminProducts.AdminGetProductById(pid);
            if (productInfo == null)
                return PromptView("商品不存在");

            int pid2 = AdminProducts.AdminGetProductIdByName(model.ProductName);
            if (pid2 > 0 && pid2 != pid)
                ModelState.AddModelError("ProductName", "名称已经存在");

            if (ModelState.IsValid)
            {
                productInfo.PSN = model.PSN ?? "";
                productInfo.BrandId = model.BrandId;
                productInfo.Name = model.ProductName;
                productInfo.ShopPrice = model.ShopPrice;
                productInfo.MarketPrice = model.MarketPrice;
                productInfo.CostPrice = model.CostPrice;
                productInfo.State = model.State;
                productInfo.IsBest = model.IsBest == true ? 1 : 0;
                productInfo.IsHot = model.IsHot == true ? 1 : 0;
                productInfo.IsNew = model.IsNew == true ? 1 : 0;
                productInfo.DisplayOrder = model.DisplayOrder;
                productInfo.Weight = model.Weight;
                productInfo.Description = model.Description ?? "";

                AdminProducts.UpdateProduct(productInfo, model.StockNumber, model.StockLimit);
                AddAdminOperateLog("修改商品", "修改商品,商品ID为:" + pid);
                return PromptView("商品修改成功");
            }


            //商品属性列表
            List<ProductAttributeInfo> productAttributeList = Products.GetProductAttributeList(pid);

            //商品sku项列表
            DataTable productSKUItemList = AdminProducts.GetProductSKUItemList(productInfo.Pid);

            ViewData["pid"] = productInfo.Pid;
            ViewData["cateId"] = productInfo.CateId;
            ViewData["categoryName"] = AdminCategories.GetCategoryById(productInfo.CateId).Name;
            ViewData["productAttributeList"] = productAttributeList;
            ViewData["productSKUItemList"] = productSKUItemList;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 添加SKU
        /// </summary>
        [HttpGet]
        public ActionResult AddSKU()
        {
            AddSKUModel model = new AddSKUModel();

            string backUrl = ShopUtils.GetAdminRefererCookie();
            if (backUrl.Length == 0 || backUrl == "/admin/home/shopruninfo")
            {
                backUrl = Url.Action("onsaleproductlist");
                ShopUtils.SetAdminRefererCookie(backUrl);
            }
            ViewData["referer"] = backUrl;
            return View(model);
        }

        /// <summary>
        /// 添加SKU
        /// </summary>
        [HttpPost]
        public ActionResult AddSKU(AddSKUModel model)
        {
            if (AdminProducts.AdminGetProductIdByName(model.ProductName) > 0)
                ModelState.AddModelError("ProductName", "名称已经存在");
            if (model.AttrIdList == null || model.AttrIdList.Length < 1)
                ModelState.AddModelError("CateId", "请选择SKU");

            if (ModelState.IsValid)
            {
                //商品信息
                ProductInfo productInfo = new ProductInfo()
                {
                    PSN = "",
                    CateId = model.CateId,
                    BrandId = model.BrandId,
                    SKUGid = 0,
                    Name = model.ProductName,
                    ShopPrice = model.ShopPrice,
                    MarketPrice = model.MarketPrice,
                    CostPrice = model.CostPrice,
                    State = (int)ProductState.OutSale,//设为下架状态
                    IsBest = model.IsBest == true ? 1 : 0,
                    IsHot = model.IsHot == true ? 1 : 0,
                    IsNew = model.IsNew == true ? 1 : 0,
                    DisplayOrder = model.DisplayOrder,
                    Weight = model.Weight,
                    ShowImg = "",
                    Description = model.Description ?? "",
                    AddTime = DateTime.Now
                };

                //SKU处理
                List<ProductSKUItemInfo> productSKUItemList = new List<ProductSKUItemInfo>();
                for (int i = 0; i < model.AttrIdList.Length; i++)
                {
                    int attrId = model.AttrIdList[i];//属性id
                    int attrValueId = model.AttrValueIdList[i];//属性值id
                    string inputValue = model.AttrInputValueList[i];//属性输入值
                    if (attrId > 0 && attrValueId > 0)
                    {
                        productSKUItemList.Add(new ProductSKUItemInfo()
                        {
                            AttrId = attrId,
                            AttrValueId = attrValueId,
                            InputValue = inputValue ?? ""
                        });
                    }
                }

                AdminProducts.AddSKU(productInfo, productSKUItemList);
                AddAdminOperateLog("添加SKU商品", "添加SKU商品,商品为:" + model.ProductName);

                return PromptView(Url.Action("outsaleproductlist"), "SKU商品添加成功");
            }
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 上架商品
        /// </summary>
        public ActionResult OnSaleProduct(int[] pidList)
        {
            bool result = AdminProducts.OnSaleProductById(pidList);
            if (result)
            {
                AddAdminOperateLog("上架商品", "上架商品,商品ID为:" + CommonHelper.IntArrayToString(pidList));
                if (WorkContext.IsHttpAjax)
                    return Content("1");
                else
                    return PromptView("商品上架成功");
            }
            else
            {
                if (WorkContext.IsHttpAjax)
                    return Content("0");
                else
                    return PromptView("商品上架失败");
            }
        }

        /// <summary>
        /// 下架商品
        /// </summary>
        public ActionResult OutSaleProduct(int[] pidList)
        {
            bool result = AdminProducts.OutSaleProductById(pidList);
            if (result)
            {
                AddAdminOperateLog("下架商品", "下架商品,商品ID为:" + CommonHelper.IntArrayToString(pidList));
                if (WorkContext.IsHttpAjax)
                    return Content("1");
                else
                    return PromptView("商品下架成功");
            }
            else
            {
                if (WorkContext.IsHttpAjax)
                    return Content("0");
                else
                    return PromptView("商品下架失败");
            }
        }

        /// <summary>
        /// 回收商品
        /// </summary>
        public ActionResult RecycleProduct(int[] pidList)
        {
            AdminProducts.RecycleProductById(pidList);
            AddAdminOperateLog("回收商品", "回收商品,商品ID为:" + CommonHelper.IntArrayToString(pidList));
            return PromptView("商品删除成功");
        }

        /// <summary>
        /// 恢复商品
        /// </summary>
        public ActionResult RestoreProduct(int[] pidList)
        {
            AdminProducts.RestoreProductById(pidList);
            AddAdminOperateLog("还原商品", "还原商品,商品ID为:" + CommonHelper.IntArrayToString(pidList));
            return PromptView("商品还原成功");
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        public ActionResult DelProduct(int[] pidList)
        {
            AdminProducts.DeleteProductById(pidList);
            AddAdminOperateLog("删除商品", "删除商品,商品ID为:" + CommonHelper.IntArrayToString(pidList));
            return PromptView("商品删除成功");
        }

        /// <summary>
        /// 改变商品新品状态
        /// </summary>
        public ActionResult ChangeProductIsNew(int[] pidList, int state = 0)
        {
            bool result = AdminProducts.ChangeProductIsNew(pidList, state);
            if (result)
            {
                AddAdminOperateLog("修改商品新品状态", "修改商品新品状态,商品ID和状态为:" + CommonHelper.IntArrayToString(pidList) + "_" + state);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 改变商品热销状态
        /// </summary>
        public ActionResult ChangeProductIsHot(int[] pidList, int state = 0)
        {
            bool result = AdminProducts.ChangeProductIsHot(pidList, state);
            if (result)
            {
                AddAdminOperateLog("修改商品热销状态", "修改商品热销状态,商品ID和状态为:" + CommonHelper.IntArrayToString(pidList) + "_" + state);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 改变商品精品状态
        /// </summary>
        public ActionResult ChangeProductIsBest(int[] pidList, int state = 0)
        {
            bool result = AdminProducts.ChangeProductIsBest(pidList, state);
            if (result)
            {
                AddAdminOperateLog("修改商品精品状态", "修改商品精品状态,商品ID和状态为:" + CommonHelper.IntArrayToString(pidList) + "_" + state);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 修改商品排序
        /// </summary>
        public ActionResult UpdateProductDisplayOrder(int pid = -1, int displayOrder = 0)
        {
            bool result = AdminProducts.UpdateProductDisplayOrder(pid, displayOrder);
            if (result)
            {
                AddAdminOperateLog("修改商品顺序", "修改商品顺序,商品ID:" + pid);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 修改商品商城价格
        /// </summary>
        public ActionResult UpdateProductShopPrice(int pid = -1, decimal shopPrice = 0.00M)
        {
            bool result = AdminProducts.UpdateProductShopPrice(pid, shopPrice);
            if (result)
            {
                AddAdminOperateLog("修改商品本店价格", "修改商品本店价格,商品ID和价格为:" + pid + "_" + shopPrice);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 修改商品库存数量
        /// </summary>
        public ActionResult UpdateProductStockNumber(int pid = -1, int stockNumber = 0)
        {
            bool result = AdminProducts.UpdateProductStockNumber(pid, stockNumber);
            if (result)
            {
                AddAdminOperateLog("更新商品库存数量", "更新商品库存数量,商品ID和库存数量为:" + pid + "_" + stockNumber);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }




        /// <summary>
        /// 更新商品属性
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="attrId">属性id</param>
        /// <param name="attrValueId">属性值id</param>
        /// <param name="inputValue">输入值</param>
        /// <param name="type">更新类型</param>
        /// <returns></returns>
        public ActionResult UpdateProductAttribute(int pid = -1, int attrId = -1, int attrValueId = -1, string inputValue = "", int type = -1)
        {
            bool result = false;
            ProductAttributeInfo productAttributeInfo = AdminProducts.GetProductAttributeByPidAndAttrId(pid, attrId);
            if (productAttributeInfo == null)
            {
                productAttributeInfo = new ProductAttributeInfo();

                productAttributeInfo.Pid = pid;
                productAttributeInfo.AttrId = attrId;
                productAttributeInfo.AttrValueId = attrValueId;
                if (AdminCategories.GetAttributeValueById(attrValueId).IsInput == 0 || string.IsNullOrWhiteSpace(inputValue))
                    productAttributeInfo.InputValue = "";
                else
                    productAttributeInfo.InputValue = inputValue;

                result = AdminProducts.CreateProductAttribute(productAttributeInfo);
            }
            else
            {
                if (type == 1)
                {
                    productAttributeInfo.AttrValueId = attrValueId;
                    productAttributeInfo.InputValue = inputValue;
                    result = AdminProducts.UpdateProductAttribute(productAttributeInfo);
                }
                else if (type == 0)
                {
                    productAttributeInfo.AttrValueId = attrValueId;
                    productAttributeInfo.InputValue = "";
                    result = AdminProducts.UpdateProductAttribute(productAttributeInfo);
                }
            }
            if (result)
            {
                AddAdminOperateLog("修改商品属性", "修改商品属性,商品属性ID:" + pid + "_" + attrId);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 删除商品属性
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        public ActionResult DelProductAttribute(int pid = -1, int attrId = -1)
        {
            bool result = AdminProducts.DeleteProductAttributeByPidAndAttrId(pid, attrId);
            if (result)
            {
                AddAdminOperateLog("删除商品属性", "删除商品属性,商品属性ID:" + pid + "_" + attrId);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }





        /// <summary>
        /// 商品图片列表
        /// </summary>
        public ActionResult ProductImageList(int pid = -1)
        {
            PartProductInfo partProductInfo = AdminProducts.AdminGetPartProductById(pid);
            if (partProductInfo == null)
                return PromptView("商品不存在");

            ProductImageListModel model = new ProductImageListModel()
            {
                ProductImageList = AdminProducts.GetProductImageList(pid),
                Pid = pid
            };
            Load();
            return View(model);
        }

        /// <summary>
        /// 添加商品图片
        /// </summary>
        public ActionResult AddProductImage(string showImg, int displayOrder = 0, int pid = -1)
        {
            PartProductInfo partProductInfo = AdminProducts.AdminGetPartProductById(pid);
            if (partProductInfo == null)
                return PromptView(Url.Action("productimagelist", new { pid = pid }), "商品不存在");

            if (string.IsNullOrWhiteSpace(showImg))
                return PromptView(Url.Action("productimagelist", new { pid = pid }), "图片不能为空");

            ProductImageInfo productImageInfo = new ProductImageInfo
            {
                Pid = pid,
                ShowImg = showImg,
                IsMain = 0,
                DisplayOrder = displayOrder
            };
            AdminProducts.CreateProductImage(productImageInfo);
            AddAdminOperateLog("添加商品图片", "添加商品图片,商品ID为:" + pid);
            return PromptView(Url.Action("productimagelist", new { pid = pid }), "商品图片添加成功");
        }

        /// <summary>
        /// 删除商品图片
        /// </summary>
        public ActionResult DelProductImage(int pImgId = -1)
        {
            bool result = AdminProducts.DeleteProductImageById(pImgId);
            if (result)
            {
                AddAdminOperateLog("删除商品图片", "删除商品图片,商品图片ID:" + pImgId);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 设置图片为商品主图
        /// </summary>
        public ActionResult SetProductMianImage(int pImgId = -1)
        {
            bool result = AdminProducts.SetProductMainImage(pImgId);
            if (result)
            {
                AddAdminOperateLog("设置商品主图", "设置商品主图,商品图片ID:" + pImgId);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 改变商品图片的排序
        /// </summary>
        public ActionResult ChangeProductImageDisplayOrder(int pImgId = -1, int displayOrder = 0)
        {
            bool result = AdminProducts.ChangeProductImageDisplayOrder(pImgId, displayOrder);
            if (result)
            {
                AddAdminOperateLog("修改商品图片顺序", "修改商品图片顺序,商品图片ID:" + pImgId);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        private void Load()
        {
            string allowImgType = string.Empty;
            string[] imgTypeList = StringHelper.SplitString(BSPConfig.ShopConfig.UploadImgType, ",");
            foreach (string imgType in imgTypeList)
                allowImgType += string.Format("*{0};", imgType.ToLower());

            string[] sizeList = StringHelper.SplitString(WorkContext.ShopConfig.ProductShowThumbSize);

            ViewData["size"] = sizeList[sizeList.Length / 2];
            ViewData["allowImgType"] = allowImgType;
            ViewData["maxImgSize"] = BSPConfig.ShopConfig.UploadImgSize;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
        }





        /// <summary>
        /// 商品关键词列表
        /// </summary>
        public ActionResult ProductKeywordList(int pid = -1)
        {
            PartProductInfo partProductInfo = AdminProducts.AdminGetPartProductById(pid);
            if (partProductInfo == null)
                return PromptView("商品不存在");

            ProductKeywordListModel model = new ProductKeywordListModel()
            {
                ProductKeywordList = AdminProducts.GetProductKeywordList(pid),
                Pid = pid
            };
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加商品关键词
        /// </summary>
        public ActionResult AddProductKeyword(string keyword, int relevancy = 0, int pid = -1)
        {
            PartProductInfo partProductInfo = AdminProducts.AdminGetPartProductById(pid);
            if (partProductInfo == null)
                return PromptView(Url.Action("productkeywordlist", new { pid = pid }), "商品不存在");

            if (string.IsNullOrWhiteSpace(keyword))
                return PromptView(Url.Action("productkeywordlist", new { pid = pid }), "关键词不能为空");

            if (keyword.Length > 20)
                return PromptView(Url.Action("productkeywordlist", new { pid = pid }), "关键词最多只能输入20个字");

            if (AdminProducts.IsExistProductKeyWord(pid, keyword))
                return PromptView(Url.Action("productkeywordlist", new { pid = pid }), "关键词已经存在");

            ProductKeywordInfo productKeywordInfo = new ProductKeywordInfo
            {
                Keyword = keyword,
                Pid = pid,
                Relevancy = relevancy
            };
            AdminProducts.CreateProductKeyword(productKeywordInfo);
            AddAdminOperateLog("添加商品关键词", "添加商品关键词,商品ID为:" + pid);
            return PromptView(Url.Action("productkeywordlist", new { pid = pid }), "商品关键词添加成功");
        }

        /// <summary>
        /// 更新商品关键词的相关性
        /// </summary>
        public ActionResult UpdateProductKeywordRelevancy(int keywordId = -1, int relevancy = 0)
        {
            bool result = AdminProducts.UpdateProductKeywordRelevancy(keywordId, relevancy);
            if (result)
            {
                AddAdminOperateLog("修改商品关键词的相关性", "修改商品关键词的相关性,商品关键词ID:" + keywordId);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 删除商品关键词
        /// </summary>
        public ActionResult DelProductKeyword(int[] keywordIdList)
        {
            bool result = AdminProducts.DeleteProductKeyword(keywordIdList);
            if (result)
            {
                AddAdminOperateLog("删除商品关键词", "删除商品关键词,商品关键词ID:" + CommonHelper.IntArrayToString(keywordIdList));
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }




        /// <summary>
        /// 关联商品列表
        /// </summary>
        /// <param name="pid">主商品id</param>
        /// <returns></returns>
        public ActionResult RelateProductList(int pid = -1)
        {
            PartProductInfo partProductInfo = AdminProducts.AdminGetPartProductById(pid);
            if (partProductInfo == null)
                return PromptView("商品不存在");

            RelateProductListModel model = new RelateProductListModel()
            {
                RelateProductList = AdminProducts.AdminGetRelateProductList(pid),
                Pid = pid
            };
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加关联商品
        /// </summary>
        /// <param name="pid">主商品id</param>
        /// <param name="relatePid">关联商品id</param>
        /// <returns></returns>
        public ActionResult AddRelateProduct(int pid = -1, int relatePid = -1)
        {
            PartProductInfo partProductInfo1 = AdminProducts.AdminGetPartProductById(pid);
            if (partProductInfo1 == null)
                return PromptView(Url.Action("relateproductlist", new { pid = pid }), "主商品不存在");

            PartProductInfo partProductInfo2 = AdminProducts.AdminGetPartProductById(relatePid);
            if (partProductInfo2 == null)
                return PromptView(Url.Action("relateproductlist", new { pid = pid }), "关联商品不存在");

            if (pid == relatePid)
                return PromptView(Url.Action("relateproductlist", new { pid = pid }), "不能关联自身");

            if (AdminProducts.IsExistRelateProduct(pid, relatePid))
                return PromptView(Url.Action("relateproductlist", new { pid = pid }), "此关联商品已经存在");

            AdminProducts.AddRelateProduct(pid, relatePid);
            AddAdminOperateLog("添加关联商品", "添加关联商品,关联商品为:" + partProductInfo2.Name);
            return PromptView(Url.Action("relateproductlist", new { pid = pid }), "关联商品添加成功");
        }

        /// <summary>
        /// 删除关联商品
        /// </summary>
        /// <param name="pid">主商品id</param>
        /// <param name="relatePid">关联商品id</param>
        /// <returns></returns>
        public ActionResult DelRelateProduct(int pid = -1, int relatePid = -1)
        {
            bool result = AdminProducts.DeleteRelateProductByPidAndRelatePid(pid, relatePid);
            if (result)
            {
                AddAdminOperateLog("删除关联商品", "删除关联商品品,商品ID为" + pid + "_" + relatePid);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }





        /// <summary>
        /// 签名商品列表
        /// </summary>
        public ActionResult SignProductList(string sign, int pageSize = 15, int pageNumber = 1)
        {
            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminProducts.AdminGetSignProductCount(sign));
            SignProductListModel model = new SignProductListModel()
            {
                PageModel = pageModel,
                SignProductList = AdminProducts.AdminGetSignProductList(pageSize, pageNumber, sign),
                Sign = sign
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sign={3}",
                                                          Url.Action("signproductlist"),
                                                          pageModel.PageNumber, pageModel.PageSize, sign));
            return View(model);
        }

        /// <summary>
        /// 添加签名商品
        /// </summary>
        [HttpGet]
        public ActionResult AddSignProduct()
        {
            SignProductModel model = new SignProductModel();
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加签名商品
        /// </summary>
        [HttpPost]
        public ActionResult AddSignProduct(SignProductModel model)
        {
            PartProductInfo partProductInfo = AdminProducts.AdminGetPartProductById(model.Pid);
            if (partProductInfo == null)
                ModelState.AddModelError("Pid", "请选择商品");

            if (AdminProducts.IsExistSignProduct(model.Sign, model.Pid))
                ModelState.AddModelError("Pid", "此商品已经存在");

            if (ModelState.IsValid)
            {
                AdminProducts.AddSignProduct(model.Sign, model.Pid);
                AddAdminOperateLog("添加签名商品", "添加签名商品,签名商品为:" + partProductInfo.Name);
                return PromptView("签名商品添加成功");
            }
            return View(model);
        }

        /// <summary>
        /// 删除签名商品
        /// </summary>
        /// <param name="recordId">记录id</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public ActionResult DelSignProduct(int recordId = -1, string sign = "")
        {
            bool result = AdminProducts.DeleteSignProduct(recordId, sign);
            if (result)
            {
                AddAdminOperateLog("删除签名商品", "删除签名商品,记录ID为" + recordId);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }








        /// <summary>
        /// 定时商品列表
        /// </summary>
        public ActionResult TimeProductList(string productName, int pageSize = 15, int pageNumber = 1)
        {
            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminProducts.GetTimeProductCount(productName));
            TimeProductListModel model = new TimeProductListModel()
            {
                PageModel = pageModel,
                TimeProductList = AdminProducts.GetTimeProductList(pageSize, pageNumber, productName),
                ProductName = productName
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&productName={3}",
                                                          Url.Action("timeproductlist"),
                                                          pageModel.PageNumber, pageModel.PageSize, productName));
            return View(model);
        }

        /// <summary>
        /// 添加定时商品
        /// </summary>
        [HttpGet]
        public ActionResult AddTimeProduct()
        {
            TimeProductModel model = new TimeProductModel();
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加定时商品
        /// </summary>
        [HttpPost]
        public ActionResult AddTimeProduct(TimeProductModel model)
        {
            PartProductInfo partProductInfo = AdminProducts.AdminGetPartProductById(model.Pid);
            if (partProductInfo == null)
                ModelState.AddModelError("Pid", "请选择商品");
            if (AdminProducts.IsExistTimeProduct(model.Pid))
                ModelState.AddModelError("Pid", "此商品已经存在");

            if (ModelState.IsValid)
            {
                DateTime noTime = new DateTime(1900, 1, 1);
                TimeProductInfo timeProductInfo = new TimeProductInfo()
                {
                    Pid = model.Pid,
                    OnSaleState = model.OnSaleTime == null ? 0 : 1,
                    OutSaleState = model.OutSaleTime == null ? 0 : 1,
                    OnSaleTime = model.OnSaleTime == null ? noTime : model.OnSaleTime.Value,
                    OutSaleTime = model.OutSaleTime == null ? noTime : model.OutSaleTime.Value
                };
                AdminProducts.AddTimeProduct(timeProductInfo);
                AddAdminOperateLog("添加定时商品", "添加定时商品,定时商品为:" + partProductInfo.Name);
                return PromptView("定时商品添加成功");
            }
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑定时商品
        /// </summary>
        [HttpGet]
        public ActionResult EditTimeProduct(int recordId = -1)
        {
            TimeProductInfo timeProductInfo = AdminProducts.GetTimeProductByRecordId(recordId);
            if (timeProductInfo == null)
                return PromptView("定时商品不存在");

            DateTime? nullTime = null;
            DateTime noTime = new DateTime(1900, 1, 1);

            TimeProductModel model = new TimeProductModel();
            model.Pid = timeProductInfo.Pid;
            model.ProductName = AdminProducts.GetPartProductById(timeProductInfo.Pid).Name;
            model.OnSaleTime = timeProductInfo.OnSaleTime == noTime ? nullTime : timeProductInfo.OnSaleTime;
            model.OutSaleTime = timeProductInfo.OutSaleTime == noTime ? nullTime : timeProductInfo.OutSaleTime;

            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑定时商品
        /// </summary>
        [HttpPost]
        public ActionResult EditTimeProduct(TimeProductModel model, int recordId = -1)
        {
            TimeProductInfo timeProductInfo = AdminProducts.GetTimeProductByRecordId(recordId);
            if (timeProductInfo == null)
                return PromptView("定时商品不存在");

            if (ModelState.IsValid)
            {
                DateTime noTime = new DateTime(1900, 1, 1);
                timeProductInfo.OnSaleTime = model.OnSaleTime == null ? noTime : model.OnSaleTime.Value;
                timeProductInfo.OutSaleTime = model.OutSaleTime == null ? noTime : model.OutSaleTime.Value;

                if (model.OnSaleTime != timeProductInfo.OnSaleTime)
                    timeProductInfo.OnSaleState = model.OnSaleTime == null ? 0 : 1;
                if (model.OutSaleTime != timeProductInfo.OutSaleTime)
                    timeProductInfo.OutSaleState = model.OutSaleTime == null ? 0 : 1;

                AdminProducts.UpdateTimeProduct(timeProductInfo);
                AddAdminOperateLog("修改定时商品", "修改定时商品,定时商品ID为:" + timeProductInfo.Pid);
                return PromptView("定时商品修改成功");
            }

            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 删除定时商品
        /// </summary>
        public ActionResult DelTimeProduct(int recordId = -1)
        {
            AdminProducts.DeleteTimeProductByRecordId(recordId);
            AddAdminOperateLog("删除定时商品", "删除定时商品,记录ID为" + recordId);
            return PromptView("定时商品修改成功");
        }
    }
}
