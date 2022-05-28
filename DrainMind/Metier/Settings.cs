using DrainMind.Stockage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DrainMind.Metier
{
    /// <summary>
    /// Settings of the game
    /// </summary>
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
        [DataMember]
        private string _culturename;

       

        /// <summary>
        /// Max of the sound : 100
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
        /// Allow to turn on or turn off the sound
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
        /// True if full screen else False
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

        /// <summary>
        /// Allow to chose the language
        /// </summary>
        public string Culturename
        {
            get { return _culturename; }
            set 
            {
                _culturename = value;
                this.NotifyPropertyChanged();
            }
        }

        private static Settings instance;

        /// <summary>
        /// Save settings in a Json file
        /// </summary>
        /// <Author>Charif</Author>
        private Settings()
        {
            LoadSettings();
        }

        /// <summary>
        /// Get or create an instance
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
        /// Load settings file in the json file StreamReader
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
                this._culturename = s.Culturename;
            }
            if (s != null && s.Culturename == null  )
            {
                this._culturename = Thread.CurrentThread.CurrentCulture.Name;
            }
               
        }
    }
}
