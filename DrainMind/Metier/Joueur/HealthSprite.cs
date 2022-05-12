using IUTGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DrainMind
{
    /// <summary>
    /// life of the main character
    /// </summary>
    class HealthSprite : IUTGame.GameItem
    {
        //life start at 0.5 (will be changed)
        private double life;

        /// <summary>
        /// change sprite when gain life (will change and be when lose life)
        /// </summary>
        public double LifePoint
        {
            get { return life; }
            set
            {
                if (value == 0.5)
                {
                    life -= value;
                    ChangeSprite(Path.Combine("Vie", $"{1}.png"));
                }
            }
        }

        //TypeName of the life is "Vie"
        public override string TypeName => "Heart";

        /// <summary>
        /// Constructor of the life
        /// </summary>
        /// <param name="c">canvas of the game</param>
        /// <param name="g">drainMind</param>
        public HealthSprite(Canvas c, Game g, int pointLife, int x) : base(x, 5, c, g, Path.Combine("Vie",$"{pointLife + 1}.png")) 
        {
            life = pointLife;
        }

        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other">the other object</param>
        public override void CollideEffect(GameItem other) 
        { 
        
        }
    }
}
