using DrainMind.Metier;
using DrainMind.Stockage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
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
    /// <Author>Charif</Author>
    public partial class Options : Page
    {
        private Page _windowPrecedente;        
        private MainWindow mainwindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
        StockOptionsFav Stock = new StockOptionsFav(Environment.CurrentDirectory);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="windowPrecedente">Recupère la Fenêtre precedente</param>
        /// <Author>Charif</Author>
        public Options(Page windowPrecedente)
        {
            InitializeComponent();
            InitItemComboBox();
            LoadSettings();
            _windowPrecedente = windowPrecedente;
        }
        /// <summary>
        /// Initialise les controls avec les settings
        /// </summary>
        /// <Author>Charif</Author>
        public void LoadSettings()
        {
           checkBoxFullScreen.IsChecked = Settings.Get().PLeinEcran;
           checkBoxSound.IsChecked = Settings.Get().SonOnOff; 
           slider_Son.Value = Settings.Get().Son;
           SliderValueTextBox.Text = "Son : " + Settings.Get().Son.ToString();
        }

        #region ComboBox

        /// <summary>
        /// Initialise les Items de la ComboBox
        /// </summary>
        /// <Author>Charif</Author>
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
        /// <Author>Charif</Author>
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
        #endregion

        /// <summary>
        /// Click pour revenir a la fenètre precedente et Sauvgarde les Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.Content = _windowPrecedente;
            Stock.SauverSettings(Settings.Get());
        }


        #region CheckBox

        /// <summary>
        /// Active le son a la valeur du slider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void checkBoxSound_Checked(object sender, RoutedEventArgs e)
        {
            if (DrainMindGame.Instance != null)
                DrainMindGame.Instance.BackgroundVolume = slider_Son.Value / slider_Son.Maximum;
            Settings.Get().SonOnOff = checkBoxSound.IsChecked.Value;

        }

        /// <summary>
        /// Desactive le son
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void checkBoxSound_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DrainMindGame.Instance != null)
                DrainMindGame.Instance.BackgroundVolume = 0;
            Settings.Get().SonOnOff = checkBoxSound.IsChecked.Value;
        }

        /// <summary>
        /// Active le plein Ecran
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void checkBoxFullScreen_Checked(object sender, RoutedEventArgs e)
        {
            mainwindow.WindowStyle = WindowStyle.None;
            mainwindow.WindowState = WindowState.Maximized;
            Settings.Get().PLeinEcran = checkBoxFullScreen.IsChecked.Value;
        }

        /// <summary>
        /// Ecran Fenetrer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void checkBoxFullScreen_Unchecked(object sender, RoutedEventArgs e)
        {
            mainwindow.WindowState = WindowState.Normal;
            mainwindow.WindowStyle = WindowStyle.ThreeDBorderWindow;
            Settings.Get().PLeinEcran = checkBoxFullScreen.IsChecked.Value;
        }

        #endregion
        /// <summary>
        /// Slider Value Change la valeur de la musique du jeux
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void slider_Son_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (DrainMindGame.Instance != null)
                DrainMindGame.Instance.BackgroundVolume = slider_Son.Value / slider_Son.Maximum;
            Settings.Get().Son = slider_Son.Value;

            if (SliderValueTextBox != null)
                SliderValueTextBox.Text = "Son : " + slider_Son.Value.ToString();
        }
    }
}
