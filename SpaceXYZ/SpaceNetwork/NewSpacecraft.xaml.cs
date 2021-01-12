using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for NewSpacecraft.xaml
    /// </summary>
    public partial class NewSpacecraft : Window
    {
        public NewSpacecraft()
        {
            InitializeComponent();
            comboPayload.Items.Add("Select");
            comboPayload.Items.Add("Scientific");
            comboPayload.Items.Add("Communication");
            comboPayload.Items.Add("Spy");
        }

        private void btnAddSpacecraft_Click(object sender, RoutedEventArgs e)
        {
            MissionControl mControl = new MissionControl();

            if (txtSpacecraftName.Text != "" && txtOrbitRadius.Text != "" && txtPayloadName.Text != "" && comboPayload.SelectedItem.ToString() != "Select")
            { 
            string vehicleName = txtSpacecraftName.Text;
            int orbitRadius = Convert.ToInt32(txtOrbitRadius.Text);
            string payloadName = txtPayloadName.Text;
            string typePayload = comboPayload.SelectedItem.ToString();
            Payload newPayload = new Payload(payloadName, typePayload);
            Vehicle newVehicle = new Vehicle(vehicleName, orbitRadius, newPayload);
            MainWindow.AllSpacecrafts.Add(vehicleName,newVehicle);


           
                mControl.comboNewSpacecrafts.Items.Add(txtSpacecraftName.Text);
                mControl.listNonActiveSpacecrafts.Items.Add(txtSpacecraftName.Text);
                MainWindow.NonActiveSpacecrafts.Add(txtSpacecraftName.Text);
                txtSpacecraftName.Text = "";
            }
            else
            {
                MessageBox.Show("All fields required !");
            }

            MessageBox.Show("Spacecraft and Payload Added to Queue");

            mControl.UpdateData();
            var missionWindow = new MissionControl(); //create your new form.
            missionWindow.Show(); //show the new form.
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var missionWindow = new MissionControl(); //create your new form.
            missionWindow.Show(); //show the new form.
            this.Close();
        }
    }
}
