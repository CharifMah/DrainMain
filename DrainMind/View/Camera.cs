using IUTGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DrainMind.Metier
{
    public class Camera : GameItem
    {
        private static ScrollViewer _Camera;

        
        private static double _x = 0, _y = 0;
        public static double X
            {        
            get { return _x; } set { _x = value; }           
            }
        public static double Y
            { get { return _y; } set { _y = value; } }

        public Camera(double x, double y, Canvas canvas, Game game,ScrollViewer camera) : base(x, y, canvas, game)
        {       
            _Camera = camera;

        }

        public override string TypeName => "Camera";

        public override void CollideEffect(GameItem other)
        {
        }
        
        public static void MoveCamera(double x, double y)
        {
            double Height = Application.Current.MainWindow.ActualHeight;
            double Width = Application.Current.MainWindow.ActualWidth;
            _Camera.ScrollToVerticalOffset(y - (Height / 2));
            _Camera.ScrollToHorizontalOffset(x - (Width / 2));
        }
    }
}
