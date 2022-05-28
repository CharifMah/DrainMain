using DrainMind.metier.joueur;
using DrainMind.View;
using IUTGame;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrainMind.metier.Grille
{
    /// <summary>
    /// grid of the game
    /// </summary>
    public class MyGrid
    {
        private static double hauteurligne;
        private static double largeurColonne;
        private static int nombreligne;
        private static int nombrecollumn;
        private static Canvas actualgrid;

        #region Property

        public static int NombreDeLigne
        {
            get { return nombreligne; }
            set { nombreligne = value; }
        }

        public static int NombreDeCollumn
        {
            get {  return nombrecollumn; }
            set { nombrecollumn = value; }
        }    

        public static Canvas Grid
        {
            get 
            { 
                return actualgrid;
            }
            set
            {
                actualgrid = value;
            }
        }

        #endregion

        /// <summary>
        /// Draw lines
        /// </summary>
        /// <param name="x1">beginning of the line on x axis</param>
        /// <param name="y1">beginning of the line on y axis</param>
        /// <param name="x2">ending of the line on x axis</param>
        /// <param name="y2">ending of the line on y axis</param>
        /// <author>Charif</author>
        private static Line drawGridLine(int x1, int y1, int x2, int y2)
        {
            Line l = new Line();

            l.X1 = x1;
            l.Y1 = y1;
            l.X2 = x2;
            l.Y2 = y2;

            l.Stroke = new SolidColorBrush(Colors.Black);

            return l;

        }

        /// <summary>
        /// Draw a visible line
        /// </summary>
        /// <param name="numberLigne">Grid's number of line</param>
        /// <param name="NumberCollumn">Grid's number of column</param>
        /// <author>Charif</author>
        public static Canvas drawGrid()
        {
            Canvas c = new Canvas();
            hauteurligne = MainWindow.GetMainWindow.ActualHeight / nombreligne;
            largeurColonne = MainWindow.GetMainWindow.ActualWidth / nombrecollumn;

            for (int i = 0; i < nombrecollumn; i++)
            {
                int x = (int)(i * largeurColonne);
                c.Children.Add(drawGridLine(x, 0, x, (int)MainWindow.GetMainWindow.ActualWidth)) ;
            }

            for (int i = 0; i < nombreligne; i++)
            {
                int y = (int)(i * hauteurligne);
                c.Children.Add(drawGridLine(0, y, (int)MainWindow.GetMainWindow.ActualWidth, y));
            }
            return c;
        }

        /// <summary>
        /// Draw sprite of the grid
        /// </summary>
        /// <param name="idCol">Number of life from the top</param>
        /// <param name="idCol">Number of column from left</param>
        /// <param name="width">Width of the sprite</param>
        /// <param name="height">Height of the sprite</param>
        /// <param name="sprite">Sprite to draw</param>
        /// <author>Charif</author>
        public static FrameworkElement PutSpriteInCase(int idCol, int idLigne, FrameworkElement sprite)
        {
            hauteurligne = MainWindow.GetMainWindow.ActualHeight / nombreligne;
            largeurColonne = MainWindow.GetMainWindow.ActualWidth / nombrecollumn;

            int x = (int)(idCol * largeurColonne);
            int y = (int)(idLigne * hauteurligne);
            if (sprite != null)
            {
                sprite.Width = (int)largeurColonne - 2;
                sprite.Height = (int)hauteurligne - 1;
                Canvas.SetLeft(sprite, x + 1);
                Canvas.SetTop(sprite, y + 1);            
            }
            return sprite;
        }

        /// <summary>
        /// Refresh sprite depending of the width of the window
        /// Rafraichi les sprite en fonction de la taile de la fenetre
        /// </summary>
        /// <param name="canvas">Canvas of added element</param>
        /// <author>Charif</author>
        public static void ResizeCanvas()
        {
            DrainMindView.UIcanvas.Children.Clear();

            foreach (KeyValuePair<FrameworkElement,Coordonnees> life in Vie.ListLife)
            {
                PutSpriteInCase(life.Value.Colonne,life.Value.Ligne, life.Key);
                DrainMindView.UIcanvas.Children.Add(life.Key);
            }

            foreach (KeyValuePair<FrameworkElement, Coordonnees> life_empty in Vie.ListEmptyLife)
            {
                PutSpriteInCase(life_empty.Value.Colonne, life_empty.Value.Ligne, life_empty.Key);
                DrainMindView.UIcanvas.Children.Add(life_empty.Key);
            }

            actualgrid = drawGrid();
        }
    }
}
