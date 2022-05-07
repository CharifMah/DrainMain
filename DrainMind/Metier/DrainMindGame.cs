using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DrainMind
{
    class DrainMindGame : IUTGame.Game
    {
        public DrainMindGame(Canvas canvas):base(canvas,"Sprites","Sounds")
        {

        }
        protected override void InitItems()
        {
            double y = this.Canvas.ActualHeight - 60;
            double x = this.Canvas.ActualWidth / 2;
            Joueur j = new Joueur(x, y, Canvas, this);
            AddItem(j);
            AddItem(new GenerateurEnemie(this,Canvas));
            //PlayBackgroundMusic("music.mp3");
        }

        protected override void RunWhenLoose()
        {
            //System.Windows.MessageBox.Show(Res.Strings.Perdu);
        }

        protected override void RunWhenWin()
        {
           // System.Windows.MessageBox.Show(Res.Strings.Gagne);
        }
    }
}
