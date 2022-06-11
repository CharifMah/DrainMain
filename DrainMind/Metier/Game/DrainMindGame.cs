using DrainMind.metier.Grille;
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
            _joueur = new Joueur(500, 500);
            AddItem(_joueur);

            _weapon = new WeaponBase();
            AddItem(_weapon);

            _generateurEnemie = new GenerateurEnemie();
            AddItem(_generateurEnemie);

            _generateurItem = new GenerateurItem();
            AddItem(_generateurItem);

            selectMusic();
        }

        public void selectMusic()
        {

            Random r = new Random();
            int selectMusic = r.Next(1, 3);

            if (selectMusic <= 1)
                PlayBackgroundMusic("Son_ambiance_Action.mp3");
            if (selectMusic == 2)
                PlayBackgroundMusic("Angello.mp3");
            if (selectMusic >= 3)
                PlayBackgroundMusic("Gouttes.mp3");
        }

        /// <summary>
        /// What to do when the player looses the game
        /// </summary>
        /// <Author>Charif</Author>
        protected override void RunWhenLoose()
        {
            MessageBox.Show(Res.Strings.Perdu);
            //Affecte le contenu de la mainwindow actuel a un nouveau menu principal
            (Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow).Content = new MenuPrincipale();
            PlayBackgroundMusic("LooseSound.mp3");
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
