using IUTGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrainMind.Metier.Joueur
{
    /// <summary>
    /// Classe des niveau du personnage la partie actuel
    /// </summary>
    public class Experience : observable.Observable
    {
        private double _xp;
        private double _xpMax = 1000;
        private int _niveau;

        #region Property
        /// <summary>
        /// Set Xp et lvl up si xp egal xpmax
        /// </summary>
        /// <Author>Charif</Author>
        public double XP
        {
            get { return _xp; }
            set 
            {
                _xp = value;
                if (_xp >= _xpMax)
                {
                    _niveau += 1;
                    _xp -= _xpMax;
                    _xpMax *= 1.5;                  
                }
                this.NotifyPropertyChanged();
                this.NotifyPropertyChanged("XPMax");
                this.NotifyPropertyChanged("Niveau");
            }
        }

        public double XPMax
        {
            get { return _xpMax; }
        }

        public int Niveau
        {
            get { return _niveau; }
        }

        #endregion

        private static Experience instance;
        public static Experience Instance
        {
            get { return instance; }
        }

        public Experience(double Xp, int niveau)
        {
            _xp = Xp;
            _niveau = niveau;
            instance = this;
        }
    }
}
