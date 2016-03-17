using System;
using System.ComponentModel;
using System.IO;
using Inedo.BuildMaster.Extensibility.Configurers.Extension;
using Inedo.Serialization;
using Microsoft.Win32;

namespace Inedo.BuildMasterExtensions.NSIS
{
    /// <summary>
    /// Contains global configuration for the NSIS extension.
    /// </summary>
    public sealed class NSISExtensionConfigurer : ExtensionConfigurerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NSISExtensionConfigurer"/> class.
        /// </summary>
        public NSISExtensionConfigurer()
        {
            this.NSISPath = GetNSISPath();
        }

        [Persistent]
        [DisplayName("NSIS Path")]
        [Category("Configuration")]
        [Description(@"Full path to the directory where NSIS is installed. (Example: C:\Program Files\NSIS)")]
        public string NSISPath { get; set; }

        public override string ToString()
        {
            return string.Empty;
        }

        private static string GetNSISPath()
        {
            string nsisPath = null;

            using (var key1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\NSIS", false))
            {
                if(key1 == null || string.IsNullOrEmpty(nsisPath = (key1.GetValue(null) as string)))
                {
                    using (var key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\NSIS", false))
                    {
                        if (key2 != null)
                            nsisPath = key2.GetValue(null) as string;

                            
                    }
                }
            }

            if (string.IsNullOrEmpty(nsisPath))
                nsisPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "NSIS");

            return nsisPath;
        }
    }
}
