using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Input;

namespace DrainMind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Centre la fenetre
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            DrainMindGame drainMind = new DrainMindGame(playerCanvas,CanvasViewer);
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
    }
}
