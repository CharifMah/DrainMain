using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using DrainMind.Metier;
using IUTGame;
using System.IO;
using DrainMind.Metier.Joueur;

namespace DrainMind
{
    /// <summary>
    /// main character of the game
    /// </summary>
    public class Joueur : GameItem, IAnimable, IKeyboardInteract
    {        
        private bool compte = false;
        private bool goLeft = false, goRight = false, goUp = false, goDown = false;
        private double time = 0;
        private double speed = 20;
        private int niveau;
        private Vie playerLife;
        private Experience XP;
        private Game DrainMind;
        private TimeSpan Waiting = new TimeSpan(0);

        // TypeName of the player is "Joueur"
        public override string TypeName => "Joueur";

        /// <summary>
        /// Constructor of the main character
        /// </summary>
        /// <param name="x">position x of the mc</param>
        /// <param name="y">position y of the mc</param>
        /// <param name="c">canvas of the application</param>
        /// <param name="g">drainMind</param>
        /// <param name="ui">canvas</param>
        public Joueur(double x, double y, Canvas c, Game g, Canvas ui, int life) : base(x,y,c,g,"face.png")
        {
            DrainMind = g;
            
            //Creation de la Vie
            playerLife = new Vie(ui,life);

            //Creation de l'experience
            XP = new Experience(0,0);
        }      

        #region Animation
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

            if (goLeft)
            {
                DeplacerJoueur(-speed + 05 * dt.TotalSeconds, 0);

            }
            if (goRight)
            {
                DeplacerJoueur(speed + 05 * dt.TotalSeconds, 0);

            }
            if (goUp)
            {
                DeplacerJoueur(0, -speed + 05 * dt.TotalSeconds);

            }
            if (goDown)
            {
                DeplacerJoueur(0, speed + 05 * dt.TotalSeconds);
            }
            AnimationJoueur();



            if (compte)
            {
                time += dt.TotalMilliseconds;
                if (time > 500)
                    compte = false;
            }

        }

        /// <summary>
        /// Animation, change sprite when the mc move
        /// </summary>
        /// <param name="key"></param>
        /// <param name="KeyEvent">True for KeyUp False for KeyDown</param>
        public void AnimationJoueur()
        {
            if (goLeft)
                ChangeSprite("gauche.png");
            if (goRight)
                ChangeSprite("droite.png");
            if (goUp)
                ChangeSprite("dos.png");
            if (goDown)
                ChangeSprite("face.png");
            if (goDown && goLeft)
                ChangeSprite("diagoSQ.png");
            if (goUp && goLeft)
                ChangeSprite("diagoZQ.png");
            if (goDown && goRight)
                ChangeSprite("diagoSD.png");
            if (goUp && goRight)
                ChangeSprite("diagoZD.png");
        }
        #endregion

        #region Collision
        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other">the other object</param>
        public override void CollideEffect(GameItem other)
        {
           
            if (!compte)
            {
                if (Waiting.TotalMilliseconds <= 0)
                {
                    if (other.TypeName == "Enemie")
                    {
                        LooseLife(1);
                        other.Dispose();
                        Score.Get().EnemieKilled += 1;
                        Score.Get().Point += 10;
                        compte = true;
                        time = 0;
                        XP.XP += 500;
                        PlaySound("Bruit.mp3");
                        LvlUpEffect();
                    }
                }
                Waiting = new TimeSpan(0, 0, 0, 0, 50);

                if (other.TypeName == "Peach")
                {
                    GainLife(1);
                    other.Dispose();
                    compte = true;
                    time = 0;
                    PlaySound("SoundTake.mp3");
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
        #endregion

        #region KeyboardInteract
        /// <summary>
        /// when a button is pressed
        /// </summary>
        /// <param name="key">key pressed</param>
        public void KeyDown(Key key)
        {
            if (DrainMind.IsRunning)
            {
                switch (key)
                {
                    case Key.Z:
                        goUp = true;
                        break;

                    case Key.Q:
                        goLeft = true;
                        break;

                    case Key.S:
                        goDown = true;
                        break;

                    case Key.D:
                        goRight = true;
                        break;


                    case Key.Up:
                        goUp = true;
                        break;

                    case Key.Left:
                        goLeft = true;
                        break;

                    case Key.Down:
                        goDown = true;
                        break;

                    case Key.Right:
                        goRight = true;
                        break;
                }
            }
        }

        /// <summary>
        /// when the button is not pressed anymore
        /// </summary>
        /// <param name="key">key not pressed anymore</param>
        public void KeyUp(Key key)
        {
            if (DrainMind.IsRunning)
            {
                switch (key)
                {
                    case Key.Z:
                        goUp = false;
                        break;

                    case Key.Q:
                        goLeft = false;
                        break;

                    case Key.S:
                        goDown = false;
                        break;

                    case Key.D:
                        goRight = false;
                        break;


                    case Key.Up:
                        goUp = false;
                        break;

                    case Key.Left:
                        goLeft = false;
                        break;

                    case Key.Down:
                        goDown = false;
                        break;

                    case Key.Right:
                        goRight = false;
                        break;
                }
            }
        }
        #endregion

        #region Vie
        /// <summary>
        /// Enleve des point de vie au joueur
        /// </summary>
        /// <param name="damage"></param>
        public void LooseLife(int damage)
        {
            if (playerLife._Vie - damage > 0)
            {
                playerLife._Vie -= damage;
            }
            else
            {
                Game.Loose();
            }
        }
        /// <summary>
        /// Gagne de la vie
        /// </summary>
        /// <param name="Healh"></param>
        public void GainLife(int Healh)
        {
            playerLife._Vie += Healh;
        }
        #endregion

        #region Niveau
        public void LvlUpEffect()
        {
            if (Experience.Instance.Niveau > niveau)
            {
                niveau = Experience.Instance.Niveau;
                this.speed *= 1.2;
                PlaySound("LvlUp.mp3");
            }

        }
        #endregion

        #region Mouvement
        /// <summary>
        /// move the player
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        public void DeplacerJoueur(double x, double y)
        {

            if (this.Left + x >= 0 && this.Right + x <= GameWidth && this.Bottom + y <= GameHeight && this.Top + y >= 0)
            {
                MoveXY(x, y);
            }

            Camera.X = this.Left + (this.Width) / 2;
            Camera.Y = this.Top + (this.Height) / 2;
            Camera.MoveCamera(this.Left + (this.Width) / 2, this.Top + (this.Height) / 2);
        }
        #endregion

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
