using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DSN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateNewButton_Click(object sender, RoutedEventArgs e)
        {


            Process p = new Process();
            p.StartInfo = new ProcessStartInfo("C:\\Users\\dmali\\source\\repos\\SpaceZ\\SpaceZ\\bin\\Debug\\SpaceZ.exe");
            // p.StartInfo = new ProcessStartInfo("C:\\Program Files\\WordWeb\\wweb32.exe", this.Arrange(rectangledict));
            //p.WaitForInputIdle();
            p.Start();


            // var newSpaceCraftForm = new MainWindow(); //create your new form.
            //newSpaceCraftForm.Show(); //show the new form.
            // this.Close(); //only if you want to close the current form.
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newSpaceCraftForm = new ViewSpacecrafts(); //create your new form.
            newSpaceCraftForm.Show(); //show the new form.
            this.Close(); //only if you want to close the current form.
        }
    }
}
