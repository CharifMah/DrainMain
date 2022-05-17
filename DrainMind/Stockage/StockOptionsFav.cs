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
    /// Classe chargée du stockage persistant des devises favorites
    /// </summary>
    public class StockOptionsFav
    {
        private string folder;

        public StockOptionsFav(string folder)
        {
            this.folder = folder;
        }

        /// <summary>
        /// Sauvegarde la devise cible
        /// </summary>
        /// <param name="cible">la devise cible</param>
        public void SauverCheckBoxScreen(string CheckBox)
        {
            if (Directory.Exists(folder))
            {
                using (FileStream stream = File.OpenWrite(Path.Combine(folder, "CheckBox.json")))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(string));
                    ser.WriteObject(stream, CheckBox);
                    stream.Close();
                }
            }        
        }


        /// <summary>
        /// Charge la devise cible
        /// </summary>
        /// <returns>la devise cible (null si aucune)</returns>
        //public bool ChargerCheckBoxScreen()
        //{
        //    string d2;
        //    bool res;
        //    if (File.Exists(Path.Combine(folder, "CheckBox.json")))
        //    {
        //        using (FileStream stream = File.OpenRead(Path.Combine(folder, "CheckBox.json")))
        //        {
        //            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(string));
        //            d2 = (string)ser.ReadObject(stream);
        //            stream.Close();
        //        }
        //    }
        //    else
        //        res = false;

        //    return res;
        //}
    }
}
