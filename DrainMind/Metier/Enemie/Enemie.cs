using DrainMind.metier.Items;
using DrainMind.metier.joueur;
using DrainMind.Metier.joueur;
using IUTGame;
using System;
using System.Windows.Controls;

namespace DrainMind.Metier.enemie
{
    /// <summary>
    /// ennemies of the game
    /// </summary>
    public class Enemie : GameItem, IAnimable
    {
        //ennemies's speed
        private double vitesse = 200;

        //ennemies's angle
        private double angle = 315;

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

        /// <summary>
        /// ennemies constructor
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <param name="c">canvas</param>
        /// <param name="g">game</param>
        /// <param name="Spritename">name of the sprite</param>
        public Enemie(double x, double y, Game g, Joueur _player, string spritename) : base(x, y, DrainMind.View.DrainMindView.MainCanvas,g,spritename)
        {           
            ChangeScale(0.7, 0.7);
            EnemiesModel.Get().NombreEnemie++;
        }

        //TypeNme of ennemies is "Enemie"
        public override string TypeName => "Enemie";

        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other">the other object</param>
        public override void CollideEffect(GameItem other)
        {
               

            if (other.TypeName == "Joueur")
            {
                this.Dispose();
                if (this.Collidable)
                {
                    ExpItem xp = new ExpItem(this.Left, this.Top);
                    Game.AddItem(xp);

                    PlaySound("Bruit.mp3");
                    enemie.EnemiesModel.Get().NombreEnemie--;

                    Score.Get().EnemieKilled += 1;
                    Score.Get().Point += 10;
                }                            
                this.Collidable = false;
            }

            else if (other.TypeName == this.TypeName)
            {
                angle = (angle + 180) % 360;
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
            }
            else if (Bottom > GameHeight)
            {
                Bottom = 0;
            }
            else if (Left < 0)
            {
                Left = 0;
            }
            else if (Right > GameWidth)
            {
                Right = 0;
            }

            MoveEnemie();

        }

        /// <summary>
        /// Deplacement en direction du joueur
        /// </summary>
        /// <Author>Ryan</Author>
        /// <Co-Author>Charif</Co-Author>
        public void MoveEnemie()
        {

            double moveX = -(Left - (StatsPersoModel.Instance.posX - 75)) / 90;
            double moveY = -(Top - (StatsPersoModel.Instance.posY + 20)) / 90;
            
            MoveXY(moveX, moveY);

           
        }
    }
}
