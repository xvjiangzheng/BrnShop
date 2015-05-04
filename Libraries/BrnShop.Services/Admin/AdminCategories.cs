using System;
using System.IO;
using System.Text;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 后台分类操作管理类
    /// </summary>
    public partial class AdminCategories : Categories
    {
        //分类选择列表缓存文件
        private const string _admincategoryselectlistcachefile = "/administration/cache/category/selectlist.js";

        /// <summary>
        /// 更新分类
        /// </summary>
        public static void UpdateCategory(CategoryInfo categoryInfo, int oldParentId)
        {
            if (categoryInfo.ParentId != oldParentId)//父分类改变时
            {
                int changeLayer = 0;
                List<CategoryInfo> categoryList = BrnShop.Data.Categories.GetCategoryList();
                CategoryInfo oldParentCategoryInfo = categoryList.Find(x => x.CateId == oldParentId);//旧的父分类
                if (categoryInfo.ParentId > 0)//非顶层分类时
                {
                    CategoryInfo newParentCategoryInfo = categoryList.Find(x => x.CateId == categoryInfo.ParentId);//新的父分类
                    if (oldParentCategoryInfo == null)
                    {
                        changeLayer = newParentCategoryInfo.Layer - 1;
                    }
                    else
                    {
                        changeLayer = newParentCategoryInfo.Layer - oldParentCategoryInfo.Layer;
                    }
                    categoryInfo.Layer = newParentCategoryInfo.Layer + 1;
                    categoryInfo.Path = newParentCategoryInfo.Path + "," + categoryInfo.CateId;

                    if (newParentCategoryInfo.HasChild == 0)
                    {
                        newParentCategoryInfo.HasChild = 1;
                        BrnShop.Data.Categories.UpdateCategory(newParentCategoryInfo);
                    }
                }
                else//顶层分类时
                {
                    changeLayer = 1 - oldParentCategoryInfo.Layer;
                    categoryInfo.Layer = 1;
                    categoryInfo.Path = categoryInfo.CateId.ToString();
                }

                if (oldParentId > 0 && categoryList.FindAll(x => x.ParentId == oldParentId).Count == 1)
                {
                    oldParentCategoryInfo.HasChild = 0;
                    BrnShop.Data.Categories.UpdateCategory(oldParentCategoryInfo);
                }

                foreach (CategoryInfo info in categoryList.FindAll(x => x.ParentId == categoryInfo.CateId))
                {
                    UpdateChildCategoryLayerAndPath(categoryList, info, changeLayer, categoryInfo.Path);
                }
            }

            BrnShop.Data.Categories.UpdateCategory(categoryInfo);

            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_LIST);

            WriteAdminCategorySelectListCache(GetCategoryList());
        }

        /// <summary>
        /// 创建分类
        /// </summary>
        public static void CreateCategory(CategoryInfo categoryInfo)
        {
            if (categoryInfo.ParentId > 0)
            {
                List<CategoryInfo> categoryList = BrnShop.Data.Categories.GetCategoryList();
                CategoryInfo parentCategoryInfo = categoryList.Find(x => x.CateId == categoryInfo.ParentId);//父分类
                categoryInfo.Layer = parentCategoryInfo.Layer + 1;
                categoryInfo.HasChild = 0;
                categoryInfo.Path = "";
                int categoryId = BrnShop.Data.Categories.CreateCategory(categoryInfo);

                categoryInfo.CateId = categoryId;
                categoryInfo.Path = parentCategoryInfo.Path + "," + categoryId;
                BrnShop.Data.Categories.UpdateCategory(categoryInfo);

                if (parentCategoryInfo.HasChild == 0)
                {
                    parentCategoryInfo.HasChild = 1;
                    BrnShop.Data.Categories.UpdateCategory(parentCategoryInfo);
                }
            }
            else
            {
                categoryInfo.Layer = 1;
                categoryInfo.HasChild = 0;
                categoryInfo.Path = "";
                int categoryId = BrnShop.Data.Categories.CreateCategory(categoryInfo);

                categoryInfo.CateId = categoryId;
                categoryInfo.Path = categoryId.ToString();
                BrnShop.Data.Categories.UpdateCategory(categoryInfo);
            }


            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_LIST);

            WriteAdminCategorySelectListCache(GetCategoryList());
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns>-3代表分类不存在,-2代表此分类下还有子分类未删除,-1代表此分类下还有属性分组未删除,0代表此分类下还有商品未删除,1代表删除成功</returns>
        public static int DeleteCategoryById(int cateId)
        {
            List<CategoryInfo> categoryList = BrnShop.Data.Categories.GetCategoryList();
            CategoryInfo categoryInfo = categoryList.Find(x => x.CateId == cateId);
            if (categoryInfo == null)
                return -3;
            if (categoryInfo.HasChild == 1)
                return -2;
            if (GetAttributeGroupListByCateId(cateId).Count > 0)
                return -1;
            if (AdminProducts.AdminGetCategoryProductCount(cateId) > 0)
                return 0;

            BrnShop.Data.Categories.DeleteCategoryById(cateId);
            if (categoryInfo.Layer > 1 && categoryList.FindAll(x => x.ParentId == categoryInfo.ParentId).Count == 1)
            {
                CategoryInfo parentCategoryInfo = categoryList.Find(x => x.CateId == categoryInfo.ParentId);
                parentCategoryInfo.HasChild = 0;
                BrnShop.Data.Categories.UpdateCategory(parentCategoryInfo);
            }

            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_LIST);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_BRANDLIST + cateId);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_FILTERAANDVLIST + cateId);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_AANDVLISTJSONCACHE + cateId);

            WriteAdminCategorySelectListCache(GetCategoryList());
            return 1;
        }

        /// <summary>
        /// 递归更新分类及其子分类的级别和路径
        /// </summary>
        /// <param name="categoryList">分类列表</param>
        /// <param name="categoryInfo">分类信息</param>
        /// <param name="changeLayer">变化的级别</param>
        /// <param name="parentPath">父路径</param>
        private static void UpdateChildCategoryLayerAndPath(List<CategoryInfo> categoryList, CategoryInfo categoryInfo, int changeLayer, string parentPath)
        {
            categoryInfo.Layer = categoryInfo.Layer + changeLayer;
            categoryInfo.Path = parentPath + "," + categoryInfo.CateId;
            BrnShop.Data.Categories.UpdateCategory(categoryInfo);
            foreach (CategoryInfo info in categoryList.FindAll(x => x.ParentId == categoryInfo.CateId))
            {
                UpdateChildCategoryLayerAndPath(categoryList, info, changeLayer, categoryInfo.Path);
            }
        }

        /// <summary>
        /// 将分类选择列表写入缓存文件
        /// </summary>
        private static void WriteAdminCategorySelectListCache(List<CategoryInfo> categoryList)
        {
            StringBuilder sb = new StringBuilder("<div id=\"categoryTree\"><table width=\"100%\"><thead><tr><th align=\"left\">分类名称</th><th width=\"100\" align=\"left\">管理操作</th></tr></thead><tbody>");

            foreach (CategoryInfo categoryInfo in categoryList)
            {
                sb.AppendFormat("<tr layer=\"{0}\" {1}><th>{2}<span {3} onclick=\"categoryTree(this,{0})\"></span>{4}</th><td>", categoryInfo.Layer, categoryInfo.Layer == 1 ? "" : "style=\"display:none;\"", CommonHelper.GetHtmlSpan(categoryInfo.Layer - 1), categoryInfo.HasChild == 1 ? "class=\"open\"" : "", categoryInfo.Name);
                if (categoryInfo.HasChild == 0)
                    sb.AppendFormat("<a href=\"javascript:setSelectedCategory({0},'{1}')\" class=\"editOperate\">[选择]</a>", categoryInfo.CateId, categoryInfo.Name);
                sb.Append("</td></tr>");
            }

            sb.Append("</tbody></table></div>");

            try
            {
                string filePath = IOHelper.GetMapPath(_admincategoryselectlistcachefile);
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    Byte[] info = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
                    fs.Write(info, 0, info.Length);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch
            { }
        }





        /// <summary>
        /// 更新属性分组
        /// </summary>
        /// <param name="newAttributeGroupInfo">新属性分组</param>
        /// <param name="oldAttributeGroupInfo">原属性分组</param>
        public static void UpdateAttributeGroup(AttributeGroupInfo attributeGroupInfo)
        {
            BrnShop.Data.Categories.UpdateAttributeGroup(attributeGroupInfo);
        }

        /// <summary>
        /// 创建属性分组
        /// </summary>
        public static void CreateAttributeGroup(AttributeGroupInfo attributeGroupInfo)
        {
            BrnShop.Data.Categories.CreateAttributeGroup(attributeGroupInfo);
        }

        /// <summary>
        /// 删除属性分组
        /// </summary>
        /// <param name="attrGroupId">属性分组id</param>
        /// <returns>0代表此分类下还有属性未删除,1代表删除成功</returns>
        public static int DeleteAttributeGroupById(int attrGroupId)
        {
            if (GetAttributeListByAttrGroupId(attrGroupId).Count > 0)
                return 0;
            BrnShop.Data.Categories.DeleteAttributeGroupById(attrGroupId);
            return 1;
        }






        /// <summary>
        /// 更新属性
        /// </summary>
        /// <param name="newAttributeInfo">新属性</param>
        /// <param name="oldAttributeInfo">原属性</param>
        public static void UpdateAttribute(AttributeInfo attributeInfo)
        {
            BrnShop.Data.Categories.UpdateAttribute(attributeInfo);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_FILTERAANDVLIST + attributeInfo.CateId);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_FILTERAANDVLIST + attributeInfo.CateId);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_AANDVLISTJSONCACHE + attributeInfo.CateId);
        }

        /// <summary>
        /// 创建属性
        /// </summary>
        /// <param name="attributeInfo">属性信息</param>
        /// <param name="attributeGroupInfo">属性组信息</param>
        public static void CreateAttribute(AttributeInfo attributeInfo, AttributeGroupInfo attributeGroupInfo)
        {
            BrnShop.Data.Categories.CreateAttribute(attributeInfo, attributeGroupInfo.AttrGroupId, attributeGroupInfo.Name, attributeGroupInfo.DisplayOrder);
            //BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_FILTERAANDVLIST + attributeInfo.CateId);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_AANDVLISTJSONCACHE + attributeInfo.CateId);
        }

        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <returns>0代表此属性下还有属性值未删除,1代表删除成功</returns>
        public static int DeleteAttributeById(int attrId)
        {
            if (GetAttributeValueListByAttrId(attrId).Count > 1)
                return 0;
            AttributeInfo attributeInfo = GetAttributeById(attrId);
            BrnShop.Data.Categories.DeleteAttributeById(attrId);
            if (attributeInfo.IsFilter == 1)
                BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_FILTERAANDVLIST + attributeInfo.CateId);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_AANDVLISTJSONCACHE + attributeInfo.CateId);
            return 1;
        }

        /// <summary>
        /// 后台获得属性列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetAttributeList(int cateId, string sort)
        {
            return BrnShop.Data.Categories.AdminGetAttributeList(cateId, sort);
        }

        /// <summary>
        /// 后台获得属性列表排序
        /// </summary>
        /// <param name="sortColumn">排序字段</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetAttributeListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Data.Categories.AdminGetAttributeListSort(sortColumn, sortDirection);
        }







        /// <summary>
        /// 创建属性值
        /// </summary>
        public static void CreateAttributeValue(AttributeValueInfo attributeValueInfo)
        {
            BrnShop.Data.Categories.CreateAttributeValue(attributeValueInfo);
            AttributeInfo attributeInfo = GetAttributeById(attributeValueInfo.AttrId);
            if (attributeInfo.IsFilter == 1)
            {
                BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_FILTERAANDVLIST + attributeInfo.CateId);
            }
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_AANDVLISTJSONCACHE + attributeInfo.CateId);
        }

        /// <summary>
        /// 删除属性值
        /// </summary>
        /// <param name="attrValueId">属性值id</param>
        /// <returns>-2代表此属性值不存在,-1代表此属性值为输入属性值,0代表此属性值下还有商品未删除,1代表删除成功</returns>
        public static int DeleteAttributeValueById(int attrValueId)
        {
            AttributeValueInfo attributeValueInfo = GetAttributeValueById(attrValueId);
            if (attributeValueInfo == null)
                return -2;
            if (attributeValueInfo.IsInput == 1)
                return -1;
            if (AdminProducts.AdminGetAttrValueProductCount(attrValueId) > 0)
                return 0;

            AttributeInfo attributeInfo = GetAttributeById(attributeValueInfo.AttrId);
            BrnShop.Data.Categories.DeleteAttributeValueById(attrValueId);
            if (attributeInfo.IsFilter == 1)
            {
                BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_FILTERAANDVLIST + attributeInfo.CateId);
            }
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_AANDVLISTJSONCACHE + attributeInfo.CateId);
            return 1;
        }

        /// <summary>
        /// 更新属性值
        /// </summary>
        public static void UpdateAttributeValue(AttributeValueInfo attributeValueInfo)
        {
            BrnShop.Data.Categories.UpdateAttributeValue(attributeValueInfo);
            AttributeInfo attributeInfo = GetAttributeById(attributeValueInfo.AttrId);
            if (attributeInfo.IsFilter == 1)
            {
                BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_FILTERAANDVLIST + attributeInfo.CateId);
            }
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_CATEGORY_AANDVLISTJSONCACHE + attributeInfo.CateId);
        }
    }
}
