// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : QuestForm.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.Windows.Forms;
using KFlearning.Core;
using KFlearning.Core.Services;
using KFlearning.Properties;

namespace KFlearning.Views
{
    public partial class QuestForm : Form
    {
        private readonly IQuestService _quest = Program.Container.Resolve<IQuestService>();
        private readonly IUserService _user = Program.Container.Resolve<IUserService>();

        public QuestForm()
        {
            InitializeComponent();
        }

        private void QuestForm_Load(object sender, EventArgs e)
        {
            UpdateScores();
        }

        private async void cmdUpdate_Click(object sender, EventArgs e)
        {
            _quest.UpdateScores();
            UpdateScores();

            try
            {
                if (_user.IsLogged) await _user.Sync();
            }
            catch (KFlearningException kf)
            {
                MessageBox.Show(kf.Message, Resources.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch
            {
                MessageBox.Show(Resources.NetworkErrorMessage, Resources.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private async void cmdLogin_Click(object sender, EventArgs e)
        {
            try
            {
                cmdLogin.Enabled = false;
                if (cmdLogin.Text == "Login")
                {
                    using (var frm = Program.Container.Resolve<AuthenticationForm>())
                    {
                        if (frm.ShowDialog() != DialogResult.OK) return;
                        await _user.Login(frm.AccessCode);
                    }
                }
                else
                {
                    _user.Logout();
                }

                UpdateScores();
            }
            catch (KFlearningException kf)
            {
                MessageBox.Show(kf.Message, Resources.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch
            {
                MessageBox.Show(Resources.NetworkErrorMessage, Resources.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                cmdLogin.Enabled = true;
            }
        }

        private void UpdateScores()
        {
            var stats = _quest.GetStatistics();
            lblCoderLevel.Text = QuestStatistics.LevelToString(stats.CoderLevel);
            lblCoderDesc.Text = stats.CoderDescription;
            lblFocusLevel.Text = QuestStatistics.LevelToString(stats.FocusLevel);
            lblFocusDesc.Text = stats.FocusDescription;
            lblProjectLevel.Text = QuestStatistics.LevelToString(stats.ProjectLevel);
            lblProjectDesc.Text = stats.ProjectDescription;

            lblCodeCount.Text = stats.CodeCount.ToString("N0");
            lblProjectCount.Text = stats.ProjectCount.ToString("N0");
            lblCodeTime.Text = stats.CodingTime.ToString("g");

            lblUsername.Text = _user.Username;
            lblSync.Text = _user.LastSync.ToString("g");
            cmdLogin.Text = _user.IsLogged ? "Logout" : "Login";
        }
    }
}