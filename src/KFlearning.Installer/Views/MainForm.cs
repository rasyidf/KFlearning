using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using KFlearning.Core.API;
using KFlearning.Core.Services.Graph;
using KFlearning.Installer.ApplicationServices;

namespace KFlearning.Installer.Views
{
    public partial class MainForm : Form
    {
        public IProgressBroker ProgressBroker { get; set; }
        public ITaskGraph TaskGraph { get; set; }
        public IKodesianaService KodesianaService { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            var broker = (ProgressBroker) ProgressBroker;
            broker.MessageAction = MessageAction;
            broker.ProgressAction= ProgressAction;

            base.OnLoad(e);
        }

        private async void CmdInstall_Click(object sender, EventArgs e)
        {
            //var catalog = await KodesianaService.GetPackageCatalog(PackagePlatform.x86);
            var definition = new InstallerDefinition(x => Program.Container.Resolve(x))
            {
                Mode = InstallMode.Install,
                //Packages = catalog
                Packages = new PackageCatalog
                {
                    Mingw = new PackageEntry
                    {
                        Uri = new Uri("https://osdn.net/dl/mingw/mingw-get-0.6.3-mingw32-pre-20170905-1-bin.zip"),
                        FileName = "mingw-get-0.6.3-mingw32-pre-20170905-1-bin.zip"
                    },
                    Glut = new PackageEntry
                    {
                        Uri = new Uri("https://www.transmissionzero.co.uk/files/software/development/GLUT/GLUT-MinGW-3.7.6-6.mp.zip"),
                        FileName = "GLUT-MinGW-3.7.6-6.mp.zip"
                    },

                    Httpd = new PackageEntry
                    {
                        Uri = new Uri("https://home.apache.org/~steffenal/VC15/binaries/httpd-2.4.39-win32-VC15.zip"),
                        FileName = "httpd-2.4.39-o111b-x86-vc15.zip"
                    },
                    PhpMyAdmin = new PackageEntry
                    {
                        Uri = new Uri("https://files.phpmyadmin.net/phpMyAdmin/4.9.0.1/phpMyAdmin-4.9.0.1-english.zip"),
                        FileName = "phpMyAdmin-4.9.0.1-english.zip"
                    },

                    MariaDb = new PackageEntry
                    {
                        Uri = new Uri("https://downloads.mariadb.org/f/mariadb-10.4.6/win32-packages/mariadb-10.4.6-win32.zip"),
                        FileName = "mariadb-10.4.6-win32.zip"
                    },

                    Php = new PackageEntry
                    {
                        Uri = new Uri("https://windows.php.net/downloads/releases/php-7.3.7-Win32-VC15-x86.zip"),
                        FileName = "php-7.3.7-Win32-VC15-x86.zip"
                    },
                    Composer = new PackageEntry
                    {
                        Uri = new Uri("https://getcomposer.org/download/1.8.6/composer.phar"),
                        FileName = "composer.phar"
                    },

                    Xdebug = new PackageEntry
                    {
                        Uri = new Uri("https://xdebug.org/files/php_xdebug-2.8.0alpha1-7.3-vc15.dll"),
                        FileName = "php_xdebug-2.8.0alpha1-7.3-vc15.dll"
                    },

                    Vscode = new PackageEntry
                    {
                        Uri = new Uri("https://go.microsoft.com/fwlink/?LinkID=623231"),
                        FileName = "VSCode-win32-ia32-1.34.0.zip"
                    },
                    VscodeExtensions = new List<string> {"ms-python.python"},
                    ProjectTemplates = new List<PackageEntry>
                    {
                        new PackageEntry
                        {
                            Uri = new Uri("https://docs.google.com/uc?export=download&id=1hB2-1iZRoy-6SYgVuivBXahR6c4VKSIG"),
                            FileName = "cpp.zip"
                        }
                    },

                    Kflearning = new PackageEntry
                    {
                        Uri = new Uri("https://docs.google.com/uc?export=download&id=18DiEVB2hSb-yNDl9Vppvt90Hg88ffM_c"),
                        FileName = "kflearning-ide.zip"
                    }
                }
            };
            TaskGraph.RunGraph(definition, Program.Container.Resolve<InstallGraph>());
        }

        private void CmdOpen_Click(object sender, System.EventArgs e)
        {
            TaskGraph.Cancel();
        }

        private void ProgressAction(int obj)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(ProgressAction), obj);
            }
            else
            {
                if (obj > 100 || obj < 0)
                {
                    prgCurrent.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    prgCurrent.Style = ProgressBarStyle.Blocks;
                    prgCurrent.Value = obj;
                }
            }
        }

        private void MessageAction(string obj)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(MessageAction), obj);
            }
            else
            {
                Debug.Print(obj);
                txtLog.AppendText(obj);
                txtLog.AppendText(Environment.NewLine);
            }
        }
    }
}
