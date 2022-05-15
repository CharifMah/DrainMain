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

namespace DrainMind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        /// <summary>
        /// Window du jeux et du MenuPause
        /// </summary>
        public MainWindow()
        {
            //Centre la fenetre
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.Content = new MenuPrincipale();
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
