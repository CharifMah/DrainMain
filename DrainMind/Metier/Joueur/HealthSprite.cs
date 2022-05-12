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
                    ChangeSprite("1.png");
                }

                //MessageBox.Show(vie.ToString()); //J'ai mis la ligne en commentaire la MessageBox est trop chiante quand tu tests remettez au besoin
            }
        }

        //TypeName of the life is "Vie"
        public override string TypeName => "Heart";

        /// <summary>
        /// Constructor of the life
        /// </summary>
        /// <param name="c">canvas of the game</param>
        /// <param name="g">drainMind</param>
        public HealthSprite(Canvas c, Game g, int pointLife, int x): base(x, 5, c, g, pointLife+1+".png") 
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
