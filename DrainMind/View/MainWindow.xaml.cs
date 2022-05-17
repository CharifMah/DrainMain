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
    public partial class MainWindow : Window
    {

        private StockOptionsFav _StockOptionsFav = new StockOptionsFav(Directory.GetCurrentDirectory());

        /// <summary>
        /// Window du jeux et du MenuPause
        /// </summary>
        public MainWindow()
        {
            //Centre la fenetre
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();


        }
        /// <summary>
        /// Selectionne les item enregister dans les fichiers JSON 
        /// </summary>
        /// <param name="_source"></param>
        /// <param name="_cible"></param>
        private void SelectFav(bool _cible)
        {

        }

        /// <summary>
        /// Environment.Exit a la fermeture de la fenetre 
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FentrePrincipalDrainMain_Closed(object sender, System.EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
