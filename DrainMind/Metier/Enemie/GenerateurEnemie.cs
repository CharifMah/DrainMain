using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using DrainMind.metier.enemie;
using DrainMind.metier.joueur;
using DrainMind.Metier.joueur;
using DrainMind.View;
using DrainMind.ViewModel;
using IUTGame;

namespace DrainMind.Metier.enemie
{
    /// <summary>
    /// generate the enemies
    /// </summary>
    /// <Author>Charif</Author>
    public class GenerateurEnemie : GameItem
    {
        //Time
        private DateTime _timer;
        //Minuteur
        private static DispatcherTimer timer;

        /// <summary>
        /// Timer de creation 
        /// </summary>
        public static DispatcherTimer GeneratorTimer
        {
            get { return timer; }
        }



        /// <summary>
        /// enemies's constructor 
        /// </summary>
        /// <param name="g">drainMind</param>
        /// <param name="c">canvas</param>
        public GenerateurEnemie(): base(0,0,DrainMindView.MainCanvas,DrainMindGame.Instance)
        {       
            //Minuteur
            _timer = new DateTime(0);
            timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            timer.Tick += CreateEnemieWave;
        }


        //Type name of the generator is "generateur"
        public override string TypeName => "generateur";

        /// <summary>
        /// Create enemy depending of timer
        /// </summary>
        /// <param name="sender">timer.Tick</param>
        /// <param name="e">event timer</param>
        /// <Author>Charif</Author>
        internal void CreateEnemieWave(object sender, EventArgs e)
        {
            if (DrainMindGame.Instance != null)
            {
                _timer = _timer.AddMilliseconds(100);

                if (100 == _timer.TimeOfDay.TotalMilliseconds)
                {
                    CreateEnemie(TypeEnemie.fantome, 160, 100);
                    CreateEnemie(TypeEnemie.fantomevert, 20, 100);
                }

                if (10000 == _timer.TimeOfDay.TotalMilliseconds)
                {
                    CreateEnemie(TypeEnemie.zebre, 10, 300);
                    CreateEnemie(TypeEnemie.gloom, 70, 1000);
                }

                if (35000 == _timer.TimeOfDay.TotalMilliseconds)
                {
                    CreateEnemie(TypeEnemie.boss, 5, 100);
                }

                if (90000 == _timer.TimeOfDay.TotalMilliseconds)
                {
                    CreateEnemie(TypeEnemie.zebre, 50, 500);
                    CreateEnemie(TypeEnemie.gloom, 100, 1000);
                }

                if (130000 == _timer.TimeOfDay.TotalMilliseconds)
                {
                    CreateEnemie(TypeEnemie.boss, 30, 1500);
                }
            }
            else
            {
                this.Dispose();
                timer.Stop();
                timer = null;
            }
           
        }

        /// <summary>
        /// Create enemy at a random position
        /// </summary>
        /// <param name="typeEnemie">type of enemy</param>
        /// <param name="number">number of enemy</param>
        /// <param name="delaymilisecond">time between creation</param>
        /// <Author>Charif</Author>
        private static async void CreateEnemie(TypeEnemie typeEnemie,int number,int delaymilisecond)
        {
            Random r = new Random();
            EnemieBase n = null;
            for (int i = 0; i < number; i++)
            {
                double x = r.NextDouble() * DrainMindView.MainCanvas.Width;
                double y = r.NextDouble() * DrainMindView.MainCanvas.Height;
                if (DrainMindGame.Instance != null)
                {
                    switch (typeEnemie)
                    {
                        case TypeEnemie.fantomevert:
                            n = new EnemieFvert(x, y);
                            break;
                        case TypeEnemie.boss:
                            n = new EnemieBoss(x, y);                     
                            break;
                        case TypeEnemie.gloom:
                            n = new EnemieGloom(x, y);
                            break;
                        case TypeEnemie.zebre:
                            n = new EnemieNightmare(x, y);
                            break;
                        default: n = new EnemieBase(x, y);
                            break;                  
                    }
                    if (n != null)
                    {
                        DrainMindGame.Instance.AddItem(n);
                        EnemiesModel.Get().NombreEnemie++;
                        EnemiesModel.Get().Lesenemies.Add(n);
                    }
            
                    //Delay de delaymilisecond ms entre chaque spawn
                    await Task.Delay(delaymilisecond);
                }                                  
            }                     
        }

        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other">the other object</param>
        /// <Author>Charif</Author>
        public override void CollideEffect(GameItem other) 
        {
            this.Collidable = false;
        }
    }
}
