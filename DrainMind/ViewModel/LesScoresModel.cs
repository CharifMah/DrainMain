using DrainMind.Stockage;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DrainMind.Metier.joueur
{
    [DataContract]
    public class LesScoresModel
    {
        private StockScore Stock = new StockScore(Environment.CurrentDirectory);

        [DataMember]
        private List<Score> _scores;

        public List<Score> Scores
        {
            get { return _scores; }
        }

        /// <summary>
        /// Charge les scores sinon en cree une nouvelle liste
        /// </summary>
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

        private static LesScoresModel instance;

        public static LesScoresModel Get()
        {
            if (instance == null)
                instance = new LesScoresModel();
            return instance;
        }

        private LesScoresModel()
        {
            LoadScores();
        }
    }
}
