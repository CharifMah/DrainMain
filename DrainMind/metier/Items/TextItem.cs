using DrainMind.metier.joueur;
using IUTGame;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace DrainMind.metier.Items
{
    public class TextItem
    {
        private Label t;
        DispatcherTimer timer;

        /// <summary>
        /// Cree un label et l affiche pendant une certaine duree
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public TextItem(double x, double y)
        {
            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += timer_Tick;
            PopLabel(x,y);
        }

        /// <summary>
        /// Affiche un label pendant une certaine duree
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public void PopLabel(double x, double y)
        {
            t = new Label();
            t.Content = $"+{10 * StatsPersoModel.Instance.Xpmult}";
            t.FontSize = 9;
            t.Foreground = Brushes.Yellow;
            Canvas.SetLeft(t, x);
            Canvas.SetTop(t, y);
            DrainMind.View.DrainMindView.MainCanvas.Children.Add(t);
            timer.Start();
        }


        /// <summary>
        /// Refresh le Timer Toute les secondes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void timer_Tick(object sender, EventArgs e)
        {
            DrainMind.View.DrainMindView.MainCanvas.Children.Remove(t);
        }
    }
}
