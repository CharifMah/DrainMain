using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Input;
using System.Windows.Interop;
using System;
using DrainMind.Metier;
using DrainMind.View;
using System.Linq;
using DrainMind.Stockage;

namespace DrainMind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <Author>Charif</Author>
    public partial class MainWindow : Window
    {
        //StockDesOptions
        private StockOptions _StockOptionsFav = new StockOptions(Directory.GetCurrentDirectory());

        //Recupère la MainWindow Actuel
        public static MainWindow GetMainWindow
        { get { return Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow; } }

        /// <summary>
        /// Window du jeux et du MenuPause
        /// </summary>
        /// <Author>Charif</Author>
        /// <exception cref="System.Windows.Markup.XamlParseException"></exception>
        public MainWindow()
        {
            //Centre la fenetre
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            StartupSettings();


        }
        /// <summary>
        /// Selectionne les settings a charger au lancement
        /// </summary>
        /// <Author>Charif</Author>
        /// <exception cref="System.Windows.Markup.XamlParseException"></exception>
        private void StartupSettings()
        {
            if (Settings.Get().PLeinEcran)
            {
                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.ThreeDBorderWindow;
            }
        }

        /// <summary>
        /// Environment.Exit a la fermeture de la fenetre 
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void FentrePrincipalDrainMain_Closed(object sender, System.EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
