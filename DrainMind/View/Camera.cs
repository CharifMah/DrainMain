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
        private ScrollViewer _Camera;
        private double w = System.Windows.SystemParameters.FullPrimaryScreenWidth;
        private double h = System.Windows.SystemParameters.FullPrimaryScreenHeight;
        public Camera(double x, double y, Canvas canvas, Game game,ScrollViewer camera) : base(x, y, canvas, game, "Carte.jpg")
        {       
            _Camera = camera;
        }

        public override string TypeName => "Map";

        public override void CollideEffect(GameItem other)
        {
        }
        
        public void UpdateCamera(double x, double y)
        {
            w = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            h = System.Windows.SystemParameters.FullPrimaryScreenHeight;

            _Camera.ScrollToHorizontalOffset(x);
            _Camera.ScrollToVerticalOffset(y);


        }
    }
}
