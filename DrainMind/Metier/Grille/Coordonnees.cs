using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrainMind.metier.Grille
{
    /// <summary>
    /// Coordinates of anything on the game
    /// </summary>
    public class Coordonnees
    {
        //Nombre de ligne sur la carte
        private int ligne;

        //Nombre de colonne sur la carte
        private int colonne;

        public int Colonne
        { get { return colonne; } }

        public int Ligne
        { get { return ligne; } }

        /// <summary>
        /// Constructor of cordinates with lines and columns
        /// </summary>
        /// <param name="ligne">line of the map</param>
        /// <param name="colonne">column on the map</param>
        /// <author>Inès</author>
        public Coordonnees(int colonne, int ligne)
        {
            this.ligne = ligne;
            this.colonne = colonne;
        }

        /// <summary>
        /// Tells if two instances are the same or not
        /// </summary>
        /// <param name="obj">Object of the comparaision</param>
        /// <returns>if equal or not</returns>
        /// <author>Charif</author>
        public override bool Equals(object obj)
        {
            return obj is Coordonnees coordonnees &&
                   ligne == coordonnees.ligne &&
                   colonne == coordonnees.colonne;
        }

        /// <summary>
        /// Hash code for the object
        /// </summary>
        /// <returns>return combinaise hashcode</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(ligne, colonne);
        }
    }
}
