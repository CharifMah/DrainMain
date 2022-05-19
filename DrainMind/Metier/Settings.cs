using DrainMind.Stockage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DrainMind.Metier
{
    [DataContract]
    public class Settings
    {
        StockOptionsFav Stock = new StockOptionsFav(Environment.CurrentDirectory);

        [DataMember]
        private bool _pleinEcran;
        [DataMember]
        private bool _SonOnOff;
        [DataMember]
        private double _Son;

        public double Son
        {
            get { return _Son; }
            set { _Son = value; }  
        }

        public bool SonOnOff
        {
            get { return _SonOnOff; }
            set { _SonOnOff = value; }
        }

        public bool PLeinEcran
        {
            get { return _pleinEcran; }
            set { _pleinEcran = value; }
        }

        private static Settings instance;

        private Settings()
        {
            LoadSettings();
        }

        public static Settings Get()
        {
            if (instance == null)
                instance = new Settings();
            return instance;
        }

        /// <summary>
        /// Charge le fichiers des settings StreamReader
        /// </summary>
        private void LoadSettings()
        {
            Settings s = Stock.ChargerSettings(); 
            if (s != null)
            {
                this._pleinEcran = s.PLeinEcran;
                this._SonOnOff = s.SonOnOff;
                this._Son = s.Son;
            }            
        }
    }
}
