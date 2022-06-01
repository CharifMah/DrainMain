using DrainMind.Metier;
using DrainMind.Metier.enemie;
using DrainMind.Metier.Items;
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
        private double _angle;

        private TimeSpan delayTargetNull;
        public AmmoBase(string spriteName = "AmmoArme1.png") : base(Joueur.PosX, Joueur.PosY, DrainMindView.MainCanvas, DrainMindGame.Instance, spriteName)
        {
            _firespeed = 30;
            _target = EnemiesModel.Get().GetNearestEnemie();
            _angle = Math.Atan2(_target.Top - this.Top, _target.Left - this.Left) * (180 / Math.PI);

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
