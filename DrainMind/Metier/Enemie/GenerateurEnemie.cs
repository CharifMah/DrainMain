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
            
            timeToCreate = new TimeSpan(0, 0, 0,0,500);
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
       
            if (timeToCreate.TotalSeconds < 0)
            {            
                CreateEnemie("fantome.png");
                CreateEnemie("fantomeVert.png"); 
                CreateEnemie("Gloom.png");
                CreateEnemie("nightmare.png");
                CreateEnemie("boss.png");
            
            }
        }

        public void CreateEnemie(string NameSprite )
        {
            Random r = new Random();
            double x = r.NextDouble() * GameWidth;
            double y = r.NextDouble() * GameHeight;
    
            Enemie enemie = new Enemie(x, y, Game, player, NameSprite);
            Game.AddItem(enemie);

            Sec = r.Next(3000, 5000);
            timeToCreate = new TimeSpan(0, 0, 0, 0, Sec);    
        }
        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other">the other object</param>
        public override void CollideEffect(GameItem other) { }
    }
}
