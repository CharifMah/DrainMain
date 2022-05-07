using IUTGame;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DrainMind
{
    class Enemie : GameItem, IAnimable
    {
        private double vitesse = 200;
        private double angle = 315;
        private static int nombre = 0;  
        private TimeSpan Waiting = new TimeSpan(0);

        protected double Vitesse
        {
            get { return vitesse; }
            set { vitesse = value; }
        }

        protected double Angle
        {
            get { return angle; }
            set { angle = value; }
        }


        public Enemie(double x, double y, Canvas c, Game g,string nom = "Enemie.png") :
            base(x, y, c,g,nom)
        {
            ++nombre;
        }

        private void Rebondir()
        {            
            vitesse += 10;
        }
        public override string TypeName => "Enemie";

        public override void CollideEffect(GameItem other)
        {
            if (Waiting.TotalMilliseconds <= 0)
            {
                        
                if (other.TypeName == "Joueur")
                {
                    angle = 360 - angle;
                }
                else if (other.TypeName == this.TypeName)
                {
                    angle = (angle + 180) % 360;
                }
                Rebondir();

                Waiting = new TimeSpan(0, 0, 0,0, 500);
            }
        }

        public void Animate(TimeSpan dt)
        {
            if (Waiting.TotalMilliseconds > 0)
            {
                Waiting = Waiting.Subtract(dt);
            }

            if (this.Top < 0)
            {
                Top = 0;
                angle = 360 - angle;
                Rebondir();
            }
            else if (Bottom > GameHeight)
            {
                Top = 0;
                angle = 360 + angle;
                Rebondir();
            }
            else if (Left < 0)
            {
                angle = (360 + 180 - angle) % 360;
                Left = 0;
                Rebondir();
            }
            else if (Right > GameWidth)
            {
                angle = (360 + 180 - angle) % 360;
                Right = GameWidth;
                Rebondir();
            }
            MoveDA(vitesse * dt.TotalSeconds, angle);
        }
    }
}
