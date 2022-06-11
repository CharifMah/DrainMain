using DrainMind.metier.Grille;
using IUTGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DrainMind.metier.joueur
{
    /// <summary>
    /// Life's class of the player
    /// </summary>
    public class Vie
    {
        //List of the lives
        private static List<FrameworkElement> _listlife;
        //List of th empty lives
        private static List<FrameworkElement> _listemptylife;
        //Canvas of the lives
        private Canvas uiLife;
        //Life
        private int vie;
        //ligne where the lives are
        private int ligne = 0;



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
                    RemoveLife(_listlife.Count - value);
                }

                if (_listlife.Count < value)
                {
                    AddLife(value - _listlife.Count);
                }
            }
        }

        /// <summary>
        /// Constructor Vie create a life bar with amount of lifes
        /// </summary>
        /// <param name="lifeUI">Canvas life bar</param>
        /// <param name="g">Game Instance</param>
        /// <param name="pointLife">Nomber of life</param>
        /// <Author>Charif</Author>
        public Vie(int pointLife, int MaxPv)
        {
            uiLife = View.DrainMindView.UIcanvas;
            _listlife = new List<FrameworkElement>();
            _listemptylife = new List<FrameworkElement>();
            AddEmptyLife(MaxPv);
            AddLife(pointLife);
        }

        /// <summary>
        /// Remove number of vieMax
        /// </summary>
        /// <param name="numberOfEmptyLife">number of life max to remove</param>
        /// <Author>Charif</Author>
        public void RemoveEmptyLife(int numberOfEmptyLife)
        {
            for (int i = 0; i < numberOfEmptyLife; i++)
            {
                if (_listemptylife.Count - 1 != 0)
                {
                    uiLife.Children.Remove(_listemptylife.Last());
                    _listemptylife.Remove(_listemptylife.Last());
                }
            }
        }

        /// <summary>
        /// Add number of Vie Max
        /// </summary>
        /// <param name="numberOfEmptyLife">Nomber of VieMax to add</param>
        /// <author>Charif</author>
        public void AddEmptyLife(int numberOfEmptyLife)
        {
            for (int i = 0; i < numberOfEmptyLife; i++)
            {
                Sprite EmptyCoeur = new Sprite(SpriteStore.Get(Path.Combine("Vie", "1.png")).Image);
                EmptyCoeur.Image.Width = 50;
                EmptyCoeur.Image.Height = 50;

                if (_listemptylife.Count <= MyGrid.NombreDeCollumn && ligne == 0)
                {
                    uiLife.Children.Add(MyGrid.PutSpriteInCase(new Coordonnees(_listemptylife.Count, ligne), EmptyCoeur.Image));
                    _listemptylife.Add(EmptyCoeur.Image);
                }
            }
        }

        /// <summary>
        /// Remove life
        /// </summary>
        /// <param name="numberOfLife"></param>
        /// <Author>Charif</Author>
        public void RemoveLife(int numberOfLife)
        {
            for (int i = 0; i < numberOfLife; i++)
            {
                if (_listlife.Count - 1 != 0)
                {
                    uiLife.Children.Remove(_listlife.Last());
                    _listlife.Remove(_listlife.Last());
                }
            }
        }

        /// <summary>
        /// Add Life to the list of lives
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

                uiLife.Children.Add(MyGrid.PutSpriteInCase(new Coordonnees(_listlife.Count,0), Coeur.Image));

                _listlife.Add(Coeur.Image);
            }
        }
    }
}