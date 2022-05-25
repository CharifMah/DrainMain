using DrainMind.Stockage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrainMind.Metier
{
    [DataContract]
    public class Settings : observable.Observable
    {
        StockOptions Stock = new StockOptions(Environment.CurrentDirectory);

        [DataMember]
        private bool _pleinEcran;
        [DataMember]
        private bool _SonOnOff;
        [DataMember]
        private double _Son;

        /// <summary>
        /// Son Max 100
        /// </summary>
        public double Son
        {
            get { return _Son; }
            set 
            { 
                _Son = value;
                if (DrainMindGame.Instance != null)
                DrainMindGame.Instance.BackgroundVolume = _Son / 100;
          
                if (_Son > 1)
                    _SonOnOff = true;
                if (_Son < 1)
                    _SonOnOff = false;
                   
                this.NotifyPropertyChanged("Son");

            }
        }

        /// <summary>
        /// Active ou Desactive le son
        /// </summary>
        public bool SonOnOff
        {
            get { return _SonOnOff; }
            set 
            {
                _SonOnOff = value;
                if (!_SonOnOff && DrainMindGame.Instance != null)
                {
                    DrainMindGame.Instance.BackgroundVolume = 0;
                }
                if (_SonOnOff && DrainMindGame.Instance != null)
                {
                    DrainMindGame.Instance.BackgroundVolume = _Son / 100;
                }
                this.NotifyPropertyChanged("SonOnOff");
            }
        }

        /// <summary>
        /// True Si pleinEcran sinon False
        /// </summary>
        public bool PLeinEcran
        {
            get { return _pleinEcran; }
            set 
            {
                _pleinEcran = value;
                if (_pleinEcran)
                {
                    MainWindow.GetMainWindow.WindowStyle = WindowStyle.None;
                    MainWindow.GetMainWindow.WindowState = WindowState.Maximized;
                }
                else
                {
                    MainWindow.GetMainWindow.WindowState = WindowState.Normal;
                    MainWindow.GetMainWindow.WindowStyle = WindowStyle.ThreeDBorderWindow;
                }
                this.NotifyPropertyChanged();
            }
        }

        private static Settings instance;

        /// <summary>
        /// Charge les settings dans le fichier Json
        /// </summary>
        /// <Author>Charif</Author>
        private Settings()
        {
            LoadSettings();
        }

        /// <summary>
        /// Cree une instance si il y en as pas sinon get instance
        /// </summary>
        /// <returns>instance settings</returns>
        /// <Author>Charif</Author>
        public static Settings Get()
        {
            if (instance == null)
                instance = new Settings();
            return instance;
        }

        /// <summary>
        /// Charge le fichiers des settings dans le fichier json StreamReader
        /// </summary>
        /// <Author>Charif</Author>
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
