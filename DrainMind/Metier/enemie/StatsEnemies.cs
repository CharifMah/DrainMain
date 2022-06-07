using DrainMind.Metier.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrainMind.Metier.enemie
{
    public class StatsEnemies : observable.Observable
    {
        private List<EnemieBase> _lesenemies;
        private int _nombreenemie;

        public List<EnemieBase> LesEnemies
        {
            get { return _lesenemies; }
        }

        public int NombreEnemie
        {
            get { return _nombreenemie; }
            set
            {
                _nombreenemie = value;
                if (_nombreenemie < 0)
                {
                    _nombreenemie = 0;
                }
                NotifyPropertyChanged();
            }
        }




        /// <summary>
        /// Cree le model qui permet de stocke les stats du personnage et dans le communique a IHM
        /// </summary>
        public StatsEnemies()
        {
            _nombreenemie = 0;
            _lesenemies = new List<EnemieBase>();
        }

        /// <summary>
        /// Recupere lenemie le plus proche
        /// </summary>
        /// <returns>EnemieBase le plus proche du joueur</returns>
        /// <Author>Charif</Author>
        public EnemieBase GetNearestEnemie()
        {
            double distance = CalculDistance(DrainMindGame.Get().Joueur.PosX, _lesenemies[0].Left, DrainMindGame.Get().Joueur.PosY, _lesenemies[0].Top);
            EnemieBase enemieBase = _lesenemies[0];

            for (int i = 0; i < _lesenemies.Count; i++)
            {
                if (distance >= CalculDistance(DrainMindGame.Get().Joueur.PosX, _lesenemies[i].Left, DrainMindGame.Get().Joueur.PosY, _lesenemies[i].Top))
                {
                    distance = CalculDistance(DrainMindGame.Get().Joueur.PosX, _lesenemies[i].Left, DrainMindGame.Get().Joueur.PosY, _lesenemies[i].Top);
                    enemieBase = _lesenemies[i];
                }
            }

            return enemieBase;
        }

        /// <summary>
        /// Calcule la distance entre deux point
        /// </summary>
        /// <param name="x1">Position x1</param>
        /// <param name="x2">Position x2</param>
        /// <param name="y1">Position y1</param>
        /// <param name="y2">Position y2</param>
        /// <Author>Charif</Author>
        /// <returns></returns>
        public double CalculDistance(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

    }
}
