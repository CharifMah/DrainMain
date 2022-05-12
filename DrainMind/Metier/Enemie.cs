using IUTGame;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DrainMind
{
    /// <summary>
    /// ennemies of the game
    /// </summary>
    class Enemie : GameItem, IAnimable
    {
        //ennemies's speed
        private double vitesse = 200;

        //ennemies's angle
        private double angle = 315;

        //ennemies's number
        private static int nombre = 0;

        //time interval
        private TimeSpan Waiting = new TimeSpan(0);

        //spd property's of ennemies
        protected double Vitesse
        {
            get { return vitesse; }
            set { vitesse = value; }
        }

        //angle property's of ennemies
        protected double Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        private Joueur player;

        /// <summary>
        /// ennemies constructor
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <param name="c">canvas</param>
        /// <param name="g">game</param>
        /// <param name="nom">name of the sprite</param>
        public Enemie(double x, double y, Canvas c, Game g, Joueur _player, string nom = "fantome.png") : base(x, y, c,g,nom)
        {
            ++nombre;
            player = _player;
        }

        /// <summary>
        /// ennemies gain spd
        /// </summary>
        private void Rebondir()
        {            
            vitesse += 1;
        }

        //TypeNme of ennemies is "Enemie"
        public override string TypeName => "Enemie";

        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other">the other object</param>
        public override void CollideEffect(GameItem other)
        {
            if (Waiting.TotalMilliseconds <= 0)
            {
                        
                if (other.TypeName == "Joueur")
                {
                    this.Dispose();
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

        /// <summary>
        /// Animate the item
        /// </summary>
        /// <param name="dt">timespan elasped since last animation</param>
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
                //Rebondir();
            }
            else if (Bottom > GameHeight)
            {
                Bottom = 0;
                angle = 360 + angle;
                //Rebondir();
            }
            else if (Left < 0)
            {
                angle = (360 + 180 - angle) % 360;
                Left = 0;
                //Rebondir();
            }
            else if (Right > GameWidth)
            {
                angle = (360 + 180 - angle) % 360;
                Right = 0;
                //Rebondir();
            }

            double[] coordsPlayer = player.GetCoordsPlayer();

            //MessageBox.Show((coordsPlayer[0] / 360).ToString());
            //MessageBox.Show((coordsPlayer[1] / 360).ToString());

            MoveXY(-(Left - coordsPlayer[1])/360, -(Top - coordsPlayer[0]) / 360);
            //MoveDA(vitesse * dt.TotalSeconds, angle);
        }
    }
}
