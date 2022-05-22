using DrainMind.View.Map;
using IUTGame;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrainMind.metier.Grille
{
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
        /// Dessine les ligne
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
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
        /// Dessine une gille visible
        /// </summary>
        /// <param name="numberLigne">Nombre de ligne de la grille</param>
        /// <param name="NumberCollumn">Nombre de Collumn de la Grille</param>
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
        /// Dessine un sprite dans la case souhaite
        /// </summary>
        /// <param name="idCol">Nombre de la ligne en partant du haut</param>
        /// <param name="idCol">Nombre de la collumn en partant de la gauche</param>
        /// <param name="width">Largeur de sprite</param>
        /// <param name="height">Hauteur du sprite</param>
        /// <param name="sprite">le sprite a dessinee</param>
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
        /// Rafraichi les sprite en fonction de la taile de la fenetre
        /// </summary>
        /// <param name="canvas">Le canvas des element ajoute</param>
        public static void ResizeCanvas(ref Canvas ui)
        {
            ui.Children.Clear();

            foreach (KeyValuePair<FrameworkElement,Coordonnees> life in Vie.ListLife)
            {
                PutSpriteInCase(life.Value.Colonne,life.Value.Ligne, life.Key);
                ui.Children.Add(life.Key);
            }

            foreach (KeyValuePair<FrameworkElement, Coordonnees> life_empty in Vie.ListEmptyLife)
            {
                PutSpriteInCase(life_empty.Value.Colonne, life_empty.Value.Ligne, life_empty.Key);
                ui.Children.Add(life_empty.Key);
            }
            actualgrid = drawGrid();
        }
    }
}
