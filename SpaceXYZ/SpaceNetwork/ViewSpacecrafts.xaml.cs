using MessageInterface;
using MessagingServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SpaceNetwork
{
    /// <summary>
    /// Interaction logic for ViewSpacecrafts.xaml
    /// </summary>
    public partial class ViewSpacecrafts : Window
    {
        public static IMessagingService server;
        //public static ITelemetryService telemetryserver;
        private static DuplexChannelFactory<IMessagingService> _channelFactory;
        //private static DuplexChannelFactory<ITelemetryService> _telemetryFactory;
        private string SelectedSpacecraft;
        private string SelectedSpacecraftForData;


        DispatcherTimer dt;


        public ViewSpacecrafts()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IMessagingService>(new SpacecraftCallBack(), "MessagingServiceEndpoint");
            server = _channelFactory.CreateChannel();

            Update();

           // _telemetryFactory = new DuplexChannelFactory<ITelemetryService>(new SpacecraftCallBack(), "MessagingServiceEndpoint");
           // telemetryserver = _telemetryFactory.CreateChannel();
        }

        public void Update()
        {
            List<String> listS = server.GetCurrentUsers();
            comboBoxSpacecrafts.Items.Clear();

            comboBoxSpacecrafts.Items.Add("Select");
            spacecraftConnectedList.Items.Clear();

            foreach (var craft in listS)
            {
                if (!spacecraftConnectedList.Items.Contains(craft))
                {
                    spacecraftConnectedList.Items.Add(craft);
                    comboBoxSpacecrafts.Items.Add(craft);
                }
               
            }

            foreach (var craft in MainWindow.AllSpacecrafts.Keys)
            {
                if (!comboForData.Items.Contains(craft))
                {
                
                    comboForData.Items.Add(craft);
                }

            }

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var viewMain = new MainWindow(); //create your new form.
            viewMain.Show(); //show the new form.
            this.Close();
        }

    
        private void ButtonStartTelemetry_Click(object sender, RoutedEventArgs e)
        {
          
            int epoch = 5 * 1000;
            StartTelemetryCycle(epoch);
            
            LabelTelemetry.Content = "Telemetry Start : " + SelectedSpacecraft;
            
        }
        private void StartTelemetryCycle(int epoch)
        {
            /*
            runTelemetry = true;
            //List<String> listS = server.GetCurrentUsers();GetItemText
           while (runTelemetry)
           {
                string data = server.RequestTelemetry(SelectedSpacecraft);
                TextTelemetry.Text += data;
               Thread.Sleep(5000);
           }
           */
            ButtonStopTelemetry.Visibility = System.Windows.Visibility.Visible;
            this.dt = new DispatcherTimer();
            TextTelemetry.Text = "";


            this.dt.Interval = TimeSpan.FromSeconds(2);
            this.dt.Tick += dtTicker;
            this.dt.Start();
        }

        private void ButtonStopTelemetry_Click(object sender, RoutedEventArgs e)
        {
            this.dt.Stop();
            ButtonStopTelemetry.Visibility = System.Windows.Visibility.Hidden;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
         
        }


        private void dtTicker(object sender, EventArgs e)
        {
            /*
            increament += 2;
            TimerLabel.Content = increament.ToString();
            */
            string data = server.RequestTelemetry(SelectedSpacecraft);
            if (data == "ORBIT REACHED")
            {
                TextTelemetry.Text += "\n=[ORBIT REACHED FOR : "+ SelectedSpacecraft+"]=";
                this.dt.Stop();
            }
            else
            {
                TextTelemetry.Text += SelectedSpacecraft+" :\n"+  data;
                TextTelemetry.Text += "\n_______________\n";
            }
            TextTelemetry.ScrollToEnd();
        }

        private void btnDeorbit_Click(object sender, RoutedEventArgs e)
        {
            string spacecraft = SelectedSpacecraft;
            Process P = MainWindow.ActiveSpacecraftProcess[spacecraft];
            P.Kill();
            MessageBox.Show(spacecraft + " is DEORBITED");

            server.LogoutByServer(spacecraft);
            var mainWin = new MainWindow();
            mainWin.Show(); //show the new form.
            this.Close();
        }

        private void comboBoxSpacecrafts_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
           
                SelectedSpacecraft = comboBoxSpacecrafts.SelectedItem.ToString();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Data = "Name : " + MainWindow.AllSpacecrafts[SelectedSpacecraftForData].Name + "\n" + "Orbit : " + MainWindow.AllSpacecrafts[SelectedSpacecraftForData].Orbit+"\n";
            Data += "Payload Name : " + MainWindow.AllSpacecrafts[SelectedSpacecraftForData].PayloadLink.Name + "\n" + "Payload Type : " + MainWindow.AllSpacecrafts[SelectedSpacecraftForData].PayloadLink.Type + "\n";
            txtSpacecraftData.Text = Data;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedSpacecraftForData = comboForData.SelectedItem.ToString();
        }
    }
}
