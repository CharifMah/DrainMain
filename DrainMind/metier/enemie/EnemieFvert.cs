using DrainMind.Metier.enemie;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrainMind.metier.enemie
{
    /// <summary>
    /// Type of enemy
    /// </summary>
    public class EnemieFvert : EnemieBase
    {
        /// <summary>
        /// constructor of the enemy
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <param name="spritename">name of the sprite</param>
        public EnemieFvert(double x, double y, string spritename = "Enemie/fantomeVert.png") : base(x, y, spritename)
        {
            this._speed = 4;
            _typeenemie = TypeEnemie.fantomevert;
            this._soundKill = "Hit1.mp3";
        }
    }
}
