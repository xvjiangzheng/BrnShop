using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 分类操作管理类
    /// </summary>
    public partial class Categories
    {
        /// <summary>
        /// 获得分类列表
        /// </summary>
        /// <returns></returns>
        public static List<CategoryInfo> GetCategoryList()
        {
            List<CategoryInfo> categoryList = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_CATEGORY_LIST) as List<CategoryInfo>;
            if (categoryList == null)
            {
                categoryList = new List<CategoryInfo>();
                List<CategoryInfo> sourceCategoryList = BrnShop.Data.Categories.GetCategoryList();
                CreateCategoryTree(sourceCategoryList, categoryList, 0);
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_CATEGORY_LIST, categoryList);
            }
            return categoryList;
        }

        /// <summary>
        /// 递归创建分类列表树
        /// </summary>
        protected static void CreateCategoryTree(List<CategoryInfo> sourceCategoryList, List<CategoryInfo> resultCategoryList, int parentId)
        {
            foreach (CategoryInfo categoryInfo in sourceCategoryList)
            {
                if (categoryInfo.ParentId == parentId)
                {
                    resultCategoryList.Add(categoryInfo);
                    CreateCategoryTree(sourceCategoryList, resultCategoryList, categoryInfo.CateId);
                }
            }
        }

        /// <summary>
        /// 通过分类名称获得分类id
        /// </summary>
        /// <param name="name">分类名称</param>
        /// <returns></returns>
        public static int GetCateIdByName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                foreach (CategoryInfo categoryInfo in GetCategoryList())
                {
                    if (categoryInfo.Name == name)
                        return categoryInfo.CateId;
                }
            }
            return 0;
        }

        /// <summary>
        /// 通过分类名称获得分类信息
        /// </summary>
        /// <param name="name">分类名称</param>
        /// <returns></returns>
        public static CategoryInfo GetCategoryByName(string name)
        {
            foreach (CategoryInfo categoryInfo in GetCategoryList())
            {
                if (categoryInfo.Name == name)
                    return categoryInfo;
            }
            return null;
        }

        /// <summary>
        /// 获得分类
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public static CategoryInfo GetCategoryById(int cateId)
        {
            foreach (CategoryInfo categoryInfo in GetCategoryList())
            {
                if (categoryInfo.CateId == cateId)
                    return categoryInfo;
            }

            return null;
        }

        /// <summary>
        /// 获得分类
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="categoryList">分类列表</param>
        /// <returns></returns>
        public static CategoryInfo GetCategoryById(int cateId, List<CategoryInfo> categoryList)
        {
            foreach (CategoryInfo categoryInfo in categoryList)
            {
                if (categoryInfo.CateId == cateId)
                    return categoryInfo;
            }

            return null;
        }

        /// <summary>
        /// 获得子分类列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="layer">级别</param>
        /// <returns></returns>
        public static List<CategoryInfo> GetChildCategoryList(int cateId, int layer)
        {
            return GetChildCategoryList(cateId, layer, false);
        }

        /// <summary>
        /// 获得子分类列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="layer">级别</param>
        /// <param name="isAllChildren">是否包括全部子节点</param>
        /// <returns></returns>
        public static List<CategoryInfo> GetChildCategoryList(int cateId, int layer, bool isAllChildren)
        {
            List<CategoryInfo> categoryList = new List<CategoryInfo>();

            if (cateId > 0)
            {
                bool flag = false;
                if (isAllChildren)
                {
                    foreach (CategoryInfo categoryInfo in GetCategoryList())
                    {
                        if (flag && categoryInfo.Layer == layer)
                        {
                            flag = false;
                        }
                        if (flag || categoryInfo.ParentId == cateId)
                        {
                            flag = true;
                            categoryList.Add(categoryInfo);
                        }
                    }
                }
                else
                {
                    foreach (CategoryInfo categoryInfo in GetCategoryList())
                    {
                        if (categoryInfo.ParentId == cateId)
                        {
                            flag = true;
                            categoryList.Add(categoryInfo);
                        }
                        else if (flag && categoryInfo.Layer == layer)
                        {
                            break;
                        }
                    }
                }
            }
            return categoryList;
        }

        /// <summary>
        /// 获得子分类列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="categoryList">分类列表</param>
        /// <returns></returns>
        public static List<CategoryInfo> GetChildCategoryList(int cateId, List<CategoryInfo> categoryList)
        {
            List<CategoryInfo> list = new List<CategoryInfo>();

            if (cateId > 0)
            {
                foreach (CategoryInfo categoryInfo in categoryList)
                {
                    if (categoryInfo.ParentId == cateId)
                        list.Add(categoryInfo);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得子分类列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="layer">级别</param>
        /// <param name="categoryList">分类列表</param>
        /// <returns></returns>
        public static List<CategoryInfo> GetChildCategoryList(int cateId, int layer, List<CategoryInfo> categoryList)
        {
            return GetChildCategoryList(cateId, layer, false, categoryList);
        }

        /// <summary>
        /// 获得子分类列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="layer">级别</param>
        /// <param name="isAllChildren">是否包括全部子节点</param>
        /// <param name="categoryList">分类列表</param>
        /// <returns></returns>
        public static List<CategoryInfo> GetChildCategoryList(int cateId, int layer, bool isAllChildren, List<CategoryInfo> categoryList)
        {
            List<CategoryInfo> list = new List<CategoryInfo>();

            if (cateId > 0)
            {
                bool flag = false;
                if (isAllChildren)
                {
                    foreach (CategoryInfo categoryInfo in categoryList)
                    {
                        if (flag && categoryInfo.Layer == layer)
                        {
                            flag = false;
                        }
                        if (flag || categoryInfo.ParentId == cateId)
                        {
                            flag = true;
                            list.Add(categoryInfo);
                        }
                    }
                }
                else
                {
                    foreach (CategoryInfo categoryInfo in categoryList)
                    {
                        if (categoryInfo.ParentId == cateId)
                        {
                            flag = true;
                            list.Add(categoryInfo);
                        }
                        else if (flag && categoryInfo.Layer == layer)
                        {
                            break;
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得分类关联的品牌
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public static List<BrandInfo> GetCategoryBrandList(int cateId)
        {
            List<BrandInfo> brandList = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_CATEGORY_BRANDLIST + cateId) as List<BrandInfo>;
            if (brandList == null)
            {
                brandList = BrnShop.Data.Categories.GetCategoryBrandList(cateId);
                if (brandList != null)
                    BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_CATEGORY_BRANDLIST + cateId, brandList);
            }

            return brandList;
        }

        /// <summary>
        /// 获得分类筛选属性列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public static List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> GetCategoryFilterAAndVList(int cateId)
        {
            List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> itemList = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_CATEGORY_FILTERAANDVLIST + cateId) as List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>>;
            if (itemList == null)
            {
                itemList = new List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>>();
                List<AttributeInfo> filterAttributeList = GetFilterAttributeListByCateId(cateId);
                foreach (AttributeInfo attributeInfo in filterAttributeList)
                {
                    List<AttributeValueInfo> attributeValueList = GetAttributeSelectValueListByAttrId(attributeInfo.AttrId);
                    itemList.Add(new KeyValuePair<AttributeInfo, List<AttributeValueInfo>>(attributeInfo, attributeValueList));
                }

                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_CATEGORY_FILTERAANDVLIST + cateId, itemList);
            }

            return itemList;
        }

        /// <summary>
        /// 获得分类属性列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public static List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> GetCategoryAAndVList(int cateId)
        {
            List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> itemList = new List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>>();

            List<AttributeInfo> attributeList = GetAttributeListByCateId(cateId);
            foreach (AttributeInfo attributeInfo in attributeList)
            {
                List<AttributeValueInfo> attributeValueList = GetAttributeValueListByAttrId(attributeInfo.AttrId);
                itemList.Add(new KeyValuePair<AttributeInfo, List<AttributeValueInfo>>(attributeInfo, attributeValueList));
            }

            return itemList;
        }

        /// <summary>
        /// 获得分类属性列表json缓存
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public static string GetCategoryAAndVListJsonCache(int cateId)
        {
            string jsonCache = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_CATEGORY_AANDVLISTJSONCACHE + cateId) as string;
            if (jsonCache == null)
            {
                StringBuilder json = new StringBuilder("[");

                List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> itemList = GetCategoryAAndVList(cateId);
                foreach (KeyValuePair<AttributeInfo, List<AttributeValueInfo>> item in itemList)
                {
                    AttributeInfo attributeInfo = item.Key;
                    json.AppendFormat("{0}\"attrId\":\"{1}\",\"name\":\"{2}\",\"attrValueList\":[", "{", attributeInfo.AttrId, attributeInfo.Name);
                    foreach (AttributeValueInfo attributeValueInfo in item.Value)
                    {
                        json.AppendFormat("{0}\"attrValueId\":\"{1}\",\"attrValue\":\"{2}\",\"isInput\":\"{3}\"{4},", "{", attributeValueInfo.AttrValueId, attributeValueInfo.AttrValue, attributeValueInfo.IsInput, "}");
                    }
                    if (item.Value.Count > 0)
                        json.Remove(json.Length - 1, 1);

                    json.AppendFormat("]{0},", "}");
                }
                if (itemList.Count > 0)
                    json.Remove(json.Length - 1, 1);

                json.Append("]");

                jsonCache = json.ToString();
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_CATEGORY_AANDVLISTJSONCACHE + cateId, jsonCache);
            }

            return jsonCache;
        }







        /// <summary>
        /// 获得分类的属性分组列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public static List<AttributeGroupInfo> GetAttributeGroupListByCateId(int cateId)
        {
            return BrnShop.Data.Categories.GetAttributeGroupListByCateId(cateId);
        }

        /// <summary>
        /// 获得属性分组
        /// </summary>
        /// <param name="attrGroupId">属性分组id</param>
        public static AttributeGroupInfo GetAttributeGroupById(int attrGroupId)
        {
            return BrnShop.Data.Categories.GetAttributeGroupById(attrGroupId);
        }

        /// <summary>
        /// 通过分类id和属性分组名称获得分组id
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="name">分组名称</param>
        /// <returns></returns>
        public static int GetAttributeGroupIdByCateIdAndName(int cateId, string name)
        {
            if (cateId < 1 || string.IsNullOrWhiteSpace(name))
                return 0;
            return BrnShop.Data.Categories.GetAttrGroupIdByCateIdAndName(cateId, name);
        }






        /// <summary>
        /// 获得属性
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        public static AttributeInfo GetAttributeById(int attrId)
        {
            return BrnShop.Data.Categories.GetAttributeById(attrId);
        }

        /// <summary>
        /// 通过分类id和属性名称获得属性id
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="attributeName">属性名称</param>
        /// <returns></returns>
        public static int GetAttrIdByCateIdAndName(int cateId, string attributeName)
        {
            if (cateId < 1 || string.IsNullOrWhiteSpace(attributeName))
                return 0;
            return BrnShop.Data.Categories.GetAttrIdByCateIdAndName(cateId, attributeName);
        }

        /// <summary>
        /// 获得属性列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public static List<AttributeInfo> GetAttributeListByCateId(int cateId)
        {
            return BrnShop.Data.Categories.GetAttributeListByCateId(cateId);
        }

        /// <summary>
        /// 获得属性列表
        /// </summary>
        /// <param name="attrGroupId">属性分组id</param>
        /// <returns></returns>
        public static List<AttributeInfo> GetAttributeListByAttrGroupId(int attrGroupId)
        {
            return BrnShop.Data.Categories.GetAttributeListByAttrGroupId(attrGroupId);
        }

        /// <summary>
        /// 获得筛选属性列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public static List<AttributeInfo> GetFilterAttributeListByCateId(int cateId)
        {
            return BrnShop.Data.Categories.GetFilterAttributeListByCateId(cateId);
        }





        /// <summary>
        /// 获得属性的可选值列表
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        public static List<AttributeValueInfo> GetAttributeSelectValueListByAttrId(int attrId)
        {
            List<AttributeValueInfo> attributeValueList1 = GetAttributeValueListByAttrId(attrId);
            if (attributeValueList1.Count < 2)
                return new List<AttributeValueInfo>();

            List<AttributeValueInfo> attributeValueList2 = new List<AttributeValueInfo>(attributeValueList1.Count - 1);
            foreach (AttributeValueInfo attributeValueInfo in attributeValueList1)
            {
                if (attributeValueInfo.IsInput == 0)
                    attributeValueList2.Add(attributeValueInfo);
            }
            return attributeValueList2;
        }

        /// <summary>
        /// 获得属性值列表
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        public static List<AttributeValueInfo> GetAttributeValueListByAttrId(int attrId)
        {
            return BrnShop.Data.Categories.GetAttributeValueListByAttrId(attrId);
        }

        /// <summary>
        /// 获得属性值
        /// </summary>
        /// <param name="attrValueId">属性值id</param>
        /// <returns></returns>
        public static AttributeValueInfo GetAttributeValueById(int attrValueId)
        {
            return BrnShop.Data.Categories.GetAttributeValueById(attrValueId);
        }

        /// <summary>
        /// 通过属性id和属性值获得属性值的id
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <param name="attrValue">属性值</param>
        /// <returns></returns>
        public static int GetAttributeValueIdByAttrIdAndValue(int attrId, string attrValue)
        {
            if (attrId < 1 || string.IsNullOrWhiteSpace(attrValue))
                return 0;
            return BrnShop.Data.Categories.GetAttributeValueIdByAttrIdAndValue(attrId, attrValue);
        }
    }
}
