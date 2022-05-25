using DrainMind.metier.Grille;
using DrainMind.metier.joueur;
using DrainMind.Metier;
using DrainMind.Metier.enemie;
using DrainMind.Metier.joueur;
using DrainMind.Stockage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private SortAdorner listViewSortAdorner = null;
        private GridViewColumnHeader listViewSortCol = null;

        StockScore Stock = new StockScore(Environment.CurrentDirectory);
        public DrainMindView(MenuPrincipale Menu)
        {      
            ShowsNavigationUI = false;       
            InitializeComponent();
           
            GroupBoxUpgradeSkill.Visibility = Visibility.Hidden;
            GroupBoxInfoPerso.Visibility = Visibility.Visible;
             _MenuPrincipale = Menu;
        }

        #region Saisi des information GroupBox (Debut du jeux)
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
            LesScoresModel.Get().Scores.Add(Score.Get());
            ResumeOrCreateGame();
        }
        #endregion 

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
            MyGrid.Grid = MyGrid.drawGrid();
        }

        /// <summary>
        /// Load Settings (Sounds ...) at the start of the game
        /// </summary>
        public void StartupSettings()
        {
            //Deletes Old instance          
            EnemiesModel.Destroy();
            ListViewLoadScores();
            InitDataContext();
            if (Settings.Get().Son == 0 || !Settings.Get().SonOnOff)
            {
                drainMind.BackgroundVolume = 0;
                Settings.Get().Son = 0;
            }
            else
                drainMind.BackgroundVolume = Settings.Get().Son;
        
            StatsPersoModel.LvlUpGrpBox = GroupBoxUpgradeSkill;
        }

        /// <summary>
        /// initialise les dataContext
        /// </summary>
        public void InitDataContext()
        {
            XpProgressBar.DataContext = StatsPersoModel.Instance;
            TextBlockNiveau.DataContext = StatsPersoModel.Instance;
            TextBlockXpProgressBar.DataContext = StatsPersoModel.Instance;
            TextBlockEnemieLeft.DataContext = EnemiesModel.Get();
            TextBlockSpeed.DataContext = StatsPersoModel.Instance;
        }

        /// <summary>
        /// Charge les scores dans la listview
        /// </summary>
        public void ListViewLoadScores()
        {
            foreach (Score score in LesScoresModel.Get().Scores)
            {
                ScoreListView.Items.Add(score);
            }
               
        }

        #endregion

        #region DrainMindViewEvents


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
                Camera.MoveCamera(StatsPersoModel.Instance.posX, StatsPersoModel.Instance.posY);
                MyGrid.ResizeCanvas(ref UI);
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
                    if (StatsPersoModel.LvlUpGrpBox.Visibility == Visibility.Hidden)
                    {
                        drainMind.Resume();
                    }
                    
                    GroupBoxPause.Visibility = Visibility.Hidden;
                    Pressed = true;
                }
            }

            if (e.Key == Key.G)
            {
                bool Pressed = false;
                
                //Pause
                if (!Pressed && !UI.Children.Contains(MyGrid.Grid))
                {
                    UI.Children.Add(MyGrid.Grid);
                    
                    Pressed = true;


                }
                //Resume
                if (!Pressed && UI.Children.Contains(MyGrid.Grid))
                {
                    UI.Children.Remove(MyGrid.Grid);
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
            if (StatsPersoModel.LvlUpGrpBox.Visibility == Visibility.Hidden)
            {
                drainMind.Resume();
            }
            
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
            Stock.SauverScore(LesScoresModel.Get().Scores);
        }

        #endregion

        #region ScoreGroupBox

        /// <summary>
        /// Cache la fentre onclick button Retour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RetourScoreButton_Click(object sender, RoutedEventArgs e)
        {
            GroupBoxPause.Visibility = Visibility.Visible;
            ScoreMenuGroupBox.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Event On Click sur le Header de la collumn des scores (Sort)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvUsersColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                ScoreListView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            ScoreListView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }
    

        #endregion

        #region LvlUp Skill Updgrade

        private void ButtonADDSPEED_Click(object sender, RoutedEventArgs e)
        {
            StatsPersoModel.Instance.Speed += 5;
            StatsPersoModel.LvlUpGrpBox.Visibility = Visibility.Hidden;
            DrainMindGame.Instance.Resume();
        }



        #endregion

    }
}
