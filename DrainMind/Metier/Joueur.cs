using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using DrainMind.Metier;
using IUTGame;
namespace DrainMind
{
    class Joueur : GameItem, IAnimable, IKeyboardInteract
    {        
        private bool compte = false;
        private double time = 0;
        private double speed = 50;
        
 
        public Joueur(double x, double y, Canvas c, Game g) :base(x,y,c,g,"Joueur.png")
        {
        }      

        public override string TypeName => "Joueur";

        public void Animate(TimeSpan dt)
        {
            if(compte)
            {
                time += dt.TotalMilliseconds;
                if (time > 500)
                    compte = false;
            }
            compte = true;
        }

        public override void CollideEffect(GameItem other)
        {
            if (!compte)
            {
                if (other.TypeName == "Enemie")
                {
                   // MessageBox.Show("Enemie Hit " + other.TypeName);
                }
                else
                {

                }
            }
        }

        public override bool IsCollide(GameItem other)
        {
            return base.IsCollide(other);
        }

        public void KeyDown(Key key)
        {
            switch(key)
            {
                case Key.Q:
                    MoveXY(.05 - speed, 0);                                 
                    break;
                case Key.D:
                    MoveXY(.05 + speed, 0);      
                    break;
                case Key.S:
                    MoveXY(0, .05 + speed);
                    break;
                case Key.Z:
                    MoveXY(0, .05 - speed);
                    break;
                case Key.Left:
                    MoveXY(-10, 0); break;
                case Key.Right:
                    MoveXY(10, 0); break;
                case Key.Down:
                    MoveXY(0, 10); break;
                case Key.Up:
                    MoveXY(0, -10); break;
            }
            Camera.X = this.Left;
            Camera.Y = this.Top;
            Camera.MoveCamera(this.Left,this.Top);
        }

        public void KeyUp(Key key)
        {
          
        }
    }
}
