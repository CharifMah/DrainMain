using DrainMind.metier.Grille;
using DrainMind.metier.joueur;
using DrainMind.Metier;
using DrainMind.Metier.enemie;
using DrainMind.Metier.Game;
using DrainMind.Metier.joueur;
using DrainMind.Metier.ScoreFolder;
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
        private MenuPrincipale _MenuPrincipale;
        private SortAdorner listViewSortAdorner = null;
        private GridViewColumnHeader listViewSortCol = null;

        private StockScore Stock = new StockScore(Environment.CurrentDirectory);

        //game's canvas
        private static Canvas _uicanvas;
        private static ScrollViewer _scrollviewer;
        private static Canvas _maincanvas;
        private static GroupBox _upgradeSkillGrpBox;

        private DateTime _timer;
        private static DispatcherTimer timer;

        #region Property

        public static Canvas MainCanvas
        {
            get { return _maincanvas; }
            set { _maincanvas = value; }
        }

        public static Canvas UIcanvas
        {
            get { return _uicanvas; }
            set { _uicanvas = value; }
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

            GroupBoxUpgradeSkill.Visibility = Visibility.Hidden;
            GroupBoxInfoPerso.Visibility = Visibility.Visible;
            _scrollviewer = scrollviewer;
            _uicanvas = UI;
            _maincanvas = canvas;
            _upgradeSkillGrpBox = GroupBoxUpgradeSkill;
            _MenuPrincipale = Menu;

            MyGrid.NombreDeLigne = 20;
            MyGrid.NombreDeCollumn = 40;

            //Minuteur
            _timer = new DateTime(0);
            timer  = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;

            MainWindow.GetMainWindow.Deactivated += GetMainWindow_Deactivated;

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
            LesScores.Get().Scores.Add(Score.Get());

            //DebutDuJeux
            CreateGame();

            InitDataContext();
            ListViewLoadScores();
        }

        /// <summary>
        /// Launch the Game
        /// </summary>
        /// <Author>Charif</Author>
        public void CreateGame()
        {

            DrainMindGame.Get().Run();
            DrainMindGame.Get().BackgroundVolume = (Settings.Get().Son / 100);
            Settings.Get().GameIsRunning = true;
            MyGrid.Grid = MyGrid.drawGrid();
            timer.Start();
            GenerateurEnemie.GeneratorTimer.Start();
        }

        #endregion

        #region Init Methode


        /// <summary>
        /// initialise les dataContext
        /// </summary>
        public void InitDataContext()
        {
            XpProgressBar.DataContext = DrainMindGame.Get().Joueur.Stats;
            TextBlockNiveau.DataContext = DrainMindGame.Get().Joueur.Stats;
            TextBlockXpProgressBar.DataContext = DrainMindGame.Get().Joueur.Stats;
            TextBlockEnemieLeft.DataContext = DrainMindGame.Get().generateurEnemie.statsEnemies;
            TextBlockSpeed.DataContext = DrainMindGame.Get().Joueur.Stats;
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
                DrainMindGame.Get().Resume();
                GenerateurEnemie.GeneratorTimer.Start();
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
            DrainMindGame.Get().Pause();
            timer.Stop();
            GenerateurEnemie.GeneratorTimer.Stop();

            _MenuPrincipale.PlayButton.Content = DrainMind.Res.Strings.Reprendre;
            MainWindow.GetMainWindow.Content = _MenuPrincipale;
            Stock.SauverScore(LesScores.Get().Scores);
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
            GenerateurEnemie.GeneratorTimer.Stop();
            DrainMindGame.Get().Pause();
            DrainMindGame.Get().Joueur.StopMove();
                        
        }

        /// <summary>
        /// Cache la groupebox et Ajoute de la Vitesse au joueur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddSpeed_Click(object sender, RoutedEventArgs e)
        {
            DrainMindGame.Get().Joueur.Stats.Speed += 1;

            CloseUpgradeGrpBox();
        }


        private void AddLife_Button_Click(object sender, RoutedEventArgs e)
        {
            DrainMindGame.Get().Joueur.Stats.Life.AddLife(2);
            CloseUpgradeGrpBox();
        }

        private void Bombe_Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (EnemieBase enemie in DrainMindGame.Get().generateurEnemie.statsEnemies.LesEnemies.ToList())
            {
                enemie.LooseLife(1);
            }
            CloseUpgradeGrpBox();
        }

        /// <summary>
        /// Ferme la groupe Box et relance le timer
        /// </summary>
        private void CloseUpgradeGrpBox()
        {
            GroupBoxUpgradeSkill.Visibility = Visibility.Hidden;
            DrainMindGame.Get().Resume();
            GenerateurEnemie.GeneratorTimer.Start();
            timer.Start();
        }

        #endregion

        #region Events DrainMindView

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
            if (DrainMindGame.Get().IsRunning)
            {
                Camera.MoveCamera(DrainMindGame.Get().Joueur.PosX, DrainMindGame.Get().Joueur.PosY);
                MyGrid.ResizeCanvas();
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
                    DrainMindGame.Get().Pause();
                    GenerateurEnemie.GeneratorTimer.Stop();
                    timer.Stop();
                    GroupBoxPause.Visibility = Visibility.Visible;
                    DrainMindGame.Get().Joueur.StopMove();
                    Pressed = true;
                }
                //Resume
                if (GroupBoxPause.Visibility == Visibility.Visible && !Pressed)
                {
                    if (GroupBoxUpgradeSkill.Visibility == Visibility.Hidden)
                    {
                        DrainMindGame.Get().Resume();
                        GenerateurEnemie.GeneratorTimer.Start();
                        timer.Start();
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

        private void GetMainWindow_Deactivated(object sender, EventArgs e)
        {
            if (GroupBoxPause.Visibility == Visibility.Hidden && Settings.Get().GameIsRunning)
            {
                DrainMindGame.Get().Pause();
                GenerateurEnemie.GeneratorTimer.Stop();
                timer.Stop();
                GroupBoxPause.Visibility = Visibility.Visible;
                DrainMindGame.Get().Joueur.StopMove();
            }
        }

        #endregion

    }
}
