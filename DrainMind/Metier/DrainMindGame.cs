using DrainMind.Metier;
using DrainMind.View;
using System.Windows;
using System.Windows.Controls;

namespace DrainMind
{
    /// <summary>
    /// Game : drainMind
    /// </summary>
    class DrainMindGame : IUTGame.Game
    {
        //camera which follow the player
        private ScrollViewer Camera;
        //game's canvas
        private Canvas UIcanvas;

        /// <summary>
        /// constructor of the game
        /// </summary>
        /// <param name="canvas">game's canvas</param>
        /// <param name="camera">camera which follow the player</param>
        /// <param name="UI">canvas</param>
        public DrainMindGame(Canvas canvas, ScrollViewer camera,Canvas UI) : base(canvas,"Sprites","Sounds")
        {
            Camera = camera;
            UIcanvas = UI;
        }

        /// <summary>
        /// Init the first items of the game
        /// </summary>
        protected override void InitItems()
        {
            double Height = Application.Current.MainWindow.ActualHeight;
            double Width = Application.Current.MainWindow.ActualWidth;

            //Creation de la Camera
            Camera cam = new Camera(Width / 2, Height / 2, Canvas, this, Camera);
            AddItem(cam);

            //Creation du joueur       
            Joueur player = new Joueur(Width / 2, Height / 2, Canvas, this,UIcanvas);
            AddItem(player);
 
            AddItem(new GenerateurEnemie(this, Canvas));
            //PlayBackgroundMusic("music.mp3");
        }

        /// <summary>
        /// What to do when the player looses the game
        /// </summary>
        protected override void RunWhenLoose()
        {
            System.Windows.MessageBox.Show(Res.Strings.Perdu);
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
