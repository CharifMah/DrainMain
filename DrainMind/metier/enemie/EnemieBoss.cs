using DrainMind.Metier.enemie;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrainMind.metier.enemie
{
    /// <summary>
    /// Boss of the game
    /// </summary>
    public class EnemieBoss : EnemieBase
    {
        /// <summary>
        /// Constructor of the boss
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <param name="spritename">name of sprite</param>
        public EnemieBoss(double x, double y, string spritename = "Enemie/boss.png") : base(x, y, spritename)
        {
            this._speed = 13;
            this._XPpoint = 100;
            this._damage = 3;
            this._soundKill = "Hit5.mp3";
            this._traverseEnemie = true;
            _minspeed = _speed;
            _maxspeed = _speed * 4;
            _typeenemie = TypeEnemie.boss;
        }
    }
}
