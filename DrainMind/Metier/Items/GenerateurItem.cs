using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using DrainMind.Metier.Items;
using IUTGame;

namespace DrainMind
{
    /// <summary>
    /// generate the enemies
    /// </summary>
    public class GenerateurItem : GameItem, IAnimable
    {
        //time interval
        private TimeSpan timeToCreate;
        private Canvas PeachCanvas;
        private Game g;
        public GenerateurItem(Canvas canvas, Game game) : base(0,0,canvas, game, "")
        {
            g = game;
            PeachCanvas = canvas;        
            timeToCreate = new TimeSpan(0, 0, 5);
        }

        public override string TypeName => "generateuritem";

        public void Animate(TimeSpan dt)
        {
            timeToCreate = timeToCreate - dt;

            Random r = new Random();

            if (timeToCreate.TotalMilliseconds < 0)
            {
                double x = r.NextDouble() * GameWidth;
                double y = r.NextDouble() * GameHeight / 2;

                Food food = new Food(x,y, PeachCanvas,g);
                Game.AddItem(food);

                int Sec = r.Next(2000, 12000);
                timeToCreate = new TimeSpan(0, 0, 0, 0, Sec);
            }
        }

        public override void CollideEffect(GameItem other)
        {

        }
    }
}
