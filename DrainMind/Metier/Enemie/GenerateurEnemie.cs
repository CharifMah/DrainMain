using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using DrainMind.metier.joueur;
using DrainMind.Metier.joueur;
using IUTGame;

namespace DrainMind.Metier.enemie
{
    /// <summary>
    /// generate the enemies
    /// </summary>
    public class GenerateurEnemie : GameItem, IAnimable
    {
        //time interval
        private TimeSpan timeToCreate;

        //canvas of the game
        private Canvas canvas;

        private Joueur player;

        private int Sec = 0;


        /// <summary>
        /// enemies's constructor 
        /// </summary>
        /// <param name="g">drainMind</param>
        /// <param name="c">canvas</param>
        public GenerateurEnemie(Game g, Canvas c, Joueur j): base(0,0,c,g)
        {
            this.canvas = c;
            
            timeToCreate = new TimeSpan(0, 0, 1);
            player = j;
        }

        //Type name of the generator is "generateur"
        public override string TypeName => "generateur";

        /// <summary>
        /// Animate the item
        /// </summary>
        /// <param name="dt">timespan elasped since last animation</param>
        public void Animate(TimeSpan dt)
        {
            timeToCreate = timeToCreate - dt;

            Random r = new Random();
            double x = r.NextDouble() * GameWidth;
            double y = r.NextDouble() * GameHeight;


            
            if (timeToCreate.Seconds < 0)
            {            
  
                Enemie fantomeViolet = new Enemie(x, y, this.canvas, Game, player, "fantome.png");
                Game.AddItem(fantomeViolet);

                Sec = r.Next(5000 / (StatsPersoModel.Instance.Niveau * 2 + 1), 10000 / (StatsPersoModel.Instance.Niveau * 2 + 1));
                timeToCreate = new TimeSpan(0, 0, 0, Sec);               
            }

            if (timeToCreate.Seconds == Sec/5)
            {
                x = r.NextDouble() * GameWidth;
                y = r.NextDouble() * GameHeight;

                Enemie fantomeVert = new Enemie(x, y, this.canvas, Game, player, "fantomeVert.png");
                Game.AddItem(fantomeVert);
            }
            if (timeToCreate.Seconds == Sec / 4)
            {
                x = r.NextDouble() * GameWidth;
                y = r.NextDouble() * GameHeight;

                Enemie Gloom = new Enemie(x, y, this.canvas, Game, player, "Gloom.png");
                Game.AddItem(Gloom);
            }
            if (timeToCreate.Seconds == Sec / 3)
            {
                x = r.NextDouble() * GameWidth;
                y = r.NextDouble() * GameHeight;

                Enemie nightmare = new Enemie(x, y, this.canvas, Game, player, "nightmare.png");
                Game.AddItem(nightmare);
            }
            if (timeToCreate.Seconds == Sec / 2)
            {
                Enemie boss = new Enemie(x, y, this.canvas, Game, player, "boss.png");
                Game.AddItem(boss);
            }
        }
        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other">the other object</param>
        public override void CollideEffect(GameItem other) { }
    }
}
