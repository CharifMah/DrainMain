using DrainMind.Stockage;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DrainMind.metier.joueur.ScoreFolder
{
    /// <summary>
    /// Class score of the player
    /// </summary>
    [DataContract]
    public class Score : observable.Observable
    {
        //name of the player
        [DataMember]
        private string name;
        //number of the ennemies the player killed
        [DataMember]
        private int enemiekilled;
        //point gain
        [DataMember]
        private int point;

        #region Property

        /// <summary>
        /// Allow to get and set the name of the player
        /// </summary>
        /// <author>Charif</author>
        public string Nom
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged();
            }

        }

        /// <summary>
        /// Allow to get and set the number of ennemies killed
        /// </summary>
        /// <author>Charif</author>
        public int EnemieKilled
        {
            get { return enemiekilled; }
            set
            {
                enemiekilled = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Allow to get and set the point the player has
        /// </summary>
        /// <author>Charif</author>
        public int Point
        {
            get { return point; }

            set
            {
                point = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        private Score()
        {
            name = "Bob";
            enemiekilled = 0;
            point = 0;
        }

        private static Score instance;
        public static Score Get()
        {
            if (instance == null)
                instance = new Score();
            return instance;
        }



        public static void Destroy()
        {
            if (instance != null)
                instance = null;
        }
    }
}
