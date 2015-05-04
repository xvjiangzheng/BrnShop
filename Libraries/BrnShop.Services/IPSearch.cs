using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// IP搜索操作管理类
    /// </summary>
    public partial class IPSearch
    {
        private static object _locker = new object();//锁对象

        private const byte MODE1 = 0x01;//第一种模式
        private const byte MODE2 = 0x02;//第二种模式
        private const int LENGTH = 7;//每条记录长度

        private static bool _state = false;//状态
        private static FileStream _ipdatefile;//ip数据文件流
        private static long _indexareabegin;//索引区域开始偏移量
        private static long _indexareaend;//索引区域结束偏移量
        private static long _indexcount;//索引个数

        static IPSearch()
        {
            string filePath = IOHelper.GetMapPath("/App_Data/ipdata.config");
            if (File.Exists(filePath))
            {
                try
                {
                    _ipdatefile = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                }
                catch
                {
                    _ipdatefile.Close();
                    _ipdatefile.Dispose();
                    return;
                }
                _ipdatefile.Position = 0;
                _indexareabegin = ReadByte4();
                _indexareaend = ReadByte4();
                _indexcount = (_indexareaend - _indexareabegin) / LENGTH + 1;

                if (_indexcount > 0)
                {
                    _state = true;
                }
                else
                {
                    _ipdatefile.Close();
                    _ipdatefile.Dispose();
                }
            }
        }

        /// <summary>
        /// 搜索ip位置
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        public static IPLocation SearchLocation(string ip)
        {
            lock (_locker)
            {
                if (_state)
                {
                    try
                    {
                        string[] ipList = ip.Split('.');
                        if (ipList.Length != 4)
                            return null;

                        byte[] ipByteArray = new byte[4];
                        for (int i = 0; i < 4; i++)
                        {
                            ipByteArray[3 - i] = (byte)(Convert.ToInt32(ipList[i]) & 0xFF);
                        }
                        long ipUInt32 = BitConverter.ToUInt32(ipByteArray, 0);

                        //以下为二分查找算法
                        long searchBegin = 0;
                        long searchEnd = _indexcount - 1;
                        long begin = 0;
                        long offset = 0;
                        long end = 0;
                        while (true)
                        {
                            SetIndex(searchBegin, out begin, out offset, out end);
                            if (ipUInt32 >= begin && ipUInt32 <= end)
                                return ReadIPLocation(offset);

                            SetIndex(searchEnd, out begin, out offset, out end);
                            if (ipUInt32 >= begin && ipUInt32 <= end)
                                return ReadIPLocation(offset);

                            SetIndex((searchBegin + searchEnd) / 2, out begin, out offset, out end);
                            if (ipUInt32 >= begin && ipUInt32 <= end)
                                return ReadIPLocation(offset);

                            if (ipUInt32 < begin)
                                searchEnd = (searchBegin + searchEnd) / 2;
                            else
                                searchBegin = (searchBegin + searchEnd) / 2;
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 获得ip对应区域
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        public static RegionInfo SearchRegion(string ip)
        {
            IPLocation ipLocation = SearchLocation(ip);
            if (ipLocation == null || ipLocation.Country.Length == 0)
                return null;

            string country = ipLocation.Country;

            int startIndex = country.IndexOf("省");
            int endIndex = country.IndexOf("市");
            if (startIndex > 0 && endIndex > 0 && endIndex > startIndex)
            {
                RegionInfo regionInfo = Regions.GetRegionByNameAndLayer(country.Substring(startIndex + 1, endIndex - startIndex), 2);
                if (regionInfo != null)
                    return regionInfo;
            }

            if (country.StartsWith("北京"))
            {
                return Regions.GetRegionById(35);
            }
            else if (country.StartsWith("天津"))
            {
                return Regions.GetRegionById(36);
            }
            else if (country.StartsWith("上海"))
            {
                return Regions.GetRegionById(107);
            }
            else if (country.StartsWith("重庆"))
            {
                return Regions.GetRegionById(269);
            }
            else if (country.StartsWith("四川省"))
            {
                if (country.Length == 3)
                    return Regions.GetRegionById(23);
                else if (country.StartsWith("四川省阿坝州"))
                    return Regions.GetRegionById(288);
                else if (country.StartsWith("四川省甘孜州"))
                    return Regions.GetRegionById(289);
                else if (country.StartsWith("四川省凉山州"))
                    return Regions.GetRegionById(290);
                else if (country.Contains("市"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(3, country.IndexOf('市') - 3), 2);
            }
            else if (country.StartsWith("湖南省"))
            {
                if (country.Length == 3)
                    return Regions.GetRegionById(18);
                else if (country.StartsWith("湖南省湘西州"))
                    return Regions.GetRegionById(230);
                else if (country.Contains("市"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(3, country.IndexOf('市') - 3), 2);
            }
            else if (country.StartsWith("湖北省"))
            {
                if (country.Length == 3)
                    return Regions.GetRegionById(17);
                else if (country.StartsWith("湖北省神农架林区"))
                    return Regions.GetRegionById(1997);
                else if (country.Contains("市"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(3, country.IndexOf('市') - 3), 2);
            }
            else if (country.StartsWith("广西"))
            {
                if (country.Length == 2)
                    return Regions.GetRegionById(20);
                else if (country.Contains("市"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(2, country.IndexOf('市') - 2), 2);
            }
            else if (country.StartsWith("云南省"))
            {
                if (country.Length == 3)
                    return Regions.GetRegionById(25);
                else if (country.StartsWith("云南省楚雄州"))
                    return Regions.GetRegionById(308);
                else if (country.StartsWith("云南省红河州"))
                    return Regions.GetRegionById(309);
                else if (country.StartsWith("云南省文山州"))
                    return Regions.GetRegionById(310);
                else if (country.StartsWith("云南省西双版纳州"))
                    return Regions.GetRegionById(311);
                else if (country.StartsWith("云南省大理州"))
                    return Regions.GetRegionById(312);
                else if (country.StartsWith("云南省德宏州"))
                    return Regions.GetRegionById(313);
                else if (country.StartsWith("云南省怒江州"))
                    return Regions.GetRegionById(314);
                else if (country.StartsWith("云南省迪庆州"))
                    return Regions.GetRegionById(315);
                else if (country.Contains("市"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(3, country.IndexOf('市') - 3), 2);
            }
            else if (country.StartsWith("黑龙江省"))
            {
                if (country.Length == 4)
                    return Regions.GetRegionById(8);
                else if (country.Contains("市"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(4, country.IndexOf('市') - 4), 2);
                else if (country.Contains("地区"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(4, country.IndexOf("地区") - 3), 2);
            }
            else if (country.StartsWith("吉林省"))
            {
                if (country.Length == 3)
                    return Regions.GetRegionById(7);
                else if (country.StartsWith("吉林省延边州"))
                    return Regions.GetRegionById(93);
                else if (country.Contains("市"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(3, country.IndexOf('市') - 3), 2);
            }
            else if (country.StartsWith("内蒙古"))
            {
                if (country.Length == 3)
                    return Regions.GetRegionById(5);
                else if (country.Contains("盟"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(3, country.IndexOf('盟') - 3), 2);
                else if (country.Contains("市"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(3, country.IndexOf('市') - 3), 2);
            }
            else if (country.StartsWith("贵州省"))
            {
                if (country.Length == 3)
                    return Regions.GetRegionById(24);
                else if (country.StartsWith("贵州省黔西南"))
                    return Regions.GetRegionById(296);
                else if (country.StartsWith("贵州省黔东南"))
                    return Regions.GetRegionById(298);
                else if (country.StartsWith("贵州省黔南州"))
                    return Regions.GetRegionById(299);
                else if (country.Contains("市"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(3, country.IndexOf('市') - 3), 2);
            }
            else if (country.StartsWith("甘肃省"))
            {
                if (country.Length == 3)
                    return Regions.GetRegionById(28);
                else if (country.StartsWith("甘肃省临夏州"))
                    return Regions.GetRegionById(345);
                else if (country.StartsWith("甘肃省甘南"))
                    return Regions.GetRegionById(346);
                else if (country.Contains("市"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(3, country.IndexOf('市') - 3), 2);
            }
            else if (country.StartsWith("新疆"))
            {
                if (country.Length == 2)
                    return Regions.GetRegionById(31);
                else if (country.StartsWith("新疆阿勒泰"))
                    return Regions.GetRegionById(373);
                else if (country.StartsWith("新疆昌吉州"))
                    return Regions.GetRegionById(364);
                else if (country.StartsWith("新疆博尔塔拉州"))
                    return Regions.GetRegionById(365);
                else if (country.StartsWith("新疆巴音郭楞州"))
                    return Regions.GetRegionById(366);
                else if (country.StartsWith("新疆克孜勒苏柯尔州"))
                    return Regions.GetRegionById(368);
                else if (country.StartsWith("新疆伊犁州"))
                    return Regions.GetRegionById(371);
                else if (country.Contains("市"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(2, country.IndexOf('市') - 2), 2);
                else if (country.Contains("地区"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(2, country.IndexOf("地区") - 1), 2);
            }
            else if (country.StartsWith("青海省"))
            {
                if (country.Length == 3)
                    return Regions.GetRegionById(29);
                else if (country.StartsWith("青海省海北州"))
                    return Regions.GetRegionById(349);
                else if (country.StartsWith("青海省黄南州"))
                    return Regions.GetRegionById(350);
                else if (country.StartsWith("青海省海南州"))
                    return Regions.GetRegionById(351);
                else if (country.StartsWith("青海省果洛州"))
                    return Regions.GetRegionById(352);
                else if (country.StartsWith("青海省玉树州"))
                    return Regions.GetRegionById(353);
                else if (country.StartsWith("青海省海西州"))
                    return Regions.GetRegionById(354);
                else if (country.Contains("市"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(3, country.IndexOf('市') - 3), 2);
            }
            else if (country.StartsWith("西藏"))
            {
                if (country.Length == 2)
                    return Regions.GetRegionById(26);
                else if (country.Contains("市"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(2, country.IndexOf('市') - 2), 2);
                else if (country.Contains("地区"))
                    return Regions.GetRegionByNameAndLayer(country.Substring(2, country.IndexOf("地区") - 1), 2);
            }
            else if (country.StartsWith("香港"))
            {
                return Regions.GetRegionById(3241);
            }
            else if (country.StartsWith("澳门"))
            {
                return Regions.GetRegionById(3242);
            }
            else if (country.StartsWith("台湾"))
            {
                return Regions.GetRegionById(3240);
            }
            return null;
        }

        /// <summary>
        /// 读取位置
        /// </summary>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        private static IPLocation ReadIPLocation(long offset)
        {
            IPLocation ipLocation = new IPLocation();

            _ipdatefile.Position = offset + 4;

            byte flag = (byte)_ipdatefile.ReadByte();
            if (flag == MODE1)
            {
                long countryOffset = ReadByte3();
                _ipdatefile.Position = countryOffset;
                flag = (byte)_ipdatefile.ReadByte();
                if (flag == MODE2)
                {
                    ipLocation.Country = ReadString(ReadByte3());
                    _ipdatefile.Position = countryOffset + 4;
                }
                else
                {
                    ipLocation.Country = ReadString(countryOffset);
                }
                ipLocation.Area = ReadArea(_ipdatefile.Position);
            }
            else if (flag == MODE2)
            {
                ipLocation.Country = ReadString(ReadByte3());
                ipLocation.Area = ReadArea(offset + 8);
            }
            else
            {
                ipLocation.Country = ReadString(--_ipdatefile.Position);
                ipLocation.Area = ReadString(_ipdatefile.Position);
            }
            return ipLocation;
        }

        /// <summary>
        /// 读取区域
        /// </summary>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        private static string ReadArea(long offset)
        {
            _ipdatefile.Position = offset;
            byte flag = (byte)_ipdatefile.ReadByte();
            if (flag == MODE1 || flag == MODE2)
            {
                long areaOffset = ReadByte3(offset + 1);
                if (areaOffset == 0)
                    return "";
                else
                    return ReadString(areaOffset);
            }
            else
            {
                return ReadString(offset);
            }
        }

        private static string ReadString(long offset)
        {
            _ipdatefile.Position = offset;
            byte[] byteArray = new byte[256];
            int i = 0;
            for (i = 0, byteArray[i] = (byte)_ipdatefile.ReadByte(); byteArray[i] != (byte)(0); byteArray[++i] = (byte)_ipdatefile.ReadByte()) ;
            if (i > 0)
                return Encoding.GetEncoding("GB2312").GetString(byteArray, 0, i).TrimEnd('\0').Trim();
            else
                return "";
        }

        private static long ReadByte4()
        {
            byte[] byteArray = new byte[4];
            _ipdatefile.Read(byteArray, 0, 4);
            return BitConverter.ToUInt32(byteArray, 0);
        }

        private static long ReadByte3()
        {
            byte[] byteArray = new byte[4];
            byteArray[0] = (byte)_ipdatefile.ReadByte();
            byteArray[1] = (byte)_ipdatefile.ReadByte();
            byteArray[2] = (byte)_ipdatefile.ReadByte();
            byteArray[3] = 0;
            return BitConverter.ToUInt32(byteArray, 0);
        }

        private static long ReadByte3(long offset)
        {
            _ipdatefile.Position = offset;
            byte[] byteArray = new byte[4];
            byteArray[0] = (byte)_ipdatefile.ReadByte();
            byteArray[1] = (byte)_ipdatefile.ReadByte();
            byteArray[2] = (byte)_ipdatefile.ReadByte();
            byteArray[3] = 0;
            return BitConverter.ToUInt32(byteArray, 0);
        }

        private static void SetIndex(long pos, out long begin, out long offset, out long end)
        {
            _ipdatefile.Position = _indexareabegin + LENGTH * pos;
            begin = ReadByte4();
            offset = ReadByte3();
            _ipdatefile.Position = offset;
            end = ReadByte4();
        }
    }

    /// <summary>
    /// IP位置
    /// </summary>
    public class IPLocation
    {
        private string _country;//国家
        private string _area;//区域

        /// <summary>
        /// 国家
        /// </summary>
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }
        /// <summary>
        /// 区域
        /// </summary>
        public string Area
        {
            get { return _area; }
            set { _area = value; }
        }
    }
}
