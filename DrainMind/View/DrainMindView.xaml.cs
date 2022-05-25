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
using System.Windows.Threading;

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

        private StockScore Stock = new StockScore(Environment.CurrentDirectory);
        //game's canvas
        private static Canvas _uicanvas;
        private static ScrollViewer _scrollviewer;
        private static Canvas _maincanvas;
        private static DrainMindView _drainMindView;
        private static GroupBox _upgradeSkillGrpBox;
        private DateTime _timer;
        private static DispatcherTimer timer;

        #region Property

        public static Canvas MainCanvas
        {
            get { return _maincanvas; }
        }

        public static Canvas UIcanvas
        {
            get { return _uicanvas; }
        }

        public static ScrollViewer ScrollViewer
        {
            get { return _scrollviewer; }
        }

        #endregion

        public DrainMindView(MenuPrincipale Menu)
        {      
            ShowsNavigationUI = false;       
            InitializeComponent();
            _drainMindView = this;

            GroupBoxUpgradeSkill.Visibility = Visibility.Hidden;
            GroupBoxInfoPerso.Visibility = Visibility.Visible;
            _scrollviewer = scrollviewer;
            _uicanvas = UI;
            _maincanvas = canvas;
            _upgradeSkillGrpBox = GroupBoxUpgradeSkill;
            _MenuPrincipale = Menu;


            //Minuteur
            _timer = new DateTime(0);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
           
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

            //DebutDuJeux
            ResumeOrCreateGame();
            timer.Start();
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
                drainMind = new DrainMindGame();
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

        #region ButtonMenuPause

        /// <summary>
        /// Reprends la partie en cours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void Resumebutton_Click(object sender, RoutedEventArgs e)
        {
            if (GroupBoxUpgradeSkill.Visibility == Visibility.Hidden)
            {
                drainMind.Resume();
                timer.Start();
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
            timer.Stop();
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

        /// <summary>
        /// Affiche et met en pause la groupe Box
        /// </summary>
        public static void ShowUpgradeGrpBox()
        {      
            _upgradeSkillGrpBox.Visibility = Visibility.Visible;
            timer.Stop();
            DrainMindGame.Instance.Pause();          
        }

        /// <summary>
        /// Cache la groupebox et Ajoute de la Vitesse au joueur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonADDSPEED_Click(object sender, RoutedEventArgs e)
        {
            StatsPersoModel.Instance.Speed += 5;
            GroupBoxUpgradeSkill.Visibility = Visibility.Hidden;
            DrainMindGame.Instance.Resume();
            timer.Start();
        }

        #endregion

        #region Events

        /// <summary>
        /// Refresh le Timer Toute les secondes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            _timer = _timer.AddSeconds(1);
            TimerTextBlock.Text = _timer.ToString("mm:ss");
        }

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
                    timer.Stop();
                    GroupBoxPause.Visibility = Visibility.Visible;
                    Pressed = true;
                }
                //Resume
                if (GroupBoxPause.Visibility == Visibility.Visible && !Pressed)
                {
                    if (GroupBoxUpgradeSkill.Visibility == Visibility.Hidden)
                    {
                        drainMind.Resume();
                        timer.Start();
                    }

                    GroupBoxPause.Visibility = Visibility.Hidden;
                    Pressed = true;
                }
            }

            if (e.Key == Key.G && DrainMindGame.Instance != null)
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

    }
}
