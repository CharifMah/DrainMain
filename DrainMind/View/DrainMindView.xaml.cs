using DrainMind.Metier;
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

        public DrainMindView()
        {        
            ShowsNavigationUI = false;
            InitializeComponent();
            drainMind = new DrainMindGame(canvas, CanvasViewer, UI);
            drainMind.Run();
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

        /// <summary>
        /// Event qui empeche le deplacement de la camera avec les touche up,left,down,right
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasViewer_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
            //{
            //    e.Handled = true;
            //}
        }

        #region MainWindowEvents
        /// <summary>
        /// Event quand la taille de l'ecran change rescale les element de la fenetre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Camera.MoveCamera(Camera.X, Camera.Y);
        }


        /// <summary>
        /// Affiche ou rend invisible le menu pause
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #region MenuPauseEvents
        /// <summary>
        /// Reprends la partie en cours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Resumebutton_Click(object sender, RoutedEventArgs e)
        {
            drainMind.Resume();
            GroupBoxPause.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Quitte completement le jeux
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Window mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mw.Content = new MenuPrincipale();
        }

        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            Window mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mw.Content = new Options(this);
        }

        #endregion
    }
}
