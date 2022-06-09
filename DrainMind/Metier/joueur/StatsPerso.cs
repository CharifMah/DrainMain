using DrainMind.metier.Grille;
using DrainMind.metier.joueur;
using DrainMind.metier.weapon;
using DrainMind.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DrainMind.Metier.joueur
{
    /// <summary>
    /// Classe des niveau du personnage la partie actuel
    /// </summary>
    public class StatsPerso : observable.Observable
    {
        private double _speed;
        private double _xp;
        private double _xpMax;
        private int _niveau;
        private double _Xpmult;
        private Vie _vie;

        #region Property

        /// <summary>
        /// Vitesse du Personnage
        /// </summary>
        public double Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Defini le multiplicateur XP
        /// </summary>
        public double Xpmult
        {
            get { return _Xpmult; }
            set
            {
                _Xpmult = value;
                if (_Xpmult <= 0)
                {
                    _Xpmult = 1;
                }
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
                _xp += value * _Xpmult;

                if (_xp >= _xpMax)
                {
                    _niveau += 1;
                    _xp -= _xpMax;
                    _xpMax *= 1.5;
                    DrainMindView.ShowUpgradeGrpBox();
                    NotifyPropertyChanged("XPMax");
                    NotifyPropertyChanged("Niveau");
                }
                NotifyPropertyChanged();
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



        /// <summary>
        /// Get actual life
        /// </summary>
        public Vie Life
        {
            get { return _vie; }
        }

        #endregion

        /// <summary>
        /// Cree le model qui permet de stocke les stats du personnage et dans le communique a IHM
        /// </summary>
        public StatsPerso(int speed, int life, int maxlife)
        {
            _speed = speed;
            _vie = new Vie(life, maxlife);
            _xp = 0.0;
            _niveau = 1;
            _Xpmult = 1.0;
            _xpMax = 100;
        }

    }
}
