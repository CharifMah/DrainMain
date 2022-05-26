using DrainMind.View;
using IUTGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DrainMind.metier.joueur
{
    /// <summary>
    /// game's camera, here to follow the mc
    /// </summary>
    /// <Author>Charif</Author>
    public class Camera
    {
        /// <summary>
        /// move the camera to follow the mc
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <Author>Charif</Author>
        public static void MoveCamera(double x, double y)
        {
            DrainMindView.ScrollViewer.ScrollToVerticalOffset(y - Application.Current.MainWindow.ActualHeight / 2);
            DrainMindView.ScrollViewer.ScrollToHorizontalOffset(x - Application.Current.MainWindow.ActualWidth / 2);
        }
    }
}
