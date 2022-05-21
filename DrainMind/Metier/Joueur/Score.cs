using DrainMind.Stockage;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DrainMind.Metier.Joueur
{
    [DataContract]
    public class Score
    {
        StockScore Stock = new StockScore(Environment.CurrentDirectory);

        [DataMember]
        private string name;
        [DataMember]
        private int enemiekilled;
        [DataMember]
        private int point;

        #region Property

        public string Nom
        { 
            get { return name; }
            set { name = value; }
        }

        public int EnemieKilled
        { get { return enemiekilled; } set { enemiekilled = value; } }

        public int Point
        { get { return point; } set { point = value; } }

        #endregion

        private Score()
        {
            LoadScore();
        }

        private static Score instance;

        public static Score Get()
        {
            if (instance == null)
                instance = new Score();
            return instance;
        }

        private void LoadScore()
        {
            Score s = Stock.ChargerScore();
            if (s != null)
            {
                this.Nom = s.name;
                this.EnemieKilled = s.enemiekilled;
                this.Point = s.point;
            }
        }
    }
}
