using DrainMind.Metier;
using DrainMind.Metier.Game;
using DrainMind.Metier.joueur;
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
        private double _amountXp;
        /// <summary>
        /// Experience
        /// </summary>
        /// <param name="x">Postion x</param>
        /// <param name="y">Positon y</param>
        public ExpItem(double x, double y,double amountXP) : base(x, y, DrainMind.View.DrainMindView.MainCanvas, DrainMindGame.Get(), "Exp.png")
        {
            _speed = 15;
            _XpBase = 1;
            this.ChangeScale(0.2, 0.2);
            _amountXp = amountXP;
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
                if (Settings.Get().GameIsRunning)
                {
                    MoveXpToPlayer();
                }
                else
                {
                    this.Dispose();
                }
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
                    if (Settings.Get().GameIsRunning && DrainMindGame.Get().Joueur.Stats.XP + (_amountXp * DrainMindGame.Get().Joueur.Stats.Xpmult) >= DrainMindGame.Get().Joueur.Stats.XPMax)
                    {                                    
                        PlaySound("LvlUp.mp3");
                    }
                    DrainMindGame.Get().Joueur.Stats.XP = _amountXp;
                    new TextItem(this.Left, this.Top, $"+{_amountXp * DrainMindGame.Get().Joueur.Stats.Xpmult}", Brushes.Yellow);
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
            double _angle = 0;
            if (Settings.Get().GameIsRunning)
            {
                _angle = Math.Atan2(DrainMindGame.Get().Joueur.PosY - ePosY, DrainMindGame.Get().Joueur.PosX - ePosX) * (180 / Math.PI);
            }
            MoveDA(_speed, _angle);
        }
    }
}
