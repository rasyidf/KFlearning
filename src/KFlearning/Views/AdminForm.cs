// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : AdminForm.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.IO;
using System.Windows.Forms;
using KFlearning.Core.Diagnostics;
using KFlearning.Core.IO;
using KFlearning.Core.Security;
using KFlearning.Core.Services;
using KFlearning.Properties;

namespace KFlearning.Views
{
    public partial class AdminForm : Form
    {
        private readonly ISystemTweaker _tweaker;
        private readonly ICredentialService _credential;
        private readonly IPathManager _path;
        private string _selectedWallpaperPath;

        public AdminForm()
        {
            _tweaker = Program.Container.Resolve<ISystemTweaker>();
            _credential = Program.Container.Resolve<ICredentialService>();
            _path = Program.Container.Resolve<IPathManager>();

            InitializeComponent();

            Reload();
        }

        private void Reload()
        {
            _tweaker.Query();

            chkWriteProtect.Checked = _tweaker.LockUsbCopying;
            chkRegistry.Checked = _tweaker.LockRegistryEditor;
            chkTaskManager.Checked = _tweaker.LockTaskManager;
            chkControlPanel.Checked = _tweaker.LockControlPanel;
            chkWallpaper.Checked = _tweaker.LockWallpaper;
            chkDesktop.Checked = _tweaker.LockDesktop;
            chkRaf.Checked = Settings.Default.Raf;

            if (string.IsNullOrEmpty(_tweaker.WallpaperPath))
            {
                rdWDefault.Checked = true;
            }
            else
            {
                rdWCustom.Checked = true;
                lblFileName.Text = Path.GetFileName(_tweaker.WallpaperPath);
            }
        }

        private void chkWallpaper_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Enabled = chkWallpaper.Checked;
        }

        private void cmdCredential_Click(object sender, EventArgs e)
        {
            using (var frm = Program.Container.Resolve<AuthenticationForm>())
            {
                if (frm.ShowDialog(this) != DialogResult.OK) return;
                if (!string.IsNullOrEmpty(frm.AccessCode)) _credential.Save(frm.AccessCode);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            _tweaker.LockUsbCopying = chkWriteProtect.Checked;
            _tweaker.LockRegistryEditor = chkRegistry.Checked;
            _tweaker.LockTaskManager = chkTaskManager.Checked;
            _tweaker.LockControlPanel = chkControlPanel.Checked;
            _tweaker.LockWallpaper = chkWallpaper.Checked;
            _tweaker.LockDesktop = chkDesktop.Checked;
            if (rdWDefault.Checked || string.IsNullOrEmpty(_selectedWallpaperPath))
            {
                _tweaker.WallpaperPath = "";
            }
            else
            {
                var savePath = _path.GetPath(PathKind.WallpaperPath);
                if (File.Exists(savePath)) File.Delete(savePath);

                File.Copy(_selectedWallpaperPath, savePath);
                _tweaker.WallpaperPath = savePath;
            }

            _tweaker.Apply();
            Settings.Default.Raf = chkRaf.Checked;
            Settings.Default.Save();
            Close();
        }

        private void cmdBrowseWallpaper_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ofd.ShowDialog() != DialogResult.OK) return;
            _selectedWallpaperPath = ofd.FileName;
        }
    }
}