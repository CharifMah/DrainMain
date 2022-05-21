using DrainMind.Metier.joueur;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrainMind.Metier.enemie
{
    public class EnemieObservable : observable.Observable
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

        private EnemieObservable()
        {
            _nombreenemie = 0;
        }

        private static EnemieObservable Instance;

        public static EnemieObservable Get()
        {
            if (Instance == null)
                Instance = new EnemieObservable();
            return Instance;
        }
    }
}
