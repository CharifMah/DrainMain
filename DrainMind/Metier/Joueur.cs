using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using IUTGame;
namespace DrainMind
{
    class Joueur : GameItem, IAnimable, IKeyboardInteract
    {        
        private bool compte = false;
        private double time = 0;

        public Joueur(double x, double y, Canvas c, Game g):base(x,y,c,g,"Joueur.png")
        {

        }

        public override string TypeName => "Joueur";

        public void Animate(TimeSpan dt)
        {
            if(compte)
            {
                time += dt.TotalMilliseconds;
                if (time > 500)
                    compte = false;
            }
        }

        public override void CollideEffect(GameItem other)
        {
            if (!compte)
            {
                if (other.TypeName == "Superballe")
                {
                    
                }
                else
                {

                }
            }
        }

        public void KeyDown(Key key)
        {
            switch(key)
            {
                case Key.Q:
                    MoveXY(-10, 0);break;
                case Key.D:
                    MoveXY(10, 0);break;
                case Key.S:
                    MoveXY(0, 10); break;
                case Key.Z:
                    MoveXY(0, -10); break;
            }
        }

        public void KeyUp(Key key)
        {
            
        }
    }
}
