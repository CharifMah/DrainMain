using IUTGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DrainMind
{
    class Vie : IUTGame.GameItem
    {
        private List<Sprite> ListLife = new List<Sprite>();
        private Canvas uiLife;


        /// <summary>
        /// Constructor Vie create une barre de vie with amount of lifes
        /// </summary>
        /// <param name="lifeUI">Canvas de la barre de vie</param>
        /// <param name="g">Game Instance</param>
        /// <param name="pointLife">Nombre de vie</param>
        public Vie(Canvas lifeUI, Game g, double pointLife) : base(10, 10, lifeUI, g)
        {
            uiLife = lifeUI;
            InitLife(pointLife);
           
        }

        /// <summary>
        /// Initialise le nombre de vie dans la barre de vie
        /// </summary>
        /// <param name="vie">Nombre de vie</param>
        public void InitLife(double vie)
        {        
            for (int i = 0; i < vie; i++)
            {
                Sprite DemiCoeur = new Sprite(SpriteStore.Get(Path.Combine("Vie", "1.png")).Image);
                Sprite Coeur = new Sprite(SpriteStore.Get(Path.Combine("Vie", "2.png")).Image);

                ListLife.Add(Coeur);
                uiLife.Children.Add(ListLife[i].Image);
                ListLife[i].Put(i * 50, 0);
            }
        }
        /// <summary>
        /// Enleve le nombre de vie souhaite
        /// </summary>
        /// <param name="numberOfLife"></param>
        public void RemoveLife(double numberOfLife)
        {
            for (int i = 0; i < numberOfLife; i++)
            {
                if (ListLife.Count - 1 != 0)
                {
                    uiLife.Children.Remove(ListLife[ListLife.Count - 1].Image);
                    ListLife.Remove(ListLife[ListLife.Count - 1]);                    
                }            
            }               
        }
        /// <summary>
        /// Ajoute de la vie a la liste de vie
        /// </summary>
        /// <param name="numberOfLife">Number of lifes you want to add</param>
        public void AddLife(double numberOfLife)
        {
            for (int i = 0; i < numberOfLife; i++)
            {
                Sprite DemiCoeur = new Sprite(SpriteStore.Get(Path.Combine("Vie", "1.png")).Image);
                Sprite Coeur = new Sprite(SpriteStore.Get(Path.Combine("Vie", "2.png")).Image);

                ListLife.Add(Coeur);
                uiLife.Children.Add(ListLife[ListLife.Count - 1].Image);
                ListLife[ListLife.Count - 1].Put(i * 50, 0);
            }
        }
        /// <summary>
        /// Set Ajoute ou envleve de la vie
        /// </summary>
        public double _Vie
        {
            get { return ListLife.Count; }
            set
            {
                
                double vie = Math.Min(ListLife.Count, value);
                if (ListLife.Count - vie != 0)
                {
                    RemoveLife(ListLife.Count - vie);
                }
               
                
            }
        }



        public override string TypeName => "Vie";

        public override bool IsCollide(GameItem other)
        {
            return false;
        }
        public override void CollideEffect(GameItem other)
        {

        }


    }
}