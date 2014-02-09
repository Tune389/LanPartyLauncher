using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String nickname = null;
        private String host_ip = null;
       // private System.Threading.Thread BoradcastListner
       // { get; set; }


       ResourceManager rm = null;

        public MainWindow()
        {
            InitializeComponent();      
           /* BoradcastListner = new System.Threading.Thread(delegate()
            {
                CheckBroadcast.RefMethods = this;
                var tempStart = new CheckBroadcast();
                tempStart.StartListen();
            });
            BoradcastListner.Start();*/
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

       /* private UdpClient Listener = new UdpClient(1234);
        private void Listening()
        {
           this.Listener.BeginReceive(Receive, new object());
        }
        private void Receive(IAsyncResult packet)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 10001);
            byte[] bytes = Listener.EndReceive(packet, ref ip);
            string message = Encoding.ASCII.GetString(bytes);
            //////this.Listening();
        }*/

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
           // Listening();
            String imageFileName = Button.Name;
            if (on) imageFileName += "_on";
            imageFileName += ".png";
            setBackgroundImage(imageFileName, Button);

        }

        public void setBackgroundImage(String imageFileName, Label myLabel)
        {
            myLabel.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\Images\\" + imageFileName, UriKind.Relative)));
        }

      /*  private void bt_cod_mw_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Window1.myWindow1 == null)
                Window1.myWindow1 = new Window1("cod_mw");

            Window1.myWindow1.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Window1.myWindow1 != null)
                Window1.myWindow1.Close();
        }*/

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

            try
            {
                FileInfo myFi = new FileInfo("data\\games\\cod_mw2\\alterIWnet.ini");
                myFi.Delete();
                myFi = new FileInfo("data\\start_ts3.bat");
                myFi.Delete();
                myFi = new FileInfo("data\\games\\csgo\\rev.ini");
                myFi.Delete();
            }
            catch { }
            try
            {
                // Datei anlegen
                StreamWriter mySw = new StreamWriter("data\\games\\cod_mw2\\alterIWnet.ini");
                mySw.WriteLine("[Configuration]");
                mySw.WriteLine("Server=" + host_ip);
                mySw.WriteLine("WebHost=auto");
                mySw.WriteLine("Nickname=" + nickname);
                mySw.Close();

                mySw = new StreamWriter("data\\start_ts3.bat");
                mySw.WriteLine("cd data/files/ts3/");
                mySw.WriteLine("@start ts3client_win32.exe ts3server://"+host_ip+"?nickname="+nickname);
                mySw.WriteLine("exit");
                mySw.Close();

                mySw = new StreamWriter("data\\games\\csgo\\rev.ini");
                mySw.WriteLine("");
                mySw.WriteLine("[steamclient]");
                mySw.WriteLine("");
                mySw.WriteLine("PlayerName="+nickname);
                mySw.WriteLine("Logging=false");
                mySw.WriteLine("ClanTag=[LanParty]");
                mySw.WriteLine("Use_avatar = true");
                mySw.WriteLine("");
                mySw.WriteLine("[Loader]");
                mySw.WriteLine("");
                mySw.WriteLine("ProcName=csgo.exe -steam -silent -novid -connect "+ host_ip);
                mySw.WriteLine("");
                mySw.WriteLine("[Emulator]");
                mySw.WriteLine("");
                mySw.WriteLine("CacheEnabled = false");
                mySw.WriteLine("");
                mySw.WriteLine("CachePath = D:\\Steam\\SteamApps");
                mySw.WriteLine("");
                mySw.WriteLine("Language = English");
                mySw.WriteLine("");
                mySw.WriteLine("Logging=false");
                mySw.WriteLine("");
                mySw.WriteLine("SteamDll=.\\Steam\\Steam.dll");
                mySw.WriteLine("");
                mySw.WriteLine("SteamClient = True");
                mySw.WriteLine("");
                mySw.WriteLine("SteamUser = SteamPlayer");
                mySw.WriteLine("");
                mySw.WriteLine("[Log]");
                mySw.WriteLine("");
                mySw.WriteLine("FileSystem=False");
                mySw.WriteLine("Account=False");
                mySw.WriteLine("UserID=False");
                mySw.WriteLine("");
                mySw.WriteLine("[GameServer]");
                mySw.WriteLine("");
                mySw.WriteLine("AllowOldRev74=False");
                mySw.WriteLine("");
                mySw.WriteLine("AllowOldRev=False");
                mySw.WriteLine("");
                mySw.WriteLine("AllowUnknown=False");
                mySw.WriteLine("");
                mySw.WriteLine("Fake_player= false");
                mySw.WriteLine("");
                mySw.WriteLine("RevEmu_2012 = false");
                mySw.WriteLine("");
                mySw.WriteLine("AddCountPlayerInServerName = false");
                mySw.WriteLine("");
                mySw.WriteLine("");
                mySw.WriteLine("[GameServerNSNet]");
                mySw.WriteLine("");
                
                mySw.Close();
            }
            catch { }

            MessageBox.Show("Die IP: " + host_ip + " und der Nickname: " + nickname + " wurden erfolgreich eingetragen!");
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
