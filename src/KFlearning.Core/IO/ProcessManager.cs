// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProcessManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using KFlearning.Core.Native;
using Microsoft.Win32;

#endregion

namespace KFlearning.Core.IO
{
    public class ProcessManager : IProcessManager
    {
        public bool IsRunning(string name)
        {
            var processes = Process.GetProcessesByName("httpd");
            return processes.Length > 0;
        }

        public bool IsProcessElevated()
        {
            if (!IsUacEnabled())
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                bool result = principal.IsInRole(WindowsBuiltInRole.Administrator)
                              || principal.IsInRole(0x200); //Domain Administrator
                return result;
            }

            if (!NativeMethods.OpenProcessToken(Process.GetCurrentProcess().Handle, NativeConstants.TOKEN_READ,
                out TokenSafeHandle tokenHandle))
            {
                throw new Win32Exception();
            }

            using (tokenHandle)
            {
                int elevationResultSize = Marshal.SizeOf(Enum.GetUnderlyingType(typeof(TOKEN_ELEVATION_TYPE)));
                IntPtr elevationTypePtr = Marshal.AllocHGlobal(elevationResultSize);

                try
                {
                    bool success = NativeMethods.GetTokenInformation(tokenHandle,
                        TOKEN_INFORMATION_CLASS.TokenElevationType, elevationTypePtr, (uint) elevationResultSize,
                        out uint _);
                    if (success)
                    {
                        var elevationResult = (TOKEN_ELEVATION_TYPE) Marshal.ReadInt32(elevationTypePtr);
                        bool isProcessAdmin = elevationResult == TOKEN_ELEVATION_TYPE.TokenElevationTypeFull;
                        return isProcessAdmin;
                    }
                    else
                    {
                        throw new ApplicationException("Unable to determine the current elevation.");
                    }
                }
                finally
                {
                    if (elevationTypePtr != IntPtr.Zero)
                        Marshal.FreeHGlobal(elevationTypePtr);
                }
            }
        }

        public bool IsUacEnabled()
        {
            using (RegistryKey uacKey = Registry.LocalMachine.OpenSubKey(NativeConstants.UacRegistryKey, false))
            {
                return uacKey != null && uacKey.GetValue(NativeConstants.UacRegistryValue).Equals(1);
            }
        }

        public void Run(string filename, string args, bool show = false)
        {
            Process.Start(filename, args);
        }

        public void RunWait(string filename, string args, bool show = false)
        {
            var procInfo = new ProcessStartInfo
            {
                FileName = filename,
                Arguments = args,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(procInfo)?.WaitForExit();
        }

        public void RunJob(string filename, string args, bool show = false)
        {
            Process.Start(filename, args);
        }

        public void TerminateJob(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            if (!processes.Any()) return;
            foreach (Process process in processes)
            {
                process.Kill();
            }
        }
    }
}