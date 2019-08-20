using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace work___Calculator
{
    [Table("Calculations")]
    class Calculations
    {
        private int Id;
        private string Report;
        private string Name;
        private float Total;
        private string Calculation;

        [PrimaryKey, AutoIncrement]
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }

        public string name
        {
            get { return Name; }
            set { Name = value; }
        }

        public string calculations
        {
            get { return Calculation; }
            set { Calculation = value; }
        }

        public float total
        {
            get { return Total; }
            set { Total = value; }
        }

        public string report
        {
            get { return Report; }
            set { Report = value; }
        }

        public Calculations() { }
    }
}