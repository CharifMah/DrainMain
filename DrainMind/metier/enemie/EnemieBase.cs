using DrainMind.metier.enemie;
using DrainMind.metier.Grille;
using DrainMind.metier.Items;
using DrainMind.Metier.Game;
using DrainMind.Metier.Items;
using DrainMind.Metier.joueur;
using DrainMind.Metier.ScoreFolder;
using DrainMind.View;
using IUTGame;
using System;
using System.Windows.Controls;
using System.Windows.Media;

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
        protected int _maxlife;
        protected TypeEnemie _typeenemie;
        protected string _soundHit;
   
        public int maxlife
        {
            get { return _maxlife; }
        }
        public int life
        {
            get { return _life; }
        }

        /// <summary>
        /// ennemies constructor
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <param name="c">canvas</param>
        /// <param name="g">game</param>
        /// <param name="Spritename">name of the sprite</param>
        /// <Author>Charif</Author>
        public EnemieBase(double x, double y,string spritename = "Enemie/fantome.png") : base(x, y, DrainMindView.MainCanvas, DrainMindGame.Get(), spritename)
        {
            ChangeScale(0.7, 0.7);
            _life = 1;
            _maxlife = 1;
            _ePosX = x;
            _ePosY = y;
            _Iscollide = false;   
            _soundKill = "hurt.mp3";
            _XPpoint = 10;
            _damage = 1;
            _traverseEnemie = false;
            _speed = 5;
            _minspeed = 0;
            _maxspeed = 20;
            _typeenemie = TypeEnemie.fantome;
            _soundHit = "Hit1.mp3";
            DrainMindGame.Get().generateurEnemie.statsEnemies.NombreEnemie++;
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
                    int val = DrainMindGame.Get().Joueur.Stats.Life._Vie - (_damage * _life);

                    if (val > 0)
                    {
                        DrainMindGame.Get().Joueur.Stats.Life._Vie = val;
                    }
                    else
                    {
                        DrainMindGame.Get().Joueur.Stats.Life._Vie = 0;
                        Game.Loose();
                    }
                    Destroy();
                }
            }
            if (other.TypeName == "Enemie")
            {
                _Iscollide = true;
            }

            if (other.TypeName == "Ammunition")
            {
                if (other.Collidable)
                {
                    other.Collidable = false;
                    other.Dispose();

                    if (DrainMindGame.Get() != null)
                    DrainMindGame.Get().RemoveItem(other);

                    if (this.Collidable)
                    {
                        LooseLife(1);
                    }

                    new TextItem(other.Left,other.Top,$"-{1}",Brushes.Red);               
                }              
            }
        }

        /// <summary>
        /// Kill the enemy
        /// </summary>
        public void Destroy()
        {
            if (DrainMindGame.Get() != null)
            {
                ExpItem xp = new ExpItem(this.Left + (this.Width / 2), this.Top + (this.Height / 2),_XPpoint);
                DrainMindGame.Get().AddItem(xp);

                this.Dispose();
                this.Collidable = false;                        
            }

            PlaySound(_soundKill);

            Score.Get().EnemieKilled += 1;
            Score.Get().Point += _XPpoint;

            if (_typeenemie == TypeEnemie.boss)
            {
                Food food = new Food(this.Left, this.Top);
                Game.AddItem(food);
            }

            if (Settings.Get().GameIsRunning)
            {
                DrainMindGame.Get().generateurEnemie.statsEnemies.LesEnemies.Remove(this);
                DrainMindGame.Get().RemoveItem(this);
                DrainMindGame.Get().generateurEnemie.statsEnemies.NombreEnemie--;
            }
        }

        public void LooseLife(int dammage)
        {
            if (_life - dammage > 0)
            {
                _life -= dammage;
                PlaySound(_soundHit);
            }
            else
            {
                _life = 0;
                Destroy();
            }
        }

        /// <summary>
        /// Animate the item
        /// </summary>
        /// <param name="dt">timespan elasped since last animation</param>
        public void Animate(TimeSpan dt)
        {
            if (Collidable)
            {
                MoveEnemie();
            }           
        }

        /// <summary>
        /// Moving towards the player
        /// </summary>
        ///<Author>Charif</Author>
        public void MoveEnemie()
        {
            _ePosX = this.Left + (this.Width / 2);
            _ePosY = this.Top + (this.Height / 2);
            double _angle = 0;

            if (DrainMindGame.Get().Joueur != null)
                _angle = Math.Atan2(DrainMindGame.Get().Joueur.PosY - _ePosY, DrainMindGame.Get().Joueur.PosX - _ePosX) * (180 / Math.PI);

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
