using Inedo.BuildMaster;
using Inedo.BuildMaster.Extensibility.Actions;
using Inedo.BuildMaster.Web.Controls;
using Inedo.BuildMaster.Web.Controls.Extensions;

namespace Inedo.BuildMasterExtensions.NSIS
{
    internal sealed class MakeNSISActionEditor : ActionEditorBase
    {
        private SourceControlFileFolderPicker ctlScriptFile;

        public override void BindToForm(ActionBase extension)
        {
            this.EnsureChildControls();

            var action = (MakeNSISAction)extension;
            this.ctlScriptFile.Text = string.IsNullOrEmpty(action.OverriddenSourceDirectory) ? action.ScriptFile : Util.Path2.Combine(action.OverriddenSourceDirectory, action.ScriptFile);
        }
        
        public override ActionBase CreateFromForm()
        {
            this.EnsureChildControls();

            return new MakeNSISAction
            {
                ScriptFile = Util.Path2.GetFileName(this.ctlScriptFile.Text),
                OverriddenSourceDirectory = Util.Path2.GetDirectoryName(this.ctlScriptFile.Text)
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
                new FormFieldGroup(
                    "Script File",
                    "The script file (.nsi) to execute.",
                    true,
                    new StandardFormField("Script File:", this.ctlScriptFile)
                )
            );
        }
    }
}
