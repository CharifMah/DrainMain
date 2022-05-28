using DrainMind.metier.Items;
using DrainMind.View;
using DrainMind.ViewModel;
using IUTGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace DrainMind.Metier.Items
{
    /// <summary>
    /// Food, allows to gain life back
    /// </summary>
    public class Food : IUTGame.GameItem
    {
        //ennemies's number
        private static int foodnumber = 0;

        /// <summary>
        /// Create the food
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        public Food(double x, double y) : base(x, y, DrainMindView.MainCanvas, DrainMindGame.Instance, "FoodPeach.png")
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
                    StatsPersoModel.Instance.Life._Vie += 2;
                    PlaySound("SoundTake.mp3");
                    new TextItem(this.Left, this.Top, $"+❤❤", Brushes.Red);
                }
               
                this.Collidable = false;
            
            }
        }
    }
}
