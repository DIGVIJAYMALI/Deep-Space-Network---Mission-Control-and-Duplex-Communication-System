using MessageInterface;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpaceNetwork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IMessagingService server;
        private static DuplexChannelFactory<IMessagingService> _channelFactory;

        public static Dictionary<string, Vehicle> AllSpacecrafts = new Dictionary<string, Vehicle>();
        public static List<string> ActiveSpacecrafts = new List<string>();
        public static List<string> NonActiveSpacecrafts = new List<string>();
        public static Dictionary<string,Process> ActiveSpacecraftProcess = new Dictionary<string, Process>();


        public MainWindow()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IMessagingService>(new SpacecraftCallBack(), "MessagingServiceEndpoint");
            server = _channelFactory.CreateChannel();
        }

        private void CreateNewButton_Click(object sender, RoutedEventArgs e)
        {

            var missionControlSys = new MissionControl(); //create your new form.
            missionControlSys.Show(); //show the new form.
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var viewSpaceCraft = new ViewSpacecrafts(); //create your new form.
            viewSpaceCraft.Show(); //show the new form.
            this.Close();
        }

        private void Show_Logs_Click(object sender, RoutedEventArgs e)
        {
            DisplayLogs.Text = server.GetAllLogs().ToString();
            DisplayLogs.ScrollToEnd();
        }

    }
}
