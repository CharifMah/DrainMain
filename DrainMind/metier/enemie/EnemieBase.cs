using DrainMind.metier.enemie;
using DrainMind.metier.Grille;
using DrainMind.metier.Items;
using DrainMind.metier.joueur.ScoreFolder;
using DrainMind.View;
using DrainMind.ViewModel;
using IUTGame;
using System;
using System.Windows.Controls;

namespace DrainMind.Metier.enemie
{
    /// <summary>
    /// ennemies of the game
    /// </summary>
    public class EnemieBase : GameItem, IAnimable
    {
        //time interval
        private TimeSpan _waiting = new TimeSpan(0);

        protected int _speed;
        protected string _soundKill;
        protected int _XPpoint;
        protected int _damage;
        protected bool _Iscollide;
        protected bool _traverseEnemie;
        protected int _minspeed;
        protected int _maxspeed;
        protected double _ePosX;
        protected double _ePosY;
        protected int _life;
        protected TypeEnemie _typeenemie;
        protected string _soundHit;

        /// <summary>
        /// ennemies constructor
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <param name="c">canvas</param>
        /// <param name="g">game</param>
        /// <param name="Spritename">name of the sprite</param>
        /// <Author>Charif</Author>
        public EnemieBase(double x, double y,string spritename = "Enemie/fantome.png") : base(x, y, DrainMindView.MainCanvas, DrainMindGame.Instance, spritename)
        {
            ChangeScale(0.7, 0.7);
            EnemiesModel.Get().NombreEnemie++;
            EnemiesModel.Get().Lesenemies.Add(this);
            _life = 1;
            _ePosX = x;
            _ePosY = y;
            _Iscollide = false;   
            _soundKill = "hurt.mp3";
            _XPpoint = 10;
            _damage = 1;
            _traverseEnemie = false;
            _speed = 3;
            _minspeed = 0;
            _maxspeed = _speed * 2;
            _typeenemie = TypeEnemie.fantome;
            _soundHit = "Hit1.mp3";
        }

        //TypeNme of ennemies is "Enemie"
        public override string TypeName => "Enemie";

        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other">the other object</param>
        /// <Author>Charif</Author>
        public override void CollideEffect(GameItem other)
        {
            if (other.TypeName == "Joueur")
            {
                if (this.Collidable)
                {
                    int val = StatsPersoModel.Instance.Life._Vie - _damage;

                    if (val > 0)
                    {
                        StatsPersoModel.Instance.Life._Vie = val;
                    }
                    else
                    {
                        StatsPersoModel.Instance.Life._Vie = 0;
                        Game.Loose();
                    }

                    if (_life > 0)
                    {
                        _life -= 1;
                        PlaySound(_soundHit);
                    }
                    else
                    {
                        _life = 0;
                        Destroy();
                    }               
                }
            }
            if (other.TypeName == "Enemie")
            {
                _Iscollide = true;

            }
        }

        /// <summary>
        /// Kill the enemy
        /// </summary>
        public void Destroy()
        {
            this.Dispose();
            this.Collidable = false;

            if (DrainMindGame.Instance != null)
            {
                ExpItem xp = new ExpItem(this.Left + (this.Width / 2), this.Top + (this.Height / 2));
                DrainMindGame.Instance.AddItem(xp);
            }

            PlaySound(_soundKill);

            Score.Get().EnemieKilled += 1;
            Score.Get().Point += _XPpoint;

            EnemiesModel.Get().NombreEnemie--;
            EnemiesModel.Get().Lesenemies.Remove(this);
        }

        /// <summary>
        /// Animate the item
        /// </summary>
        /// <param name="dt">timespan elasped since last animation</param>
        public void Animate(TimeSpan dt)
        {
            if (_waiting.TotalMilliseconds > 0)
            {
                _waiting = _waiting.Subtract(dt);
            }

            if (this.Top < 0)
            {
                Top = 0;
            }
            else if (Bottom > GameHeight)
            {
                Bottom = 0;
            }
            else if (Left < 0)
            {
                Left = 0;
            }
            else if (Right > GameWidth)
            {
                Right = 0;
            }

            MoveEnemie();
        }

        /// <summary>
        /// Moving towards the player
        /// </summary>
        ///<Author>Charif</Author>
        public void MoveEnemie()
        {
            _ePosX = this.Left + (this.Width / 2);
            _ePosY = this.Top + (this.Height / 2);

            double _angle = Math.Atan2(StatsPersoModel.Instance.posY - _ePosY, StatsPersoModel.Instance.posX - _ePosX) * (180 / Math.PI);

            if (!_Iscollide || _traverseEnemie)
            {
                MoveDA(_speed, _angle);
            }
            else
            {
                _Iscollide = false;
                Random r = new Random();

                //Reste sur place
                if (r.Next(1, 5) < 3)
                {
                    return;
                }

                int rdm = r.Next(1, 10);
                int angle = 0;

                switch (rdm)
                {
                    case 1:
                        angle = 0;
                        break;
                    case 2:
                        angle = 45;
                        break;
                    case 4:
                        angle = 90;
                        break;
                    case 5:
                        angle = 135;
                        break;
                    case 6:
                        angle = 180;
                        break;
                    case 7:
                        angle = 225;
                        break;
                    case 8:
                        angle = 270;
                        break;
                    case 9:
                        angle = 315;
                        break;

                    default:
                        break;
                }
   
                MoveDA(r.Next(_minspeed, _maxspeed), angle);
            }
        }
    }
}
