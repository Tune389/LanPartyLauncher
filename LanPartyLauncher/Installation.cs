using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace LanPartyLauncher
{
    public partial class Installation : Form
    {
        BackgroundWorker worker;
        string sDateiname = "";
        bool progress = false;
       // double iProgress = 0;
        double filesize = 0;

        public Installation()
        {
            InitializeComponent();
        }

        private int unzip()
        {
            int iReturn = 0;
            string[] filePaths = Directory.GetFiles(@"data\games\", "*.zip");
            foreach (String path in filePaths)
            {
                try {
                    sDateiname = Path.GetFileName(path);
                    string extractPath = @"data\games\";
                    System.IO.Compression.ZipFile.ExtractToDirectory(path, extractPath);
                }
                catch {
                    return 3;
                }
                
                System.IO.FileInfo fi = new System.IO.FileInfo(path);

                try {
                  //  fi.Delete();
                    iReturn = 1;
                }
                catch {
                    return 2;
                }
            }
            return iReturn;
        }

        private void Installation_Load(object sender, EventArgs e)
        {
            bool gamesFound = false;
            string[] filePaths = Directory.GetFiles(@"data\games\", "*.zip");
            foreach (String path in filePaths)
            {
                gamesFound = true;
            }

            if (gamesFound) { 
                worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.WorkerSupportsCancellation = true;

                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            }
            else
            {
                this.Close();
            }
        }

        private void btnInstallation_Click(object sender, EventArgs e)
        {

            if (progress)
            {
                this.Close();
            }
            else
            {
                worker.RunWorkerAsync(0);
                timTimeout.Enabled = true;
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = unzip();
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timTimeout.Enabled = false;
            String sMsg = "";
            switch (Convert.ToInt16(e.Result))
            {
                case 1:
                    sMsg = "Spiel(e) erfolgreich installiert";
                    break;
                case 2:
                    sMsg = "Fehler: Eine Installationsdatei konnte nicht entfernt werden";
                    break;
                case 3:
                    sMsg = "Fehler: Das Spiel konnte nicht entpackt werden bitte prüfe deine Spieldateien";
                    break;
            }
            label1.Text = "Installation erfolgreich durchgeführt.";
            btnInstallation.Text = "Fortsetzten";
            //progressBar1.Value = 100;
            if (sMsg != "") MessageBox.Show(sMsg);
            progress = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // progressBar1.Value = Convert.ToInt16(Math.Pow(1.007222, iProgress));
           // iProgress++;
            label1.Text = sDateiname + " wird installiert";
        }
    }
}
