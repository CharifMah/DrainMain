using DrainMind.metier.Grille;
using DrainMind.Metier;
using DrainMind.Stockage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
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
        
        private MainWindow mainwindow = MainWindow.GetMainWindow;

        StockOptions Stock = new StockOptions(Environment.CurrentDirectory);

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
            DataContext = Settings.Get();
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
        }

        #region ComboBox

        /// <summary>
        /// Initialise les Items de la ComboBox
        /// </summary>
        /// <Author>Charif</Author>
        public void InitItemComboBox()
        {
            //Resolution ComboBox init
            ResolutionComboBox.Items.Add("1280 x 720");
            ResolutionComboBox.Items.Add("1280 x 1024");
            ResolutionComboBox.Items.Add("1600 x 900");
            ResolutionComboBox.Items.Add("1680 x 1050");
            ResolutionComboBox.Items.Add("1920 x 1080");

            ResolutionComboBox.SelectedItem = $"{Application.Current.MainWindow.Width} x {Application.Current.MainWindow.Height}";

            //Langue ComboBox init
            LangueComboBox.Items.Add("en-US");
            LangueComboBox.Items.Add("fr-FR");

            //GridSizeComboBox.Items.Add("10 x 30");
            //GridSizeComboBox.Items.Add("20 x 40");
            //GridSizeComboBox.Items.Add("30 x 50");
            //GridSizeComboBox.Items.Add("40 x 60");
            //GridSizeComboBox.Items.Add("50 x 70");


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

        private void GridSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (e.AddedItems[0].ToString())
            {
                case "10 x 30":
                    MyGrid.NombreDeLigne = 10;
                    MyGrid.NombreDeCollumn = 30;
                    break;
                case "20 x 40":
                    MyGrid.NombreDeLigne = 20;
                    MyGrid.NombreDeCollumn = 40;
                    break;
                case "30 x 50":
                    MyGrid.NombreDeLigne = 20;
                    MyGrid.NombreDeCollumn = 50;
                    break;
                case "40 x 60":
                    MyGrid.NombreDeLigne = 40;
                    MyGrid.NombreDeCollumn = 60;
                    break;
                case "50 x 70":
                    MyGrid.NombreDeLigne = 50;
                    MyGrid.NombreDeCollumn = 70;
                    break;
            }
            MyGrid.ResizeCanvas();
        }

        private void LangueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {           
            Settings.Get().Culturename = e.AddedItems[0].ToString();
            Res.Strings.Culture = new CultureInfo(Settings.Get().Culturename);
        }


        #endregion

        #region Button
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
        #endregion

        #region CheckBox

        /// <summary>
        /// Active le son a la valeur du slider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void checkBoxSound_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Get().SonOnOff = checkBoxSound.IsChecked.Value;
            RefreshControl();
        }

        /// <summary>
        /// Desactive le son
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void checkBoxSound_Unchecked(object sender, RoutedEventArgs e)
        {          
            Settings.Get().SonOnOff = checkBoxSound.IsChecked.Value;
            RefreshControl();
        }

        /// <summary>
        /// Active le plein Ecran
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void checkBoxFullScreen_Checked(object sender, RoutedEventArgs e)
        {
           
            Settings.Get().PLeinEcran = checkBoxFullScreen.IsChecked.Value;
            RefreshControl();
        }

        /// <summary>
        /// Ecran Fenetrer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void checkBoxFullScreen_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Get().PLeinEcran = checkBoxFullScreen.IsChecked.Value;
            RefreshControl();
        }

        #endregion

        #region Slider
        /// <summary>
        /// Slider Value Change la valeur de la musique du jeux
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void slider_Son_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            Settings.Get().Son = slider_Son.Value;
            RefreshControl();
        }
        #endregion

        /// <summary>
        /// Sychronise les donnee avec les controles graphique
        /// </summary>
        /// <Author>Charif</Author>
        public void RefreshControl()
        {
            checkBoxSound.IsChecked = Settings.Get().SonOnOff;
        }

    }
}
