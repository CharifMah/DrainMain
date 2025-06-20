﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using DrainMind.Metier.Game;
using DrainMind.Metier.Items;
using DrainMind.View;
using IUTGame;

namespace DrainMind.metier.Items
{
    /// <summary>
    /// generate the enemies
    /// </summary>
    public class GenerateurItem : GameItem, IAnimable
    {
        //time interval
        private TimeSpan timeToCreate;

        /// <summary>
        /// Generateur Item
        /// </summary>
        /// <param name="canvas">canvas</param>
        /// <param name="game">drain mind</param>
        public GenerateurItem() : base(0, 0, DrainMindView.MainCanvas, DrainMindGame.Get(), "")
        {
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

                Food food = new Food(x, y);
                Game.AddItem(food);

                int Sec = r.Next(5000 / (DrainMindGame.Get().Joueur.Stats.Niveau + 1), 10000 / (DrainMindGame.Get().Joueur.Stats.Niveau + 1));

                timeToCreate = new TimeSpan(0, 0, 0, 0, Sec);
            }
        }

        public override void CollideEffect(GameItem other)
        {

        }
    }
}
