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
        private MainWindow mainwindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="windowPrecedente">Recupère la Fenêtre precedente</param>
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
        /// <param name="sender">ComboBox</param>
        /// <param name="e">Selection qui a changer sur la combobox</param>
        private void ResolutionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double width = Application.Current.MainWindow.Width;
            double height = Application.Current.MainWindow.Height;
    
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            switch (e.AddedItems[0].ToString())
            {
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
                mainwindow.Width = width;
                mainwindow.Height = height;
                mainwindow.Left = (screenWidth / 2) - (mainwindow.Width / 2);
                mainwindow.Top = (screenHeight / 2) - (mainwindow.Height / 2);
        }

        /// <summary>
        /// Click pour revenir a la fenètre precedente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.Content = _windowPrecedente;
        }

        /// <summary>
        /// Slider Value Change la valeur de la musique du jeux
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slider_Son_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (DrainMindGame.Instance != null)
            DrainMindGame.Instance.BackgroundVolume = slider_Son.Value / slider_Son.Maximum;       
        }
    }
}
