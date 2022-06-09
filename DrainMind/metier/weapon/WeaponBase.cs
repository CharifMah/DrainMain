using DrainMind.metier.Grille;
using DrainMind.Metier.enemie;
using DrainMind.Metier.Game;
using DrainMind.Metier.joueur;
using DrainMind.View;
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

        //Minuteur
        private DispatcherTimer timer;

        public WeaponBase(string spriteName = "Type1Arme1.png") : base(DrainMindGame.Get().Joueur.PosX, DrainMindGame.Get().Joueur.PosY, DrainMindView.MainCanvas, DrainMindGame.Get(), spriteName)
        {
            _firesdelay = 700;

            DrainMindView.UIcanvas.Children.Add(MyGrid.PutSpriteInCase(2, 16, SpriteStore.Get(spriteName).Image));
           
            //Minuteur
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
            if (DrainMindGame.Get().generateurEnemie.statsEnemies.LesEnemies.Count > 0)
            {
                AmmoBase n = new AmmoBase();
                DrainMindGame.Get().AddItem(n);
            }
        }
    }
}
