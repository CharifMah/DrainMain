using DrainMind.metier.Grille;
using DrainMind.metier.joueur;
using DrainMind.Metier;
using DrainMind.Metier.enemie;
using DrainMind.Metier.joueur;
using DrainMind.View;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DrainMind.Metier
{
    /// <summary>
    /// Game : drainMind
    /// </summary>
    public class DrainMindGame : IUTGame.Game
    {  
        private static DrainMindGame game;
        public static DrainMindGame Instance
        {
            get { return game; }
            set { game = value; }
        }

        /// <summary>
        /// constructor of the game
        /// </summary>
        /// <param name="canvas">game's canvas</param>
        /// <param name="camera">camera which follow the player</param>
        /// <param name="UI">canvas</param>
        /// <Author>Charif</Author>
        public DrainMindGame() : base(DrainMindView.MainCanvas,"Sprites","Sounds")
        {    
            game = this;  
        }

        /// <summary>
        /// Init the first items of the game
        /// </summary>
        /// <Author>Charif</Author>
        protected override void InitItems()
        {
            double Height = Application.Current.MainWindow.ActualHeight;
            double Width = Application.Current.MainWindow.ActualWidth;
            //Creation de la Camera
            Camera cam = new Camera(Width / 2, Height / 2);
            AddItem(cam);

            //Creation du joueur
            Joueur player = new Joueur(Width / 2, Height / 2, new Vie(DrainMindView.UIcanvas, 30, 30));
            AddItem(player);
 
            AddItem(new GenerateurEnemie());
            AddItem(new GenerateurItem());

            Random r = new Random();
            int selectMusic = r.Next(1,4);

            if (selectMusic == 1)
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
            System.Windows.MessageBox.Show(Res.Strings.Perdu);
            //Affecte le contenu de la mainwindow actuel a un nouveau menu principal
            (Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow).Content = new MenuPrincipale();
            PlayBackgroundMusic("LooseSound.mp3");
        }

        /// <summary>
        /// What to do when the player wins the game
        /// </summary>
        protected override void RunWhenWin()
        {
           System.Windows.MessageBox.Show(Res.Strings.Gagne);
        }
    }
}
