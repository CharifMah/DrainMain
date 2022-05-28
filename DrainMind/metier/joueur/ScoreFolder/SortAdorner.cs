using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace DrainMind.metier.joueur.ScoreFolder
{
    /// <summary>
    /// sorting score columns
    /// </summary>
    public class SortAdorner : Adorner
    {
        private static Geometry ascGeometry =
            Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

        private static Geometry descGeometry =
            Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

        /// <summary>
        /// Specifies the direction of the sorting
        /// </summary>
        public ListSortDirection Direction { get; private set; }

        /// <summary>
        /// Creates a Adorner to indicate sort direction. 
        /// </summary>
        /// <param name="element">element</param>
        /// <param name="dir">direction</param>
        public SortAdorner(UIElement element, ListSortDirection dir)
            : base(element)
        {
            Direction = dir;
        }

        /// <summary>
        /// Participates in rendering operations directed by the disposition system
        /// </summary>
        /// <param name="drawingContext">Drawing instructions for a specific element</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
                return;

            TranslateTransform transform = new TranslateTransform
                (
                    AdornedElement.RenderSize.Width - 15,
                    (AdornedElement.RenderSize.Height - 5) / 2
                );
            drawingContext.PushTransform(transform);

            Geometry geometry = ascGeometry;
            if (Direction == ListSortDirection.Descending)
                geometry = descGeometry;
            drawingContext.DrawGeometry(Brushes.Black, null, geometry);

            drawingContext.Pop();
        }
    }
}
