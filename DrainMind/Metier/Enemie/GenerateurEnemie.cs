using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using DrainMind.metier.joueur;
using DrainMind.Metier.joueur;
using DrainMind.View;
using IUTGame;

namespace DrainMind.Metier.enemie
{
    /// <summary>
    /// generate the enemies
    /// </summary>
    public class GenerateurEnemie : GameItem
    {
        //Time
        private DateTime _timer;
        //Minuteur
        private DispatcherTimer timer;

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
            timer.Start();
        }
        ~GenerateurEnemie()
        {
            timer = null;
            this.Dispose();
        }

        //Type name of the generator is "generateur"
        public override string TypeName => "generateur";

        internal void CreateEnemieWave(object sender, EventArgs e)
        {
            _timer = _timer.AddMilliseconds(100);

            if (100 == _timer.TimeOfDay.TotalMilliseconds)
            {
                CreateEnemie("fantomeVert.png", 10, 300);
            }

            if (5000 == _timer.TimeOfDay.TotalMilliseconds)
            {
                CreateEnemie("fantomeVert.png", 6, 300);
                CreateEnemie("fantome.png", 6, 300);
            }

            if (10000 == _timer.TimeOfDay.TotalMilliseconds)
            {
                CreateEnemie("boss.png", 3, 300);
            }

            if (10000 == _timer.TimeOfDay.TotalMilliseconds)
            {
                CreateEnemie("Gloom.png", 500, 1000);
            }                    
        }

        private static async void CreateEnemie(string NameSprite,int number,int delaymilisecond)
        {
            Random r = new Random();
            for (int i = 0; i < number; i++)
            {

                double x = r.NextDouble() * DrainMindView.MainCanvas.Width;
                double y = r.NextDouble() * DrainMindView.MainCanvas.Height;
                if (DrainMindGame.Instance != null)
                {
                    Enemie enemie = new Enemie(x, y, NameSprite);
                    DrainMindGame.Instance.AddItem(enemie);

                    //Delay de delaymilisecond ms entre chaque spawn
                    await Task.Delay(delaymilisecond);
                }                                  
            }                     
        }

        /// <summary>
        /// Executes the effect of the collision
        /// </summary>
        /// <param name="other">the other object</param>
        public override void CollideEffect(GameItem other) 
        {
            this.Collidable = false;
        }
    }
}
