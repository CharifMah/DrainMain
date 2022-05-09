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
        private Vie vie;
        private TimeSpan Waiting = new TimeSpan(0);

        public Joueur(double x, double y, Canvas c, Game g, Canvas ui) :base(x,y,c,g,"Joueur.png")
        {
            vie = new Vie( 0, 0, ui, g);
            Game.AddItem(vie);


        }      

        public override string TypeName => "Joueur";

        public void Animate(TimeSpan dt)
        {
            if (Waiting.TotalMilliseconds > 0)
            {
                Waiting = Waiting.Subtract(dt);
            }

            if (compte)
            {
                time += dt.TotalMilliseconds;
                if (time > 500)
                    compte = false;
            }
        }

        public override void CollideEffect(GameItem other)
        {
            if (Waiting.TotalMilliseconds <= 0)
            {
                if (other.TypeName == "Enemie")
                {
                    vie._Vie += 0.5;
                    Waiting = new TimeSpan(0, 0, 0, 1);
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
            switch (key)
            {
                case Key.Q:
                    if (this.Left + .05 - speed > -1)
                        MoveXY(.05 - speed, 0);
                    break;
                case Key.D:
                    if (this.Left - .05 + speed < 10000)
                        MoveXY(.05 + speed, 0);
                    break;
                case Key.S:
                    if (this.Top - .05 + speed < 10000)
                        MoveXY(0, .05 + speed);
                    break;
                case Key.Z:
                    if (this.Top + .05 - speed > -1)
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
            Camera.MoveCamera(this.Left, this.Top);
        }

        public void KeyUp(Key key)
        {
          
        }
    }
}
