﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KFlearning.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("KFlearning.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to KFlearning.
        /// </summary>
        internal static string AppName {
            get {
                return ResourceManager.GetString("AppName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kode akses salah!.
        /// </summary>
        internal static string InvalidAccessCode {
            get {
                return ResourceManager.GetString("InvalidAccessCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Folder ini bukan proyek KFlearning..
        /// </summary>
        internal static string InvalidProjectMessage {
            get {
                return ResourceManager.GetString("InvalidProjectMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap KFlearning_logo48 {
            get {
                object obj = ResourceManager.GetObject("KFlearning_logo48", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to KFlearning harus dijalankan sebagai Administrator untuk mengubah pengaturan sistem..
        /// </summary>
        internal static string NotElevatedMessage {
            get {
                return ResourceManager.GetString("NotElevatedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Proyek pada lokasi tersebut sudah ada. Pilih lokasi lain..
        /// </summary>
        internal static string ProjectExistsMessage {
            get {
                return ResourceManager.GetString("ProjectExistsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nama proyek tidak boleh kosong atau spasi..
        /// </summary>
        internal static string ProjectNameEmptyMessage {
            get {
                return ResourceManager.GetString("ProjectNameEmptyMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to KFlearning dalam mode Run-and-Forget. Tidak dapat melakukan aksi tersebut..
        /// </summary>
        internal static string RafModeMessage {
            get {
                return ResourceManager.GetString("RafModeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sedang dalam perbaikan....
        /// </summary>
        internal static string UnderMaintenanceMessage {
            get {
                return ResourceManager.GetString("UnderMaintenanceMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Visual Studio Code tidak ditemukan. Harap install Visual Studio Code terlebih dahulu!.
        /// </summary>
        internal static string VscodeNotInstalled {
            get {
                return ResourceManager.GetString("VscodeNotInstalled", resourceCulture);
            }
        }
    }
}
