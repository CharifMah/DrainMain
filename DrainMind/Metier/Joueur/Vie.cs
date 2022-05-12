using IUTGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DrainMind
{
    /// <summary>
    /// life of the main character
    /// </summary>
    class Vie : IUTGame.GameItem
    {
        //life start at 0.5 (will be changed)
        private double vie = 0.5;

        /// <summary>
        /// Constructor of the life
        /// </summary>
        /// <param name="c">canvas of the game</param>
        /// <param name="g">drainMind</param>
        public Vie(Canvas c, Game g):base(0,0,c,g,"1.png") { }

        /// <summary>
        /// change sprite when gain life (will change and be when lose life)
        /// </summary>
        public double _Vie
        {
            get { return vie; }
            set
            {
                
                vie = Math.Min(3, value);
                //MessageBox.Show(vie.ToString()); //J'ai mis la ligne en commentaire la MessageBox est trop chiante quand tu tests remettez au besoin
                ChangeSprite($"{vie*2}.png");
            }
        }

        //TypeName of the life is "Vie"
        public override string TypeName => "Vie";

        /// <summary>
        /// Tells if the object touch the other object
        /// </summary>
        /// <param name="other">the other object</param>
        /// <returns>true if this collide with other</returns>
        public override bool IsCollide(GameItem other)
        {
            return false;
        }

        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other">the other object</param>
        public override void CollideEffect(GameItem other) { }
    }
}
