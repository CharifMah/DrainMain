using DrainMind.Metier;
using DrainMind.Metier.enemie;
using DrainMind.Metier.joueur;
using DrainMind.View;
using DrainMind.ViewModel;
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
        private double _damage;


        private TimeSpan delayTargetNull;
        public AmmoBase(string spriteName = "AmmoArme1.png") : base(Joueur.PosX, Joueur.PosY, DrainMindView.MainCanvas, DrainMindGame.Instance, spriteName)
        {
            _firespeed = 50;
            
            _target = EnemiesModel.Get().GetNearestEnemie();
            delayTargetNull = new TimeSpan(0);
        }

        public override string TypeName => "Ammunition";

        public void Animate(TimeSpan dt)
        {
            delayTargetNull += (dt);
            if (_target != null && _target.Collidable)
            {
                MoveToEnemie();

                if (_target.life - _damage <= 0)
                {
                    if (_firespeed > 5)
                    {
                        _firespeed -= 1;
                    }
                    MoveDA(_firespeed, Orientation);
                }
            }
            else 
            {
                if ( delayTargetNull.TotalMilliseconds > 1000)
                {
                    this.Collidable = false;
                    this.Dispose();
                }
                MoveDA(_firespeed, Orientation);
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
            double _angle = Math.Atan2(_target.Top - this.Top, _target.Left - this.Left) * (180 / Math.PI);

            this.Orientation = _angle;
            MoveDA(_firespeed, _angle);
            
        }
    }
}
