﻿using DrainMind.metier.Grille;
using DrainMind.metier.Items;
using DrainMind.metier.joueur;
using DrainMind.metier.weapon;
using DrainMind.Metier.enemie;
using DrainMind.Metier.joueur;
using DrainMind.View;
using IUTGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DrainMind.Metier.Game
{
    /// <summary>
    /// Game : drainMind
    /// </summary>
    public class DrainMindGame : IUTGame.Game
    {
        private static DrainMindGame _instance;
        private Joueur _joueur;
        private GenerateurEnemie _generateurEnemie;
        private GenerateurItem _generateurItem;
        private WeaponBase _weapon;


        #region Property

        public Joueur Joueur
        { get { return _joueur; } }

        public GenerateurEnemie generateurEnemie
        {
            get { return _generateurEnemie; }
        }

        #endregion

        public static DrainMindGame Get()
        {
            if (_instance == null)
                _instance = new DrainMindGame();
            return _instance;
        }

        /// <summary>
        /// constructor of the game
        /// </summary>
        /// <Author>Charif</Author>
        private DrainMindGame() : base(DrainMindView.MainCanvas, "Sprites", "Sounds")
        {
         
        }

        /// <summary>
        /// Init the first items of the game
        /// </summary>
        /// <Author>Charif</Author>
        protected override void InitItems()
        {
            //Creation du joueur
            if (_joueur != null)
            {
                _joueur.Dispose();
                RemoveItem(_joueur);
            }

            _joueur = new Joueur(500, 500);
            AddItem(_joueur);

            //Création armes
            if (_weapon != null)
            {
                _weapon.Dispose();
                RemoveItem(_weapon);
            }

            _weapon = new WeaponBase();
            AddItem(_weapon);

            //Création générateur ennemie
            if (_generateurEnemie != null)
            {
                _generateurEnemie.Dispose();
                RemoveItem(_generateurEnemie);
            }

            _generateurEnemie = new GenerateurEnemie();
            AddItem(_generateurEnemie);

            //Création générateur item
            if (_generateurItem != null)
            {
                _generateurItem.Dispose();
                RemoveItem(_generateurItem);
            }

            _generateurItem = new GenerateurItem();
            AddItem(_generateurItem);

            //Selection musique
            selectMusic();
        }

        /// <summary>
        /// Select a random background music
        /// </summary>
        public void selectMusic()
        {

            Random r = new Random();
            int selectMusic = r.Next(1, 3);

            switch (selectMusic)
            {
                case 1:
                    PlayBackgroundMusic("Son_ambiance_Action.mp3");
                    break;

                case 2:
                    PlayBackgroundMusic("Angello.mp3");
                    break;

                case 3:
                    PlayBackgroundMusic("Gouttes.mp3");
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// What to do when the player looses the game
        /// </summary>
        /// <Author>Charif</Author>
        protected override void RunWhenLoose()
        {
            Settings.Get().GameIsRunning = false;
            MessageBox.Show(Res.Strings.Perdu);
            //Affecte le contenu de la mainwindow actuel a un nouveau menu principal
            (Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow).Content = new MenuPrincipale();
            PlayBackgroundMusic("LooseSound.mp3");
            _generateurEnemie.statsEnemies.DestroyAllEnnemies();
            StopBackgroundMusic();
        }

        /// <summary>
        /// What to do when the player wins the game
        /// </summary>
        protected override void RunWhenWin()
        {
            MessageBox.Show(Res.Strings.Gagne);
        }    
    }
}
