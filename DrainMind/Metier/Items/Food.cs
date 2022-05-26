using DrainMind.metier.Items;
using DrainMind.metier.joueur;
using DrainMind.View;
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
    public class Food : IUTGame.GameItem
    {
        //ennemies's number
        private static int foodnumber = 0;


        public Food(double x, double y) : base(x, y, DrainMindView.MainCanvas, DrainMindGame.Instance, "FoodPeach.png")
        {
            ++foodnumber;
            this.ChangeScale(2,2);
        }

        public override string TypeName => "Peach";

        public override void CollideEffect(GameItem other)
        {
            if (other.TypeName == "Joueur")
            {
                this.Dispose();

                if (this.Collidable)
                {
                    new TextItem(this.Left, this.Top, $"+❤", Brushes.Red);
                }
               
                this.Collidable = false;
            
            }
        }
    }
}
