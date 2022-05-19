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
        //Represents a scrollable area, might contain other visible items
        private static ScrollViewer _Camera;
        //get position
        private static double _x = 0, _y = 0;
        public static double X
            {        
            get { return _x; } set { _x = value; }           
            }
        public static double Y
            { get { return _y; } set { _y = value; } }

        /// <summary>
        /// camera's constructor
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <param name="canvas">canvas</param>
        /// <param name="game">drainMind</param>
        /// <param name="camera">camera</param>
        /// <Author>Charif</Author>
        public Camera(double x, double y, Canvas canvas, Game game,ScrollViewer camera) : base(x, y, canvas, game)
        {       
            _Camera = camera;
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
            _Camera.ScrollToVerticalOffset(y - (Application.Current.MainWindow.ActualHeight / 2));
            _Camera.ScrollToHorizontalOffset(x - (Application.Current.MainWindow.ActualWidth / 2));
        }
    }
}
