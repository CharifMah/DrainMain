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
using System.Windows.Threading;

namespace DrainMind.metier.weapon
{
    public class WeaponBase : IUTGame.GameItem
    {
        protected double _firesdelay;
        //Time
        private DateTime _timer;
        //Minuteur
        private DispatcherTimer timer;

        public WeaponBase(string spriteName = "") : base(Joueur.PosX, Joueur.PosY, DrainMindView.MainCanvas, DrainMindGame.Instance, spriteName)
        {
            _firesdelay = 700;
            //Minuteur
            _timer = new DateTime(0);
            timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(_firesdelay) };
            timer.Tick += Fire;
            timer.Start();
        }

        public override string TypeName => "Weapon";

        public override void CollideEffect(GameItem other)
        {

        }
      
        /// <summary>
        /// Tire toute les x seconde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Fire(object sender, EventArgs e)
        {
            if (EnemiesModel.Get().Lesenemies.Count > 0 && DrainMindGame.Instance != null)
            {
                AmmoBase n = new AmmoBase();
                DrainMindGame.Instance.AddItem(n);
            }
        }
    }
}
