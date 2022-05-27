using DrainMind.Metier.enemie;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrainMind.metier.enemie
{
    public class EnemieNightmare : EnemieBase
    {
        public EnemieNightmare(double x, double y, string spritename = "nightmare.png") : base(x, y, spritename)
        {
            this._speed = 7;
            this._XPpoint = 40;
            this._damage = 2;
        }
    }
}
