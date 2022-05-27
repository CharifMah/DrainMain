using DrainMind.Metier;
using DrainMind.ViewModel;
using IUTGame;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

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
                    new TextItem(this.Left, this.Top, $"+{10 * StatsPersoModel.Instance.Xpmult}", Brushes.Yellow);
                }
                this.Collidable = false;
            }
        }



        /// <summary>
        /// Deplacement en direction du joueur
        /// </summary>
        /// <Author>Charif</Author>
        public void MoveXpToPlayer()
        {
            double ePosX = this.Left + (this.Width / 2);
            double ePosY = this.Top + (this.Height / 2);
            double _angle = Math.Atan2(StatsPersoModel.Instance.posY - ePosY, StatsPersoModel.Instance.posX - ePosX) * (180 / Math.PI);
            MoveDA(10, _angle);

        }
    }
}
