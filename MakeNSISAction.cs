using System.ComponentModel;
using Inedo.BuildMaster;
using Inedo.BuildMaster.Extensibility.Actions;
using Inedo.BuildMaster.Web;
using Inedo.IO;
using Inedo.Serialization;

namespace Inedo.BuildMasterExtensions.NSIS
{
    /// <summary>
    /// Build NSIS Project action.
    /// </summary>
    [DisplayName("Build NSIS Project")]
    [Description("Executes MakeNSIS to build an NSIS project.")]
    [Tag("nsis")]
    [CustomEditor(typeof(MakeNSISActionEditor))]
    public sealed class MakeNSISAction : AgentBasedActionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MakeNSISAction"/> class.
        /// </summary>
        public MakeNSISAction()
        {
        }

        /// <summary>
        /// Gets or sets the script file to execute.
        /// </summary>
        [Persistent]
        public string ScriptFile { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        /// <remarks>
        /// This should return a user-friendly string describing what the Action does
        /// and the state of its important persistent properties.
        /// </remarks>
        public override string ToString()
        {
            return string.Format(
                "Build {0} using NSIS",
                PathEx.Combine(this.OverriddenSourceDirectory ?? string.Empty, this.ScriptFile ?? string.Empty)
            );
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <remarks>
        /// This method is always called on the BuildMaster server.
        /// </remarks>
        protected override void Execute()
        {
            var configurer = (NSISExtensionConfigurer)this.GetExtensionConfigurer();
            if (string.IsNullOrEmpty(configurer.NSISPath))
            {
                this.LogError("NSIS could not be found on this server. Verify that it is installed and that the extension configuration is correct.");
                return;
            }

            if (string.IsNullOrEmpty(this.ScriptFile))
            {
                this.LogError("Script File has not been specified.");
                return;
            }

            var nsisPath = PathEx.Combine(configurer.NSISPath, "makensis.exe");

            this.LogInformation("Running MakeNSIS...");

            int result = this.ExecuteCommandLine(nsisPath, "\"" + this.ScriptFile + "\"");

            if (result == 0)
                this.LogInformation("MakeNSIS complete.");
            else
                this.LogError("MakeNSIS returned error code {0}", result);
        }
    }
}
