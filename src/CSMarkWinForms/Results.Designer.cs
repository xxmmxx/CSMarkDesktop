﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CSMarkWinForms {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Results : global::System.Configuration.ApplicationSettingsBase {
        
        private static Results defaultInstance = ((Results)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Results())));
        
        public static Results Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::CSMarkLib.BenchmarkLib.Result BenchmarkResult {
            get {
                return ((global::CSMarkLib.BenchmarkLib.Result)(this["BenchmarkResult"]));
            }
            set {
                this["BenchmarkResult"] = value;
            }
        }
    }
}
