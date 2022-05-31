using DrainMind.metier.Grille;
using DrainMind.Metier.enemie;
using DrainMind.Metier.joueur;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrainMind.ViewModel
{
    public class EnemiesModel : observable.Observable
    {

        private int _nombreenemie;
        private List<EnemieBase> _lesenemies;

        #region

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

        public List<EnemieBase> Lesenemies
        { get { return _lesenemies; } }

        #endregion

        private EnemiesModel()
        {
            _nombreenemie = 0;
            _lesenemies = new List<EnemieBase>();
        }

        public static void Destroy()
        {
            if (Instance != null)
                Instance = null;
        }

        /// <summary>
        /// Recupere lenemie le plus proche
        /// </summary>
        /// <returns>EnemieBase le plus proche du joueur</returns>
        /// <Author>Charif</Author>
        public EnemieBase GetNearestEnemie()
        {
            double distance = CalculDistance(Joueur.PosX, _lesenemies[0].Left, Joueur.PosY, _lesenemies[0].Top);
            EnemieBase enemieBase = _lesenemies[0];

            for (int i = 0; i < _lesenemies.Count; i++)
            {    
                if (distance >= CalculDistance(Joueur.PosX, _lesenemies[i].Left, Joueur.PosY, _lesenemies[i].Top))
                {
                    distance = CalculDistance(Joueur.PosX, _lesenemies[i].Left, Joueur.PosY, _lesenemies[i].Top);
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
        private double CalculDistance(double x1, double x2 , double y1, double y2 )
        {
           return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        private static EnemiesModel Instance;

        public static EnemiesModel Get()
        {
            if (Instance == null)
                Instance = new EnemiesModel();
            return Instance;
        }
    }
}
