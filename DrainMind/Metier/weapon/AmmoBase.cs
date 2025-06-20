﻿using DrainMind.Metier;
using DrainMind.Metier.enemie;
using DrainMind.Metier.Game;
using DrainMind.Metier.Items;
using DrainMind.Metier.joueur;
using DrainMind.View;
using IUTGame;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DrainMind.metier.weapon
{
    public class AmmoBase : IUTGame.GameItem,IAnimable
    {
        private static EnemieBase _target;
        private double _firespeed;
        private double _angle;

        private TimeSpan delayTargetNull;
        public AmmoBase(string spriteName = "AmmoArme1.png") : base(DrainMindGame.Get().Joueur.PosX, DrainMindGame.Get().Joueur.PosY, DrainMindView.MainCanvas, DrainMindGame.Get(), spriteName)
        {
            _firespeed = 30;
            _target = DrainMindGame.Get().generateurEnemie.statsEnemies.GetNearestEnemie();
            _angle = Math.Atan2((_target.Top + _target.Height/2) - this.Top, (_target.Left + _target.Width/2) - this.Left) * (180 / Math.PI);

            delayTargetNull = new TimeSpan(0);
        }

        public override string TypeName => "Ammunition";

        public void Animate(TimeSpan dt)
        {
            if (Settings.Get().GameIsRunning)
            {
                delayTargetNull += (dt);
                if (_target != null && _target.Collidable)
                {
                    MoveToEnemie();
                }
                else
                {
                    MoveDA(_firespeed, Orientation);
                }
            }
            else
            {
                this.Dispose();
            }
        }

        public override void CollideEffect(GameItem other)
        {
            if (other.TypeName == "Enemie")
            {
                _target = null;
            }
        }

        /// <summary>
        /// Va en direction de lenemie le plus proche
        /// </summary>
        private void MoveToEnemie()
        {        
            this.Orientation = _angle;
            MoveDA(_firespeed, _angle);
            
        }

  

    }
}
