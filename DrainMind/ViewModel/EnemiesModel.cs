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


        private static EnemiesModel Instance;

        public static EnemiesModel Get()
        {
            if (Instance == null)
                Instance = new EnemiesModel();
            return Instance;
        }
    }
}
