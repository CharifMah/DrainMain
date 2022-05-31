using DrainMind.Metier;
using DrainMind.Metier.joueur;
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
        protected double _speed;
        protected double _XpBase;
        /// <summary>
        /// Experience
        /// </summary>
        /// <param name="x">Postion x</param>
        /// <param name="y">Positon y</param>
        public ExpItem(double x, double y) : base(x, y, DrainMind.View.DrainMindView.MainCanvas, DrainMindGame.Instance, "Exp.png")
        {
            _speed = StatsPersoModel.Get().Speed * 2;
            _XpBase = 10;
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
                    if (StatsPersoModel.Get().XP + (_XpBase * StatsPersoModel.Get().Xpmult) >= StatsPersoModel.Get().XPMax)
                    {                                    
                        PlaySound("LvlUp.mp3");
                    }
                    StatsPersoModel.Get().XP = _XpBase;
                    new TextItem(this.Left, this.Top, $"+{_XpBase * StatsPersoModel.Get().Xpmult}", Brushes.Yellow);
                }
                this.Collidable = false;
            }
        }



        /// <summary>
        /// Xp movement towards the player
        /// </summary>
        /// <Author>Charif</Author>
        public void MoveXpToPlayer()
        {
            double ePosX = this.Left + (this.Width / 2);
            double ePosY = this.Top + (this.Height / 2);
            double _angle = Math.Atan2(Joueur.PosY - ePosY, Joueur.PosX - ePosX) * (180 / Math.PI);
            MoveDA(_speed, _angle);
        }
    }
}
