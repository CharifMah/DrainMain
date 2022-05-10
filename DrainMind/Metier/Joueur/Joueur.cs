using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using DrainMind.Metier;
using IUTGame;
namespace DrainMind
{
    /// <summary>
    /// 
    /// </summary>
    class Joueur : GameItem, IAnimable, IKeyboardInteract
    {        
        private bool compte = false;
        private double time = 0;
        private double speed = 50;
        private Vie vie;
        private TimeSpan Waiting = new TimeSpan(0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="c"></param>
        /// <param name="g"></param>
        /// <param name="ui"></param>
        public Joueur(double x, double y, Canvas c, Game g, Canvas ui) :base(x,y,c,g,"face.png")
        {
            vie = new Vie(ui, g);
            Game.AddItem(vie);


        }      

        /// <summary>
        /// 
        /// </summary>
        public override string TypeName => "Joueur";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
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
        /// 
        /// </summary>
        /// <param name="other"></param>
        public override void CollideEffect(GameItem other)
        {
            if (Waiting.TotalMilliseconds <= 0)
            {
                if (other.TypeName == "Enemie")
                {
                    vie._Vie += 0.5;
                    Waiting = new TimeSpan(0, 0, 0, 1);
                }
                else
                {

                }
               
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool IsCollide(GameItem other)
        {
            return base.IsCollide(other);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void DeplacerJoueur(double x, double y)
        {
            if (this.Left + x > -1 && this.Right + x < 6915 && this.Bottom + y < 4421 && this.Top + y > -1)
                MoveXY(x,y);
            Camera.X = this.Left;
            Camera.Y = this.Top;
            Camera.MoveCamera(this.Left + (this.Width) / 2,this.Top + (this.Height)/2 );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
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
        /// Permet lorsque la touche enclenchée est relachée de faire revenir le personnage de face
        /// </summary>
        /// <param name="key">Touche clavier presse par le joueur</param>
        public void KeyUp(Key key)
        {
            ChangeSprite("face.png");
        }


        /// <summary>
        /// Méthode animation, permet de changer le sprite du personnage pour aller à gauche, à droite et en haut
        /// </summary>
        /// <param name="key">Touche clavier presse par le joueur</param>
        public void AnimationJoueur(Key key)
        {
            if (key == Key.Q || key == Key.Left) { ChangeSprite("gauche.png"); }
            if (key == Key.D || key == Key.Right) { ChangeSprite("droite.png"); }
            if (key == Key.Z || key == Key.Up) { ChangeSprite("dos.png"); }
        }
    }
}
