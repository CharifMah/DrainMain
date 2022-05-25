using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using DrainMind.metier.joueur;
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

        /// <summary>
        /// Generateur Item
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="game"></param>
        public GenerateurItem(Canvas canvas, Game game) : base(0,0,canvas, game, "")
        {
            g = game;
            PeachCanvas = canvas;        
            timeToCreate = new TimeSpan(0, 0, 1);
        }

        public override string TypeName => "generateuritem";

        /// <summary>
        /// Animate
        /// </summary>
        /// <param name="dt">delay before the last animation</param>
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

                int Sec = r.Next(5000 / (StatsPersoModel.Instance.Niveau + 1), 10000 / (StatsPersoModel.Instance.Niveau + 1));

                timeToCreate = new TimeSpan(0, 0, 0, 0, 200);
            }
        }

        public override void CollideEffect(GameItem other)
        {

        }
    }
}
