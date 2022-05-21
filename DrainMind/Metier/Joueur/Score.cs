using DrainMind.Stockage;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DrainMind.Metier.joueur
{
    [DataContract]
    public class Score : observable.Observable
    {
        
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
            set
            { 
                name = value;
                this.NotifyPropertyChanged();
            }
            
        }

        public int EnemieKilled
        { get { return enemiekilled; } set 
            { 
                enemiekilled = value;
                this.NotifyPropertyChanged();
            }
        }

        public int Point
        { 
            get { return point; } 

            set 
            { 
                point = value;
                this.NotifyPropertyChanged();
            } 
        }

        #endregion


        private Score()
        {
            name = "Bob";
            enemiekilled=0;
            point=0;
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
