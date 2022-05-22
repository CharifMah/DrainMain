using DrainMind.metier.Grille;
using DrainMind.View.Map;
using IUTGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DrainMind
{
    public class Vie
    {
        private static Dictionary<FrameworkElement,Coordonnees> _listlife;
        private static Dictionary<FrameworkElement, Coordonnees> _listemptylife;
        private Canvas uiLife;
        private int vie;

        public static Dictionary<FrameworkElement, Coordonnees> ListLife
        {
            get { return _listlife; }
        }

        public static Dictionary<FrameworkElement, Coordonnees> ListEmptyLife
        {
            get { return _listemptylife; }
        }

        /// <summary>
        /// Set Ajoute ou envleve de la vie
        /// </summary>
        /// <Author>Charif</Author>
        public int _Vie
        {
            get { return _listlife.Count; }
            set
            {
                vie = value;
                if (_listlife.Count > value)
                {
                    RemoveLife(_listlife.Count  - value);
                }

                if (_listlife.Count < value)
                {
                    AddLife(value - _listlife.Count);
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
            _listlife = new Dictionary<FrameworkElement, Coordonnees>();
            _listemptylife = new Dictionary<FrameworkElement, Coordonnees>();
            MyGrid.NombreDeLigne = 20;
            MyGrid.NombreDeCollumn = 40;
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
                if (_listemptylife.Count - 1 != 0)
                {
                    uiLife.Children.Remove(_listemptylife.Keys.Last());
                    _listemptylife.Remove(_listemptylife.Keys.Last());
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
                Sprite EmptyCoeur = new Sprite(SpriteStore.Get(Path.Combine("Vie", "1.png")).Image);
                EmptyCoeur.Image.Width = 50;
                EmptyCoeur.Image.Height = 50;
               
                uiLife.Children.Add(MyGrid.PutSpriteInCase(_listemptylife.Count, 0, EmptyCoeur.Image));
                _listemptylife.Add(EmptyCoeur.Image, new Coordonnees(_listemptylife.Count,0));

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
                if (_listlife.Count - 1 != 0)
                {
                    uiLife.Children.Remove(_listlife.Keys.Last());
                    _listlife.Remove(_listlife.Keys.Last());
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
            if (_listlife.Count + numberOfLife > _listemptylife.Count)
            {
                numberOfLife = _listemptylife.Count - _listlife.Count;
            }
            for (int i = 0; i < numberOfLife; i++)
            {
                Sprite Coeur = new Sprite(SpriteStore.Get(Path.Combine("Vie", "2.png")).Image);
             
                uiLife.Children.Add(MyGrid.PutSpriteInCase(_listlife.Count, 0, Coeur.Image));
                _listlife.Add(Coeur.Image,new Coordonnees(_listlife.Count, 0));


            }
             
        }
    }
}