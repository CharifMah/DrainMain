using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DrainMind.metier.Grille
{
    public class MyGrid : Canvas
    {

        private double hauteurcqligne;
        private double largeurcqColonne;
        private bool _showgrid;


        #region Property

        public bool ShowGrid
        {
            get { return _showgrid; }
            set
            {
                _showgrid = value;
                if (_showgrid == true)
                {
                    drawGrid(10,10);
                }
                 
                if (_showgrid == false)
                    Clear();
            }
        }

        #endregion
       

        private void drawGridLine(int x1, int y1, int x2, int y2)
        {
            System.Windows.Shapes.Line l = new System.Windows.Shapes.Line();

            l.X1 = x1;
            l.Y1 = y1;
            l.X2 = x2;
            l.Y2 = y2;

            l.Stroke = new SolidColorBrush(Colors.Black);

            this.Children.Add(l);

        }

        private void drawGrid(int numberLigne, int NumberCollumn)
        {

            hauteurcqligne = MainWindow.GetMainWindow.ActualHeight / numberLigne;
            largeurcqColonne = MainWindow.GetMainWindow.ActualWidth / NumberCollumn;

            for (int i = 0; i < 10; i++)
            {
                int x = (int)(i * largeurcqColonne);
                drawGridLine(x, 0, x, (int)MainWindow.GetMainWindow.ActualWidth);
            }

            for (int i = 0; i < 10; i++)
            {
                int y = (int)(i * hauteurcqligne);
                drawGridLine(0, y, (int)MainWindow.GetMainWindow.ActualWidth, y);
            }          
        }

        private void drawRect(int X, int Y, int width, int height, Color color)
        {
            SolidColorBrush colorPen = new SolidColorBrush(color);
            System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle();
            rect.Stroke = colorPen;
            rect.Fill = colorPen;
            rect.Width = width - 2;
            rect.Height = height - 2;
            Canvas.SetLeft(rect, X + 1);
            Canvas.SetTop(rect, Y + 1);
            this.Children.Add(rect);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
           
            if (e.ButtonState == Mouse.MiddleButton)
            {
                ShowGrid = true;
            }

            if (e.ButtonState == Mouse.RightButton)
            {
                ShowGrid = false;
            }



            //Point pos = e.GetPosition(this);

            //int idLigne = (int)(pos.X / largeurcqColonne);
            //int idCol = (int)(pos.Y / hauteurcqligne);

            //int x = (int)(idLigne * largeurcqColonne);
            //int y = (int)(idCol * hauteurcqligne);

            //drawRect(x, y, (int)largeurcqColonne, (int)hauteurcqligne, Colors.Black);
        }

        public void Clear()
        {
            this.Children.Clear();
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
        }
    }
}
