using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using DrainMind.Metier;
using IUTGame;
using System.IO;
using DrainMind.Metier.joueur;
using System.Windows;
using DrainMind.metier.joueur;

namespace DrainMind.Metier.joueur
{
    /// <summary>
    /// main character of the game
    /// </summary>
    public class Joueur : GameItem, IAnimable, IKeyboardInteract
    {        
      
        private bool goLeft = false, goRight = false, goUp = false, goDown = false;
  
        private int niveau;
        private Vie playerLife;


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
        public Joueur(double x, double y, Canvas c, DrainMindGame g,Vie vie) : base(x,y,c,g,"face.png")
        {
       
            new StatsPersoModel(10, 0, 0, 1.2);
            //Creation de la Vie
            playerLife = vie;          
            
        }      

        #region Animation
        /// <summary>
        /// Animate the item
        /// </summary>
        /// <param name="dt">timespan elasped since last animation</param>
        public void Animate(TimeSpan dt)
        {

            if (goLeft)
            {
                DeplacerJoueur(-StatsPersoModel.Instance.Speed + 05 * dt.TotalSeconds, 0);

            }
            if (goRight)
            {
                DeplacerJoueur(StatsPersoModel.Instance.Speed + 05 * dt.TotalSeconds, 0);

            }
            if (goUp)
            {
                DeplacerJoueur(0, -StatsPersoModel.Instance.Speed + 05 * dt.TotalSeconds);

            }
            if (goDown)
            {
                DeplacerJoueur(0, StatsPersoModel.Instance.Speed + 05 * dt.TotalSeconds);
            }
            AnimationJoueur();        
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
            if (other.TypeName == "Enemie")
            {
                other.Dispose();
                other.Collidable = false;   
                    
                LooseLife(1);
                PlaySound("Bruit.mp3");
                enemie.EnemiesModel.Get().NombreEnemie--;
                StatsPersoModel.Instance.XP += 500;
                
                Score.Get().EnemieKilled += 1;
                Score.Get().Point += 10;

                LvlUpEffect();
            }


            if (other.TypeName == "Peach")
            {
                other.Dispose();
                other.Collidable = false;

                GainLife(1);
                PlaySound("SoundTake.mp3");
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
            if (DrainMindGame.Instance.IsRunning)
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
            if (DrainMindGame.Instance.IsRunning)
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
            if (StatsPersoModel.Instance.Niveau > niveau)
            {
                niveau = StatsPersoModel.Instance.Niveau;                  
                StatsPersoModel.LvlUpGrpBox.Visibility = Visibility.Visible;
                DrainMindGame.Instance.Pause();
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

            StatsPersoModel.Instance.posX = this.Left + (this.Width) / 2;
            StatsPersoModel.Instance.posY = this.Top + (this.Height) / 2;

            Camera.MoveCamera(StatsPersoModel.Instance.posX, StatsPersoModel.Instance.posY);     
        }
        #endregion
    }
}
