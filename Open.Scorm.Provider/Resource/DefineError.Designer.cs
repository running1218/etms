﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Open.Scorm.Message {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class DefineError {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DefineError() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Open.Scorm.Provider.Resource.DefineError", typeof(DefineError).Assembly);
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
        ///   Looks up a localized string similar to 【imsmanifest.xml】格式不正确，请检查！.
        /// </summary>
        public static string Error_ImsmanifestFile {
            get {
                return ResourceManager.GetString("Error_ImsmanifestFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 课件资源无效，请核对是否正确！.
        /// </summary>
        public static string Error_InvalidResource {
            get {
                return ResourceManager.GetString("Error_InvalidResource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 清单文件命名空间不符合标准，请核实是否正确！.
        /// </summary>
        public static string Error_Namesapce {
            get {
                return ResourceManager.GetString("Error_Namesapce", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 出现未知的错误，请与管理员联系！.
        /// </summary>
        public static string Error_NotKnow {
            get {
                return ResourceManager.GetString("Error_NotKnow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 【imsmanifest.xml】清单文件路径未能找到，请核实是否存在！.
        /// </summary>
        public static string Error_Path {
            get {
                return ResourceManager.GetString("Error_Path", resourceCulture);
            }
        }
    }
}
