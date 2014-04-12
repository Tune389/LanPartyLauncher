using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace LanPartyLauncher
{
    public partial class MainWindow : Window
    {
        private String nickname = null;
        private String host_ip = null;

        public MainWindow()
        {
            InitializeComponent();      
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try 
            { 
                this.DragMove();
            }
            catch{}
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
                this.WindowState = System.Windows.WindowState.Normal;
            if (this.Width != 1320) this.Width = 1320;
            if (this.Height != 700) this.Height = 700;
        }

        private void Auswahl_MouseEnter(object sender, MouseEventArgs e)
        {
            Label myLabel = (Label)sender;
            ImageAnimation(myLabel, true);
        }
        private void Auswahl_MouseLeave(object sender, MouseEventArgs e)
        {
            Label myLabel = (Label)sender;
            ImageAnimation(myLabel, false);
        }

        public void ImageAnimation(Label Button, bool on)
        {
            String imageFileName = Button.Name;
            if (on) imageFileName += "_on";
            imageFileName += ".png";
            setBackgroundImage(imageFileName, Button);

        }

        public void setBackgroundImage(String imageFileName, Label myLabel)
        {
            myLabel.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\Images\\" + imageFileName, UriKind.Relative)));
        }

        private void Start_Bat(object sender, MouseButtonEventArgs e)
        {
            if (nickname != null)
            {
                Label myLabel = (Label)sender;
                String gameBatName = "start_" + myLabel.Name.Substring(3);
                Process prc = new Process();
                prc.StartInfo.FileName = "data\\" + gameBatName + ".bat";

                prc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                try
                {
                    prc.Start();
                }
                catch
                {
                    MessageBox.Show("Anweisung nicht gefunden!");
                }
            }
            else MessageBox.Show("Gebe zuerst einen Nicknamen und eine Host-IP ein!");
        }

        private void bt_setData_Click(object sender, RoutedEventArgs e)
        {

            host_ip = tb_ip.Text;
            nickname = tb_nick.Text;

            String[] myFiles = new String[3];
            myFiles[0] = "data\\games\\cod_mw2\\alterIWnet.ini";
            myFiles[1] = "data\\start_ts3.bat";
            myFiles[2] = "data\\games\\cod_bo\\bgset.ini";

            String[] myFilesContent = new String[3];
            myFilesContent[0] =  "[Configuration]{0}";
            myFilesContent[0] += "Server=" + host_ip + "{0}";
            myFilesContent[0] += "WebHost=auto" + "{0}";
            myFilesContent[0] += "Nickname=" + nickname + "{0}";

            myFilesContent[1] =  "cd data/files/ts3/{0}";
            myFilesContent[1] += "@start ts3client_win32.exe ts3server://"+host_ip+"?nickname="+nickname+"{0}";
            myFilesContent[1] += "exit";

            myFilesContent[2] =  "[Config]{0}";
            myFilesContent[2] += "Host=" + host_ip + "{0}";
            myFilesContent[2] += "[Config]{0}";
            myFilesContent[2] += "Nickname=" + nickname + "{0}";

            int count = 0;
            foreach(String sFile in myFiles) {
                if (File.Exists(sFile))
                {
                    FileInfo myFi = new FileInfo(sFile);
                    myFi.Delete();
                    StreamWriter mySw = new StreamWriter(sFile);
                    mySw.WriteLine(myFilesContent[count], Environment.NewLine);
                    mySw.Close();
                }
                count++;
            }
            if (count > 0)
            {
                MessageBox.Show("Die IP: " + host_ip + " und der Nickname: " + nickname + " wurden erfolgreich in " + count + " Files eingetragen!");
            }
            else
            {
                MessageBox.Show("Keine Files Gefunden in denen die einstellungen geändert werden könnten");
            }

        }

        private void bt_app_close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void bt_app_min_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Text_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox myTextBox = (TextBox)sender;
            myTextBox.Select(0, myTextBox.GetLineLength(0));
        }

        private void LanPartyLauncher_Loaded(object sender, RoutedEventArgs e)
        {
            Label myLabel = (Label)sender;
            String imageFileName = myLabel.Name;
            imageFileName += ".png";
            setBackgroundImage(imageFileName, myLabel);
        }


        private void LanPartyLauncher_Loaded_1(object sender, RoutedEventArgs e)
        {

            Installation myInstallation = new Installation();
            myInstallation.ShowDialog();
            this.Background = new ImageBrush(new BitmapImage(new Uri("images\\frame.png", UriKind.Relative)));

        }

        private void bt_fix_cmw_Click(object sender, RoutedEventArgs e)
        {
            Process prc = new Process();
            prc.StartInfo.FileName = "data\\start_fix_cmw.bat";
            try
            {
                prc.Start();
            }
            catch
            { }
        }

        private void bt_fov_mw2_Click(object sender, RoutedEventArgs e)
        {
            Process prc = new Process();
            prc.StartInfo.FileName = "data\\start_fov_mw2.bat";
            prc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            try
            {
                prc.Start();
            }
            catch
            { }
        }
    }
}
