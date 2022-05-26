using DrainMind.metier.Grille;
using DrainMind.metier.Items;
using DrainMind.metier.joueur.ScoreFolder;
using DrainMind.View;
using DrainMind.ViewModel;
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
        //time interval
        private TimeSpan _waiting = new TimeSpan(0);
        
        /// <summary>
        /// ennemies constructor
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <param name="c">canvas</param>
        /// <param name="g">game</param>
        /// <param name="Spritename">name of the sprite</param>
        public Enemie(double x, double y,string spritename) : base(x, y,DrainMindView.MainCanvas,DrainMindGame.Instance,spritename)
        {           
            ChangeScale(0.7, 0.7);
            EnemiesModel.Get().NombreEnemie++;
            EnemiesModel.Get().Lesenemies.Add(this,new Coordonnees((int)x, (int)y));
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
                
                if (this.Collidable)
                {                   
                    if (StatsPersoModel.Instance.Life._Vie - 1 > 0)
                    {
                        StatsPersoModel.Instance.Life._Vie -= 1;
                    }
                    else
                    {
                        Game.Loose();
                    }

                    Destroy();

                }                                           
            }   
        }

        public void Destroy()
        {
            this.Dispose();
            this.Collidable = false;

            if (DrainMindGame.Instance != null)
            {         
            ExpItem xp = new ExpItem(this.Left + (this.Width / 2), this.Top + (this.Height / 2));
            DrainMindGame.Instance.AddItem(xp);
            }

            PlaySound("Bruit.mp3");
     
            Score.Get().EnemieKilled += 1;
            Score.Get().Point += 10;

            EnemiesModel.Get().NombreEnemie--;
            EnemiesModel.Get().Lesenemies.Remove(this);
        }

        /// <summary>
        /// Animate the item
        /// </summary>
        /// <param name="dt">timespan elasped since last animation</param>
        public void Animate(TimeSpan dt)
        {         
            if (_waiting.TotalMilliseconds > 0)
            {
                _waiting = _waiting.Subtract(dt);
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

            double moveX = -(this.Left + (this.Width / 2) - (StatsPersoModel.Instance.posX)) / 90;
            double moveY = -(this.Top + (this.Height / 2) - (StatsPersoModel.Instance.posY)) / 90;
            
            MoveXY(moveX, moveY);

           
        }
    }
}
