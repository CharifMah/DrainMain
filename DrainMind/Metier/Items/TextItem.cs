using DrainMind.metier.joueur;
using IUTGame;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace DrainMind.metier.Items
{
    /// <summary>
    /// Text of the item
    /// </summary>
    public class TextItem
    {

        /// <summary>
        /// Create label et show it for some time
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public TextItem(double x, double y,string txt, Brush couleur)
        {
            PopLabel(x,y,txt, couleur);
        }

        /// <summary>
        /// Show the label for some time
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        private async void PopLabel(double x, double y,string txt,Brush couleur)
        {
            Label t = new Label();
            t.Content = txt;
            t.FontSize = 9;
            t.Foreground = couleur;

            Canvas.SetLeft(t, x);
            Canvas.SetTop(t, y);

            DrainMind.View.DrainMindView.MainCanvas.Children.Add(t);
            await Task.Delay(200);
            DrainMind.View.DrainMindView.MainCanvas.Children.Remove(t);
        }
    }
}
