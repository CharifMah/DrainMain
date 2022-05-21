using DrainMind.Metier.Joueur;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace DrainMind.Stockage
{
    public class StockScore
    {

        private string folder;

        /// <summary>
        /// Initialise le chemin du dossier
        /// </summary>
        /// <param name="folder">Le path vers la sauvgarde du fichier</param>
        /// <Author>Charif</Author>
        public StockScore(string folder)
        {
            this.folder = folder;
        }

        /// <summary>
        /// Crée un fichier Json avec les Settings
        /// </summary>
        /// <param name="Settings">Settings a sauvgarde</param>
        /// <Author>Charif</Author>
        public void SauverScore(Score score)
        {
            if (Directory.Exists(folder))
            {
                if (File.Exists(Path.Combine(folder, "Score.json")))
                    File.Delete(Path.Combine(folder, "Score.json"));

                using (FileStream stream = File.OpenWrite(Path.Combine(folder, "Score.json")))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Score));
                    ser.WriteObject(stream, score);
                }
            }
        }


        /// <summary>
        /// Charge le fichier Json Settings
        /// </summary>
        /// <returns>Les Settings Sauvgarder</returns>
        /// <Author>Charif</Author>
        /// <exception cref="System.Windows.Markup.XamlParseException">arrive pas deserialiser un fichier corompu</exception>
        public Score ChargerScore()
        {
            Score d2;
            if (File.Exists(Path.Combine(folder, "Score.json")))
            {
                using (FileStream stream = File.OpenRead(Path.Combine(folder, "Score.json")))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Score));
                    d2 = ser.ReadObject(stream) as Score;
                }

            }
            else
                d2 = null;

            return d2;
        }
    }
}
