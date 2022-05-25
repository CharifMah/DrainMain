using DrainMind.metier.joueur;
using DrainMind.Metier;
using IUTGame;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DrainMind.metier.Items
{
    public class Exp : IUTGame.GameItem, IAnimable
    {
        private int xptemp = 0;
        public Exp(double x, double y, Canvas canvas) : base(x, y, canvas, DrainMindGame.Instance, "Exp.png")
        {
            this.ChangeScale(0.2, 0.2);
        }

        public override string TypeName => "Exp";

        public void Animate(TimeSpan dt)
        {
            MoveXpToPlayer();
        }

        public override void CollideEffect(GameItem other)
        {
            if (other.TypeName == "Joueur")
            {
                this.Collidable = false;
                this.Dispose();
                
                if (xptemp < 10)
                {
                    xptemp = 10;
                    StatsPersoModel.Instance.XP = 10;
                    
                }
              

        
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
