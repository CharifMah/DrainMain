using DrainMind.metier.Grille;
using DrainMind.Metier.Game;
using DrainMind.View;
using IUTGame;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace DrainMind.Metier.Items
{
    public class XpBonus : IUTGame.GameItem, IAnimable
    {
        public XpBonus(double x, double y) : base(x, y, DrainMindView.MainCanvas, DrainMindGame.Get(), "level.png")
        {
            
        }

        public override string TypeName => "DoubleXpItem";

        public override void CollideEffect(GameItem other)
        {
            if (other.TypeName == "Joueur")
            {
                DoubleXp(10000, "X2", Brushes.Yellow);
                this.Collidable = false;
                this.Dispose();
                DrainMindGame.Get().RemoveItem(this);             
            }
        }

        public async void DoubleXp(int delaymilisecond, string txt, Brush couleur)
        {
            Label t = new Label();
            t.Content = txt;
            t.FontSize = 30;
            t.Foreground = couleur;

            DrainMind.View.DrainMindView.UIcanvas.Children.Add(MyGrid.PutSpriteInCase(new Coordonnees(29, 16), t));

            DrainMindGame.Get().Joueur.Stats.Xpmult = 5;
            await Task.Delay(delaymilisecond);
            DrainMindGame.Get().Joueur.Stats.Xpmult = 1;
           
            DrainMind.View.DrainMindView.UIcanvas.Children.Remove(MyGrid.PutSpriteInCase(new Coordonnees(29, 16), t));
        }

        /// <summary>
        /// Animate the item
        /// </summary>
        /// <param name="dt">time span</param>
        /// <author>Ryan</author>
        public void Animate(TimeSpan dt)
        {
            if (Settings.Get().GameIsRunning)
            {
                PreventCollide();
            }
            else
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// Prevent Collision between the item and the player
        /// </summary>
        /// <author>Ryan</author>
        public void PreventCollide()
        {
            double PlayerPosX = DrainMindGame.Get().Joueur.PosX;
            double PlayerPosY = DrainMindGame.Get().Joueur.PosY;
            double ePosX = this.Left + (this.Width / 2);
            double ePosY = this.Top + (this.Height / 2);

            double result = Math.Sqrt(Math.Pow(PlayerPosX - ePosX, 2) + Math.Pow(PlayerPosY - ePosY, 2));

            if (result < 50)
            {
                CollideEffect(DrainMindGame.Get().Joueur);
            }
        }
    }
}
