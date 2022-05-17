using IUTGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DrainMind.Metier.Items
{
    public class Food : IUTGame.GameItem
    {
        //ennemies's number
        private static int foodnumber = 0;

        public Food(double x, double y, Canvas canvas, Game game, string spriteName = "FoodPeach.png") : base(x, y, canvas, game, spriteName)
        {
            ++foodnumber;
            this.ChangeScale(2,2);
        }

        public override string TypeName => "Peach";

        public override void CollideEffect(GameItem other)
        {
        }
    }
}
