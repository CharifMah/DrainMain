using DrainMind.metier.Grille;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrainMind.metier.map
{
    /// <summary>
    /// carte du jeu
    /// </summary>
    class Carte
    {
        public Carte()
        {
            Dictionary<Coordonnees, Case> carte = new Dictionary<Coordonnees, Case>();
        }
    }
}
