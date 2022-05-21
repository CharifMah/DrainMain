using DrainMind.Metier;
using DrainMind.Metier.Joueur;
using DrainMind.Stockage;
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
    /// Interaction logic for DrainMind.xaml
    /// </summary>
    public partial class DrainMindView : Page
    {
        private DrainMindGame drainMind;
        private MenuPrincipale _MenuPrincipale;
        StockScore Stock = new StockScore(Environment.CurrentDirectory);

        public DrainMindView(MenuPrincipale Menu)
        {        
            ShowsNavigationUI = false;
            InitializeComponent();
 
            _MenuPrincipale = Menu;
        }

        #region Init Methode

        /// <summary>
        /// Launch the Game
        /// </summary>
        /// <Author>Charif</Author>
        public void ResumeOrCreateGame()
        {
            if (drainMind == null)
            {
                drainMind = new DrainMindGame(canvas, CanvasViewer, UI);
                drainMind.Run();
            }
            if (!drainMind.IsRunning)
            {
                MainWindow.GetMainWindow.Content = this;
                drainMind.Resume();
            }
            StartupSettings();
        }

        /// <summary>
        /// Load Settings (Sounds ...) at the start of the game
        /// </summary>
        public void StartupSettings()
        {
            InitDataContext();
            ListViewLoadScores();

            if (Settings.Get().Son == 0 || !Settings.Get().SonOnOff)
            {
                drainMind.BackgroundVolume = 0;
                Settings.Get().Son = 0;
            }
            else
                drainMind.BackgroundVolume = Settings.Get().Son;              
        }

        /// <summary>
        /// initialise les dataContext
        /// </summary>
        public void InitDataContext()
        {
            XpProgressBar.DataContext = Experience.Instance;
            TextBlockNiveau.DataContext = Experience.Instance;
            TextBlockXpProgressBar.DataContext = Experience.Instance;
        }

        /// <summary>
        /// Charge les scores dans la listview
        /// </summary>
        public void ListViewLoadScores()
        {
            foreach (Score score in LesScores.Get().Scores)
            {
                ScoreListView.Items.Add(score);
            }
               
        }

        #endregion

        #region MainWindowEvents

        /// <summary>
        /// Event quand la taille de l'ecran change rescale les element de la fenetre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (drainMind != null)
            {
                Camera.MoveCamera(Camera.X, Camera.Y);
            }           
        }

        /// <summary>
        /// Affiche ou rend invisible le menu pause
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void FentrePrincipalDrainMain_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Escape)
            {
                bool Pressed = false;
                //Pause
                if (GroupBoxPause.Visibility == Visibility.Hidden && !Pressed)
                {
                    drainMind.Pause();
                    GroupBoxPause.Visibility = Visibility.Visible;
                    Pressed = true;
                }
                //Resume
                if (GroupBoxPause.Visibility == Visibility.Visible && !Pressed)
                {
                    drainMind.Resume();
                    GroupBoxPause.Visibility = Visibility.Hidden;
                    Pressed = true;
                }
            }


        }

        /// <summary>
        /// Event qui empêche le deplacement avec la mollete de la souris
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            e.Handled = true;

        }
        #endregion

        #region ButtonMenuPause

        /// <summary>
        /// Reprends la partie en cours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void Resumebutton_Click(object sender, RoutedEventArgs e)
        {
            drainMind.Resume();
            GroupBoxPause.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Ouvre la page des options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.GetMainWindow.Content = new Options(this);
        }

        /// <summary>
        /// Affiche le groupe Box des Scores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScoreButton_Click(object sender, RoutedEventArgs e)
        {
            GroupBoxPause.Visibility = Visibility.Hidden;
            ScoreMenuGroupBox.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Ouvre le Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            drainMind.Pause();          
            _MenuPrincipale.PlayButton.Content = DrainMind.Res.Strings.Reprendre;
            MainWindow.GetMainWindow.Content = _MenuPrincipale;
        }

        #endregion

        #region ScoreGroupBox

        private void RetourScoreButton_Click(object sender, RoutedEventArgs e)
        {
            GroupBoxPause.Visibility = Visibility.Visible;
            ScoreMenuGroupBox.Visibility = Visibility.Hidden;
            Stock.SauverScore(LesScores.Get().Scores);
        }

        #endregion

        #region Saisi des information GroupBox
        /// <summary>
        /// Button Terminer apres la saisi du psedo lance un partie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTerminerInfo_Click(object sender, RoutedEventArgs e)
        {
            GroupBoxInfoPerso.Visibility = Visibility.Hidden;
            Score.Destroy();
            Score.Get().Nom = NameInput.Text;
            LesScores.Get().Scores.Add(Score.Get());
            ResumeOrCreateGame();
        }

        #endregion


    }
}
