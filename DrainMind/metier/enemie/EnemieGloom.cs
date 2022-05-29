using DrainMind.Metier.enemie;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrainMind.metier.enemie
{
    /// <summary>
    /// Type of enemy
    /// </summary>
    public class EnemieGloom : EnemieBase
    {
        /// <summary>
        /// Create the enemy
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <param name="spritename">name of the sprite</param>
        public EnemieGloom(double x, double y, string spritename = "Enemie/Gloom.png") : base(x, y, spritename)
        {
            this._speed = 7;
            this._XPpoint = 15;
            this._damage = 1;
            _typeenemie = TypeEnemie.gloom;
            this._soundHit = "Hit3.mp3";
        }
    }
}
