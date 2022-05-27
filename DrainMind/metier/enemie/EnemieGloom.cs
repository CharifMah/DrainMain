using DrainMind.Metier.enemie;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrainMind.metier.enemie
{
    public class EnemieGloom : EnemieBase
    {
        public EnemieGloom(double x, double y, string spritename = "Gloom.png") : base(x, y, spritename)
        {
            this._speed = 5;
            this._XPpoint = 15;
            this._damage = 1;
        }
    }
}
