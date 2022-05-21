using DrainMind.Metier.joueur;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrainMind.Metier.enemie
{
    public class EnemiesModel : observable.Observable
    {
        private int _nombreenemie;
        
        public int NombreEnemie
        {
            get { return _nombreenemie; }
            set 
            {
                _nombreenemie = value;
                this.NotifyPropertyChanged();
            }
        }

        private EnemiesModel()
        {
            _nombreenemie = 0;
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
