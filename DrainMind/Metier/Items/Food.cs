using DrainMind.metier.Items;
using DrainMind.Metier.Game;
using DrainMind.View;
using IUTGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DrainMind.Metier.Items
{
    /// <summary>
    /// Food, allows to gain life back
    /// </summary>
    public class Food : IUTGame.GameItem, IAnimable
    {
        //ennemies's number
        private static int foodnumber = 0;

        /// <summary>
        /// Create the food
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        public Food(double x, double y) : base(x, y, DrainMindView.MainCanvas, DrainMindGame.Get(), "FoodPeach.png")
        {
            ++foodnumber;
            this.ChangeScale(2,2);
        }

        /// <summary>
        /// Name of the food
        /// </summary>
        public override string TypeName => "Peach";

        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other">other object</param>
        public override void CollideEffect(GameItem other)
        {
            if (other.TypeName == "Joueur")
            {
                this.Dispose();

                if (this.Collidable)
                {
                    DrainMindGame.Get().Joueur.Stats.Life._Vie += 2;
                    PlaySound("SoundTake.mp3");
                    new TextItem(this.Left, this.Top, $"+❤❤", Brushes.Red);
                }
               
                this.Collidable = false;
            
            }
        }

        /// <summary>
        /// Animate the item
        /// </summary>
        /// <param name="dt">time span</param>
        /// <author>Ryan</author>
        public void Animate(TimeSpan dt)
        {
            if (Settings.Get().GameIsRunning)
            {
                PreventCollide();
            }
            else
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// Prevent Collision between the item and the player
        /// </summary>
        /// <author>Ryan</author>
        public void PreventCollide()
        {
            double PlayerPosX = DrainMindGame.Get().Joueur.PosX;
            double PlayerPosY = DrainMindGame.Get().Joueur.PosY;
            double ePosX = this.Left + (this.Width / 2);
            double ePosY = this.Top + (this.Height / 2);

            double result = Math.Sqrt(Math.Pow(PlayerPosX - ePosX, 2) + Math.Pow(PlayerPosY - ePosY, 2));

            if (result < 50)
            {
                CollideEffect(DrainMindGame.Get().Joueur);
            }
        }
    }
}
