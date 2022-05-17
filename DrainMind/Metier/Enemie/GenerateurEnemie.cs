using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using IUTGame;
namespace DrainMind
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

        /// <summary>
        /// enemies's constructor 
        /// </summary>
        /// <param name="g">drainMind</param>
        /// <param name="c">canvas</param>
        public GenerateurEnemie(Game g, Canvas c, Joueur j): base(0,0,c,g)
        {
            this.canvas = c;
            timeToCreate = new TimeSpan(0, 0, 2);
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

            if (timeToCreate.TotalMilliseconds < 0)
            {            
                double x = r.NextDouble() * GameWidth;
                double y = r.NextDouble() * GameHeight/2;
                Enemie b = new Enemie(x, y, this.canvas, Game, player);
                Game.AddItem(b);

                int Sec = r.Next(100, 300);
                timeToCreate = new TimeSpan(0, 0, 0, 0, Sec);
            }   
        }

        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other">the other object</param>
        public override void CollideEffect(GameItem other) { }
    }
}
