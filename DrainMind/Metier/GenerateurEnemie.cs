using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using IUTGame;
namespace DrainMind
{
    public class GenerateurEnemie : GameItem, IAnimable
    {
        private TimeSpan timeToCreate;

        private Canvas canvas;

        public GenerateurEnemie(Game g, Canvas c): base(0,0,c,g)
        {
            this.canvas = c;
            timeToCreate = new TimeSpan(0, 0, 2);
        }
        public override string TypeName => "generateur";

        public void Animate(TimeSpan dt)
        {
            timeToCreate = timeToCreate - dt;

            Random r = new Random();

            if (timeToCreate.TotalMilliseconds < 0)
            {            
                double x = r.NextDouble() * GameWidth;
                double y = r.NextDouble() * GameHeight/2;
                Enemie b = new Enemie(x, y, canvas, Game);
                Game.AddItem(b);

                int Sec = r.Next(2000, 12000);
                timeToCreate = new TimeSpan(0, 0, 0, 0, Sec);
            }   
        }

        public override void CollideEffect(GameItem other)
        {
           
        }
    }
}
