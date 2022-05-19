using DrainMind.Metier;
using DrainMind.Metier.Joueur;
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
        private Window mainwindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;

        public DrainMindView(MenuPrincipale Menu)
        {        
            ShowsNavigationUI = false;
            InitializeComponent();
            
            if (drainMind == null)
            {
                drainMind = new DrainMindGame(canvas, CanvasViewer, UI);
                drainMind.Run();
            }
            if (!drainMind.IsRunning)
            {
                mainwindow.Content = this;
                drainMind.Resume();          
            }
            InitDataContext();
            _MenuPrincipale = Menu;
        }

        public void InitDataContext()
        {
            XpProgressBar.DataContext = Experience.Instance;
            TextBlockNiveau.DataContext = Experience.Instance;
            TextBlockXpProgressBar.DataContext = Experience.Instance;

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

        #region MainWindowEvents

        /// <summary>
        /// Event quand la taille de l'ecran change rescale les element de la fenetre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Camera.MoveCamera(Camera.X, Camera.Y);
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
            mainwindow.Content = new Options(this);
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
            mainwindow.Content = _MenuPrincipale;                     
        }

        #endregion
    }
}
