using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Inedo.BuildMaster.Extensibility;
using Inedo.BuildMaster.Extensibility.Configurers.Extension;
using Inedo.BuildMasterExtensions.NSIS;

[assembly: AssemblyTitle("NSIS")]
[assembly: AssemblyDescription("Contains an action for building Nullsoft Installer projects.")]

[assembly: ComVisible(false)]
[assembly: AssemblyCompany("Inedo, LLC")]
[assembly: AssemblyProduct("BuildMaster")]
[assembly: AssemblyCopyright("Copyright © 2008 - 2016")]
[assembly: AssemblyVersion("0.0.0.0")]
[assembly: AssemblyFileVersion("0.0")]
[assembly: CLSCompliant(false)]
[assembly: RequiredBuildMasterVersion("5.0.0")]
[assembly: ExtensionConfigurer(typeof(NSISExtensionConfigurer))]