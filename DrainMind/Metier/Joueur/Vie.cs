using DrainMind.metier.Grille;
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
        private static List<FrameworkElement> _listlife = new List<FrameworkElement>();
        private static List<FrameworkElement> _listemptylife = new List<FrameworkElement>();
        private Canvas uiLife;
        private int vie;

        public static List<FrameworkElement> ListLife
        {
            get { return _listlife; }
        }

        public static List<FrameworkElement> ListEmptyLife
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
            MyGrid.NombreDeLigne = 20;
            MyGrid.NombreDeCollumn = 40;
           lifeUI.Children.Add( MyGrid.drawGrid());
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
                    uiLife.Children.Remove(_listemptylife[_listemptylife.Count - 1]);
                    _listemptylife.Remove(_listemptylife[_listemptylife.Count - 1]);
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
                _listemptylife.Add(EmptyCoeur.Image);
                uiLife.Children.Add(MyGrid.PutSpriteInCase(_listemptylife.Count - 1, 0, EmptyCoeur.Image));
               
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
                    uiLife.Children.Remove(_listlife[_listlife.Count - 1]);
                    _listlife.Remove(_listlife[_listlife.Count - 1]);
                    MyGrid.ResizeCanvas(ref uiLife);
                    uiLife.Children.Add(MyGrid.drawGrid());
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
                _listlife.Add(Coeur.Image);
                uiLife.Children.Add(MyGrid.PutSpriteInCase(_listlife.Count - 1, 0, Coeur.Image));
                
                
            }
             
        }
    }
}