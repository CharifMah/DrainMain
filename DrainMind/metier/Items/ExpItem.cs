using DrainMind.metier.joueur;
using DrainMind.Metier;
using IUTGame;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DrainMind.metier.Items
{
    public class ExpItem : IUTGame.GameItem, IAnimable
    {
        /// <summary>
        /// Boule experience
        /// </summary>
        /// <param name="x">Postion x</param>
        /// <param name="y">Positon y</param>
        public ExpItem(double x, double y) : base(x, y, DrainMind.View.DrainMindView.MainCanvas, DrainMindGame.Instance, "Exp.png")
        {
            this.ChangeScale(0.3, 0.3);
        }

        public override string TypeName => "Exp";

        public void Animate(TimeSpan dt)
        {
            if (this.Collidable)
            {
                MoveXpToPlayer();
            }
        }

        public override void CollideEffect(GameItem other)
        {
            if (other.TypeName == "Joueur")
            {             
                this.Dispose();

                if (Collidable)
                {
                    if (StatsPersoModel.Instance.XP + (10 * StatsPersoModel.Instance.Xpmult) >= StatsPersoModel.Instance.XPMax)
                    {
                        PlaySound("LvlUp.mp3");
                    }
                    StatsPersoModel.Instance.XP = 10;                  
                }   
                
                this.Collidable = false;                                       
            }
        }

        /// <summary>
        /// Deplacement en direction du joueur
        /// </summary>
        /// <Author>Ryan</Author>
        /// <Co-Author>Charif</Co-Author>
        public void MoveXpToPlayer()
        {

            double moveX = -(Left - (StatsPersoModel.Instance.posX)) / 12;
            double moveY = -(Top - (StatsPersoModel.Instance.posY)) / 12;

             MoveXY(moveX, moveY);

        }
    }
}
