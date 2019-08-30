﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KFlearning.Core {
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
    public class Constants {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Constants() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("KFlearning.Core.Constants", typeof(Constants).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to database.db.
        /// </summary>
        public static string DatabaseConnectionString {
            get {
                return ResourceManager.GetString("DatabaseConnectionString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to dev.
        /// </summary>
        public static string DomainName {
            get {
                return ResourceManager.GetString("DomainName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.kodesiana.com.
        /// </summary>
        public static string EndpointBase {
            get {
                return ResourceManager.GetString("EndpointBase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;/body&gt;&lt;/html&gt;.
        /// </summary>
        public static string HtmlBodyEnd {
            get {
                return ResourceManager.GetString("HtmlBodyEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE html&gt;
        ///&lt;html lang=&quot;en&quot;&gt;
        ///
        ///&lt;head&gt;
        ///    &lt;meta charset=&quot;UTF-8&quot;&gt;
        ///    &lt;meta name=&quot;viewport&quot; content=&quot;width=device-width, initial-scale=1.0&quot;&gt;
        ///    &lt;meta http-equiv=&quot;X-UA-Compatible&quot; content=&quot;ie=edge&quot;&gt;
        ///    &lt;style&gt;
        ///        body {
        ///            background: #252525;
        ///            color: #ffffff;
        ///            font-family: &quot;Segoe UI&quot;, serif;
        ///        }
        ///        
        ///        table {
        ///            border-collapse: collapse;
        ///            width: 100%;
        ///        }
        ///        
        ///        table,
        ///        tr {
        ///             [rest of string was truncated]&quot;;.
        /// </summary>
        public static string HtmlBodyStart {
            get {
                return ResourceManager.GetString("HtmlBodyStart", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to httpd.
        /// </summary>
        public static string HttpdProcessName {
            get {
                return ResourceManager.GetString("HttpdProcessName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to mysqld.
        /// </summary>
        public static string MariadbProcessName {
            get {
                return ResourceManager.GetString("MariadbProcessName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to kf-project.json.
        /// </summary>
        public static string MetadataFileName {
            get {
                return ResourceManager.GetString("MetadataFileName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] template_cpp {
            get {
                object obj = ResourceManager.GetObject("template_cpp", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///    &quot;terminal.integrated.shell.windows&quot;: &quot;C:/Windows/System32/cmd.exe&quot;,
        ///    &quot;terminal.integrated.rendererType&quot;: &quot;dom&quot;,
        ///	
        ///	&quot;vsintellicode.modify.editor.suggestSelection&quot;: &quot;automaticallyOverrodeDefaultValue&quot;,
        ///
        ///	&quot;editor.suggestSelection&quot;: &quot;first&quot;,
        ///    &quot;[json]&quot;: {
        ///        &quot;editor.defaultFormatter&quot;: &quot;HookyQR.beautify&quot;
        ///    },	
        ///}.
        /// </summary>
        public static string VscodeConfig {
            get {
                return ResourceManager.GetString("VscodeConfig", resourceCulture);
            }
        }
    }
}
