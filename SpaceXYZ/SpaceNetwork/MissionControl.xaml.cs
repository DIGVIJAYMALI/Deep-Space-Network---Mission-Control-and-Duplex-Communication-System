using MessageInterface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpaceNetwork
{
    /// <summary>
    /// Interaction logic for MissionControl.xaml
    /// </summary>
    public partial class MissionControl : Window
    {
        public static IMessagingService server;
        //public static ITelemetryService telemetryserver;
        private static DuplexChannelFactory<IMessagingService> _channelFactory;

        public MissionControl()
        {


            InitializeComponent();
            

            _channelFactory = new DuplexChannelFactory<IMessagingService>(new SpacecraftCallBack(), "MessagingServiceEndpoint");
            server = _channelFactory.CreateChannel();

            UpdateData();
        }

        public void UpdateData()
        {
            List<String> listS = server.GetCurrentUsers();
            listNonActiveSpacecrafts.Items.Clear(); 
            comboNewSpacecrafts.Items.Clear();
            listActiveSpacecrafts.Items.Clear();
          
            comboNewSpacecrafts.Items.Add("Select");
            foreach (var craft in listS)
            {
                if (!MainWindow.ActiveSpacecrafts.Contains(craft))
                {
                    MainWindow.ActiveSpacecrafts.Add(craft);
                }
                if (MainWindow.NonActiveSpacecrafts.Contains(craft))
                {
                    MainWindow.NonActiveSpacecrafts.Remove(craft);
                }
            }

            foreach (var name in MainWindow.ActiveSpacecrafts)
            {
                if (!listActiveSpacecrafts.Items.Contains(name))
                {
                    listActiveSpacecrafts.Items.Add(name);

                }
            }
            foreach (var name in MainWindow.NonActiveSpacecrafts)
            {
                if (!listNonActiveSpacecrafts.Items.Contains(name))
                {
                    listNonActiveSpacecrafts.Items.Add(name);
                    comboNewSpacecrafts.Items.Add(name);
                }

            }
        }

      

        private void btnLaunchSpacecraft_Click(object sender, RoutedEventArgs e)
        {
            string spacecraft = comboNewSpacecrafts.SelectedItem.ToString();
            if (spacecraft != "Select")
            {
                if (!MainWindow.ActiveSpacecraftProcess.ContainsKey(spacecraft))
                {
                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo("C:\\Users\\dmali\\source\\repos\\SpaceZ\\SpaceZ\\bin\\Debug\\SpaceZ.exe");
                    // p.StartInfo = new ProcessStartInfo("C:\\Program Files\\WordWeb\\wweb32.exe", this.Arrange(rectangledict));
                    //p.WaitForInputIdle();
                    p.StartInfo.Arguments = spacecraft+" " + MainWindow.AllSpacecrafts[spacecraft].PayloadLink.Name+" " + MainWindow.AllSpacecrafts[spacecraft].Orbit.ToString();
                    p.Start();
                    MainWindow.ActiveSpacecraftProcess.Add(spacecraft, p);


                    // MainWindow.ActiveSpacecrafts.Add(spacecraft);
                    // MainWindow.NonActiveSpacecrafts.Remove(spacecraft);
                    // MessageBox.Show("Launch Successfull");
                }
                else
                {
                    MessageBox.Show("Already in Message Queue");
                }
                var mainWin = new MainWindow(); //create your new form.
                mainWin.Show(); //show the new form.
                this.Close();
            }
           
            UpdateData();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow(); //create your new form.
            mainWindow.Show(); //show the new form.
            this.Close();       
        }


        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void btnAddNewSpacecraft_Click(object sender, RoutedEventArgs e)
        {
            var newSpaceCraft = new NewSpacecraft(); //create your new form.
            newSpaceCraft.Show(); //show the new form.
            this.Close();
        }
    }
}
