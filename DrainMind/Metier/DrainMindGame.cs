using DrainMind.Metier;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DrainMind
{
    class DrainMindGame : IUTGame.Game
    {
        private ScrollViewer Camera;
        public DrainMindGame(Canvas canvas,ScrollViewer camera) : base(canvas,"Sprites","Sounds")
        {
            Camera = camera;
        }
        protected override void InitItems()
        {
            //Creation de la map
            Camera cam = new Camera(500, 0, Canvas, this, Camera);
            AddItem(cam);

            //Creation du joueur
            double y = this.Canvas.ActualHeight;
            double x = this.Canvas.ActualWidth;         
            Joueur player = new Joueur(800, 400, Canvas, this, cam);
            AddItem(player);
 
            AddItem(new GenerateurEnemie(this,Canvas));
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
