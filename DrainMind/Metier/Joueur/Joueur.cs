using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Timers;
using DrainMind.Metier;
using IUTGame;
namespace DrainMind
{
    /// <summary>
    /// main character of the game
    /// </summary>
    public class Joueur : GameItem, IAnimable, IKeyboardInteract
    {        
        private bool compte = false;
        private double time = 0;
        private double speed = 50;
        private double playerLife;
        private List<HealthSprite> sideBarHeart;
        private int LastHeartSprite;
        private TimeSpan Waiting = new TimeSpan(0);

        /// <summary>
        /// Constructor of the main character
        /// </summary>
        /// <param name="x">position x of the mc</param>
        /// <param name="y">position y of the mc</param>
        /// <param name="c">canvas of the application</param>
        /// <param name="g">drainMind</param>
        /// <param name="ui">canvas</param>
        public Joueur(double x, double y, Canvas c, Game g, Canvas ui, double life) : base(x,y,c,g,"face.png")
        {
            sideBarHeart = new List<HealthSprite>();

            for (int i = 1; i <= life; i++)
            {
                HealthSprite heart = new HealthSprite(ui, g, 1, i*50);
                sideBarHeart.Add(heart);
                Game.AddItem(heart);
                LastHeartSprite = i-1;
            }

            playerLife = life;
        }      

        // TypeName of the player is "Joueur"
        public override string TypeName => "Joueur";

        /// <summary>
        /// Animate the item
        /// </summary>
        /// <param name="dt">timespan elasped since last animation</param>
        public void Animate(TimeSpan dt)
        {
            if (Waiting.TotalMilliseconds > 0)
            {
                Waiting = Waiting.Subtract(dt);
            }

            if (compte)
            {
                time += dt.TotalMilliseconds;
                if (time > 500)
                    compte = false;
            }
        }

        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other">the other object</param>
        public override void CollideEffect(GameItem other)
        {
            if (Waiting.TotalMilliseconds <= 0)
            {
                if (other.TypeName == "Enemie")
                {
                    Waiting = new TimeSpan(0, 0, 0, 1);

                    if (playerLife - 0.5 > 0)
                    {
                        playerLife -= 0.5;

                        if (sideBarHeart[LastHeartSprite].LifePoint - 0.5 <= 0)
                        {
                            sideBarHeart[LastHeartSprite].Dispose();
                            Game.RemoveItem(sideBarHeart[LastHeartSprite]);
                            sideBarHeart[LastHeartSprite] = null;
                            LastHeartSprite -= 1;
                        }
                        else
                        {
                            sideBarHeart[LastHeartSprite].LifePoint -= 0.5;
                        }

                    }
                    else
                    {
                        Game.Loose();
                    }
                }
            }
        }

        /// <summary>
        /// Tells if the object touch the other object
        /// </summary>
        /// <param name="other">the other object</param>
        /// <returns>true if this collide with other</returns>
        public override bool IsCollide(GameItem other)
        {
            return base.IsCollide(other);
        }

        /// <summary>
        /// move the player
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        public void DeplacerJoueur(double x, double y)
        {
            if (this.Left + x > -1 && this.Right + x < 6915 && this.Bottom + y < 4421 && this.Top + y > -1)
            {
                MoveXY(x, y);
            }

            Camera.X = this.Left;
            Camera.Y = this.Top;
            Camera.MoveCamera(this.Left + (this.Width) / 2,this.Top + (this.Height)/2 );
        }

        /// <summary>
        /// when a button is pressed
        /// </summary>
        /// <param name="key">key pressed</param>
        public void KeyDown(Key key)
        {
            switch (key)
            {
                case Key.Q:
                    DeplacerJoueur(.05 - speed, 0);
                    AnimationJoueur(Key.Q);
                    break;

                case Key.D:
                    DeplacerJoueur(.05 + speed, 0);
                    AnimationJoueur(Key.D);
                    break;

                case Key.S:
                    DeplacerJoueur(0, .05 + speed);
                    break;

                case Key.Z:
                    DeplacerJoueur(0, .05 - speed);
                    AnimationJoueur(Key.Z);
                    break;

                case Key.Left:
                    DeplacerJoueur(.05 - speed, 0);
                    AnimationJoueur(Key.Left);
                    break;

                case Key.Right:
                    DeplacerJoueur(.05 + speed, 0);
                    AnimationJoueur(Key.Right);
                    break;

                case Key.Down:
                    DeplacerJoueur(0, .05 + speed);
                    break;

                case Key.Up:
                    DeplacerJoueur(0, .05 - speed);
                    AnimationJoueur(Key.Up);
                    break;
            }
        }

        /// <summary>
        /// when the button is not pressed anymore
        /// </summary>
        /// <param name="key">key not pressed anymore</param>
        public void KeyUp(Key key)
        {
            ChangeSprite("face.png");
        }

        /// <summary>
        /// Animation, change sprite when the mc move
        /// </summary>
        /// <param name="key">key pressed</param>
        public void AnimationJoueur(Key key)
        {
            if (key == Key.Q || key == Key.Left) { 
                ChangeSprite("gauche.png"); 
            }

            if (key == Key.D || key == Key.Right) {
                ChangeSprite("droite1.png");
            }
            
            if (key == Key.Z || key == Key.Up) { 
                ChangeSprite("dos.png"); 
            } 
            
        }

        /// <summary>
        /// Allows you to obtain the player's contact information
        /// </summary>
        /// <returns>Player's coords on screen</returns>
        public double[] GetCoordsPlayer()
        {
            double[] result = { this.Top, this.Right };
            return result;
        }
    }
}
