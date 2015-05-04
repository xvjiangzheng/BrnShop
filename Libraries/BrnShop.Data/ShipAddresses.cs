using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 用户配送地址数据访问类
    /// </summary>
    public partial class ShipAddresses
    {
        private static IUserNOSQLStrategy _usernosql = BSPData.UserNOSQL;//用户非关系型数据库

        #region 辅助方法

        /// <summary>
        /// 构建用户配送地址信息
        /// </summary>
        public static ShipAddressInfo BuildShipAddressFromReader(IDataReader reader)
        {
            ShipAddressInfo shipAddressInfo = new ShipAddressInfo();

            shipAddressInfo.SAId = TypeHelper.ObjectToInt(reader["said"]);
            shipAddressInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);
            shipAddressInfo.RegionId = TypeHelper.ObjectToInt(reader["regionid"]);
            shipAddressInfo.IsDefault = TypeHelper.ObjectToInt(reader["isdefault"]);
            shipAddressInfo.Alias = reader["alias"].ToString();
            shipAddressInfo.Consignee = reader["consignee"].ToString();
            shipAddressInfo.Phone = reader["phone"].ToString();
            shipAddressInfo.Mobile = reader["mobile"].ToString();
            shipAddressInfo.Email = reader["email"].ToString();
            shipAddressInfo.ZipCode = reader["zipcode"].ToString();
            shipAddressInfo.Address = reader["address"].ToString();

            return shipAddressInfo;
        }

        /// <summary>
        /// 构建完整用户配送地址信息
        /// </summary>
        public static FullShipAddressInfo BuildFullShipAddressFromReader(IDataReader reader)
        {
            FullShipAddressInfo fullShipAddressInfo = new FullShipAddressInfo();

            fullShipAddressInfo.SAId = TypeHelper.ObjectToInt(reader["said"]);
            fullShipAddressInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);
            fullShipAddressInfo.RegionId = TypeHelper.ObjectToInt(reader["regionid"]);
            fullShipAddressInfo.IsDefault = TypeHelper.ObjectToInt(reader["isdefault"]);
            fullShipAddressInfo.Alias = reader["alias"].ToString();
            fullShipAddressInfo.Consignee = reader["consignee"].ToString();
            fullShipAddressInfo.Phone = reader["phone"].ToString();
            fullShipAddressInfo.Mobile = reader["mobile"].ToString();
            fullShipAddressInfo.Email = reader["email"].ToString();
            fullShipAddressInfo.ZipCode = reader["zipcode"].ToString();
            fullShipAddressInfo.Address = reader["address"].ToString();
            fullShipAddressInfo.ProvinceId = TypeHelper.ObjectToInt(reader["provinceid"]);
            fullShipAddressInfo.ProvinceName = reader["provincename"].ToString();
            fullShipAddressInfo.CityId = TypeHelper.ObjectToInt(reader["cityid"]);
            fullShipAddressInfo.CityName = reader["cityname"].ToString();
            fullShipAddressInfo.CountyId = TypeHelper.ObjectToInt(reader["regionid"]);
            fullShipAddressInfo.CountyName = reader["name"].ToString();

            return fullShipAddressInfo;
        }

        #endregion

        /// <summary>
        /// 创建用户配送地址
        /// </summary>
        public static int CreateShipAddress(ShipAddressInfo shipAddressInfo)
        {
            int saId = BrnShop.Core.BSPData.RDBS.CreateShipAddress(shipAddressInfo);
            if (_usernosql != null)
                _usernosql.DeleteFullShipAddressList(shipAddressInfo.Uid);
            return saId;
        }

        /// <summary>
        /// 更新用户配送地址
        /// </summary>
        public static void UpdateShipAddress(ShipAddressInfo shipAddressInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateShipAddress(shipAddressInfo);
            if (_usernosql != null)
                _usernosql.DeleteFullShipAddressList(shipAddressInfo.Uid);
        }

        /// <summary>
        /// 获得完整用户配送地址列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static List<FullShipAddressInfo> GetFullShipAddressList(int uid)
        {
            List<FullShipAddressInfo> fullShipAddressList = null;

            if (_usernosql != null)
            {
                fullShipAddressList = _usernosql.GetFullShipAddressList(uid);
                if (fullShipAddressList == null)
                {
                    fullShipAddressList = new List<FullShipAddressInfo>();
                    IDataReader reader = BrnShop.Core.BSPData.RDBS.GetFullShipAddressList(uid);
                    while (reader.Read())
                    {
                        FullShipAddressInfo fullShipAddressInfo = BuildFullShipAddressFromReader(reader);
                        fullShipAddressList.Add(fullShipAddressInfo);
                    }
                    reader.Close();
                    _usernosql.CreateFullShipAddressList(uid, fullShipAddressList);
                }
            }
            else
            {
                fullShipAddressList = new List<FullShipAddressInfo>();
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetFullShipAddressList(uid);
                while (reader.Read())
                {
                    FullShipAddressInfo fullShipAddressInfo = BuildFullShipAddressFromReader(reader);
                    fullShipAddressList.Add(fullShipAddressInfo);
                }
                reader.Close();
            }

            return fullShipAddressList;
        }

        /// <summary>
        /// 获得用户配送地址数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static int GetShipAddressCount(int uid)
        {
            if (_usernosql != null)
                return GetFullShipAddressList(uid).Count;
            return BrnShop.Core.BSPData.RDBS.GetShipAddressCount(uid);
        }

        /// <summary>
        /// 获得默认完整用户配送地址
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static FullShipAddressInfo GetDefaultFullShipAddress(int uid)
        {
            FullShipAddressInfo fullShipAddressInfo = null;

            if (_usernosql != null)
            {
                foreach (FullShipAddressInfo tempFullShipAddressInfo in GetFullShipAddressList(uid))
                {
                    if (tempFullShipAddressInfo.IsDefault == 1)
                    {
                        fullShipAddressInfo = tempFullShipAddressInfo;
                        break;
                    }
                }
            }
            else
            {
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetDefaultFullShipAddress(uid);
                if (reader.Read())
                {
                    fullShipAddressInfo = BuildFullShipAddressFromReader(reader);
                }
                reader.Close();
            }

            return fullShipAddressInfo;
        }

        /// <summary>
        /// 获得完整用户配送地址
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="saId">配送地址id</param>
        /// <returns></returns>
        public static FullShipAddressInfo GetFullShipAddressBySAId(int uid, int saId)
        {
            FullShipAddressInfo fullShipAddressInfo = null;

            if (_usernosql != null)
            {
                foreach (FullShipAddressInfo tempFullShipAddressInfo in GetFullShipAddressList(uid))
                {
                    if (tempFullShipAddressInfo.SAId == saId)
                    {
                        fullShipAddressInfo = tempFullShipAddressInfo;
                        break;
                    }
                }
            }
            else
            {
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetFullShipAddressBySAId(saId);
                if (reader.Read())
                {
                    fullShipAddressInfo = BuildFullShipAddressFromReader(reader);
                }
                reader.Close();
            }

            return fullShipAddressInfo;
        }

        /// <summary>
        /// 获得用户配送地址
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="saId">配送地址id</param>
        /// <returns></returns>
        public static ShipAddressInfo GetShipAddressBySAId(int uid, int saId)
        {
            ShipAddressInfo shipAddressInfo = null;

            if (_usernosql != null)
            {
                foreach (FullShipAddressInfo tempFullShipAddressInfo in GetFullShipAddressList(uid))
                {
                    if (tempFullShipAddressInfo.SAId == saId)
                    {
                        shipAddressInfo = tempFullShipAddressInfo;
                        break;
                    }
                }
            }
            else
            {
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetShipAddressBySAId(saId);
                if (reader.Read())
                {
                    shipAddressInfo = BuildShipAddressFromReader(reader);
                }
                reader.Close();
            }

            return shipAddressInfo;
        }

        /// <summary>
        /// 删除用户配送地址
        /// </summary>
        /// <param name="saId">配送地址id</param>
        /// <param name="uid">用户id</param>
        public static bool DeleteShipAddress(int saId, int uid)
        {
            bool result = BrnShop.Core.BSPData.RDBS.DeleteShipAddress(saId, uid);
            if (_usernosql != null)
                _usernosql.DeleteShipAddress(saId, uid);
            return result;
        }

        /// <summary>
        /// 更新用户配送地址的默认状态
        /// </summary>
        /// <param name="saId">配送地址id</param>
        /// <param name="uid">用户id</param>
        /// <param name="isDefault">状态</param>
        /// <returns></returns>
        public static bool UpdateShipAddressIsDefault(int saId, int uid, int isDefault)
        {
            bool result = BrnShop.Core.BSPData.RDBS.UpdateShipAddressIsDefault(saId, uid, isDefault);
            if (_usernosql != null)
                _usernosql.UpdateShipAddressIsDefault(saId, uid, isDefault);
            return result;
        }
    }
}
