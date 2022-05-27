using DrainMind.Metier.enemie;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrainMind.metier.enemie
{
    public class EnemieBoss : EnemieBase
    {
        public EnemieBoss(double x, double y, string spritename = "Enemie/boss.png") : base(x, y, spritename)
        {
            this._speed = 13;
            this._XPpoint = 100;
            this._damage = 3;
            this._soundKill = "LooseSound.mp3";
            this._traverseEnemie = true;
            _minspeed = _speed;
            _maxspeed = _speed * 4;
        }
    }
}
