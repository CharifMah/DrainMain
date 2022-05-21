using DrainMind.Stockage;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DrainMind.Metier.joueur
{
    [DataContract]
    public class LesScores
    {
        private StockScore Stock = new StockScore(Environment.CurrentDirectory);

        [DataMember]
        private List<Score> _scores;

        public List<Score> Scores
        {
            get { return _scores; }
        }

        private void LoadScores()
        {
            List<Score> s = Stock.ChargerScore();
            if (s != null)
            {
                _scores = new List<Score>(s);
            }
            else
                _scores = new List<Score>();
        }

        private static LesScores instance;

        public static LesScores Get()
        {
            if (instance == null)
                instance = new LesScores();
            return instance;
        }

        private LesScores()
        {
            LoadScores();
        }
    }
}
