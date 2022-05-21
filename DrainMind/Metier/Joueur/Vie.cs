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
    public class Vie
    {
        private List<Sprite> ListLife = new List<Sprite>();
        private List<Sprite> ListEmptyLife = new List<Sprite>();
        private Canvas uiLife;
        private int vie;

        /// <summary>
        /// Set Ajoute ou envleve de la vie
        /// </summary>
        /// <Author>Charif</Author>
        public int _Vie
        {
            get { return ListLife.Count; }
            set
            {
                vie = value;
                if (ListLife.Count > value)
                {
                    RemoveLife(ListLife.Count  - value);
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
        public Vie(Canvas lifeUI, int pointLife, int MaxPv)
        {
            uiLife = lifeUI;
            AddEmptyLife(MaxPv);
            AddLife(pointLife);        
        }

        /// <summary>
        /// Enleve le nombre de vieMax
        /// </summary>
        /// <param name="numberOfEmptyLife">Nombre de vie Max a envelver</param>
        /// <Author>Charif</Author>
        public void RemoveEmptyLife(int numberOfEmptyLife)
        {
            for (int i = 0; i < numberOfEmptyLife; i++)
            {
                if (ListEmptyLife.Count - 1 != 0)
                {
                    uiLife.Children.Remove(ListEmptyLife[ListEmptyLife.Count - 1].Image);
                    ListEmptyLife.Remove(ListEmptyLife[ListEmptyLife.Count - 1]);
                }
            }
        }

        /// <summary>
        /// Ajoute le nombre de Vie Max
        /// </summary>
        /// <param name="numberOfEmptyLife">Nombre de Vide max a Ajoute</param>
        public void AddEmptyLife(int numberOfEmptyLife)
        {
            for (int i = 0; i < numberOfEmptyLife; i++)
            {
                double width = Application.Current.MainWindow.Width;

                Sprite EmptyCoeur = new Sprite(SpriteStore.Get(Path.Combine("Vie", "1.png")).Image);
                ListEmptyLife.Add(EmptyCoeur);
                uiLife.Children.Add(ListEmptyLife[ListEmptyLife.Count - 1].Image);
                if (ListEmptyLife.Count * 50 < width)
                {
                    ListEmptyLife[ListEmptyLife.Count - 1].Put((ListEmptyLife.Count - 1) * 50, 0);
                }
                if (ListEmptyLife.Count * 50 > width)
                {
                    ListEmptyLife[ListEmptyLife.Count - 1].Put((ListEmptyLife.Count * 50) - width, 40);
                }
            }
        }

        /// <summary>
        /// Enleve le nombre de vie souhaite
        /// </summary>
        /// <param name="numberOfLife"></param>
        /// <Author>Charif</Author>
        public void RemoveLife(int numberOfLife)
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
        public void AddLife(int numberOfLife)
        {
            if (ListLife.Count + numberOfLife > ListEmptyLife.Count)
            {
                numberOfLife = ListEmptyLife.Count - ListLife.Count;
            }
            for (int i = 0; i < numberOfLife; i++)
            {
                double width = Application.Current.MainWindow.Width;

                Sprite Coeur = new Sprite(SpriteStore.Get(Path.Combine("Vie", "2.png")).Image);
                ListLife.Add(Coeur);
                uiLife.Children.Add(ListLife[ListLife.Count - 1].Image);
                if (ListLife.Count * 50 < width)
                {
                    ListLife[ListLife.Count - 1].Put((ListLife.Count - 1) * 50, 0);
                }
                if (ListLife.Count * 50 > width)
                {
                    ListLife[ListLife.Count - 1].Put((ListLife.Count * 50) - width , 40);
                }
            }
        }
    }
}