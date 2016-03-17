using Inedo.BuildMaster.Extensibility.Actions;
using Inedo.BuildMaster.Web.Controls;
using Inedo.BuildMaster.Web.Controls.Extensions;
using Inedo.IO;
using Inedo.Web.Controls;

namespace Inedo.BuildMasterExtensions.NSIS
{
    internal sealed class MakeNSISActionEditor : ActionEditorBase
    {
        private SourceControlFileFolderPicker ctlScriptFile;

        public override void BindToForm(ActionBase extension)
        {
            this.EnsureChildControls();

            var action = (MakeNSISAction)extension;
            this.ctlScriptFile.Text = string.IsNullOrEmpty(action.OverriddenSourceDirectory) ? action.ScriptFile : PathEx.Combine(action.OverriddenSourceDirectory, action.ScriptFile);
        }
        
        public override ActionBase CreateFromForm()
        {
            this.EnsureChildControls();

            return new MakeNSISAction
            {
                ScriptFile = PathEx.GetFileName(this.ctlScriptFile.Text),
                OverriddenSourceDirectory = PathEx.GetDirectoryName(this.ctlScriptFile.Text)
            };
        }

        protected override void CreateChildControls()
        {
            this.ctlScriptFile = new SourceControlFileFolderPicker
            {
                ServerId = this.ServerId,
                DisplayMode = SourceControlBrowser.DisplayModes.FoldersAndFiles
            };

            this.Controls.Add(
                new SlimFormField("Script file (.nsi):", this.ctlScriptFile)
            );
        }
    }
}
