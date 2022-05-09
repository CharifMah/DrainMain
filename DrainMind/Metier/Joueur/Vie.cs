using IUTGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DrainMind
{
    class Vie : IUTGame.GameItem
    {
        private double vie = 0.5;
        public Vie(double x, double y, Canvas c, Game g):base(x,y,c,g,"0.5.png")
        {
        }

        public double _Vie
        {
            get { return vie; }
            set
            {
                MessageBox.Show(value.ToString()) ;
                vie = Math.Min(3, value);
                ChangeSprite($"{vie}.png");
            }
        }

       

        public override string TypeName => "Vie";

        public override bool IsCollide(GameItem other)
        {
            return false;
        }
        public override void CollideEffect(GameItem other)
        {
            
        }

        
    }
}
