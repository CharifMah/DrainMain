using DrainMind.Metier;
using DrainMind.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Controls;

namespace DrainMind.Stockage
{
    /// <summary>
    /// Classe chargée du stockage persistant des Settings
    /// </summary>
    public class StockOptionsFav
    {
        private string folder;

        /// <summary>
        /// Initialise le chemin du dossier
        /// </summary>
        /// <param name="folder">Le path vers la sauvgarde du fichier</param>
        /// <Author>Charif</Author>
        public StockOptionsFav(string folder)
        {
            this.folder = folder;
        }

        /// <summary>
        /// Crée un fichier Json avec les Settings
        /// </summary>
        /// <param name="Settings">Settings a sauvgarde</param>
        /// <Author>Charif</Author>
        public void SauverSettings(Settings Settings)
        {
            if (Directory.Exists(folder))
            {
                using (FileStream stream = File.OpenWrite(Path.Combine(folder, "Settings.json")))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Settings));
                    ser.WriteObject(stream, Settings);

                }
            }
        }


        /// <summary>
        /// Charge le fichier Json Settings
        /// </summary>
        /// <returns>Les Settings Sauvgarder</returns>
        /// <Author>Charif</Author>
        /// <exception cref="System.Windows.Markup.XamlParseException">arrive pas deserialiser un fichier corompu</exception>
        public Settings ChargerSettings()
        {
            Settings d2;
            if (File.Exists(Path.Combine(folder, "Settings.json")))
            {
                using (FileStream stream = File.OpenRead(Path.Combine(folder, "Settings.json")))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Settings));
                    d2 = ser.ReadObject(stream) as Settings;
                }
                
            }
            else
                d2 = null;

            return d2;
        }
    }
}
