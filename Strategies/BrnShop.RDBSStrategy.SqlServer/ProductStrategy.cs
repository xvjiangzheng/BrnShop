using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.RDBSStrategy.SqlServer
{
    /// <summary>
    /// SqlServer策略之商品分部类
    /// </summary>
    public partial class RDBSStrategy : IRDBSStrategy
    {
        #region 品牌

        /// <summary>
        /// 获得品牌
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        public IDataReader GetBrandById(int brandId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@brandid", SqlDbType.Int, 4, brandId)    
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getbrandbyid", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 创建品牌
        /// </summary>
        /// <param name="brandInfo"></param>
        public void CreateBrand(BrandInfo brandInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@displayorder", SqlDbType.Int,4,brandInfo.DisplayOrder),
                                        GenerateInParam("@name", SqlDbType.NChar, 20, brandInfo.Name),
                                        GenerateInParam("@logo", SqlDbType.NChar,100,brandInfo.Logo)
                                    };
            string commandText = string.Format("INSERT INTO [{0}brands]([displayorder],[name],[logo]) VALUES(@displayorder,@name,@logo)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除品牌
        /// </summary>
        /// <param name="brandId">品牌id</param>
        public void DeleteBrandById(int brandId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@brandid", SqlDbType.Int, 4, brandId)    
                                    };
            string commandText = string.Format("DELETE FROM [{0}brands] WHERE [brandid]=@brandid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新品牌
        /// </summary>
        /// <param name="brandInfo"></param>
        public void UpdateBrand(BrandInfo brandInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@displayorder", SqlDbType.Int,4,brandInfo.DisplayOrder),
                                        GenerateInParam("@name", SqlDbType.NChar, 20, brandInfo.Name),
                                        GenerateInParam("@logo", SqlDbType.NChar,100,brandInfo.Logo),
                                        GenerateInParam("@brandid", SqlDbType.Int, 4, brandInfo.BrandId)    
                                    };

            string commandText = string.Format("UPDATE [{0}brands] SET [displayorder]=@displayorder,[name]=@name,[logo]=@logo WHERE [brandid]=@brandid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 后台获得品牌列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public DataTable AdminGetBrandList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {2} FROM [{1}brands] ORDER BY {3}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.BRANDS,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} {3} FROM [{1}brands] WHERE {2} ORDER BY {4}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                condition,
                                                RDBSFields.BRANDS,
                                                sort);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {3} FROM [{1}brands] WHERE [brandid] NOT IN (SELECT TOP {2} [brandid] FROM [{1}brands] ORDER BY {4}) ORDER BY {4}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                RDBSFields.BRANDS,
                                                sort);
                else
                    commandText = string.Format("SELECT TOP {0} {4} FROM [{1}brands] WHERE [brandid] NOT IN (SELECT TOP {2} [brandid] FROM [{1}brands] WHERE {3} ORDER BY {5}) AND {3} ORDER BY {5}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                condition,
                                                RDBSFields.BRANDS,
                                                sort);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得品牌选择列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public DataTable AdminGetBrandSelectList(int pageSize, int pageNumber, string condition)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} [brandid],[name] FROM [{1}brands] ORDER BY [displayorder] DESC",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre);

                else
                    commandText = string.Format("SELECT TOP {0} [brandid],[name] FROM [{1}brands] WHERE {2} ORDER BY [displayorder] DESC",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} [brandid],[name] FROM [{1}brands] WHERE [brandid] NOT IN (SELECT TOP {2} [brandid] FROM [{1}brands] ORDER BY [displayorder] DESC) ORDER BY [displayorder] DESC",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize);
                else
                    commandText = string.Format("SELECT TOP {0} [brandid],[name] FROM [{1}brands] WHERE [brandid] NOT IN (SELECT TOP {2} [brandid] FROM [{1}brands] WHERE {3} ORDER BY [displayorder] DESC) AND {3} ORDER BY [displayorder] DESC",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得列表搜索条件
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        public string AdminGetBrandListCondition(string brandName)
        {
            if (!string.IsNullOrWhiteSpace(brandName))
                return string.Format("[name] like '{0}%' ", brandName);
            return "";
        }

        /// <summary>
        /// 后台获得列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string AdminGetBrandListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[displayorder]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得品牌数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int AdminGetBrandCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(brandid) FROM [{0}brands]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(brandid) FROM [{0}brands] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 根据品牌名称得到品牌id
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        public int GetBrandIdByName(string brandName)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@name", SqlDbType.NChar, 20, brandName)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                    string.Format("{0}getbrandidbyname", RDBSHelper.RDBSTablePre),
                                                                    parms));
        }

        /// <summary>
        /// 获得品牌关联的分类
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        public IDataReader GetBrandCategoryList(int brandId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@brandid", SqlDbType.Int, 4, brandId)    
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getbrandcategorylist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得品牌列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        public IDataReader GetBrandList(int pageSize, int pageNumber, string brandName)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pagesize", SqlDbType.Int, 4, pageSize),
                                    GenerateInParam("@pagenumber", SqlDbType.Int, 4, pageNumber),   
                                    GenerateInParam("@brandname", SqlDbType.NChar, 20, brandName)
                                   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getbrandlist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得品牌数量
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        public int GetBrandCount(string brandName)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@brandname", SqlDbType.NChar, 20, brandName)
                                   };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getbrandcount", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        #endregion

        #region 分类

        /// <summary>
        /// 获得分类列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetCategoryList()
        {
            string commandText = string.Format("SELECT {1} FROM [{0}categories] ORDER BY [displayorder] DESC",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.CATEGORIES);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 创建分类
        /// </summary>
        /// <returns></returns>
        public int CreateCategory(CategoryInfo categoryInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@displayorder", SqlDbType.Int,4, categoryInfo.DisplayOrder),
                                        GenerateInParam("@name", SqlDbType.NChar,60, categoryInfo.Name),
                                        GenerateInParam("@pricerange", SqlDbType.Char,200,categoryInfo.PriceRange),
                                        GenerateInParam("@parentid", SqlDbType.SmallInt, 2, categoryInfo.ParentId),
                                        GenerateInParam("@layer", SqlDbType.TinyInt,1,categoryInfo.Layer),
                                        GenerateInParam("@haschild", SqlDbType.TinyInt,1,categoryInfo.HasChild),
                                        GenerateInParam("@path", SqlDbType.Char,100, categoryInfo.Path)
                                    };
            string commandText = string.Format("INSERT INTO [{0}categories]([displayorder],[name],[pricerange],[parentid],[layer],[haschild],[path]) VALUES(@displayorder,@name,@pricerange,@parentid,@layer,@haschild,@path);SELECT SCOPE_IDENTITY()",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="cateId">分类id</param>
        public void DeleteCategoryById(int cateId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@cateid", SqlDbType.SmallInt, 2, cateId)    
                                    };
            string commandText = string.Format("DELETE FROM [{0}categories] WHERE [cateid]=@cateid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新分类
        /// </summary>
        /// <returns></returns>
        public void UpdateCategory(CategoryInfo categoryInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@displayorder", SqlDbType.Int,4, categoryInfo.DisplayOrder),
                                        GenerateInParam("@name", SqlDbType.NChar,60, categoryInfo.Name),
                                        GenerateInParam("@pricerange", SqlDbType.Char,200,categoryInfo.PriceRange),
                                        GenerateInParam("@parentid", SqlDbType.SmallInt, 2, categoryInfo.ParentId),
                                        GenerateInParam("@layer", SqlDbType.TinyInt,1,categoryInfo.Layer),
                                        GenerateInParam("@haschild", SqlDbType.TinyInt,1,categoryInfo.HasChild),
                                        GenerateInParam("@path", SqlDbType.Char,100, categoryInfo.Path),
                                        GenerateInParam("@cateid", SqlDbType.SmallInt, 2, categoryInfo.CateId)    
                                    };

            string commandText = string.Format("UPDATE [{0}categories] SET [displayorder]=@displayorder,[name]=@name,[pricerange]=@pricerange,[parentId]=@parentId,[layer]=@layer,[haschild]=@haschild,[path]=@path WHERE [cateid]=@cateid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得分类关联的品牌
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public IDataReader GetCategoryBrandList(int cateId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@cateid", SqlDbType.SmallInt, 2, cateId)    
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getcategorybrandlist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        #endregion

        #region 属性分组

        /// <summary>
        /// 获得分类的属性分组列表
        /// </summary>
        /// <param name="cateId">The cate id.</param>
        /// <returns></returns>
        public IDataReader GetAttributeGroupListByCateId(int cateId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@cateid", SqlDbType.SmallInt, 2, cateId)    
                                  };
            string commandText = string.Format("SELECT {1} FROM [{0}attributegroups] WHERE [cateid]=@cateid ORDER BY [displayorder] DESC",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ATTRIBUTE_GROUPS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得属性分组
        /// </summary>
        /// <param name="attrGroupId">属性分组id</param>
        public IDataReader GetAttributeGroupById(int attrGroupId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@attrgroupid", SqlDbType.SmallInt, 2, attrGroupId)    
                                    };
            string commandText = string.Format("SELECT {1} FROM [{0}attributegroups] WHERE [attrgroupid]=@attrgroupid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ATTRIBUTE_GROUPS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 创建属性分组
        /// </summary>
        public void CreateAttributeGroup(AttributeGroupInfo attributeGroupInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@cateid", SqlDbType.SmallInt, 2, attributeGroupInfo.CateId),
                                        GenerateInParam("@name", SqlDbType.NChar, 20, attributeGroupInfo.Name),
                                        GenerateInParam("@displayorder", SqlDbType.Int,4,attributeGroupInfo.DisplayOrder)
                                    };
            string commandText = string.Format("INSERT INTO [{0}attributegroups]([cateid],[name],[displayorder]) VALUES(@cateid,@name,@displayorder)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除属性分组
        /// </summary>
        /// <param name="attrGroupId">属性分组id</param>
        public void DeleteAttributeGroupById(int attrGroupId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@attrgroupid", SqlDbType.SmallInt, 2, attrGroupId)  
                                    };
            string commandText = string.Format("DELETE FROM [{0}attributegroups] WHERE [attrgroupid]=@attrgroupid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新属性分组
        /// </summary>
        /// <param name="newAttributeGroupInfo">新属性分组</param>
        /// <param name="oldAttributeGroupInfo">原属性分组</param>
        public void UpdateAttributeGroup(AttributeGroupInfo attributeGroupInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@cateid", SqlDbType.SmallInt, 2, attributeGroupInfo.CateId),
                                        GenerateInParam("@name", SqlDbType.NChar, 20, attributeGroupInfo.Name),
                                        GenerateInParam("@displayorder", SqlDbType.Int,4,attributeGroupInfo.DisplayOrder),
                                        GenerateInParam("@attrgroupid", SqlDbType.SmallInt, 2, attributeGroupInfo.AttrGroupId)    
                                    };

            string commandText = string.Format("UPDATE [{0}attributegroups] SET [cateid]=@cateid,[name]=@name,[displayorder]=@displayorder WHERE [attrgroupid]=@attrgroupid",
                                                RDBSHelper.RDBSTablePre);
            int effetRowCount = RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
            if (effetRowCount > 0)
            {
                commandText = string.Format("UPDATE [{0}attributevalues] SET [attrgroupname]='{1}',[attrgroupdisplayorder]={2} WHERE [attrgroupid]={3}",
                                             RDBSHelper.RDBSTablePre,
                                             attributeGroupInfo.Name,
                                             attributeGroupInfo.DisplayOrder,
                                             attributeGroupInfo.AttrGroupId);
                RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
            }
        }

        /// <summary>
        /// 通过分类id和属性分组名称获得分组id
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="name">分组名称</param>
        /// <returns></returns>
        public int GetAttrGroupIdByCateIdAndName(int cateId, string name)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@name", SqlDbType.NChar, 20, name),
                                        GenerateInParam("@cateid",SqlDbType.SmallInt,2,cateId)
                                    };

            string commandText = string.Format("SELECT [attrgroupid] FROM [{0}attributegroups] WHERE [cateid]=@cateid AND [name]=@name",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获得属性列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public IDataReader GetAttributeListByCateId(int cateId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@cateid", SqlDbType.SmallInt, 2, cateId)    
                                    };
            string commandText = string.Format("SELECT {1} FROM [{0}attributes] WHERE [cateid]=@cateid ORDER BY [displayorder] DESC",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ATTRIBUTES);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得属性列表
        /// </summary>
        /// <param name="attrGroupId">属性分组id</param>
        /// <returns></returns>
        public IDataReader GetAttributeListByAttrGroupId(int attrGroupId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@attrgroupid", SqlDbType.SmallInt, 2, attrGroupId)    
                                    };
            string commandText = string.Format("SELECT {1} FROM [{0}attributes] WHERE [attrgroupid]=@attrgroupid ORDER BY [displayorder] DESC",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ATTRIBUTES);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得筛选属性列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public IDataReader GetFilterAttributeListByCateId(int cateId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@cateid", SqlDbType.SmallInt, 2, cateId)    
                                    };
            string commandText = string.Format("SELECT {1} FROM [{0}attributes] WHERE [cateid]=@cateid AND [isfilter]=1 ORDER BY [displayorder] DESC",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ATTRIBUTES);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 后台获得属性列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public DataTable AdminGetAttributeList(int cateId, string sort)
        {
            string commandText = string.Format("SELECT [temp1].[attrid],[temp1].[name],[temp1].[cateid],[temp1].[attrgroupid],[temp1].[showtype],[temp1].[isfilter],[temp1].[displayorder],[temp2].[name] AS [attrgroupname] FROM (SELECT {1} FROM [{0}attributes] WHERE [cateid]={3}) AS [temp1] LEFT JOIN [{0}attributegroups] AS [temp2] ON [temp1].[attrgroupid]=[temp2].[attrgroupid] ORDER BY [temp1].{2}",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ATTRIBUTES,
                                                sort,
                                                cateId);
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得属性列表排序
        /// </summary>
        /// <param name="sortColumn">排序字段</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string AdminGetAttributeListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[displayorder]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 获得属性
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        public IDataReader GetAttributeById(int attrId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@attrid", SqlDbType.Int, 4, attrId)    
                                    };
            string commandText = string.Format("SELECT {1} FROM [{0}attributes] WHERE [attrid]=@attrid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ATTRIBUTES);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 创建属性
        /// </summary>
        /// <param name="attributeInfo">属性信息</param>
        /// <param name="attrGroupId">属性组id</param>
        /// <param name="attrGroupName">属性组名称</param>
        /// <param name="attrGroupDisplayOrder">属性组排序</param>
        public void CreateAttribute(AttributeInfo attributeInfo, int attrGroupId, string attrGroupName, int attrGroupDisplayOrder)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@name", SqlDbType.NChar, 30, attributeInfo.Name),
                                        GenerateInParam("@cateid", SqlDbType.SmallInt,2,attributeInfo.CateId),
                                        GenerateInParam("@attrgroupid", SqlDbType.SmallInt,2,attributeInfo.AttrGroupId),
                                        GenerateInParam("@showtype", SqlDbType.TinyInt,1,attributeInfo.ShowType),
                                        GenerateInParam("@isfilter", SqlDbType.TinyInt,1,attributeInfo.IsFilter),
                                        GenerateInParam("@displayorder", SqlDbType.Int,4,attributeInfo.DisplayOrder)
                                    };
            string commandText = string.Format("INSERT INTO [{0}attributes]([name],[cateid],[attrgroupid],[showtype],[isfilter],[displayorder]) VALUES(@name,@cateid,@attrgroupid,@showtype,@isfilter,@displayorder);SELECT SCOPE_IDENTITY();",
                                                RDBSHelper.RDBSTablePre);
            int attrId = TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms));
            if (attrId > 0)
            {
                commandText = string.Format("INSERT INTO [{0}attributevalues]([attrvalue],[isinput],[attrname],[attrdisplayorder],[attrshowtype],[attrvaluedisplayorder],[attrgroupid],[attrgroupname],[attrgroupdisplayorder],[attrid]) VALUES('手动输入',1,'{1}',{2},{3},0,{4},'{5}',{6},{7})",
                                             RDBSHelper.RDBSTablePre,
                                             attributeInfo.Name,
                                             attributeInfo.DisplayOrder,
                                             attributeInfo.ShowType,
                                             attrGroupId,
                                             attrGroupName,
                                             attrGroupDisplayOrder,
                                             attrId);
                RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
            }
        }

        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="attrId">属性id</param>
        public void DeleteAttributeById(int attrId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@attrid", SqlDbType.SmallInt, 2, attrId)    
                                    };
            string commandText = string.Format("DELETE FROM [{0}attributevalues] WHERE [attrid]=@attrid;DELETE FROM [{0}attributes] WHERE [attrid]=@attrid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新属性
        /// </summary>
        /// <param name="newAttributeInfo">新属性</param>
        /// <param name="oldAttributeInfo">原属性</param>
        public void UpdateAttribute(AttributeInfo attributeInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@name", SqlDbType.NChar, 30, attributeInfo.Name),
                                        GenerateInParam("@cateid", SqlDbType.SmallInt,2,attributeInfo.CateId),
                                        GenerateInParam("@attrgroupid", SqlDbType.SmallInt,2,attributeInfo.AttrGroupId),
                                        GenerateInParam("@showtype", SqlDbType.TinyInt,1,attributeInfo.ShowType),
                                        GenerateInParam("@isfilter", SqlDbType.TinyInt,1,attributeInfo.IsFilter),
                                        GenerateInParam("@displayorder", SqlDbType.Int,4,attributeInfo.DisplayOrder),
                                        GenerateInParam("@attrid", SqlDbType.SmallInt, 2, attributeInfo.AttrId)    
                                    };

            string commandText = string.Format("UPDATE [{0}attributes] SET [name]=@name,[cateid]=@cateid,[attrgroupid]=@attrgroupid,[showtype]=@showtype,[isfilter]=@isfilter,[displayorder]=@displayorder WHERE [attrid]=@attrid",
                                                RDBSHelper.RDBSTablePre);
            int effetRowCount = RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
            if (effetRowCount > 0)
            {
                commandText = string.Format("UPDATE [{0}attributevalues] SET [attrname]='{1}',[attrdisplayorder]={2},[attrshowtype]={3} WHERE [attrid]={4}",
                                             RDBSHelper.RDBSTablePre,
                                             attributeInfo.Name,
                                             attributeInfo.DisplayOrder,
                                             attributeInfo.ShowType,
                                             attributeInfo.AttrId);
                RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
            }
        }

        /// <summary>
        /// 通过分类id和属性名称获得属性id
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="attributeName">属性名称</param>
        /// <returns></returns>
        public int GetAttrIdByCateIdAndName(int cateId, string attributeName)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@cateid", SqlDbType.SmallInt, 2, cateId),   
                                        GenerateInParam("@name", SqlDbType.NChar, 30, attributeName)    
                                    };
            string commandText = string.Format("SELECT [attrid] FROM [{0}attributes] WHERE [cateid]=@cateid AND [name]=@name",
                                               RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        #endregion

        #region 属性值

        /// <summary>
        /// 获得属性值
        /// </summary>
        /// <param name="attrValueId">属性值id</param>
        /// <returns></returns>
        public IDataReader GetAttributeValueById(int attrValueId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@attrvalueid", SqlDbType.Int, 4, attrValueId)    
                                    };
            string commandText = string.Format("SELECT {1} FROM [{0}attributevalues] WHERE [attrvalueid]=@attrvalueid;",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ATTRIBUTE_VALUES);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 创建属性值
        /// </summary>
        public void CreateAttributeValue(AttributeValueInfo attributeValueInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@attrvalue", SqlDbType.NChar,70, attributeValueInfo.AttrValue),
                                        GenerateInParam("@isinput", SqlDbType.TinyInt,1, attributeValueInfo.IsInput),
                                        GenerateInParam("@attrname", SqlDbType.NChar,30, attributeValueInfo.AttrName),
                                        GenerateInParam("@attrdisplayorder", SqlDbType.Int,4, attributeValueInfo.AttrDisplayOrder),
                                        GenerateInParam("@attrshowtype", SqlDbType.TinyInt,1, attributeValueInfo.AttrShowType),
                                        GenerateInParam("@attrvaluedisplayorder", SqlDbType.Int,4,attributeValueInfo.AttrValueDisplayOrder),
                                        GenerateInParam("@attrgroupid", SqlDbType.SmallInt,2, attributeValueInfo.AttrGroupId),
                                        GenerateInParam("@attrgroupname", SqlDbType.NChar,20, attributeValueInfo.AttrGroupName),
                                        GenerateInParam("@attrgroupdisplayorder", SqlDbType.Int,4, attributeValueInfo.AttrGroupDisplayOrder),
                                        GenerateInParam("@attrid", SqlDbType.SmallInt,2,attributeValueInfo.AttrId)
                                    };
            string commandText = string.Format("INSERT INTO [{0}attributevalues]([attrvalue],[isinput],[attrname],[attrdisplayorder],[attrshowtype],[attrvaluedisplayorder],[attrgroupid],[attrgroupname],[attrgroupdisplayorder],[attrid]) VALUES(@attrvalue,@isinput,@attrname,@attrdisplayorder,@attrshowtype,@attrvaluedisplayorder,@attrgroupid,@attrgroupname,@attrgroupdisplayorder,@attrid)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除属性值
        /// </summary>
        /// <param name="attrValueId">属性值id</param>
        public void DeleteAttributeValueById(int attrValueId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@attrvalueid", SqlDbType.Int, 4, attrValueId)    
                                    };
            string commandText = string.Format("DELETE FROM [{0}attributevalues] WHERE [attrvalueid]=@attrvalueid;",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新属性值
        /// </summary>
        public void UpdateAttributeValue(AttributeValueInfo attributeValueInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@attrvalue", SqlDbType.NChar,70, attributeValueInfo.AttrValue),
                                        GenerateInParam("@isinput", SqlDbType.TinyInt,1, attributeValueInfo.IsInput),
                                        GenerateInParam("@attrname", SqlDbType.NChar,30, attributeValueInfo.AttrName),
                                        GenerateInParam("@attrdisplayorder", SqlDbType.Int,4, attributeValueInfo.AttrDisplayOrder),
                                        GenerateInParam("@attrshowtype", SqlDbType.TinyInt,1, attributeValueInfo.AttrShowType),
                                        GenerateInParam("@attrvaluedisplayorder", SqlDbType.Int,4,attributeValueInfo.AttrValueDisplayOrder),
                                        GenerateInParam("@attrgroupid", SqlDbType.SmallInt,2, attributeValueInfo.AttrGroupId),
                                        GenerateInParam("@attrgroupname", SqlDbType.NChar,20, attributeValueInfo.AttrGroupName),
                                        GenerateInParam("@attrgroupdisplayorder", SqlDbType.Int,4, attributeValueInfo.AttrGroupDisplayOrder),
                                        GenerateInParam("@attrid", SqlDbType.SmallInt,2,attributeValueInfo.AttrId),
                                        GenerateInParam("@attrvalueid", SqlDbType.Int, 4, attributeValueInfo.AttrValueId)    
                                    };

            string commandText = string.Format("UPDATE [{0}attributevalues] SET [attrvalue]=@attrvalue,[isinput]=@isinput,[attrname]=@attrname,[attrdisplayorder]=@attrdisplayorder,[attrshowtype]=@attrshowtype,[attrvaluedisplayorder]=@attrvaluedisplayorder,[attrgroupid]=@attrgroupid,[attrgroupname]=@attrgroupname,[attrgroupdisplayorder]=@attrgroupdisplayorder,[attrid]=@attrid WHERE [attrvalueid]=@attrvalueid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得属性值列表
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        public IDataReader GetAttributeValueListByAttrId(int attrId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@attrid", SqlDbType.SmallInt, 2, attrId)    
                                    };
            string commandText = string.Format("SELECT {1} FROM [{0}attributevalues] WHERE [attrid]=@attrid ORDER BY [isinput] ASC, [attrvaluedisplayorder] DESC",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ATTRIBUTE_VALUES);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 通过属性id和属性值获得属性值的id
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <param name="attrValue">属性值</param>
        /// <returns></returns>
        public int GetAttributeValueIdByAttrIdAndValue(int attrId, string attrValue)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@attrid", SqlDbType.SmallInt, 2, attrId),
                                        GenerateInParam("@attrvalue", SqlDbType.NChar, 30, attrValue)    
                                    };
            string commandText = string.Format("SELECT [attrvalueid] FROM [{0}attributevalues] WHERE [attrid]=@attrid AND [attrvalue]=@attrvalue",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        #endregion

        #region 商品

        /// <summary>
        /// 后台获得商品列表条件
        /// </summary>
        /// <param name="productName">商品名称</param>
        /// <param name="cateId">分类id</param>
        /// <param name="brandId">品牌id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public string AdminGetProductListCondition(string productName, int cateId, int brandId, int state)
        {
            StringBuilder condition = new StringBuilder();

            if (state > -1)
                condition.AppendFormat(" AND [state]={0} ", (int)state);

            if (!string.IsNullOrWhiteSpace(productName))
                condition.AppendFormat(" AND [name] like '{0}%' ", productName);

            if (cateId > 0)
                condition.AppendFormat(" AND [cateid] = {0} ", cateId);

            if (brandId > 0)
                condition.AppendFormat(" AND [brandid] = {0} ", brandId);

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        /// <summary>
        /// 后台获得商品列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string AdminGetProductListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[displayorder]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public DataTable AdminGetProductList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT [temp1].[pid],[temp1].[name],[temp1].[shopprice],[temp1].[marketprice],[temp1].[state],[temp1].[isbest],[temp1].[ishot],[temp1].[isnew],[temp1].[displayorder],[temp2].[number] FROM (SELECT TOP {0} [pid],[name],[shopprice],[marketprice],[state],[isbest],[ishot],[isnew],[displayorder] FROM [{1}products] ORDER BY {2}) AS [temp1] LEFT JOIN [{1}productstocks] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT [temp1].[pid],[temp1].[name],[temp1].[shopprice],[temp1].[marketprice],[temp1].[state],[temp1].[isbest],[temp1].[ishot],[temp1].[isnew],[temp1].[displayorder],[temp2].[number] FROM (SELECT TOP {0} [pid],[name],[shopprice],[marketprice],[state],[isbest],[ishot],[isnew],[displayorder] FROM [{1}products] WHERE {3} ORDER BY {2}) AS [temp1] LEFT JOIN [{1}productstocks] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT [temp1].[pid],[temp1].[name],[temp1].[shopprice],[temp1].[marketprice],[temp1].[state],[temp1].[isbest],[temp1].[ishot],[temp1].[isnew],[temp1].[displayorder],[temp2].[number] FROM (SELECT TOP {0} [pid],[name],[shopprice],[marketprice],[state],[isbest],[ishot],[isnew],[displayorder] FROM [{1}products] WHERE [pid] NOT IN (SELECT TOP {3} [pid] FROM [{1}products] ORDER BY {2}) ORDER BY {2}) AS [temp1] LEFT JOIN [{1}productstocks] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                (pageNumber - 1) * pageSize);
                else
                    commandText = string.Format("SELECT [temp1].[pid],[temp1].[name],[temp1].[shopprice],[temp1].[marketprice],[temp1].[state],[temp1].[isbest],[temp1].[ishot],[temp1].[isnew],[temp1].[displayorder],[temp2].[number] FROM (SELECT TOP {0} [pid],[name],[shopprice],[marketprice],[state],[isbest],[ishot],[isnew],[displayorder] FROM [{1}products] WHERE [pid] NOT IN (SELECT TOP {3} [pid] FROM [{1}products] WHERE {4} ORDER BY {2}) AND {4} ORDER BY {2}) AS [temp1] LEFT JOIN [{1}productstocks] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                (pageNumber - 1) * pageSize,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得商品数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int AdminGetProductCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(pid) FROM [{0}products]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(pid) FROM [{0}products] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 创建商品
        /// </summary>
        /// <param name="productInfo">商品信息</param>
        /// <returns>商品id</returns>
        public int CreateProduct(ProductInfo productInfo)
        {
            DbParameter[] parms = {
									 GenerateInParam("@psn",SqlDbType.Char,30,productInfo.PSN),
									 GenerateInParam("@cateid",SqlDbType.SmallInt,2,productInfo.CateId),
									 GenerateInParam("@brandid",SqlDbType.Int,4,productInfo.BrandId),
									 GenerateInParam("@skugid",SqlDbType.Int,4,productInfo.SKUGid),
									 GenerateInParam("@name",SqlDbType.NVarChar,200,productInfo.Name),
									 GenerateInParam("@shopprice",SqlDbType.Decimal,4,productInfo.ShopPrice),
									 GenerateInParam("@marketprice",SqlDbType.Decimal,4,productInfo.MarketPrice),
									 GenerateInParam("@costprice",SqlDbType.Decimal,4,productInfo.CostPrice),
									 GenerateInParam("@state",SqlDbType.TinyInt,1,productInfo.State),
                                     GenerateInParam("@isbest",SqlDbType.TinyInt,1,productInfo.IsBest),
									 GenerateInParam("@ishot",SqlDbType.TinyInt,1,productInfo.IsHot),
									 GenerateInParam("@isnew",SqlDbType.TinyInt,1,productInfo.IsNew),
									 GenerateInParam("@displayorder",SqlDbType.Int,4,productInfo.DisplayOrder),
                                     GenerateInParam("@weight",SqlDbType.Int,4,productInfo.Weight),
									 GenerateInParam("@showimg",SqlDbType.NVarChar,100,productInfo.ShowImg),
                                     GenerateInParam("@salecount",SqlDbType.Int,4,productInfo.SaleCount),
                                     GenerateInParam("@visitcount",SqlDbType.Int,4,productInfo.VisitCount),
                                     GenerateInParam("@reviewcount",SqlDbType.Int,4,productInfo.ReviewCount),
                                     GenerateInParam("@star1",SqlDbType.Int,4,productInfo.Star1),
                                     GenerateInParam("@star2",SqlDbType.Int,4,productInfo.Star2),
                                     GenerateInParam("@star3",SqlDbType.Int,4,productInfo.Star3),
                                     GenerateInParam("@star4",SqlDbType.Int,4,productInfo.Star4),
                                     GenerateInParam("@star5",SqlDbType.Int,4,productInfo.Star5),
									 GenerateInParam("@addtime",SqlDbType.DateTime,8,productInfo.AddTime),
									 GenerateInParam("@description",SqlDbType.NText,0,productInfo.Description)
                                   };

            string commandText = string.Format(@"INSERT INTO [{0}products]([psn],[cateid],[brandid],[skugid],[name],[shopprice],[marketprice],[costprice],[state],[isbest],[ishot],[isnew],
                                                [weight],[displayorder],[showimg],[salecount],[visitcount],[reviewcount],[star1],[star2],[star3],[star4],[star5],[addtime],[description])  
                                                VALUES(@psn,@cateid,@brandid,@skugid,@name,@shopprice,@marketprice,@costprice,@state,@isbest,@ishot,@isnew,
                                                @weight,@displayorder,@showimg,@salecount,@visitcount,@reviewcount,@star1,@star2,@star3,@star4,@star5,@addtime,@description);
                                                SELECT SCOPE_IDENTITY();",
                                                RDBSHelper.RDBSTablePre);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        /// <summary>
        /// 更新商品
        /// </summary>
        public void UpdateProduct(ProductInfo productInfo)
        {
            DbParameter[] parms = {
									GenerateInParam("@psn",SqlDbType.Char,30,productInfo.PSN),
									GenerateInParam("@cateid",SqlDbType.SmallInt,2,productInfo.CateId),
									GenerateInParam("@brandid",SqlDbType.Int,4,productInfo.BrandId),
									GenerateInParam("@skugid",SqlDbType.Int,4,productInfo.SKUGid),
									GenerateInParam("@name",SqlDbType.NVarChar,200,productInfo.Name),
									GenerateInParam("@shopprice",SqlDbType.Decimal,4,productInfo.ShopPrice),
									GenerateInParam("@marketprice",SqlDbType.Decimal,4,productInfo.MarketPrice),
									GenerateInParam("@costprice",SqlDbType.Decimal,4,productInfo.CostPrice),
									GenerateInParam("@state",SqlDbType.TinyInt,1,productInfo.State),
                                    GenerateInParam("@isbest",SqlDbType.TinyInt,1,productInfo.IsBest),
									GenerateInParam("@ishot",SqlDbType.TinyInt,1,productInfo.IsHot),
									GenerateInParam("@isnew",SqlDbType.TinyInt,1,productInfo.IsNew),
									GenerateInParam("@displayorder",SqlDbType.Int,4,productInfo.DisplayOrder),
                                    GenerateInParam("@weight",SqlDbType.Int,4,productInfo.Weight),
									GenerateInParam("@showimg",SqlDbType.NVarChar,100,productInfo.ShowImg),
                                    GenerateInParam("@salecount",SqlDbType.Int,4,productInfo.SaleCount),
                                    GenerateInParam("@visitcount",SqlDbType.Int,4,productInfo.VisitCount),
                                    GenerateInParam("@reviewcount",SqlDbType.Int,4,productInfo.ReviewCount),
                                    GenerateInParam("@star1",SqlDbType.Int,4,productInfo.Star1),
                                    GenerateInParam("@star2",SqlDbType.Int,4,productInfo.Star2),
                                    GenerateInParam("@star3",SqlDbType.Int,4,productInfo.Star3),
                                    GenerateInParam("@star4",SqlDbType.Int,4,productInfo.Star4),
                                    GenerateInParam("@star5",SqlDbType.Int,4,productInfo.Star5),
									GenerateInParam("@addtime",SqlDbType.DateTime,8,productInfo.AddTime),
									GenerateInParam("@description",SqlDbType.NText,0,productInfo.Description),
                                    GenerateInParam("@pid",SqlDbType.Int,4,productInfo.Pid)
                                   };

            string commandText = string.Format(@"UPDATE [{0}products] SET [psn]=@psn,[cateid]=@cateid,[brandid]=@brandid,[skugid]=@skugid,[name]=@name,[shopprice]=@shopprice,[marketprice]=@marketprice,[costprice]=@costprice,
                                                 [state]=@state,[isbest]=@isbest,[ishot]=@ishot,[isnew]=@isnew,[displayorder]=@displayorder,[weight]=@weight,[showimg]=@showimg,[salecount]=@salecount,[visitcount]=@visitcount,[reviewcount]=@reviewcount,
                                                 [star1]=@star1,[star2]=@star2,[star3]=@star3,[star4]=@star4,[star5]=@star5,[addtime]=@addtime,[description]=@description
                                                 WHERE [pid]=@pid",
                                                 RDBSHelper.RDBSTablePre);

            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="pidList">商品id</param>
        public void DeleteProductById(string pidList)
        {
            string commandText = string.Format(@"DELETE FROM [{0}couponproducts] WHERE [pid] IN ({1});
                                                 DELETE FROM [{0}fullcutproducts] WHERE [pid] IN ({1});
                                                 DELETE FROM [{0}fullsendproducts] WHERE [pid] IN ({1});
                                                 DELETE FROM [{0}suitproducts] WHERE [pid] IN ({1});
                                                 DELETE FROM [{0}buysendproducts] WHERE [pid] IN ({1});
                                                 DELETE FROM [{0}gifts] WHERE [pid] IN ({1}) OR [giftid] IN ({1});
                                                 DELETE FROM [{0}giftpromotions] WHERE [pid] IN ({1});
                                                 DELETE FROM [{0}singlepromotions] WHERE [pid] IN ({1});
                                                 DELETE FROM [{0}timeproducts] WHERE [pid] IN ({1});
                                                 DELETE FROM [{0}signproducts] WHERE [pid] IN ({1});
                                                 DELETE FROM [{0}relateproducts] WHERE [pid] IN ({1}) OR [relatepid] IN ({1});
                                                 DELETE FROM [{0}productstats] WHERE [pid] IN ({1});
                                                 DELETE FROM [{0}productkeywords] WHERE [pid] IN ({1});
                                                 DELETE FROM [{0}productimages] WHERE [pid] IN ({1});
                                                 DELETE FROM [{0}productattributes] WHERE [pid] IN ({1});
                                                 DELETE FROM [{0}productskus] WHERE [pid] IN ({1});
                                                 DELETE FROM [{0}productstocks] WHERE [pid] IN ({1});
                                                 DELETE FROM [{0}products] WHERE [pid] IN ({1});",
                                               RDBSHelper.RDBSTablePre,
                                               pidList);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 后台获得商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public IDataReader AdminGetProductById(int pid)
        {
            DbParameter[] parms =  { 
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                    };
            string commandText = string.Format("SELECT {0} FROM [{1}products] WHERE [pid]=@pid",
                                                RDBSFields.PRODUCTS,
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 后台获得部分商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public IDataReader AdminGetPartProductById(int pid)
        {
            DbParameter[] parms =  { 
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                    };
            string commandText = string.Format("SELECT {0} FROM [{1}products] WHERE [pid]=@pid",
                                                RDBSFields.PART_PRODUCTS,
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public IDataReader GetProductById(int pid)
        {
            DbParameter[] parms =  { 
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getproductbyid", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得部分商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public IDataReader GetPartProductById(int pid)
        {
            DbParameter[] parms =  { 
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getpartproductbyid", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 修改商品状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="state">商品状态</param>
        public bool UpdateProductState(string pidList, ProductState state)
        {
            string commandText = string.Format("UPDATE [{0}products] SET [state]={1} WHERE [pid] IN ({2})",
                                                RDBSHelper.RDBSTablePre,
                                                (int)state,
                                                pidList);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText), -1) > 0;
        }

        /// <summary>
        /// 修改商品排序
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="displayOrder">商品排序</param>
        public bool UpdateProductDisplayOrder(int pid, int displayOrder)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@displayorder", SqlDbType.Int, 4, displayOrder),
                                     GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                   };
            string commandText = string.Format("UPDATE [{0}products] SET [displayorder]=@displayorder WHERE [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms), -1) > 0;
        }

        /// <summary>
        /// 改变商品新品状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="isNew">是否新品</param>
        public bool ChangeProductIsNew(string pidList, int isNew)
        {
            string commandText = string.Format("UPDATE [{0}products] SET [isnew]={1} WHERE [pid] IN ({2})",
                                                RDBSHelper.RDBSTablePre,
                                                isNew,
                                                pidList);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText), -1) > 0;
        }

        /// <summary>
        /// 改变商品热销状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="isHot">是否热销</param>
        public bool ChangeProductIsHot(string pidList, int isHot)
        {
            string commandText = string.Format("UPDATE [{0}products] SET [ishot]={1} WHERE [pid] IN ({2})",
                                                RDBSHelper.RDBSTablePre,
                                                isHot,
                                                pidList);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText), -1) > 0;
        }

        /// <summary>
        /// 改变商品精品状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="isBest">是否精品</param>
        public bool ChangeProductIsBest(string pidList, int isBest)
        {
            string commandText = string.Format("UPDATE [{0}products] SET [isbest]={1} WHERE [pid] IN ({2})",
                                                RDBSHelper.RDBSTablePre,
                                                isBest,
                                                pidList);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText), -1) > 0;
        }

        /// <summary>
        /// 修改商品商城价格
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="shopPrice">商城价格</param>
        public bool UpdateProductShopPrice(int pid, decimal shopPrice)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@shopprice", SqlDbType.Decimal, 4, shopPrice),
                                     GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                   };
            string commandText = string.Format("UPDATE [{0}products] SET [shopprice]=@shopprice WHERE [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms), -1) > 0;
        }

        /// <summary>
        /// 更新商品图片
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="showImg">商品图片</param>
        public void UpdateProductShowImage(int pid, string showImg)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@showimg", SqlDbType.NVarChar, 100, showImg),
                                     GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                   };
            string commandText = string.Format("UPDATE [{0}products] SET [showimg]=@showimg WHERE [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 后台通过商品名称获得商品id
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <returns></returns>
        public int AdminGetProductIdByName(string name)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@name", SqlDbType.NVarChar, 200, name)
                                   };
            string commandText = string.Format("SELECT [pid] FROM [{0}products] WHERE [name]=@name",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms));
        }

        /// <summary>
        /// 获得部分商品列表
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        public IDataReader GetPartProductList(string pidList)
        {
            DbParameter[] parms =  { 
                                        GenerateInParam("@pidlist", SqlDbType.NVarChar, 1000, pidList)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getpartproductlistbypidlist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得商品的影子访问数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public int GetProductShadowVisitCountById(int pid)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                   };
            string commandText = string.Format("SELECT [visitcount] FROM [{0}products] WHERE [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        /// <summary>
        /// 更新商品的影子访问数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="visitCount">访问数量</param>
        public void UpdateProductShadowVisitCount(int pid, int visitCount)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@visitcount", SqlDbType.Int, 4, visitCount),
                                     GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                   };
            string commandText = string.Format("UPDATE [{0}products] SET [visitcount]=@visitcount WHERE [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 增加商品的影子评价数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="saleCount">销售数量</param>
        public void AddProductShadowSaleCount(int pid, int saleCount)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@salecount", SqlDbType.Int, 4, saleCount),
                                     GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                   };
            string commandText = string.Format("UPDATE [{0}products] SET [salecount]=[salecount]+@salecount WHERE [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 增加商品的影子评价数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="starType">星星类型</param>
        public void AddProductShadowReviewCount(int pid, int starType)
        {
            string commandText = string.Format("UPDATE [{0}products] SET [reviewcount]=[reviewcount]+1,[star{2}]=[star{2}]+1 WHERE [pid]={1}",
                                                RDBSHelper.RDBSTablePre,
                                                pid,
                                                starType);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得分类商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="cateId">分类id</param>
        /// <param name="brandId">品牌id</param>
        /// <param name="filterPrice">筛选价格</param>
        /// <param name="catePriceRangeList">分类价格范围列表</param>
        /// <param name="attrValueIdList">属性值id列表</param>
        /// <param name="onlyStock">是否只显示有货</param>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public IDataReader GetCategoryProductList(int pageSize, int pageNumber, int cateId, int brandId, int filterPrice, string[] catePriceRangeList, List<int> attrValueIdList, int onlyStock, int sortColumn, int sortDirection)
        {
            StringBuilder commandText = new StringBuilder();

            if (pageNumber == 1)
            {
                commandText.AppendFormat("SELECT TOP {1} [p].[pid],[p].[psn],[p].[cateid],[p].[brandid],[p].[skugid],[p].[name],[p].[shopprice],[p].[marketprice],[p].[costprice],[p].[state],[p].[isbest],[p].[ishot],[p].[isnew],[p].[displayorder],[p].[weight],[p].[showimg],[p].[salecount],[p].[visitcount],[p].[reviewcount],[p].[star1],[p].[star2],[p].[star3],[p].[star4],[p].[star5],[p].[addtime] FROM [{0}products] AS [p]", RDBSHelper.RDBSTablePre, pageSize);

                if (onlyStock == 1)
                    commandText.AppendFormat(" LEFT JOIN [{0}productstocks] AS [ps] ON [p].[pid]=[ps].[pid]", RDBSHelper.RDBSTablePre);

                commandText.AppendFormat(" WHERE [p].[cateid]={0}", cateId);

                if (brandId > 0)
                    commandText.AppendFormat(" AND [p].[brandid]={0}", brandId);

                if (filterPrice > 0 && filterPrice <= catePriceRangeList.Length)
                {
                    string[] priceRange = StringHelper.SplitString(catePriceRangeList[filterPrice - 1], "-");
                    if (priceRange.Length == 1)
                        commandText.AppendFormat(" AND [p].[shopprice]>='{0}'", priceRange[0]);
                    else if (priceRange.Length == 2)
                        commandText.AppendFormat(" AND [p].[shopprice]>='{0}' AND [p].[shopprice]<'{1}'", priceRange[0], priceRange[1]);
                }

                commandText.Append(" AND [p].[state]=0");

                if (attrValueIdList.Count > 0)
                {
                    commandText.Append(" AND [p].[pid] IN (SELECT [pa1].[pid] FROM");
                    for (int i = 0; i < attrValueIdList.Count; i++)
                    {
                        if (i == 0)
                            commandText.AppendFormat(" (SELECT [pid] FROM [{0}productattributes] WHERE [attrvalueid]={1}) AS [pa1]", RDBSHelper.RDBSTablePre, attrValueIdList[i]);
                        else
                            commandText.AppendFormat(" INNER JOIN (SELECT [pid] FROM [{0}productattributes] WHERE [attrvalueid]={1}) AS [pa{2}] ON [pa{2}].[pid]=[pa{3}].[pid]", RDBSHelper.RDBSTablePre, attrValueIdList[i], i + 1, i);
                    }
                    commandText.Append(")");
                }

                if (onlyStock == 1)
                    commandText.Append(" AND [ps].[number]>0");

                commandText.Append(" ORDER BY ");
                switch (sortColumn)
                {
                    case 0:
                        commandText.Append("[p].[salecount]");
                        break;
                    case 1:
                        commandText.Append("[p].[shopprice]");
                        break;
                    case 2:
                        commandText.Append("[p].[reviewcount]");
                        break;
                    case 3:
                        commandText.Append("[p].[addtime]");
                        break;
                    case 4:
                        commandText.Append("[p].[visitcount]");
                        break;
                    default:
                        commandText.Append("[p].[salecount]");
                        break;
                }
                switch (sortDirection)
                {
                    case 0:
                        commandText.Append(" DESC");
                        break;
                    case 1:
                        commandText.Append(" ASC");
                        break;
                    default:
                        commandText.Append(" DESC");
                        break;
                }
            }
            else
            {
                commandText.Append("SELECT [pid],[psn],[cateid],[brandid],[skugid],[name],[shopprice],[marketprice],[costprice],[state],[isbest],[ishot],[isnew],[displayorder],[weight],[showimg],[salecount],[visitcount],[reviewcount],[star1],[star2],[star3],[star4],[star5],[addtime] FROM");
                commandText.Append(" (SELECT ROW_NUMBER() OVER (ORDER BY ");
                switch (sortColumn)
                {
                    case 0:
                        commandText.Append("[p].[salecount]");
                        break;
                    case 1:
                        commandText.Append("[p].[shopprice]");
                        break;
                    case 2:
                        commandText.Append("[p].[reviewcount]");
                        break;
                    case 3:
                        commandText.Append("[p].[addtime]");
                        break;
                    case 4:
                        commandText.Append("[p].[visitcount]");
                        break;
                    default:
                        commandText.Append("[p].[salecount]");
                        break;
                }
                switch (sortDirection)
                {
                    case 0:
                        commandText.Append(" DESC");
                        break;
                    case 1:
                        commandText.Append(" ASC");
                        break;
                    default:
                        commandText.Append(" DESC");
                        break;
                }
                commandText.AppendFormat(") AS [rowid],[p].[pid],[p].[psn],[p].[cateid],[p].[brandid],[p].[skugid],[p].[name],[p].[shopprice],[p].[marketprice],[p].[costprice],[p].[state],[p].[isbest],[p].[ishot],[p].[isnew],[p].[displayorder],[p].[weight],[p].[showimg],[p].[salecount],[p].[visitcount],[p].[reviewcount],[p].[star1],[p].[star2],[p].[star3],[p].[star4],[p].[star5],[p].[addtime] FROM [{0}products] AS [p]", RDBSHelper.RDBSTablePre);
                if (onlyStock == 1)
                    commandText.AppendFormat(" LEFT JOIN [{0}productstocks] AS [ps] ON [p].[pid]=[ps].[pid]", RDBSHelper.RDBSTablePre);

                commandText.AppendFormat(" WHERE [p].[cateid]={0}", cateId);

                if (brandId > 0)
                    commandText.AppendFormat(" AND [p].[brandid]={0}", brandId);

                if (filterPrice > 0 && filterPrice <= catePriceRangeList.Length)
                {
                    string[] priceRange = StringHelper.SplitString(catePriceRangeList[filterPrice - 1], "-");
                    if (priceRange.Length == 1)
                        commandText.AppendFormat(" AND [p].[shopprice]>='{0}'", priceRange[0]);
                    else if (priceRange.Length == 2)
                        commandText.AppendFormat(" AND [p].[shopprice]>='{0}' AND [p].[shopprice]<'{1}'", priceRange[0], priceRange[1]);
                }

                commandText.Append(" AND [p].[state]=0");

                if (attrValueIdList.Count > 0)
                {
                    commandText.Append(" AND [p].[pid] IN (SELECT [pa1].[pid] FROM");
                    for (int i = 0; i < attrValueIdList.Count; i++)
                    {
                        if (i == 0)
                            commandText.AppendFormat(" (SELECT [pid] FROM [{0}productattributes] WHERE [attrvalueid]={1}) AS [pa1]", RDBSHelper.RDBSTablePre, attrValueIdList[i]);
                        else
                            commandText.AppendFormat(" INNER JOIN (SELECT [pid] FROM [{0}productattributes] WHERE [attrvalueid]={1}) AS [pa{2}] ON [pa{2}].[pid]=[pa{3}].[pid]", RDBSHelper.RDBSTablePre, attrValueIdList[i], i + 1, i);
                    }
                    commandText.Append(")");
                }

                if (onlyStock == 1)
                    commandText.Append(" AND [ps].[number]>0");

                commandText.Append(") AS [temp]");
                commandText.AppendFormat(" WHERE [rowid] BETWEEN {0} AND {1}", pageSize * (pageNumber - 1) + 1, pageSize * pageNumber);

            }

            return RDBSHelper.ExecuteReader(CommandType.Text, commandText.ToString());
        }

        /// <summary>
        /// 获得分类商品数量
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="brandId">品牌id</param>
        /// <param name="filterPrice">筛选价格</param>
        /// <param name="catePriceRangeList">分类价格范围列表</param>
        /// <param name="attrValueIdList">属性值id列表</param>
        /// <param name="onlyStock">是否只显示有货</param>
        /// <returns></returns>
        public int GetCategoryProductCount(int cateId, int brandId, int filterPrice, string[] catePriceRangeList, List<int> attrValueIdList, int onlyStock)
        {
            StringBuilder commandText = new StringBuilder();

            commandText.AppendFormat("SELECT COUNT([p].[pid]) FROM [{0}products] AS [p]", RDBSHelper.RDBSTablePre);

            if (onlyStock == 1)
                commandText.AppendFormat(" LEFT JOIN [{0}productstocks] AS [ps] ON [p].[pid]=[ps].[pid]", RDBSHelper.RDBSTablePre);

            commandText.AppendFormat(" WHERE [p].[cateid]={0}", cateId);

            if (brandId > 0)
                commandText.AppendFormat(" AND [p].[brandid]={0}", brandId);

            if (filterPrice > 0 && filterPrice <= catePriceRangeList.Length)
            {
                string[] priceRange = StringHelper.SplitString(catePriceRangeList[filterPrice - 1], "-");
                if (priceRange.Length == 1)
                    commandText.AppendFormat(" AND [p].[shopprice]>='{0}'", priceRange[0]);
                else if (priceRange.Length == 2)
                    commandText.AppendFormat(" AND [p].[shopprice]>='{0}' AND [p].[shopprice]<'{1}'", priceRange[0], priceRange[1]);
            }

            commandText.Append(" AND [p].[state]=0");

            if (attrValueIdList.Count > 0)
            {
                commandText.Append(" AND [p].[pid] IN (SELECT [pa1].[pid] FROM");
                for (int i = 0; i < attrValueIdList.Count; i++)
                {
                    if (i == 0)
                        commandText.AppendFormat(" (SELECT [pid] FROM [{0}productattributes] WHERE [attrvalueid]={1}) AS [pa1]", RDBSHelper.RDBSTablePre, attrValueIdList[i]);
                    else
                        commandText.AppendFormat(" INNER JOIN (SELECT [pid] FROM [{0}productattributes] WHERE [attrvalueid]={1}) AS [pa{2}] ON [pa{2}].[pid]=[pa{3}].[pid]", RDBSHelper.RDBSTablePre, attrValueIdList[i], i + 1, i);
                }
                commandText.Append(")");
            }

            if (onlyStock == 1)
                commandText.Append(" AND [ps].[number]>0");

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText.ToString()));
        }

        /// <summary>
        /// 获得商品汇总列表
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        public DataTable GetProductSummaryList(string pidList)
        {
            string commandText = string.Format("SELECT [pid],[psn],[cateid],[brandid],[skugid],[name],[shopprice],[marketprice],[costprice],[state],[isbest],[ishot],[isnew],[displayorder],[weight],[showimg],[salecount],[visitcount],[reviewcount] FROM [{0}products] WHERE [pid] IN ({1}) ORDER BY CHARINDEX(','+CONVERT(varchar(10),[pid])+',',','+'{1}'+',')",
                                                RDBSHelper.RDBSTablePre,
                                                pidList);
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得指定品牌商品的数量
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        public int AdminGetBrandProductCount(int brandId)
        {
            DbParameter[] parms =  { 
                                        GenerateInParam("@brandid", SqlDbType.Int, 4, brandId)
                                    };
            string commandText = string.Format("SELECT COUNT([pid]) FROM [{0}products] WHERE [brandid]=@brandid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        /// <summary>
        /// 后台获得指定分类商品的数量
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public int AdminGetCategoryProductCount(int cateId)
        {
            DbParameter[] parms =  { 
                                        GenerateInParam("@cateid", SqlDbType.SmallInt, 2, cateId)
                                    };
            string commandText = string.Format("SELECT COUNT([pid]) FROM [{0}products] WHERE [cateid]=@cateid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        /// <summary>
        /// 后台获得属性值商品的数量
        /// </summary>
        /// <param name="attrValueId">属性值id</param>
        /// <returns></returns>
        public int AdminGetAttrValueProductCount(int attrValueId)
        {
            DbParameter[] parms =  { 
                                        GenerateInParam("@attrvalueid", SqlDbType.Int, 4, attrValueId)
                                    };
            string commandText = string.Format("SELECT (SELECT COUNT([recordid]) FROM [{0}productattributes] WHERE [attrvalueid]=@attrvalueid)+(SELECT COUNT([recordid]) FROM [{0}productskus] WHERE [attrvalueid]=@attrvalueid) AS [count]",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        #endregion

        #region 商品属性

        /// <summary>
        /// 创建商品属性
        /// </summary>
        public bool CreateProductAttribute(ProductAttributeInfo productAttributeInfo)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@pid", SqlDbType.Int, 4, productAttributeInfo.Pid),
                                     GenerateInParam("@attrid", SqlDbType.SmallInt, 2, productAttributeInfo.AttrId),
                                     GenerateInParam("@attrvalueid", SqlDbType.Int, 4, productAttributeInfo.AttrValueId),
                                     GenerateInParam("@inputvalue", SqlDbType.NVarChar, 100, productAttributeInfo.InputValue)
                                   };
            string commandText = string.Format("INSERT INTO [{0}productattributes]([pid],[attrid],[attrvalueid],[inputvalue]) VALUES(@pid,@attrid,@attrvalueid,@inputvalue)",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0;
        }

        /// <summary>
        /// 更新商品属性
        /// </summary>
        public bool UpdateProductAttribute(ProductAttributeInfo productAttributeInfo)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@pid", SqlDbType.Int, 4, productAttributeInfo.Pid),
                                     GenerateInParam("@attrid", SqlDbType.SmallInt, 2, productAttributeInfo.AttrId),
                                     GenerateInParam("@attrvalueid", SqlDbType.Int, 4, productAttributeInfo.AttrValueId),
                                     GenerateInParam("@inputvalue", SqlDbType.NVarChar, 100, productAttributeInfo.InputValue),
                                     GenerateInParam("@recordid", SqlDbType.Int, 4, productAttributeInfo.RecordId)
                                   };
            string commandText = string.Format("UPDATE [{0}productattributes] SET [pid]=@pid,[attrid]=@attrid,[attrvalueid]=@attrvalueid,[inputvalue]=@inputvalue WHERE [recordid]=@recordid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms)) > 0;
        }

        /// <summary>
        /// 删除商品属性
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        public bool DeleteProductAttributeByPidAndAttrId(int pid, int attrId)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@pid", SqlDbType.Int, 4, pid),
                                     GenerateInParam("@attrid", SqlDbType.SmallInt, 2, attrId)
                                   };
            string commandText = string.Format("DELETE FROM [{0}productattributes] WHERE [pid]=@pid AND [attrid]=@attrid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms)) > 0;
        }

        /// <summary>
        /// 获得商品属性
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        public IDataReader GetProductAttributeByPidAndAttrId(int pid, int attrId)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@pid", SqlDbType.Int, 4, pid),
                                     GenerateInParam("@attrid", SqlDbType.SmallInt, 2, attrId)
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}productattributes] WHERE [pid]=@pid AND [attrid]=@attrid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.PRODUCT_ATTRIBUTES);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得商品属性列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public IDataReader GetProductAttributeList(int pid)
        {
            DbParameter[] parms =  { 
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                    };
            string commandText = string.Format("SELECT {0} FROM [{1}productattributes] WHERE [pid]=@pid",
                                                RDBSFields.PRODUCT_ATTRIBUTES,
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得扩展商品属性列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public IDataReader GetExtProductAttributeList(int pid)
        {
            DbParameter[] parms =  { 
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getextproductattributelist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        #endregion

        #region 商品SKU

        /// <summary>
        /// 创建商品sku项
        /// </summary>
        /// <param name="productSKUItemInfo">商品sku项信息</param>
        public void CreateProductSKUItem(ProductSKUItemInfo productSKUItemInfo)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@skugid", SqlDbType.Int, 4, productSKUItemInfo.SKUGid),
                                     GenerateInParam("@pid", SqlDbType.Int, 4, productSKUItemInfo.Pid),
                                     GenerateInParam("@attrid", SqlDbType.SmallInt, 2, productSKUItemInfo.AttrId),
                                     GenerateInParam("@attrvalueid", SqlDbType.Int, 4, productSKUItemInfo.AttrValueId),
                                     GenerateInParam("@inputvalue", SqlDbType.NVarChar, 100, productSKUItemInfo.InputValue)
                                   };
            string commandText = string.Format("INSERT INTO [{0}productskus]([skugid],[pid],[attrid],[attrvalueid],[inputvalue]) VALUES(@skugid,@pid,@attrid,@attrvalueid,@inputvalue)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得商品的sku项列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public DataTable GetProductSKUItemList(int pid)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                   };
            return RDBSHelper.ExecuteDataset(CommandType.StoredProcedure,
                                             string.Format("{0}getproductskuitemlist", RDBSHelper.RDBSTablePre),
                                             parms).Tables[0];
        }

        /// <summary>
        /// 判断sku组id是否存在
        /// </summary>
        /// <param name="skuGid">sku组id</param>
        /// <returns></returns>
        public bool IsExistSKUGid(int skuGid)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@skugid", SqlDbType.Int, 4, skuGid)
                                   };
            string commandText = string.Format("SELECT [recordid] FROM [{0}productskus] WHERE [skugid]=@skugid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms)) > 0;
        }

        /// <summary>
        /// 获得商品的sku列表
        /// </summary>
        /// <param name="skuGid">sku组id</param>
        /// <returns></returns>
        public IDataReader GetProductSKUListBySKUGid(int skuGid)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@skugid", SqlDbType.Int, 4, skuGid)
                                   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getproductskulistbyskugid", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        #endregion

        #region  商品图片

        /// <summary>
        /// 创建商品图片
        /// </summary>
        public bool CreateProductImage(ProductImageInfo productImageInfo)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@pid", SqlDbType.Int, 4, productImageInfo.Pid),
                                     GenerateInParam("@showimg", SqlDbType.NVarChar, 100, productImageInfo.ShowImg),
                                     GenerateInParam("@ismain", SqlDbType.TinyInt, 1, productImageInfo.IsMain),
                                     GenerateInParam("@displayorder", SqlDbType.Int, 4, productImageInfo.DisplayOrder)
                                   };
            string commandText = string.Format("INSERT INTO [{0}productimages]([pid],[showimg],[ismain],[displayorder]) VALUES(@pid,@showimg,@ismain,@displayorder)",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms), -1) > 0;
        }

        /// <summary>
        /// 获得商品图片
        /// </summary>
        /// <param name="pImgId">图片id</param>
        /// <returns></returns>
        public IDataReader GetProductImageById(int pImgId)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@pimgid", SqlDbType.Int, 4, pImgId)
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}productimages] WHERE [pimgid]=@pimgid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.PRODUCT_IMAGES);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除商品图片
        /// </summary>
        /// <param name="pImgId">图片id</param>
        /// <returns></returns>
        public bool DeleteProductImageById(int pImgId)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@pimgid", SqlDbType.Int, 4, pImgId)
                                   };
            string commandText = string.Format("DELETE FROM [{0}productimages] WHERE [pimgid]=@pimgid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms), -1) > 0;
        }

        /// <summary>
        /// 设置图片为商品主图
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="pImgId">商品图片id</param>
        /// <returns></returns>
        public bool SetProductMainImage(int pid, int pImgId)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                   };
            string commandText = string.Format("UPDATE [{0}productimages] SET [ismain]=0 WHERE [pid]=@pid AND [ismain]=1",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);

            parms = new DbParameter[] {
                                         GenerateInParam("@pimgid", SqlDbType.Int, 4, pImgId)
                                       };
            commandText = string.Format("UPDATE [{0}productimages] SET [ismain]=1 WHERE [pimgid]=@pimgid",
                                         RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms), -1) > 0;
        }

        /// <summary>
        /// 改变商品图片排序
        /// </summary>
        /// <param name="pImgId">商品图片id</param>
        /// <param name="showImg">图片排序</param>
        /// <returns></returns>
        public bool ChangeProductImageDisplayOrder(int pImgId, int displayOrder)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@displayorder", SqlDbType.Int, 4, displayOrder),
                                     GenerateInParam("@pimgid", SqlDbType.Int, 4, pImgId)
                                   };
            string commandText = string.Format("UPDATE [{0}productimages] SET [displayorder]=@displayorder WHERE [pimgid]=@pimgid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms), -1) > 0;
        }

        /// <summary>
        /// 获得商品图片列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public IDataReader GetProductImageList(int pid)
        {
            DbParameter[] parms =  {
                                     GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getproductimagelist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        #endregion

        #region 商品库存

        /// <summary>
        /// 获得商品库存
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public IDataReader GetProductStockByPid(int pid)
        {
            DbParameter[] parms =  { 
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getproductstockbypid", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 创建商品库存
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="number">商品数量</param>
        /// <param name="limit">商品库存警戒线</param>
        /// <returns></returns>
        public bool CreateProductStock(int pid, int number, int limit)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@pid", SqlDbType.Int, 4, pid),
                                     GenerateInParam("@number", SqlDbType.Int, 4, number),
                                     GenerateInParam("@limit", SqlDbType.Int, 4, limit)
                                  };
            string commandText = string.Format("INSERT INTO [{0}productstocks]({1}) VALUES(@pid,@number,@limit)",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.PRODUCT_STOCKS);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0;
        }

        /// <summary>
        /// 更新商品库存
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="number">商品数量</param>
        /// <param name="limit">商品库存警戒线</param>
        public void UpdateProductStock(int pid, int number, int limit)
        {
            DbParameter[] parms =  {
                                      GenerateInParam("@pid", SqlDbType.Int, 4, pid),
                                      GenerateInParam("@number", SqlDbType.Int, 4, number),
                                      GenerateInParam("@limit", SqlDbType.Int, 4, limit)
                                   };
            string commandText = string.Format("UPDATE [{0}productstocks] SET [number]=@number,[limit]=@limit WHERE [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新商品库存数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="number">库存数量</param>
        /// <returns></returns>
        public bool UpdateProductStockNumber(int pid, int number)
        {
            DbParameter[] parms =  {
                                      GenerateInParam("@pid", SqlDbType.Int, 4, pid),
                                      GenerateInParam("@number", SqlDbType.Int, 4, number)
                                   };
            string commandText = string.Format("UPDATE [{0}productstocks] SET [number]=@number WHERE [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0;
        }

        /// <summary>
        /// 获得商品库存数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public int GetProductStockNumberByPid(int pid)
        {
            DbParameter[] parms =  { 
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getproductstocknumberbypid", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 增加商品库存数量
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public void IncreaseProductStockNumber(List<OrderProductInfo> orderProductList)
        {
            StringBuilder commandText = new StringBuilder();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                commandText.AppendFormat("UPDATE [{0}productstocks] SET [number]=[number]+{1} WHERE [pid]={2}",
                                          RDBSHelper.RDBSTablePre,
                                          orderProductInfo.RealCount,
                                          orderProductInfo.Pid);
            }
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText.ToString());
        }

        /// <summary>
        /// 减少商品库存数量
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public void DecreaseProductStockNumber(List<OrderProductInfo> orderProductList)
        {
            StringBuilder commandText = new StringBuilder();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                commandText.AppendFormat("UPDATE [{0}productstocks] SET [number]=[number]-{1} WHERE [pid]={2}",
                                          RDBSHelper.RDBSTablePre,
                                          orderProductInfo.RealCount,
                                          orderProductInfo.Pid);
            }
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText.ToString());
        }

        /// <summary>
        /// 获得商品库存列表
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        public IDataReader GetProductStockList(string pidList)
        {
            DbParameter[] parms =  { 
                                        GenerateInParam("@pidlist", SqlDbType.NVarChar, 1000, pidList)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getproductstocklist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        #endregion

        #region 商品关键词

        /// <summary>
        /// 创建商品关键词
        /// </summary>
        /// <param name="productKeywordInfo">商品关键词信息</param>
        public void CreateProductKeyword(ProductKeywordInfo productKeywordInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@keyword", SqlDbType.NChar,40,productKeywordInfo.Keyword),
                                        GenerateInParam("@pid", SqlDbType.Int,4,productKeywordInfo.Pid),
                                        GenerateInParam("@relevancy", SqlDbType.Int,4,productKeywordInfo.Relevancy)
                                    };
            string commandText = string.Format("INSERT INTO [{0}productkeywords]([keyword],[pid],[relevancy]) VALUES(@keyword,@pid,@relevancy)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新商品关键词
        /// </summary>
        /// <param name="productKeywordInfo">商品关键词信息</param>
        public void UpdateProductKeyword(ProductKeywordInfo productKeywordInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@keyword", SqlDbType.NChar,40,productKeywordInfo.Keyword),
                                        GenerateInParam("@pid", SqlDbType.Int,4,productKeywordInfo.Pid),
                                        GenerateInParam("@relevancy", SqlDbType.Int,4,productKeywordInfo.Relevancy),
                                        GenerateInParam("@keywordid", SqlDbType.Int, 4, productKeywordInfo.KeywordId)
                                    };

            string commandText = string.Format("UPDATE [{0}productkeywords] SET [keyword]=@keyword,[pid]=@pid,[relevancy]=@relevancy WHERE [keywordid]=@keywordid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除商品关键词
        /// </summary>
        /// <param name="keywordIdList">关键词id列表</param>
        public bool DeleteProductKeyword(string keywordIdList)
        {
            string commandText = string.Format(@"DELETE FROM [{0}productkeywords] WHERE [keywordid] IN ({1})",
                                                 RDBSHelper.RDBSTablePre,
                                                 keywordIdList);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0;
        }

        /// <summary>
        /// 获得商品关键词列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public IDataReader GetProductKeywordList(int pid)
        {
            DbParameter[] parms = {
                                   GenerateInParam("@pid", SqlDbType.Int,4,pid)
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}productkeywords] WHERE [pid]=@pid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.PRODUCT_KEYWORDS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 是否存在商品关键词
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public bool IsExistProductKeyword(int pid, string keyword)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@keyword", SqlDbType.NChar,40,keyword),
                                    GenerateInParam("@pid", SqlDbType.Int,4,pid)
                                   };
            string commandText = string.Format("SELECT [keywordid] FROM [{0}productkeywords] WHERE [keyword]=@keyword AND [pid]=@pid ",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms)) > 0;
        }

        /// <summary>
        /// 更新商品关键词的相关性
        /// </summary>
        /// <param name="keywordId">关键词id</param>
        /// <param name="relevancy">相关性</param>
        /// <returns></returns>
        public bool UpdateProductKeywordRelevancy(int keywordId, int relevancy)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@keywordid", SqlDbType.Int,4,keywordId),
                                    GenerateInParam("@relevancy", SqlDbType.Int,4,relevancy)
                                   };
            string commandText = string.Format("UPDATE [{0}productkeywords] SET [relevancy]=@relevancy WHERE [keywordid]=@keywordid",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0;
        }

        #endregion

        #region 关联商品

        /// <summary>
        /// 添加关联商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="relatePid">关联商品id</param>
        public void AddRelateProduct(int pid, int relatePid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pid", SqlDbType.Int, 4, pid),
                                    GenerateInParam("@relatepid", SqlDbType.Int,4,relatePid)
                                    };
            string commandText = string.Format("INSERT INTO [{0}relateproducts]([pid],[relatepid]) VALUES(@pid,@relatepid)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除关联商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="relatePid">关联商品id</param>
        /// <returns></returns>
        public bool DeleteRelateProductByPidAndRelatePid(int pid, int relatePid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pid", SqlDbType.Int, 4, pid),
                                    GenerateInParam("@relatepid", SqlDbType.Int,4,relatePid)
                                    };
            string commandText = string.Format("DELETE FROM [{0}relateproducts] WHERE [pid]=@pid AND [relatepid]=@relatepid",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0;
        }

        /// <summary>
        /// 关联商品是否已经存在
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="relatePid">关联商品id</param>
        public bool IsExistRelateProduct(int pid, int relatePid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pid", SqlDbType.Int, 4, pid),
                                    GenerateInParam("@relatepid", SqlDbType.Int,4,relatePid)
                                    };
            string commandText = string.Format("SELECT [recordid] FROM [{0}relateproducts] WHERE [pid]=@pid AND [relatepid]=@relatepid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1) > 0;
        }

        /// <summary>
        /// 后台获得关联商品列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public DataTable AdminGetRelateProductList(int pid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pid", SqlDbType.Int,4,pid)
                                   };
            string commandText = string.Format("SELECT [temp1].[relatepid],[temp2].[psn],[temp2].[cateid],[temp2].[brandid],[temp2].[skugid],[temp2].[name],[temp2].[shopprice],[temp2].[marketprice],[temp2].[costprice],[temp2].[state],[temp2].[isbest],[temp2].[ishot],[temp2].[isnew],[temp2].[displayorder],[temp2].[weight],[temp2].[showimg] FROM (SELECT [relatepid] FROM [{0}relateproducts] WHERE [pid]=@pid) AS [temp1] LEFT JOIN [{0}products] AS [temp2] ON [temp1].[relatepid]=[temp2].[pid] ORDER BY [temp2].[displayorder] DESC",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText, parms).Tables[0];
        }

        /// <summary>
        /// 获得关联商品列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public IDataReader GetRelateProductList(int pid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getrelateproductlist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        #endregion

        #region 签名商品

        /// <summary>
        /// 添加签名商品
        /// </summary>
        /// <param name="sign">签名</param>
        /// <param name="pid">商品id</param>
        public void AddSignProduct(string sign, int pid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@sign", SqlDbType.Char, 15, sign),
                                    GenerateInParam("@pid", SqlDbType.Int,4,pid)
                                   };
            string commandText = string.Format("INSERT INTO [{0}signproducts]([sign],[pid]) VALUES(@sign,@pid)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除签名商品
        /// </summary>
        /// <param name="recordId">记录id</param>
        /// <returns></returns>
        public bool DeleteSignProduct(int recordId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@recordid", SqlDbType.Int,4,recordId)
                                   };
            string commandText = string.Format("DELETE FROM [{0}signproducts] WHERE [recordid]=@recordid",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0;
        }

        /// <summary>
        /// 是否存在签名商品
        /// </summary>
        /// <param name="sign">签名</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public bool IsExistSignProduct(string sign, int pid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@sign", SqlDbType.Char, 15, sign),
                                    GenerateInParam("@pid", SqlDbType.Int,4,pid)
                                   };
            string commandText = string.Format("SELECT [recordid] FROM [{0}signproducts] WHERE [sign]=@sign AND [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms)) > 0;
        }

        /// <summary>
        /// 获得签名商品列表
        /// </summary>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public IDataReader GetSignProductList(string sign)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@sign", SqlDbType.Char, 15, sign)
                                   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getsignproductlist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 后台获得签名商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public DataTable AdminGetSignProductList(int pageSize, int pageNumber, string sign)
        {
            string commandText;
            if (pageNumber == 1)
            {
                if (string.IsNullOrWhiteSpace(sign))
                    commandText = string.Format("SELECT [temp1].[recordid],[temp1].[sign],[temp1].[pid],[temp2].[name],[temp2].[shopprice] FROM (SELECT TOP {0} [recordid],[sign],[pid] FROM [{1}signproducts]) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre);

                else
                    commandText = string.Format("SELECT [temp1].[recordid],[temp1].[sign],[temp1].[pid],[temp2].[name],[temp2].[shopprice] FROM (SELECT TOP {0} [recordid],[sign],[pid] FROM [{1}signproducts] WHERE [sign] LIKE '{2}%') AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sign);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(sign))
                    commandText = string.Format("SELECT [temp1].[recordid],[temp1].[sign],[temp1].[pid],[temp2].[name],[temp2].[shopprice] FROM (SELECT TOP {0} [recordid],[sign],[pid] FROM [{1}signproducts] WHERE [recordid] > (SELECT MAX([recordid]) FROM (SELECT TOP {2} [recordid] FROM [{1}signproducts]) AS [temp])) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize);
                else
                    commandText = string.Format("SELECT [temp1].[recordid],[temp1].[sign],[temp1].[pid],[temp2].[name],[temp2].[shopprice] FROM (SELECT TOP {0} [recordid],[sign],[pid] FROM [{1}signproducts] WHERE [sign] LIKE '{3}%' AND [recordid] > (SELECT MAX([recordid]) FROM (SELECT TOP {2} [recordid] FROM [{1}signproducts] WHERE [sign] LIKE '{3}%') AS [temp])) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                sign);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得签名商品数量
        /// </summary>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public int AdminGetSignProductCount(string sign)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(sign))
                commandText = string.Format("SELECT COUNT([recordid]) FROM [{0}signproducts]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT([recordid]) FROM [{0}signproducts] WHERE [sign] LIKE '{1}%'", RDBSHelper.RDBSTablePre, sign);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        #endregion

        #region 定时商品

        /// <summary>
        /// 获得定时商品列表
        /// </summary>
        /// <param name="type">类型(0代表需要上架定时商品,1代表需要下架定时商品)</param>
        /// <returns></returns>
        public IDataReader GetTimeProductList(int type)
        {
            string commandText;
            if (type == 0)
                commandText = string.Format("SELECT {1} FROM [{0}timeproducts] WHERE [onsalestate]=1 AND [onsaletime]<='{2}' AND ([outsalestate]=0 OR ([outsalestate]>0 AND [outsaletime]>'{2}'))", RDBSHelper.RDBSTablePre, RDBSFields.TIMEPRODUCTS, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            else
                commandText = string.Format("SELECT {1} FROM [{0}timeproducts] WHERE [outsalestate]=1 AND [outsaletime]<='{2}'", RDBSHelper.RDBSTablePre, RDBSFields.TIMEPRODUCTS, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得定时商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="productName">商品名称</param>
        /// <returns></returns>
        public DataTable GetTimeProductList(int pageSize, int pageNumber, string productName)
        {
            bool noCondition = string.IsNullOrWhiteSpace(productName);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT [temp1].[recordid],[temp1].[pid],[temp1].[onsalestate],[temp1].[outsalestate],[temp1].[onsaletime],[temp1].[outsaletime],[temp2].[name] FROM (SELECT TOP {0} [recordid],[pid],[onsalestate],[outsalestate],[onsaletime],[outsaletime] FROM [{1}timeproducts] ORDER BY [recordid] DESC) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre);

                else
                    commandText = string.Format("SELECT TOP {0} [temp1].[recordid],[temp1].[pid],[temp1].[onsalestate],[temp1].[outsalestate],[temp1].[onsaletime],[temp1].[outsaletime],[temp2].[name] FROM (SELECT [recordid],[pid],[onsalestate],[outsalestate],[onsaletime],[outsaletime] FROM [{1}timeproducts] ORDER BY [recordid] DESC) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid] WHERE [temp2].[name] LIKE '{2}%'",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                productName);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT [temp1].[recordid],[temp1].[pid],[temp1].[onsalestate],[temp1].[outsalestate],[temp1].[onsaletime],[temp1].[outsaletime],[temp2].[name] FROM (SELECT TOP {0} [recordid],[pid],[onsalestate],[outsalestate],[onsaletime],[outsaletime] FROM [{1}timeproducts] WHERE [recordid]<(SELECT MAX([recordid]) FROM (SELECT TOP {2} [recordid] FROM [{1}timeproducts] ORDER BY [recordid] DESC) AS [temp3]) ORDER BY [recordid] DESC) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize);
                else//颠倒排序分页
                    commandText = string.Format("SELECT [recordid],[pid],[onsalestate],[outsalestate],[onsaletime],[outsaletime],[name] FROM (SELECT TOP {0} [recordid],[pid],[onsalestate],[outsalestate],[onsaletime],[outsaletime],[name] FROM (SELECT TOP {2} [temp1].[recordid],[temp1].[pid],[temp1].[onsalestate],[temp1].[outsalestate],[temp1].[onsaletime],[temp1].[outsaletime],[temp2].[name] FROM [{1}timeproducts] AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid] WHERE [temp2].[name] LIKE '{2}%' ORDER BY [temp1].[recordid] DESC) AS [temp3] ORDER BY [temp3].[recordid] ASC) AS [temp4] ORDER BY [temp4].[recordid] DESC",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                pageNumber * pageSize,
                                                productName);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 获得定时商品数量
        /// </summary>
        /// <param name="productName">商品名称</param>
        /// <returns></returns>
        public int GetTimeProductCount(string productName)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(productName))
                commandText = string.Format("SELECT COUNT([recordid]) FROM [{0}timeproducts]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT([temp1].[recordid]) FROM [{0}timeproducts] AS [temp1] INNER JOIN [{0}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid] WHERE [temp2].[name] LIKE '{1}%'", RDBSHelper.RDBSTablePre, productName);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 获得定时商品
        /// </summary>
        /// <param name="recordId">记录id</param>
        /// <returns></returns>
        public IDataReader GetTimeProductByRecordId(int recordId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@recordid", SqlDbType.Int, 4, recordId)
                                    };
            string commandText = string.Format("SELECT {1} FROM [{0}timeproducts] WHERE [recordid]=@recordid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.TIMEPRODUCTS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 添加定时商品
        /// </summary>
        /// <param name="timeProductInfo">定时商品信息</param>
        public void AddTimeProduct(TimeProductInfo timeProductInfo)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pid", SqlDbType.Int, 4, timeProductInfo.Pid),
                                    GenerateInParam("@onsalestate", SqlDbType.TinyInt, 1, timeProductInfo.OnSaleState),
                                    GenerateInParam("@outsalestate", SqlDbType.TinyInt, 1, timeProductInfo.OutSaleState),
                                    GenerateInParam("@onsaletime", SqlDbType.DateTime, 8, timeProductInfo.OnSaleTime),
                                    GenerateInParam("@outsaletime", SqlDbType.DateTime, 8,timeProductInfo.OutSaleTime)
                                    };
            string commandText = string.Format("INSERT INTO [{0}timeproducts]([pid],[onsalestate],[outsalestate],[onsaletime],[outsaletime]) VALUES(@pid,@onsalestate,@outsalestate,@onsaletime,@outsaletime)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新定时商品
        /// </summary>
        /// <param name="timeProductInfo">定时商品信息</param>
        public void UpdateTimeProduct(TimeProductInfo timeProductInfo)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pid", SqlDbType.Int, 4, timeProductInfo.Pid),
                                    GenerateInParam("@onsalestate", SqlDbType.TinyInt, 1, timeProductInfo.OnSaleState),
                                    GenerateInParam("@outsalestate", SqlDbType.TinyInt, 1, timeProductInfo.OutSaleState),
                                    GenerateInParam("@onsaletime", SqlDbType.DateTime, 8, timeProductInfo.OnSaleTime),
                                    GenerateInParam("@outsaletime", SqlDbType.DateTime, 8,timeProductInfo.OutSaleTime),
                                    GenerateInParam("@recordid", SqlDbType.Int, 4, timeProductInfo.RecordId)
                                    };
            string commandText = string.Format("UPDATE [{0}timeproducts] SET [pid]=@pid,[onsalestate]=@onsalestate,[outsalestate]=@outsalestate,[onsaletime]=@onsaletime,[outsaletime]=@outsaletime WHERE [recordid]=@recordid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 是否存在定时商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public bool IsExistTimeProduct(int pid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                    };
            string commandText = string.Format("SELECT [recordid] FROM [{0}timeproducts] WHERE [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms)) > 0;
        }

        /// <summary>
        /// 删除定时商品
        /// </summary>
        /// <param name="recordId">记录id</param>
        public void DeleteTimeProductByRecordId(int recordId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@recordid", SqlDbType.Int, 4, recordId)
                                    };
            string commandText = string.Format("DELETE FROM [{0}timeproducts] WHERE [recordid]=@recordid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        #endregion

        #region 商品统计

        /// <summary>
        /// 更新商品统计
        /// </summary>
        /// <param name="updateProductStatState">更新商品统计状态</param>
        public void UpdateProductStat(UpdateProductStatState updateProductStatState)
        {
            string year = updateProductStatState.Time.Year.ToString();
            string month = updateProductStatState.Time.Year.ToString() + updateProductStatState.Time.Month.ToString("00");
            string day = updateProductStatState.Time.ToString("yyyy-MM-dd");
            string hour = updateProductStatState.Time.ToString("yyyy-MM-dd") + updateProductStatState.Time.Hour.ToString("00");
            string week = updateProductStatState.Time.ToString("yyyy-MM-dd") + updateProductStatState.Time.Month.ToString("00") + ((int)updateProductStatState.Time.DayOfWeek).ToString();
            bool isStatRegion = BSPConfig.ShopConfig.IsStatRegion == 1;

            string condition = string.Format("([pid]={0} AND [category]='total') OR ([pid]={0} AND [category]='year' AND [value]='{1}') OR ([pid]={0} AND [category]='month' AND [value]='{2}') OR ([pid]={0} AND [category]='day' AND [value]='{3}') OR ([pid]={0} AND [category]='hour' AND [value]='{4}') OR ([pid]={0} AND [category]='week' AND [value]='{5}'){6}",
                                                updateProductStatState.Pid,
                                                year,
                                                month,
                                                day,
                                                hour,
                                                week,
                                                isStatRegion ? string.Format(" OR ([pid]={0} AND [category]='region' AND [value]='{1}')", updateProductStatState.Pid, updateProductStatState.RegionId) : "");

            if (UpdateProductStat(condition) < (isStatRegion ? 7 : 6))
            {
                AddProductStat(updateProductStatState.Pid, "total", "");
                AddProductStat(updateProductStatState.Pid, "year", year);
                AddProductStat(updateProductStatState.Pid, "month", month);
                AddProductStat(updateProductStatState.Pid, "day", day);
                AddProductStat(updateProductStatState.Pid, "hour", hour);
                AddProductStat(updateProductStatState.Pid, "week", week);
                if (isStatRegion)
                    AddProductStat(updateProductStatState.Pid, "region", updateProductStatState.RegionId.ToString());
            }
        }

        /// <summary>
        /// 添加商品统计
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="category">分类</param>
        /// <param name="value">值</param>
        private void AddProductStat(int pid, string category, string value)
        {
            DbParameter[] parms = {
									GenerateInParam("@pid",SqlDbType.Int,4, pid),
									GenerateInParam("@category",SqlDbType.Char,10, category),
									GenerateInParam("@value",SqlDbType.Char,20, value)
			                       };
            string commandText = string.Format("SELECT [recordid] FROM [{0}productstats] WHERE [pid]=@pid AND [category]=@category AND [value]=@value",
                                                RDBSHelper.RDBSTablePre);
            if (TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms)) < 1)
            {
                commandText = string.Format("INSERT INTO [{0}productstats]([pid],[category],[value],[count]) VALUES(@pid,@category,@value,1)",
                                             RDBSHelper.RDBSTablePre);
                RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
            }
        }

        /// <summary>
        /// 更新商品统计
        /// </summary>
        /// <param name="condition">条件</param>
        private int UpdateProductStat(string condition)
        {
            string commandText = string.Format("UPDATE [{0}productstats] SET [count]=[count]+1 WHERE {1}",
                                                RDBSHelper.RDBSTablePre,
                                                condition);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得商品总访问量列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetProductTotalVisitCountList()
        {
            string commandText = string.Format("SELECT [pid],[count] FROM [{0}productstats] WHERE [category]='total'",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        #endregion

        #region 浏览历史

        /// <summary>
        /// 获得用户浏览商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public IDataReader GetUserBrowseProductList(int pageSize, int pageNumber, int uid)
        {
            DbParameter[] parms =  { 
                                     GenerateInParam("@pagesize", SqlDbType.Int, 4, pageSize),
                                     GenerateInParam("@pagenumber", SqlDbType.Int, 4, pageNumber),
                                     GenerateInParam("@uid", SqlDbType.Int, 4, uid)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getuserbrowseproductlist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得用户浏览商品数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public int GetUserBrowseProductCount(int uid)
        {
            DbParameter[] parms =  { 
                                     GenerateInParam("@uid", SqlDbType.Int, 4, uid)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getuserbrowseproductcount", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 更新浏览历史
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pid">商品id</param>
        /// <param name="updateTime">更新时间</param>
        public void UpdateBrowseHistory(int uid, int pid, DateTime updateTime)
        {
            DbParameter[] parms = {
									GenerateInParam("@uid",SqlDbType.Int,4, uid),
									GenerateInParam("@pid",SqlDbType.Int,4, pid),
									GenerateInParam("@updatetime",SqlDbType.DateTime,8, updateTime)
			                       };
            string commandText = string.Format("UPDATE [{0}browsehistories] SET [times]=[times]+1,[updatetime]=@updatetime WHERE [uid]=@uid AND [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) < 1)
            {
                commandText = string.Format("INSERT INTO [{0}browsehistories]([uid],[pid],[times],[updatetime]) VALUES(@uid,@pid,1,@updatetime)",
                                             RDBSHelper.RDBSTablePre);
                RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
            }
        }

        /// <summary>
        /// 清空过期浏览历史
        /// </summary>
        public void ClearExpiredBrowseHistory()
        {
            string commandText = string.Format("SELECT DISTINCT [uid] FROM [{0}browsehistories]", RDBSHelper.RDBSTablePre);
            IDataReader reader = RDBSHelper.ExecuteReader(CommandType.Text, commandText);
            while (reader.Read())
            {
                commandText = string.Format("DELETE FROM [{0}browsehistories] WHERE [recordid] NOT IN (SELECT TOP 10 [recordid] FROM [{0}browsehistories] WHERE [uid]={1} ORDER BY [updatetime] DESC)", RDBSHelper.RDBSTablePre, reader["uid"].ToString());
                RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
            }
            reader.Close();
        }

        #endregion

        #region 商品咨询类型

        /// <summary>
        /// 创建商品咨询类型
        /// </summary>
        public void CreateProductConsultType(ProductConsultTypeInfo productConsultTypeInfo)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@title", SqlDbType.NChar,30,productConsultTypeInfo.Title),
                                    GenerateInParam("@displayorder", SqlDbType.Int,4,productConsultTypeInfo.DisplayOrder)
                                  };
            string commandText = string.Format("INSERT INTO [{0}productconsulttypes]([title],[displayorder]) VALUES(@title,@displayorder)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新商品咨询类型
        /// </summary>
        public void UpdateProductConsultType(ProductConsultTypeInfo productConsultTypeInfo)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@title", SqlDbType.NChar,30,productConsultTypeInfo.Title),
                                    GenerateInParam("@displayorder", SqlDbType.Int,4,productConsultTypeInfo.DisplayOrder),
                                    GenerateInParam("@consulttypeid", SqlDbType.TinyInt, 1, productConsultTypeInfo.ConsultTypeId)    
                                  };

            string commandText = string.Format("UPDATE [{0}productconsulttypes] SET [title]=@title,[displayorder]=@displayorder WHERE [consulttypeid]=@consulttypeid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除商品咨询类型
        /// </summary>
        /// <param name="consultTypeId">商品咨询类型id</param>
        public void DeleteProductConsultTypeById(int consultTypeId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@consulttypeid", SqlDbType.TinyInt, 1, consultTypeId)    
                                  };
            string commandText = string.Format("DELETE FROM [{0}productconsults] WHERE [consulttypeid]=@consulttypeid;DELETE FROM [{0}productconsulttypes] WHERE [consulttypeid]=@consulttypeid;",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得商品咨询类型列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetProductConsultTypeList()
        {
            string commandText = string.Format("SELECT {1} FROM [{0}productconsulttypes] ORDER BY [displayorder] DESC",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.PRODUCT_CONSULTTYPES);
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        #endregion

        #region 商品咨询

        /// <summary>
        /// 咨询商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="consultTypeId">咨询类型id</param>
        /// <param name="consultUid">咨询用户id</param>
        /// <param name="consultTime">咨询时间</param>
        /// <param name="consultMessage">咨询内容</param>
        /// <param name="consultNickName">咨询昵称</param>
        /// <param name="pName">商品名称</param>
        /// <param name="pShowImg">商品图片</param>
        /// <param name="consultIP">咨询ip</param>
        public void ConsultProduct(int pid, int consultTypeId, int consultUid, DateTime consultTime, string consultMessage, string consultNickName, string pName, string pShowImg, string consultIP)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pid", SqlDbType.Int,4,pid),
                                        GenerateInParam("@consulttypeid", SqlDbType.TinyInt,1,consultTypeId),
                                        GenerateInParam("@consultuid", SqlDbType.Int,4,consultUid),
                                        GenerateInParam("@consulttime", SqlDbType.DateTime,8,consultTime),
                                        GenerateInParam("@consultmessage", SqlDbType.NVarChar,200,consultMessage),
                                        GenerateInParam("@consultnickname", SqlDbType.NVarChar,20,consultNickName),
                                        GenerateInParam("@pname", SqlDbType.NVarChar,200,pName),
                                        GenerateInParam("@pshowimg", SqlDbType.NVarChar,100,pShowImg),
                                        GenerateInParam("@consultip", SqlDbType.VarChar,15,consultIP)
                                    };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}consultproduct", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 回复商品咨询
        /// </summary>
        /// <param name="consultId">咨询id</param>
        /// <param name="replyUid">回复用户id</param>
        /// <param name="replyTime">回复时间</param>
        /// <param name="replyMessage">回复内容</param>
        /// <param name="replyNickName">回复昵称</param>
        /// <param name="replyIP">回复ip</param>
        public void ReplyProductConsult(int consultId, int replyUid, DateTime replyTime, string replyMessage, string replyNickName, string replyIP)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@replyuid", SqlDbType.Int,4,replyUid),
                                        GenerateInParam("@replytime", SqlDbType.DateTime,8,replyTime),
                                        GenerateInParam("@replymessage", SqlDbType.NVarChar,200,replyMessage),
                                        GenerateInParam("@replynickname", SqlDbType.NVarChar,20,replyNickName),
                                        GenerateInParam("@replyip", SqlDbType.VarChar,15,replyIP),
                                        GenerateInParam("@consultid", SqlDbType.Int,4,consultId)
                                    };
            string commandText = string.Format("UPDATE [{0}productconsults] SET [replyuid]=@replyuid,[replytime]=@replytime,[replymessage]=@replymessage,[replynickname]=@replynickname,[replyip]=@replyip WHERE [consultid]=@consultid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得商品咨询
        /// </summary>
        /// <param name="consultId">咨询id</param>
        /// <returns></returns>
        public IDataReader GetProductConsultById(int consultId)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@consultid", SqlDbType.Int, 4, consultId)    
                                   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getproductconsultbyid", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 后台获得商品咨询
        /// </summary>
        /// <param name="consultId">咨询id</param>
        /// <returns></returns>
        public IDataReader AdminGetProductConsultById(int consultId)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@consultid", SqlDbType.Int, 4, consultId)    
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}productconsults] WHERE [consultid]=@consultid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.PRODUCT_CONSULTS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除商品咨询
        /// </summary>
        /// <param name="consultIdList">咨询id</param>
        public void DeleteProductConsultById(string consultIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}productconsults] WHERE [consultid] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                consultIdList);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 后台获得商品咨询列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public IDataReader AdminGetProductConsultList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}productconsults] ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.PRODUCT_CONSULTS,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}productconsults] WHERE {4} ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.PRODUCT_CONSULTS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}productconsults]) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.PRODUCT_CONSULTS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1);
                else
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}productconsults] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.PRODUCT_CONSULTS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1,
                                                condition);
            }

            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 后台获得商品咨询列表条件
        /// </summary>
        /// <param name="consultTypeId">商品咨询类型id</param>
        /// <param name="pid">商品id</param>
        /// <param name="uid">用户id</param>
        /// <param name="consultMessage">咨询内容</param>
        /// <param name="consultStartTime">咨询开始时间</param>
        /// <param name="consultEndTime">咨询结束时间</param>
        /// <returns></returns>
        public string AdminGetProductConsultListCondition(int consultTypeId, int pid, int uid, string consultMessage, string consultStartTime, string consultEndTime)
        {
            StringBuilder condition = new StringBuilder();

            if (consultTypeId > 0)
                condition.AppendFormat(" AND [consulttypeid] = {0} ", consultTypeId);

            if (pid > 0)
                condition.AppendFormat(" AND [pid] = {0} ", pid);

            if (uid > 0)
                condition.AppendFormat(" AND [consultuid] = {0} ", uid);

            if (!string.IsNullOrWhiteSpace(consultMessage))
                condition.AppendFormat(" AND [consultmessage] like '%{0}%' ", consultMessage);

            if (!string.IsNullOrEmpty(consultStartTime))
                condition.AppendFormat(" AND [consulttime] >= '{0}' ", TypeHelper.StringToDateTime(consultStartTime).ToString("yyyy-MM-dd HH:mm:ss"));

            if (!string.IsNullOrEmpty(consultEndTime))
                condition.AppendFormat(" AND [consulttime] <= '{0}' ", TypeHelper.StringToDateTime(consultEndTime).ToString("yyyy-MM-dd HH:mm:ss"));

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        /// <summary>
        /// 后台获得商品咨询列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string AdminGetProductConsultListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[consultid]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得商品咨询数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int AdminGetProductConsultCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(consultid) FROM [{0}productconsults]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(consultid) FROM [{0}productconsults] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        /// <summary>
        /// 更新商品咨询状态
        /// </summary>
        /// <param name="consultId">咨询id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public bool UpdateProductConsultState(int consultId, int state)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@consultid", SqlDbType.Int, 4, consultId),
                                    GenerateInParam("@state", SqlDbType.TinyInt, 1, state)    
                                   };

            string commandText = string.Format("UPDATE [{0}productconsults] SET [state]=@state WHERE [consultid]=@consultid",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0;
        }

        /// <summary>
        /// 获得商品咨询列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pid">商品id</param>
        /// <param name="consultTypeId">咨询类型id</param>
        /// <param name="consultMessage">咨询内容</param>
        /// <returns></returns>
        public IDataReader GetProductConsultList(int pageSize, int pageNumber, int pid, int consultTypeId, string consultMessage)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pagesize", SqlDbType.Int,4,pageSize),
                                    GenerateInParam("@pagenumber", SqlDbType.Int,4,pageNumber),
                                    GenerateInParam("@pid", SqlDbType.Int,4,pid),
                                    GenerateInParam("@consulttypeid", SqlDbType.TinyInt,1,consultTypeId),
                                    GenerateInParam("@consultmessage", SqlDbType.NVarChar,100,consultMessage)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getproductconsultlist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得商品咨询数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="consultTypeId">咨询类型id</param>
        /// <param name="consultMessage">咨询内容</param>
        /// <returns></returns>
        public int GetProductConsultCount(int pid, int consultTypeId, string consultMessage)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pid", SqlDbType.Int,4,pid),
                                    GenerateInParam("@consulttypeid", SqlDbType.TinyInt,1,consultTypeId),
                                    GenerateInParam("@consultmessage", SqlDbType.NVarChar,100,consultMessage)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getproductconsultcount", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 获得用户商品咨询列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public IDataReader GetUserProductConsultList(int uid, int pageSize, int pageNumber)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@uid", SqlDbType.Int,4,uid),
                                    GenerateInParam("@pagesize", SqlDbType.Int,4,pageSize),
                                    GenerateInParam("@pagenumber", SqlDbType.Int,4,pageNumber)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getuserproductconsultlist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得用户商品咨询数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public int GetUserProductConsultCount(int uid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@uid", SqlDbType.Int,4,uid)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getuserproductconsultcount", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        #endregion

        #region 商品评价

        /// <summary>
        /// 获得商品评价
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        public IDataReader GetProductReviewById(int reviewId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@reviewid", SqlDbType.Int, 4, reviewId)
                                   };

            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getproductreviewbyid", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 后台获得商品评价
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        public IDataReader AdminGetProductReviewById(int reviewId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@reviewid", SqlDbType.Int, 4, reviewId)
                                   };
            string commandText = string.Format("SELECT {0} FROM [{1}productreviews] WHERE [reviewid]=@reviewid",
                                                RDBSFields.PRODUCT_REVIEWS,
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 评价商品
        /// </summary>
        public void ReviewProduct(ProductReviewInfo productReviewInfo)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pid", SqlDbType.Int, 4, productReviewInfo.Pid),
                                    GenerateInParam("@uid", SqlDbType.Int, 4, productReviewInfo.Uid),
                                    GenerateInParam("@oprid", SqlDbType.Int, 4, productReviewInfo.OPRId),
                                    GenerateInParam("@oid", SqlDbType.Int, 4, productReviewInfo.Oid),
                                    GenerateInParam("@parentid", SqlDbType.Int, 4, productReviewInfo.ParentId),
                                    GenerateInParam("@state", SqlDbType.TinyInt, 1, productReviewInfo.State),
                                    GenerateInParam("@star", SqlDbType.TinyInt, 1, productReviewInfo.Star),
                                    GenerateInParam("@quality", SqlDbType.SmallInt, 4, productReviewInfo.Quality),
                                    GenerateInParam("@message", SqlDbType.NVarChar, 200, productReviewInfo.Message),
                                    GenerateInParam("@reviewtime", SqlDbType.DateTime, 8, productReviewInfo.ReviewTime),
                                    GenerateInParam("@paycredits", SqlDbType.Int, 4, productReviewInfo.PayCredits),
                                    GenerateInParam("@pname", SqlDbType.NVarChar, 200, productReviewInfo.PName),
                                    GenerateInParam("@pshowimg", SqlDbType.NVarChar, 100, productReviewInfo.PShowImg),
                                    GenerateInParam("@buytime", SqlDbType.DateTime, 8, productReviewInfo.BuyTime),
                                    GenerateInParam("@ip", SqlDbType.VarChar, 15, productReviewInfo.IP)
                                    };

            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}reviewproduct", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 删除商品评价
        /// </summary>
        /// <param name="reviewId">评价id</param>
        public void DeleteProductReviewById(int reviewId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@reviewid", SqlDbType.Int, 4, reviewId)
                                   };
            string commandText = string.Format("DELETE FROM [{0}productreviews] WHERE [reviewid]=@reviewid OR [parentid]=@reviewid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 后台获得商品评价列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public DataTable AdminGetProductReviewList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT [temp1].[reviewid],[temp1].[pid],[temp1].[uid],[temp1].[oprid],[temp1].[oid],[temp1].[parentid],[temp1].[state],[temp1].[star],[temp1].[quality],[temp1].[message],[temp1].[reviewtime],[temp1].[pname],[temp1].[pshowimg],[temp1].[buytime],[temp1].[ip],[temp2].[nickname] FROM (SELECT TOP {0} {3} FROM [{1}productreviews] ORDER BY {2}) AS [temp1] LEFT JOIN [{1}users] AS [temp2] ON [temp1].[uid]=[temp2].[uid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                RDBSFields.PRODUCT_REVIEWS);
                else
                    commandText = string.Format("SELECT [temp1].[reviewid],[temp1].[pid],[temp1].[uid],[temp1].[oprid],[temp1].[oid],[temp1].[parentid],[temp1].[state],[temp1].[star],[temp1].[quality],[temp1].[message],[temp1].[reviewtime],[temp1].[pname],[temp1].[pshowimg],[temp1].[buytime],[temp1].[ip],[temp2].[nickname] FROM (SELECT TOP {0} {4} FROM [{1}productreviews] WHERE {3} ORDER BY {2}) AS [temp1] LEFT JOIN [{1}users] AS [temp2] ON [temp1].[uid]=[temp2].[uid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition,
                                                RDBSFields.PRODUCT_REVIEWS);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT [temp1].[reviewid],[temp1].[pid],[temp1].[uid],[temp1].[oprid],[temp1].[oid],[temp1].[parentid],[temp1].[state],[temp1].[star],[temp1].[quality],[temp1].[message],[temp1].[reviewtime],[temp1].[pname],[temp1].[pshowimg],[temp1].[buytime],[temp1].[ip],[temp2].[nickname] FROM ((SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}productreviews]) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}) AS [temp1] LEFT JOIN [{1}users] AS [temp2] ON [temp1].[uid]=[temp2].[uid]",
                                                RDBSFields.PRODUCT_REVIEWS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1);
                else
                    commandText = string.Format("SELECT [temp1].[reviewid],[temp1].[pid],[temp1].[uid],[temp1].[oprid],[temp1].[oid],[temp1].[parentid],[temp1].[state],[temp1].[star],[temp1].[quality],[temp1].[message],[temp1].[reviewtime],[temp1].[pname],[temp1].[pshowimg],[temp1].[buytime],[temp1].[ip],[temp2].[nickname] FROM ((SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}productreviews] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}) AS [temp1] LEFT JOIN [{1}users] AS [temp2] ON [temp1].[uid]=[temp2].[uid]",
                                                RDBSFields.PRODUCT_REVIEWS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得商品评价列表条件
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="message">评价内容</param>
        /// <param name="startTime">评价开始时间</param>
        /// <param name="endTime">评价结束时间</param>
        /// <returns></returns>
        public string AdminGetProductReviewListCondition(int pid, string message, string startTime, string endTime)
        {
            StringBuilder condition = new StringBuilder(" [parentid] = 0 ");

            if (pid > 0)
                condition.AppendFormat(" AND [pid] = {0} ", pid);

            if (!string.IsNullOrWhiteSpace(message))
                condition.AppendFormat(" AND [message] like '%{0}%' ", message);

            if (!string.IsNullOrEmpty(startTime))
                condition.AppendFormat(" AND [reviewtime] >= '{0}' ", TypeHelper.StringToDateTime(startTime).ToString("yyyy-MM-dd HH:mm:ss"));

            if (!string.IsNullOrEmpty(endTime))
                condition.AppendFormat(" AND [reviewtime] <= '{0}' ", TypeHelper.StringToDateTime(endTime).ToString("yyyy-MM-dd HH:mm:ss"));

            return condition.ToString();
        }

        /// <summary>
        /// 后台获得商品评价列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string AdminGetProductReviewListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[reviewid]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得商品评价数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int AdminGetProductReviewCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(reviewid) FROM [{0}productreviews]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(reviewid) FROM [{0}productreviews] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        /// <summary>
        /// 后台获得商品评价回复列表
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        public DataTable AdminGetProductReviewReplyList(int reviewId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@reviewid", SqlDbType.Int, 4, reviewId)
                                   };
            string commandText = string.Format("SELECT [temp1].[reviewid],[temp1].[pid],[temp1].[uid],[temp1].[oprid],[temp1].[oid],[temp1].[parentid],[temp1].[state],[temp1].[star],[temp1].[quality],[temp1].[message],[temp1].[reviewtime],[temp1].[pname],[temp1].[pshowimg],[temp1].[buytime],[temp1].[ip],[temp2].[nickname] FROM (SELECT {1} FROM [{0}productreviews] WHERE [parentid]=@reviewid) AS [temp1] LEFT JOIN [{0}users] AS [temp2] ON [temp1].[uid]=temp2.[uid] ORDER BY [reviewid] DESC",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.PRODUCT_REVIEWS);
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText, parms).Tables[0];
        }

        /// <summary>
        /// 对商品评价投票
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <param name="uid">用户id</param>
        /// <param name="voteTime">投票时间</param>
        public void VoteProductReview(int reviewId, int uid, DateTime voteTime)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@reviewid", SqlDbType.Int, 4, reviewId),
                                    GenerateInParam("@uid", SqlDbType.Int, 4, uid),
                                    GenerateInParam("@votetime", SqlDbType.DateTime, 8, voteTime)
                                    };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}voteproductreview", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 是否对商品评价投过票
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <param name="uid">用户id</param>
        public bool IsVoteProductReview(int reviewId, int uid)
        {
            DbParameter[] parms =  { 
                                    GenerateInParam("@reviewid", SqlDbType.Int, 4, reviewId),
                                    GenerateInParam("@uid", SqlDbType.Int, 4, uid)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                                                    string.Format("{0}isvoteproductreview", RDBSHelper.RDBSTablePre),
                                                                    parms)) > 0;
        }

        /// <summary>
        /// 更改商品评价状态
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <param name="state">评价状态</param>
        /// <returns></returns>
        public bool ChangeProductReviewState(int reviewId, int state)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@reviewid", SqlDbType.Int, 4, reviewId),
                                        GenerateInParam("@state", SqlDbType.TinyInt, 1, state)    
                                    };
            string commandText = string.Format("UPDATE [{0}productreviews] SET [state]=@state WHERE [reviewid]=@reviewid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms)) > 0;
        }

        /// <summary>
        /// 获得用户商品评价列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public IDataReader GetUserProductReviewList(int uid, int pageSize, int pageNumber)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@uid", SqlDbType.Int,4,uid),
                                    GenerateInParam("@pagesize", SqlDbType.Int,4,pageSize),
                                    GenerateInParam("@pagenumber", SqlDbType.Int,4,pageNumber)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getuserproductreviewlist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得用户商品评价数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public int GetUserProductReviewCount(int uid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@uid", SqlDbType.Int,4,uid)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getuserproductreviewcount", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 获得商品评价列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="type">类型(0代表全部评价,1代表好评,2代表中评,3代表差评)</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public DataTable GetProductReviewList(int pid, int type, int pageSize, int pageNumber)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pid", SqlDbType.Int,4,pid),
                                    GenerateInParam("@type", SqlDbType.TinyInt,1,type),
                                    GenerateInParam("@pagesize", SqlDbType.Int,4,pageSize),
                                    GenerateInParam("@pagenumber", SqlDbType.Int,4,pageNumber)
                                    };
            return RDBSHelper.ExecuteDataset(CommandType.StoredProcedure,
                                             string.Format("{0}getproductreviewlist", RDBSHelper.RDBSTablePre),
                                             parms).Tables[0];
        }

        /// <summary>
        /// 获得商品评价数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="type">类型(0代表全部评价,1代表好评,2代表中评,3代表差评)</param>
        /// <returns></returns>
        public int GetProductReviewCount(int pid, int type)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pid", SqlDbType.Int,4,pid),
                                    GenerateInParam("@type", SqlDbType.TinyInt,1,type)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getproductreviewcount", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 获得商品评价及其回复
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        public DataTable GetProductReviewWithReplyById(int reviewId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@reviewid", SqlDbType.Int,4,reviewId)
                                    };
            return RDBSHelper.ExecuteDataset(CommandType.StoredProcedure,
                                             string.Format("{0}getproductreviewwithreplybyid", RDBSHelper.RDBSTablePre),
                                             parms).Tables[0];
        }

        /// <summary>
        /// 获得商品评价列表
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public DataTable GetProductReviewList(DateTime startTime, DateTime endTime)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@starttime", SqlDbType.DateTime, 8, startTime),
                                    GenerateInParam("@endtime", SqlDbType.DateTime, 8, endTime)
                                   };
            string commandText = string.Format("SELECT [pid],[star] FROM [{0}productreviews] WHERE [reviewtime]>=@starttime AND [reviewtime]<@endtime",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText, parms).Tables[0];
        }

        #endregion

        #region 搜索历史

        /// <summary>
        /// 更新搜索历史
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="word">搜索词</param>
        /// <param name="updateTime">更新时间</param>
        public void UpdateSearchHistory(int uid, string word, DateTime updateTime)
        {
            DbParameter[] parms = {
									GenerateInParam("@uid",SqlDbType.Int,4, uid),
									GenerateInParam("@word",SqlDbType.NVarChar,60, word),
									GenerateInParam("@updatetime",SqlDbType.DateTime,8, updateTime)
			                       };
            string commandText = string.Format("UPDATE [{0}searchhistories] SET [times]=[times]+1,[updatetime]=@updatetime WHERE [uid]=@uid AND [word]=@word",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) < 1)
            {
                commandText = string.Format("INSERT INTO [{0}searchhistories]([uid],[word],[times],[updatetime]) VALUES(@uid,@word,1,@updatetime)",
                                             RDBSHelper.RDBSTablePre);
                RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
            }
        }

        /// <summary>
        /// 清空过期搜索历史
        /// </summary>
        public void ClearExpiredSearchHistory()
        {
            string commandText = string.Format("SELECT DISTINCT [uid] FROM [{0}searchhistories]", RDBSHelper.RDBSTablePre);
            IDataReader reader = RDBSHelper.ExecuteReader(CommandType.Text, commandText);
            while (reader.Read())
            {
                commandText = string.Format("DELETE FROM [{0}searchhistories] WHERE [recordid] NOT IN (SELECT TOP 20 [recordid] FROM [{0}searchhistories] WHERE [uid]={1} ORDER BY [updatetime] DESC)", RDBSHelper.RDBSTablePre, reader["uid"].ToString());
                RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
            }
            reader.Close();
        }

        /// <summary>
        /// 获得搜索词统计列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="word">搜索词</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public DataTable GetSearchWordStatList(int pageSize, int pageNumber, string word, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(word);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} [word],SUM([times]) AS [totaltimes],MAX([updatetime]) AS [lastsearchtime] FROM [{1}searchhistories] GROUP BY [word] ORDER BY {2}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} [word],SUM([times]) AS [totaltimes],MAX([updatetime]) AS [lastsearchtime] FROM [{1}searchhistories] WHERE [word] LIKE '{2}%' GROUP BY [word] ORDER BY {3}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                word,
                                                sort);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT [word],[totaltimes],[lastsearchtime] FROM (SELECT TOP {2} ROW_NUMBER() OVER (ORDER BY {1}) AS [rowid],[word],SUM([times]) AS [totaltimes],MAX([updatetime]) AS [lastsearchtime] FROM [{0}searchhistories] GROUP BY [word] ORDER BY {1}) AS [temp] WHERE [temp].[rowid] BETWEEN {3} AND {2}",
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1);
                else
                    commandText = string.Format("SELECT [word],[totaltimes],[lastsearchtime] FROM (SELECT TOP {2} ROW_NUMBER() OVER (ORDER BY {1}) AS [rowid],[word],SUM([times]) AS [totaltimes],MAX([updatetime]) AS [lastsearchtime] FROM [{0}searchhistories] WHERE [word] LIKE '{4}%' GROUP BY [word] ORDER BY {1}) AS [temp] WHERE [temp].[rowid] BETWEEN {3} AND {2}",
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1,
                                                word);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 获得搜索词统计列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string GetSearchWordStatListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[totaltimes]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 获得搜索词统计数量
        /// </summary>
        /// <param name="word">搜索词</param>
        /// <returns></returns>
        public int GetSearchWordStatCount(string word)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(word))
                commandText = string.Format("SELECT COUNT([word]) FROM [{0}searchhistories] GROUP BY [word]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT([word]) FROM [{0}searchhistories] WHERE [word] LIKE '{1}%' GROUP BY [word]", RDBSHelper.RDBSTablePre, word);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        #endregion
    }
}
