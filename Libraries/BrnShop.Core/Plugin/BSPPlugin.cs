using System;
using System.IO;
using System.Web;
using System.Reflection;
using System.Web.Compilation;
using System.Collections.Generic;

[assembly: PreApplicationStartMethod(typeof(BrnShop.Core.BSPPlugin), "Load")]
namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop插件管理类
    /// </summary>
    public class BSPPlugin
    {
        private static object _locker = new object();//锁对象

        private static string _installedfilepath = "/App_Data/installedplugin.config";//插件安装文件
        private static string _pluginfolderpath = "/plugins";//插件目录
        private static string _shadowfolderpath = "/plugins/bin";//插件影子目录

        private static List<PluginInfo> _oauthpluginlist = new List<PluginInfo>();//开放授权插件列表
        private static List<PluginInfo> _paypluginlist = new List<PluginInfo>();//支付插件列表
        private static List<PluginInfo> _shippluginlist = new List<PluginInfo>();//配送插件列表
        private static List<PluginInfo> _uninstalledpluginlist = new List<PluginInfo>();//未安装插件列表

        /// <summary>
        /// 开放授权插件列表
        /// </summary>
        public static List<PluginInfo> OAuthPluginList
        {
            get { return _oauthpluginlist; }
        }
        /// <summary>
        /// 支付插件列表
        /// </summary>
        public static List<PluginInfo> PayPluginList
        {
            get { return _paypluginlist; }
        }
        /// <summary>
        /// 配送插件列表
        /// </summary>
        public static List<PluginInfo> ShipPluginList
        {
            get { return _shippluginlist; }
        }
        /// <summary>
        /// 未安装插件列表
        /// </summary>
        public static List<PluginInfo> UnInstalledPluginList
        {
            get { return _uninstalledpluginlist; }
        }

        /// <summary>
        /// 加载插件程序集到应用程序域中
        /// </summary>
        public static void Load()
        {
            try
            {
                //插件目录
                DirectoryInfo pluginFolder = new DirectoryInfo(IOHelper.GetMapPath(_pluginfolderpath));
                if (!pluginFolder.Exists)
                    pluginFolder.Create();
                //插件bin目录
                DirectoryInfo shadowFolder = new DirectoryInfo(IOHelper.GetMapPath(_shadowfolderpath));
                if (!shadowFolder.Exists)
                {
                    shadowFolder.Create();
                }
                else
                {
                    //清空影子复制目录中的dll文件
                    foreach (FileInfo fileInfo in shadowFolder.GetFiles())
                    {
                        fileInfo.Delete();
                    }
                }

                //获得安装的插件系统名称列表
                List<string> installedPluginSystemNameList = GetInstalledPluginSystemNameList();
                //获得全部插件
                List<KeyValuePair<FileInfo, PluginInfo>> allPluginFileAndInfo = GetAllPluginFileAndInfo(pluginFolder);
                foreach (KeyValuePair<FileInfo, PluginInfo> fileAndInfo in allPluginFileAndInfo)
                {
                    FileInfo pluginFile = fileAndInfo.Key;
                    PluginInfo pluginInfo = fileAndInfo.Value;

                    if (String.IsNullOrWhiteSpace(pluginInfo.SystemName))
                        throw new BSPException(string.Format("插件'{0}'没有\"systemName\", 请输入一个唯一的\"systemName\"", pluginFile.FullName));
                    if (pluginInfo.Type < 0 || pluginInfo.Type > 2)
                        throw new BSPException(string.Format("插件'{0}'不属于任何一种类型, 请输入正确的的\"type\"", pluginFile.FullName));

                    //加载插件dll文件
                    FileInfo[] dllFiles = pluginFile.Directory.GetFiles("*.dll", SearchOption.TopDirectoryOnly);
                    foreach (FileInfo dllFile in dllFiles)
                    {
                        //部署dll文件
                        DeployDllFile(dllFile, shadowFolder);
                    }

                    if (IsInstalledlPlugin(pluginInfo.SystemName, installedPluginSystemNameList))//安装的插件
                    {
                        //根据插件类型将插件添加到相应列表
                        switch (pluginInfo.Type)
                        {
                            case 0:
                                _oauthpluginlist.Add(pluginInfo);
                                break;
                            case 1:
                                _paypluginlist.Add(pluginInfo);
                                break;
                            case 2:
                                _shippluginlist.Add(pluginInfo);
                                break;
                        }
                    }
                    else//未安装的插件
                    {
                        _uninstalledpluginlist.Add(pluginInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BSPException("加载BrnShop插件时出错", ex);
            }
        }

        /// <summary>
        /// 安装插件
        /// </summary>
        /// <param name="systemName">插件系统名称</param>
        public static void Install(string systemName)
        {
            lock (_locker)
            {
                if (string.IsNullOrWhiteSpace(systemName))
                    return;

                //在未安装的插件列表中获得对应插件
                PluginInfo pluginInfo = _uninstalledpluginlist.Find(x => x.SystemName.Equals(systemName, StringComparison.InvariantCultureIgnoreCase));

                //当插件为空时直接返回
                if (pluginInfo == null)
                    return;

                //当插件不为空时将插件添加到相应列表
                switch (pluginInfo.Type)
                {
                    case 0:
                        _oauthpluginlist.Add(pluginInfo);
                        _oauthpluginlist.Sort((first, next) => first.DisplayOrder.CompareTo(next.DisplayOrder));
                        break;
                    case 1:
                        _paypluginlist.Add(pluginInfo);
                        _paypluginlist.Sort((first, next) => first.DisplayOrder.CompareTo(next.DisplayOrder));
                        break;
                    case 2:
                        _shippluginlist.Add(pluginInfo);
                        _shippluginlist.Sort((first, next) => first.DisplayOrder.CompareTo(next.DisplayOrder));
                        break;
                }

                //在未安装的插件列表中移除对应插件
                _uninstalledpluginlist.Remove(pluginInfo);

                //将新安装的插件保存到安装的插件列表中
                List<string> installedPluginSystemNameList = GetInstalledPluginSystemNameList();
                installedPluginSystemNameList.Add(pluginInfo.SystemName);
                SaveInstalledPluginSystemNameList(installedPluginSystemNameList);
            }
        }

        /// <summary>
        /// 卸载插件
        /// </summary>
        /// <param name="systemName">插件系统名称</param>
        public static void Uninstall(string systemName)
        {
            lock (_locker)
            {
                if (string.IsNullOrEmpty(systemName))
                    return;

                PluginInfo pluginInfo = null;
                Predicate<PluginInfo> condition = x => x.SystemName.Equals(systemName, StringComparison.InvariantCultureIgnoreCase);
                pluginInfo = _oauthpluginlist.Find(condition);
                if (pluginInfo == null)
                    pluginInfo = _paypluginlist.Find(condition);
                if (pluginInfo == null)
                    pluginInfo = _shippluginlist.Find(condition);

                //当插件为空时直接返回
                if (pluginInfo == null)
                    return;

                //根据插件类型移除对应插件
                switch (pluginInfo.Type)
                {
                    case 0:
                        _oauthpluginlist.Remove(pluginInfo);
                        break;
                    case 1:
                        _paypluginlist.Remove(pluginInfo);
                        break;
                    case 2:
                        _shippluginlist.Remove(pluginInfo);
                        break;
                }

                //将插件添加到未安装插件列表
                _uninstalledpluginlist.Add(pluginInfo);

                //将卸载的插件从安装的插件列表中移除
                List<string> installedPluginSystemNameList = GetInstalledPluginSystemNameList();
                installedPluginSystemNameList.Remove(pluginInfo.SystemName);
                SaveInstalledPluginSystemNameList(installedPluginSystemNameList);
            }
        }

        /// <summary>
        /// 编辑插件信息
        /// </summary>
        /// <param name="systemName">插件系统名称</param>
        /// <param name="friendlyName">插件友好名称</param>
        /// <param name="description">插件描述</param>
        /// <param name="displayOrder">插件排序</param>
        public static void Edit(string systemName, string friendlyName, string description, int displayOrder)
        {
            lock (_locker)
            {
                bool isInstalled = true;//是否安装
                PluginInfo pluginInfo = null;
                Predicate<PluginInfo> condition = x => x.SystemName.Equals(systemName, StringComparison.InvariantCultureIgnoreCase);

                pluginInfo = _oauthpluginlist.Find(condition);
                if (pluginInfo == null)
                    pluginInfo = _paypluginlist.Find(condition);
                if (pluginInfo == null)
                    pluginInfo = _shippluginlist.Find(condition);

                //当插件为空时直接返回
                if (pluginInfo == null)
                {
                    pluginInfo = _uninstalledpluginlist.Find(condition); ;
                    //当插件为空时直接返回
                    if (pluginInfo == null)
                        return;
                    else
                        isInstalled = false;
                }

                pluginInfo.FriendlyName = friendlyName;
                pluginInfo.Description = description;
                pluginInfo.DisplayOrder = displayOrder;

                //将插件信息持久化到对应文件中
                IOHelper.SerializeToXml(pluginInfo, IOHelper.GetMapPath("/plugins/" + pluginInfo.Folder + "/plugin.config"));

                //插件列表重新排序
                if (isInstalled)
                {
                    switch (pluginInfo.Type)
                    {
                        case 0:
                            _oauthpluginlist.Sort((first, next) => first.DisplayOrder.CompareTo(next.DisplayOrder));
                            break;
                        case 1:
                            _paypluginlist.Sort((first, next) => first.DisplayOrder.CompareTo(next.DisplayOrder));
                            break;
                        case 2:
                            _shippluginlist.Sort((first, next) => first.DisplayOrder.CompareTo(next.DisplayOrder));
                            break;
                    }
                }
                else
                {
                    _uninstalledpluginlist.Sort((first, next) => first.DisplayOrder.CompareTo(next.DisplayOrder));
                }

            }
        }

        /// <summary>
        /// 设置默认插件
        /// </summary>
        /// <param name="systemName">插件系统名称</param>
        public static void Default(string systemName)
        {
            lock (_locker)
            {
                if (string.IsNullOrEmpty(systemName))
                    return;

                PluginInfo pluginInfo = null;
                Predicate<PluginInfo> condition = x => x.SystemName.Equals(systemName, StringComparison.InvariantCultureIgnoreCase);
                pluginInfo = _oauthpluginlist.Find(condition);
                if (pluginInfo == null)
                    pluginInfo = _paypluginlist.Find(condition);
                if (pluginInfo == null)
                    pluginInfo = _shippluginlist.Find(condition);
                if (pluginInfo == null)
                    pluginInfo = _uninstalledpluginlist.Find(condition);

                //当插件为空时直接返回
                if (pluginInfo == null)
                    return;

                List<PluginInfo> updatePluginList = new List<PluginInfo>();
                if (pluginInfo.Type == 0)
                {
                    foreach (PluginInfo info in _oauthpluginlist)
                    {
                        if (info.IsDefault == 1)
                        {
                            info.IsDefault = 0;
                            updatePluginList.Add(info);
                        }
                    }
                }
                else if (pluginInfo.Type == 1)
                {
                    foreach (PluginInfo info in _paypluginlist)
                    {
                        if (info.IsDefault == 1)
                        {
                            info.IsDefault = 0;
                            updatePluginList.Add(info);
                        }
                    }
                }
                else
                {
                    foreach (PluginInfo info in _shippluginlist)
                    {
                        if (info.IsDefault == 1)
                        {
                            info.IsDefault = 0;
                            updatePluginList.Add(info);
                        }
                    }
                }

                foreach (PluginInfo info in _uninstalledpluginlist)
                {
                    if (info.Type == pluginInfo.Type && info.IsDefault == 1)
                    {
                        info.IsDefault = 0;
                        updatePluginList.Add(info);
                    }
                }

                pluginInfo.IsDefault = 1;
                updatePluginList.Add(pluginInfo);

                //将插件信息持久化到对应文件中
                foreach (PluginInfo info in updatePluginList)
                    IOHelper.SerializeToXml(info, IOHelper.GetMapPath("/plugins/" + info.Folder + "/plugin.config"));
            }
        }

        /// <summary>
        /// 获得安装的插件系统名称列表
        /// </summary>
        private static List<string> GetInstalledPluginSystemNameList()
        {
            return (List<string>)IOHelper.DeserializeFromXML(typeof(List<string>), IOHelper.GetMapPath(_installedfilepath));
        }

        /// <summary>
        /// 保存安装的插件系统名称列表
        /// </summary>
        /// <param name="installedPluginSystemNameList">安装的插件系统名称列表</param>
        private static void SaveInstalledPluginSystemNameList(List<string> installedPluginSystemNameList)
        {
            IOHelper.SerializeToXml(installedPluginSystemNameList, IOHelper.GetMapPath(_installedfilepath));
        }

        /// <summary>
        /// 获得全部插件
        /// </summary>
        /// <param name="pluginFolder">插件目录</param>
        /// <returns></returns>
        private static List<KeyValuePair<FileInfo, PluginInfo>> GetAllPluginFileAndInfo(DirectoryInfo pluginFolder)
        {
            List<KeyValuePair<FileInfo, PluginInfo>> list = new List<KeyValuePair<FileInfo, PluginInfo>>();
            FileInfo[] PluginInfoes = pluginFolder.GetFiles("plugin.config", SearchOption.AllDirectories);
            Type pluginType = typeof(PluginInfo);
            foreach (FileInfo file in PluginInfoes)
            {
                PluginInfo info = (PluginInfo)IOHelper.DeserializeFromXML(pluginType, file.FullName);
                list.Add(new KeyValuePair<FileInfo, PluginInfo>(file, info));
            }

            list.Sort((firstPair, nextPair) => firstPair.Value.DisplayOrder.CompareTo(nextPair.Value.DisplayOrder));
            return list;
        }

        /// <summary>
        /// 判断插件是否已经安装
        /// </summary>
        /// <param name="systemName">插件系统名称</param>
        /// <param name="installedPluginSystemNameList">安装的插件系统名称列表</param>
        /// <returns> </returns>
        private static bool IsInstalledlPlugin(string systemName, List<string> installedPluginSystemNameList)
        {
            foreach (string name in installedPluginSystemNameList)
            {
                if (name.Equals(systemName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 部署程序集
        /// </summary>
        /// <param name="dllFile">插件程序集文件</param>
        /// <param name="shadowFolder">/Plugins/bin目录</param>
        private static void DeployDllFile(FileInfo dllFile, DirectoryInfo shadowFolder)
        {
            DirectoryInfo copyFolder;
            //根据当前的信任级别设置复制目录
            if (WebHelper.GetTrustLevel() != AspNetHostingPermissionLevel.Unrestricted)//非完全信任级别
            {
                copyFolder = shadowFolder;
            }
            else//完全信任级别
            {
                copyFolder = new DirectoryInfo(AppDomain.CurrentDomain.DynamicDirectory);
            }

            FileInfo newDllFile = new FileInfo(copyFolder.FullName + "\\" + dllFile.Name);
            try
            {
                File.Copy(dllFile.FullName, newDllFile.FullName, true);
            }
            catch (Exception oex)//在某些情况下会出现"正由另一进程使用，因此该进程无法访问该文件"错误，所以先重命名再复制
            {
                try
                {
                    File.Move(newDllFile.FullName, newDllFile.FullName + Guid.NewGuid().ToString("N") + ".locked");
                }
                catch (Exception iex)
                {
                    throw iex;
                }
                File.Copy(dllFile.FullName, newDllFile.FullName, true);
            }

            Assembly assembly = Assembly.LoadFrom(newDllFile.FullName);
            //将程序集添加到当前应用程序域
            BuildManager.AddReferencedAssembly(assembly);
        }
    }
}
