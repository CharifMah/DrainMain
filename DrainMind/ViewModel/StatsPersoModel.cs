using DrainMind.metier.joueur;
using DrainMind.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DrainMind.ViewModel
{
    /// <summary>
    /// Classe des niveau du personnage la partie actuel
    /// </summary>
    public class StatsPersoModel : observable.Observable
    {
        private double _speed;
        private double _xp;
        private double _xpMax;
        private int _niveau;
        private double _Xpmult;
        private double _posX;
        private double _posY;
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
        /// Position X du Joueur
        /// </summary>
        public double posX
        {
            get { return _posX; }
            set { _posX = value; }
        }

        /// <summary>
        /// Position Y du Joueur
        /// </summary>
        public double posY
        {
            get { return _posY; }
            set { _posY = value; }
        }

        /// <summary>
        /// Get actual life
        /// </summary>
        public Vie Life
        {
            get { return _vie; }
        }

        #endregion

        private static StatsPersoModel instance;

        //Donne acces aux stats du joueur
        public static StatsPersoModel Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Cree le model qui permet de stocke les stats du personnage et dans le communique a IHM
        /// </summary>
        /// <param name="Speed">Vitesse du joueur</param>
        /// <param name="Xp">Experience du joueur au lancement de la partie</param>
        /// <param name="niveau">niveau au lancemetn de la partie</param>
        /// <param name="CoefXp">multiplicateur xp quand lvl Up</param>
        public StatsPersoModel(double Speed, int life, int maxlife)
        {
            _speed = Speed;
            _xp = 0.0;
            _niveau = 1;
            _Xpmult = 1.0;
            _xpMax = 100;
            _vie = new Vie(life, maxlife);
            instance = this;
        }
    }
}
