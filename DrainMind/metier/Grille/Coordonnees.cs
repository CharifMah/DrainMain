using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrainMind.metier.Grille
{
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
        /// Constructeur des coordonnes avec la ligne et la colonne
        /// </summary>
        /// <param name="ligne">ligne sur map</param>
        /// <param name="colonne">colonne sur map</param>
        public Coordonnees(int colonne, int ligne)
        {
            this.ligne = ligne;
            this.colonne = colonne;
        }

        /// <summary>
        /// Détermine si les deux instances d'objet sont égales
        /// </summary>
        /// <param name="obj">objet de la comparaison</param>
        /// <returns>bool si egal ou non</returns>
        public override bool Equals(object obj)
        {
            return obj is Coordonnees coordonnees &&
                   ligne == coordonnees.ligne &&
                   colonne == coordonnees.colonne;
        }

        /// <summary>
        /// Code de hachage pour l'objet actuel
        /// </summary>
        /// <returns>retourne combinaison hashcode</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(ligne, colonne);
        }
    }
}
