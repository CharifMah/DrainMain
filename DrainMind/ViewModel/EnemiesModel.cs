using DrainMind.Metier.joueur;
using DrainMind.View.Map;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrainMind.Metier.enemie
{
    public class EnemiesModel : observable.Observable
    {

        private int _nombreenemie;
        private Dictionary<Enemie,Coordonnees> _lesenemies;

        #region

        public int NombreEnemie
        {
            get { return _nombreenemie; }
            set
            {
                _nombreenemie = value;
                this.NotifyPropertyChanged();
            }
        }

        public Dictionary<Enemie,Coordonnees> Lesenemies
        { get { return _lesenemies; } }

        #endregion




        private EnemiesModel()
        {
            _nombreenemie = 0;
            _lesenemies = new Dictionary<Enemie, Coordonnees>();
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
