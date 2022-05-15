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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrainMind.View
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Page
    {
        private Page _windowPrecedente;
        public Options(Page windowPrecedente)
        {
            InitializeComponent();
            InitItemComboBox();
            _windowPrecedente = windowPrecedente;
        }

        /// <summary>
        /// Initialise les Items de la ComboBox
        /// </summary>
        public void InitItemComboBox()
        {
            ResolutionComboBox.Items.Add("800 x 600");
            ResolutionComboBox.Items.Add("1024 x 768");
            ResolutionComboBox.Items.Add("1280 x 720");
            ResolutionComboBox.Items.Add("1280 x 1024");
            ResolutionComboBox.Items.Add("1600 x 900");
            ResolutionComboBox.Items.Add("1680 x 1050");
            ResolutionComboBox.Items.Add("1920 x 1080");

            ResolutionComboBox.SelectedItem = $"{Application.Current.MainWindow.Width} x {Application.Current.MainWindow.Height}";
        }


        /// <summary>
        /// Si la selection de la combobox a changer alors on change la resolution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResolutionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double width = Application.Current.MainWindow.Width;
            double height = Application.Current.MainWindow.Height;

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            switch (e.AddedItems[0].ToString())
            {
                case "800 x 600":
                    width = 800;
                    height = 600;
                    break;
                case "1024 x 768":
                    width = 1024;
                    height = 768;
                    break;
                case "1280 x 720":
                    width = 1280;
                    height = 720;
                    break;
                case "1280 x 1024":
                    width = 1280;
                    height = 1024;
                    break;
                case "1600 x 900":
                    width = 1600;
                    height = 900;
                    break;
                case "1680 x 1050":
                    width = 1680;
                    height = 1050;
                    break;
                case "1920 x 1080":
                    width = 1920;
                    height = 1080;
                    break;
            }
            foreach (Window windows in Application.Current.Windows)
            {
                windows.Width = width;
                windows.Height = height;
                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                windows.Top = (screenHeight / 2) - (windows.Height / 2);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mw.Content = _windowPrecedente;
        }
    }
}
