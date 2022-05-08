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
        private Canvas carte;
        private Canvas _playercanvas;

        public DrainMindGame(Canvas playerCanvas,Canvas canvas, ScrollViewer camera) : base(canvas,"Sprites","Sounds")
        {
            Camera = camera;
            _playercanvas = playerCanvas;
            carte = canvas;
        }
        protected override void InitItems()
        {
            //Creation de la Camera
            Camera cam = new Camera(0, 0, carte, this, Camera);
            AddItem(cam);

            //Creation du joueur       
            Joueur player = new Joueur(0, 0, _playercanvas, this);
            AddItem(player);
 
            AddItem(new GenerateurEnemie(this, carte));
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
