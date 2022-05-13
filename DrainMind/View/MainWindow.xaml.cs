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
using Menu = DrainMind.View.Menu;

namespace DrainMind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DrainMindGame drainMind;
        /// <summary>
        /// Window du jeux et du MenuPause
        /// </summary>
        public MainWindow()
        {
            //Centre la fenetre
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            drainMind = new DrainMindGame(canvas,CanvasViewer,UI);
            DataContext = drainMind;            
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
        /// Environment.Exit a la fermeture de la fenetre 
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FentrePrincipalDrainMain_Closed(object sender, System.EventArgs e)
        {
            Environment.Exit(0);
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
            this.Close();
            Menu mn = new Menu();
            mn.Show();
        }

        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Options option = new Options(this);
            option.Show();
        }
    }
}
