using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace KFlearning.Core.Diagnostics
{
    public interface ISystemTweaker
    {
        string WallpaperPath { get; set; }
        bool LockWallpaper { get; set; }
        bool LockDesktop { get; set; }
        bool LockUsbCopying { get; set; }
        bool LockRegistryEditor { get; set; }
        bool LockTaskManager { get; set; }
        bool LockControlPanel { get; set; }

        void Query();
        void Apply();
    }

    public class SystemTweaker : ISystemTweaker
    {
        private const string SystemPoliciesKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";
        private const string ActiveDesktopKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\ActiveDesktop";
        private const string ExplorerKey = @"Software\Microsoft\Windows\Current Version\Policies\Explorer";
        private const string StoragePoliciesKey = @"SYSTEM\Current Control Set\Control\StorageDevicePolicies";
        private const string DesktopKey = @"Control Panel\Desktop";

        private readonly IProcessManager _process;

        public SystemTweaker(IProcessManager process)
        {
            _process = process;
        }

        public string WallpaperPath { get; set; }
        public bool LockWallpaper { get; set; }
        public bool LockDesktop { get; set; }
        public bool LockUsbCopying { get; set; }
        public bool LockRegistryEditor { get; set; }
        public bool LockTaskManager { get; set; }
        public bool LockControlPanel { get; set; }

        public void Query()
        {
            using (var systemKey = Registry.CurrentUser.OpenSubKey(SystemPoliciesKey))
            using (var desktopKey = Registry.CurrentUser.OpenSubKey(DesktopKey))
            using (var explorerKey = Registry.CurrentUser.OpenSubKey(ExplorerKey))
            using (var storageKey = Registry.LocalMachine.OpenSubKey(StoragePoliciesKey))
            using (var activeDesktopKey = Registry.CurrentUser.OpenSubKey(ActiveDesktopKey))
            {
                WallpaperPath = _process.IsWindows7()
                    ? systemKey.GetStringValue("Wallpaper")
                    : desktopKey.GetStringValue("Wallpaper");
                LockWallpaper = activeDesktopKey.GetIntValue("NoChangingWallPaper", 0) == 1;
                LockDesktop = systemKey.GetIntValue("NoDispCPL", 0) == 1;
                LockRegistryEditor = systemKey.GetIntValue("DisableRegistryTools", 0) == 1;
                LockTaskManager = systemKey.GetIntValue("DisableTaskMgr", 0) == 1;
                LockUsbCopying = storageKey.GetIntValue("WriteProtect", 0) == 1;
                LockControlPanel = explorerKey.GetIntValue("NoControlPanel", 0) == 1;
            }
        }

        public void Apply()
        {
            using (var systemKey = Registry.CurrentUser.CreateSubKey(SystemPoliciesKey))
            using (var desktopKey = Registry.CurrentUser.CreateSubKey(DesktopKey))
            using (var explorerKey = Registry.CurrentUser.CreateSubKey(ExplorerKey))
            using (var storageKey = Registry.LocalMachine.CreateSubKey(StoragePoliciesKey))
            using (var activeDesktopKey = Registry.CurrentUser.CreateSubKey(ActiveDesktopKey))
            {

            }
        }

        #region Private Methods


        #endregion
    }
}
