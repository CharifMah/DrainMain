using DrainMind.Metier.enemie;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrainMind.metier.enemie
{
    /// <summary>
    /// type of enemy
    /// </summary>
    public class EnemieNightmare : EnemieBase
    {
        /// <summary>
        /// Create enemy
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <param name="spritename">name of the sprite</param>
        public EnemieNightmare(double x, double y, string spritename = "Enemie/nightmare.png") : base(x, y, spritename)
        {
            this._speed = 7;
            this._XPpoint = 40;
            this._life = 2;
            this._damage = 2;
            _typeenemie = TypeEnemie.zebre;
            this._soundHit = "Hit4.mp3";
        }
    }
}
