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
        public Vie(Canvas c, Game g):base(0,0,c,g,"1.png")
        {

        }

        public double _Vie
        {
            get { return vie; }
            set
            {
                
                vie = Math.Min(3, value);
                MessageBox.Show(vie.ToString());
                ChangeSprite($"{vie*2}.png");
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
