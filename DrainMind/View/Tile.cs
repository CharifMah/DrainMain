using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrainMind.View
{
    /// <summary>
    /// Contient la position du tile, et sa représentation graphique
    /// </summary>
    class Tile
    {
        //taille de la tuile
        private int TILESIZE = 32;
        //
        private bool solid;
        //position de la tuile sur l'axe des coordonnees
        private float posX;
        //position de la tuile sur l'axe des abscisses
        private float posY;

        /// <summary>
        /// Constructeur d'une tuile
        /// </summary>
        /// <param name="solid"></param>
        /// <param name="posX">position sur axe coordonnes</param>
        /// <param name="posY">position sur axe abscisses</param>
        public Tile(bool solid, float posX, float posY)
        {
            this.solid = false;
            this.posX = posX;
            this.posY = posY;
        }

        /// <summary>
        /// Permet d'afficher une tuile
        /// </summary>
        public void afficherTile() { }

        /// <summary>
        /// 
        /// </summary>
        public bool estSolid() { return solid; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solid"></param>
        public void setSolid(bool solid) { }

        /// <summary>
        /// 
        /// </summary>
        public float PosX
        {
            get { return posX; }
            set { }
        }

        /// <summary>
        /// 
        /// </summary>
        public float PosY
        {
            get { return posY; }
            set { }
        }
    }
}
