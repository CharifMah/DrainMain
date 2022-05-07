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

        public Joueur(double x, double y, Canvas c, Game g):base(x,y,c,g,"joueur.png")
        {

        }

        public override string TypeName => "joueur";

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
          
        }

        public void KeyDown(Key key)
        {
            switch(key)
            {
                case Key.Left:
                    MoveXY(-10, 0);break;
                case Key.Right:
                    MoveXY(10, 0);break;
            }
        }

        public void KeyUp(Key key)
        {
            
        }
    }
}
