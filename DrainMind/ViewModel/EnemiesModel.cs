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
        public EnemieBase GetNearestEnemie()
        {
            double distance = Math.Sqrt((Math.Pow(StatsPersoModel.Instance.posX - _lesenemies[0].Left, 2) + Math.Pow(StatsPersoModel.Instance.posX - _lesenemies[0].Top, 2)));
            EnemieBase enemieBase = null;

            for (int i = 0; i < _lesenemies.Count; i++)
            {    
                if (distance >= Math.Sqrt((Math.Pow(StatsPersoModel.Instance.posX - _lesenemies[i].Left, 2) + Math.Pow(StatsPersoModel.Instance.posX - _lesenemies[i].Top, 2))))
                {
                    distance = Math.Sqrt((Math.Pow(StatsPersoModel.Instance.posX - _lesenemies[i].Left, 2) + Math.Pow(StatsPersoModel.Instance.posX - _lesenemies[i].Top, 2)));
                    enemieBase = _lesenemies[i];
                }
            }

            return enemieBase;
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
