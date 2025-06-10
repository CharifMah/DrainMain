using DrainMind.metier.enemie;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrainMind.Metier.enemie
{
    /// <summary>
    /// Boss of the game
    /// </summary>
    public class EnemieBossRapide : EnemieBase
    {
        /// <summary>
        /// Constructor of the boss
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <param name="spritename">name of sprite</param>
        public EnemieBossRapide(double x, double y, string spritename = "Enemie/boss.png") : base(x, y, spritename)
        {
            this._speed = 20;
            this._XPpoint = 100;
            this._damage = 1;
            this._life = 1;
            this._maxlife = 1;
            this._soundHit = "Hit5.mp3";
            this._traverseEnemie = true;
            this._minspeed = _speed;
            this._maxspeed = _speed * 4;
            this._typeenemie = TypeEnemie.bossRapide;


        }
    }
}
