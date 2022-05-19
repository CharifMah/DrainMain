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
    class Vie
    {
        private List<Sprite> ListLife = new List<Sprite>();
        private Canvas uiLife;

        /// <summary>
        /// Set Ajoute ou envleve de la vie
        /// </summary>
        /// <Author>Charif</Author>
        public double _Vie
        {
            get { return ListLife.Count; }
            set
            {
                if (ListLife.Count > value)
                {
                    RemoveLife(ListLife.Count - value);
                }

                if (ListLife.Count < value)
                {
                    AddLife(value - ListLife.Count);
                }
            }
        }

        /// <summary>
        /// Constructor Vie create une barre de vie with amount of lifes
        /// </summary>
        /// <param name="lifeUI">Canvas de la barre de vie</param>
        /// <param name="g">Game Instance</param>
        /// <param name="pointLife">Nombre de vie</param>
        /// <Author>Charif</Author>
        public Vie(Canvas lifeUI, double pointLife)
        {
            uiLife = lifeUI;
            InitLife(pointLife);
           
        }

        /// <summary>
        /// Initialise le nombre de vie dans la barre de vie
        /// </summary>
        /// <param name="vie">Nombre de vie</param>
        /// <Author>Charif</Author>
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
        /// <Author>Charif</Author>
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
        /// <Author>Charif</Author>
        public void AddLife(double numberOfLife)
        {
            for (int i = 0; i < numberOfLife; i++)
            {
                Sprite DemiCoeur = new Sprite(SpriteStore.Get(Path.Combine("Vie", "1.png")).Image);
                Sprite Coeur = new Sprite(SpriteStore.Get(Path.Combine("Vie", "2.png")).Image);

                ListLife.Add(Coeur);
                uiLife.Children.Add(ListLife[ListLife.Count - 1].Image);
                ListLife[ListLife.Count - 1].Put((ListLife.Count - 1) * 50, 0);
            }
        }
    }
}