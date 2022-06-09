using DrainMind.metier.Grille;
using DrainMind.Metier.Game;
using DrainMind.View;
using IUTGame;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace DrainMind.Metier.Items
{
    public class XpBonus : IUTGame.GameItem
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

            DrainMind.View.DrainMindView.UIcanvas.Children.Add(MyGrid.PutSpriteInCase(29, 16, t));

            DrainMindGame.Get().Joueur.Stats.Xpmult = 5;
            await Task.Delay(delaymilisecond);
            DrainMindGame.Get().Joueur.Stats.Xpmult = 1;

            DrainMind.View.DrainMindView.UIcanvas.Children.Remove(MyGrid.PutSpriteInCase(29, 16, t));
        }
    }
}
