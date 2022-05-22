using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DrainMind.metier.joueur
{
    /// <summary>
    /// Classe des niveau du personnage la partie actuel
    /// </summary>
    public class StatsPersoModel : observable.Observable
    {
        private double speed;
        private double _xp;
        private double _xpMax = 1000;
        private int _niveau;
        private static GroupBox _LvlUpGrpBox;

        #region Property

        /// <summary>
        /// Vitesse du Personnage
        /// </summary>
        public double Speed
        {
            get { return speed; }
            set 
            {
                speed = value;
                this.NotifyPropertyChanged();
            }
        }

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

        public static GroupBox LvlUpGrpBox
        {
            get { return _LvlUpGrpBox; }
            set 
            {
                _LvlUpGrpBox = value;
            }
        }

        /// <summary>
        /// Get XpMax du Joueur
        /// </summary>
        public double XPMax
        {
            get { return _xpMax; }
        }

        /// <summary>
        /// Recupère le niveau du joueur
        /// </summary>
        public int Niveau
        {
            get { return _niveau; }
        }

        #endregion

        private static StatsPersoModel instance;
        public static StatsPersoModel Instance
        {
            get { return instance; }
        }

        public StatsPersoModel(double Speed,double Xp, int niveau)
        {
            this.speed = Speed;
            this._xp = Xp;
            this._niveau = niveau;
           
            instance = this;
        }
    }
}
