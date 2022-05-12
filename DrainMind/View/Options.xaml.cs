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

namespace DrainMind.View
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        private Window _Menu;
        public Options(Window Menu)
        {
            //Centre la fenetre
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            _Menu = Menu;
        }

        public void InitItemComboBox()
        {
            ResolutionComboBox.Items.Add("800 * 500");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _Menu.Show();
            this.Close();
        }
    }
}
