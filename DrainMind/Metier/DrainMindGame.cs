using DrainMind.Metier;
using DrainMind.View;
using System.Windows;
using System.Windows.Controls;

namespace DrainMind
{
    class DrainMindGame : IUTGame.Game
    {
        private ScrollViewer Camera;
        private Canvas UIcanvas;
        public DrainMindGame(Canvas canvas, ScrollViewer camera,Canvas UI) : base(canvas,"Sprites","Sounds")
        {
            Camera = camera;
            UIcanvas = UI;
        }
        protected override void InitItems()
        {
            //Creation de la Camera
            Camera cam = new Camera(0, 0, Canvas, this, Camera);
            AddItem(cam);

            double Height = Application.Current.MainWindow.ActualHeight;
            double Width = Application.Current.MainWindow.ActualWidth;
            //Creation du joueur       
            Joueur player = new Joueur(Width / 2, Height / 2, Canvas, this,UIcanvas);
            AddItem(player);
 
            AddItem(new GenerateurEnemie(this, Canvas));
            //PlayBackgroundMusic("music.mp3");
        }

        

        protected override void RunWhenLoose()
        {
            System.Windows.MessageBox.Show(Res.Strings.Perdu);
        }

        protected override void RunWhenWin()
        {
           System.Windows.MessageBox.Show(Res.Strings.Gagne);
        }
    }
}
