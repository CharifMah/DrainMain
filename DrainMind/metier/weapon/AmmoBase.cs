using DrainMind.Metier;
using DrainMind.Metier.enemie;
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
        private EnemieBase _target;
        private double _firespeed;
        private TimeSpan delayTargetNull;
        public AmmoBase(string spriteName = "AmmoArme1.png") : base(StatsPersoModel.Instance.posX, StatsPersoModel.Instance.posY, DrainMindView.MainCanvas, DrainMindGame.Instance, spriteName)
        {
            _firespeed = 10;
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
            }
            else
            {
                MoveDA(_firespeed, Orientation);

                if (_firespeed > 5)
                {
                    _firespeed -= 1;
                }
                
                if (delayTargetNull.TotalMilliseconds > 1000)
                {
                    this.Dispose();
                    this.Collidable = false;
                }      
            }
        }

        public override void CollideEffect(GameItem other)
        {
            if (other.TypeName == "Enemie")
            {
                ((EnemieBase)other).Destroy();
                this.Dispose();
                this.Collidable = false;
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
