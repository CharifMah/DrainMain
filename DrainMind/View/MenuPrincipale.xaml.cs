using DrainMind.Metier.Joueur;
using DrainMind.Stockage;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MenuPrincipale.xaml
    /// </summary>
    public partial class MenuPrincipale : Page
    {
        //Garde en memoire la page du jeux en cours
        private DrainMindView drainmindView;
        //Get la MainWindow
        private Window mainwindow = MainWindow.GetMainWindow;

        /// <summary>
        /// Initialise la page du menu principale
        /// </summary>
        /// <Author>Charif</Author>
        public MenuPrincipale()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Lance le jeux
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (drainmindView == null)
            {
                //Lance une Nouvelle Partie
                drainmindView = new DrainMindView(this);
                mainwindow.Content = drainmindView;
            }
            else
            {
                //Reprends la Partie en cours
                mainwindow.Content = drainmindView;
            }
        }

        /// <summary>
        /// Cree et lance une nouvelle partie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            DrainMindGame.Instance.Pause();
            DrainMindGame.Instance.BackgroundVolume = 0;
            drainmindView = new DrainMindView(this);
            mainwindow.Content = drainmindView;
        }

        /// <summary>
        /// Ouvre les options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.Content = new Options(this);
        }

        /// <summary>
        /// Quitte totalement le jeux
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            StockScore _StockScore = new StockScore(Directory.GetCurrentDirectory());
            _StockScore.SauverScore(LesScores.Get().Scores);

            Environment.Exit(0);
        }
    }
}
