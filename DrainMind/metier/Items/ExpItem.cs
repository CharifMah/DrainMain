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
    /// <summary>
    /// Exp of the item
    /// </summary>
    public class ExpItem : IUTGame.GameItem, IAnimable
    {
 
        /// <summary>
        /// Experience
        /// </summary>
        /// <param name="x">Postion x</param>
        /// <param name="y">Positon y</param>
        public ExpItem(double x, double y) : base(x, y, DrainMind.View.DrainMindView.MainCanvas, DrainMindGame.Instance, "Exp.png")
        {      
            this.ChangeScale(0.3, 0.3);
 
        }

        public override string TypeName => "Exp";

        /// <summary>
        /// Animate the item
        /// </summary>
        /// <param name="dt">time span</param>
        public void Animate(TimeSpan dt)
        {
            if (this.Collidable)
            {
                MoveXpToPlayer();
                
            }
        }

        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other">other objet</param>
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
        /// movement towards the player
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
