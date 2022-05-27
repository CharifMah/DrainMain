using DrainMind.Metier.enemie;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrainMind.metier.enemie
{
    public class EnemieFvert : EnemieBase
    {
        public EnemieFvert(double x, double y, string spritename = "fantomeVert.png") : base(x, y, spritename)
        {
            this._speed = 4;
        }
    }
}
