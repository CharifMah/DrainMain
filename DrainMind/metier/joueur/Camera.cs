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

namespace DrainMind.Metier
{
    /// <summary>
    /// game's camera, here to follow the mc
    /// </summary>
    /// <Author>Charif</Author>
    public class Camera : GameItem
    {

        /// <summary>
        /// camera's constructor
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <param name="canvas">canvas</param>
        /// <param name="game">drainMind</param>
        /// <param name="camera">camera</param>
        /// <Author>Charif</Author>
        public Camera(double x, double y,Game game) : base(x, y, new Canvas(), game)
        {       
        }

        //TypeName of the camera is "Camera"
        public override string TypeName => "Camera";

        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other"></param>
        public override void CollideEffect(GameItem other)
        {
            
        }

        /// <summary>
        /// move the camera to follow the mc
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <Author>Charif</Author>
        public static void MoveCamera(double x, double y)
        {
            DrainMindView.ScrollViewer.ScrollToVerticalOffset(y - (Application.Current.MainWindow.ActualHeight / 2));
            DrainMindView.ScrollViewer.ScrollToHorizontalOffset(x - (Application.Current.MainWindow.ActualWidth / 2));
        }
    }
}
