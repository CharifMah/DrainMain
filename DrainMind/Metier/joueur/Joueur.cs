﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using IUTGame;
using System.IO;
using System.Windows;
using DrainMind.View;
using DrainMind.metier.joueur;
using System.Windows.Threading;
using DrainMind.metier.weapon;
using DrainMind.Metier.Game;

namespace DrainMind.Metier.joueur
{
    /// <summary>
    /// main character of the game
    /// </summary>
    public class Joueur : GameItem, IAnimable, IKeyboardInteract
    {
        private double _posX;
        private double _posY;
        private StatsPerso _persoStats;
        private bool goLeft = false, goRight = false, goUp = false, goDown = false;
        private int counter = 0;
        private string[] ArrayDroite = new string[4] { "Personnage/droite.png", "Personnage/droite1.png", "Personnage/droite.png", "Personnage/droite2.png" };
        private string[] ArrayGauche = new string[4] { "Personnage/gauche.png", "Personnage/gauche1.png", "Personnage/gauche.png", "Personnage/gauche2.png" };

        /// <summary>
        /// Position X du Joueur
        /// </summary>
        public double PosX
        {
            get { return _posX; }
            set { _posX = value; }
        }

        /// <summary>
        /// Position Y du Joueur
        /// </summary>
        public double PosY
        {
            get { return _posY; }
            set { _posY = value; }
        }

        /// <summary>
        /// Class Stats du joueur
        /// </summary>
        public StatsPerso Stats
        {
            get { return _persoStats; }
        }

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
        public Joueur(double x, double y) : base(x,y,DrainMindView.MainCanvas,DrainMindGame.Get(), "Personnage/face.png")
        {
            ChangeScale(0.7,0.7);


            _posX = x + (this.Width / 2);
            _posY = y + (this.Height / 2);

            _persoStats = new StatsPerso(5,5,30);
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
                DeplacerJoueur(-DrainMindGame.Get().Joueur.Stats.Speed+ 05 * dt.TotalSeconds, 0);
            }
            if (goRight)
            {
                DeplacerJoueur(DrainMindGame.Get().Joueur.Stats.Speed + 05 * dt.TotalSeconds, 0);

            }
            if (goUp)
            {
                DeplacerJoueur(0, -DrainMindGame.Get().Joueur.Stats.Speed + 05 * dt.TotalSeconds);

            }
            if (goDown)
            {
                DeplacerJoueur(0, DrainMindGame.Get().Joueur.Stats.Speed + 05 * dt.TotalSeconds);             
            }
            AnimationJoueur();            
        }

        /// <summary>
        /// Animation, change sprite when the mc move
        /// </summary>
        /// <param name="key">key pressed</param>
        /// <param name="KeyEvent">True for KeyUp False for KeyDown</param>
        /// <author>Inès</author>
        /// <co-author>Charif</co-autor>
        private void AnimationJoueur()
        {
            if (goLeft)
                ChangeSprite(ArrayGauche[counter]);
            if (goRight)
                ChangeSprite(ArrayDroite[counter]);
            if (goUp)
                ChangeSprite("Personnage/dos.png");
            if (goDown)
                ChangeSprite("Personnage/face.png");
            if (goDown && goLeft)
                ChangeSprite("Personnage/diagoSQ.png");
            if (goUp && goLeft)
                ChangeSprite("Personnage/diagoZQ.png");
            if (goDown && goRight)
                ChangeSprite("Personnage/diagoSD.png");
            if (goUp && goRight)
                ChangeSprite("Personnage/diagoZD.png");
         
            counter++;

            if (counter == 3)
            {
                counter = 0;
            }

        }
        #endregion

        #region Collision
        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other">the other object</param>
        /// <author>Charif</author>
        public override void CollideEffect(GameItem other)
        {
            if (other.TypeName == "Enemie")
            {
                PlaySound("hurt.mp3");
            }
        }

        /// <summary>
        /// Tells if the object touch the other object
        /// </summary>
        /// <param name="other">the other object</param>
        /// <returns>true if this collide with other</returns>
        /// <author>Charif</author>
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
        /// <author>Inès</author>
        /// <co-author>Charif</co-author>
        public void KeyDown(Key key)
        {
           
            switch (key)
            {
                case Key.Z:
                case Key.Up:
                    goUp = true;
                    break;

                case Key.Q:
                case Key.Left:
                    goLeft = true;
                    break;

                case Key.S:
                case Key.Down:
                    goDown = true;
                    break;

                case Key.D:
                case Key.Right:
                    goRight = true;
                    break;
            }
            
        }

        /// <summary>
        /// when the button is not pressed anymore
        /// </summary>
        /// <param name="key">key not pressed anymore</param>
        /// <author>Inès</author>
        /// <co-author>Charif</co-author>
        public void KeyUp(Key key)
        {
     
            switch (key)
            {
                case Key.Z:
                case Key.Up:
                    goUp = false;
                    break;

                case Key.Q:
                case Key.Left:
                    goLeft = false;
                    break;

                case Key.S:
                case Key.Down:
                    goDown = false;
                    break;

                case Key.D:
                case Key.Right:
                    goRight = false;
                    break;
            }
            
        }

        #endregion

        #region Mouvement

        /// <summary>
        /// move the player
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <author>Charif</author>
        public void DeplacerJoueur(double x, double y)
        {

            if (this.Left + x >= 0 && this.Right + x <= GameWidth && this.Bottom + y <= GameHeight && this.Top + y >= 0)
            {
                MoveXY(x, y);
            }

            _posX = this.Left + (this.Width) / 2;
            _posY = this.Top + (this.Height) / 2;

            Camera.MoveCamera(_posX, _posY);     
        }

        public void StopMove()
        {
            goDown = false;
            goLeft = false;
            goRight = false;
            goUp = false;
        }

        #endregion
    }
}
