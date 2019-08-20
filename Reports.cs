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
    [Table("Reports")]
    class Reports
    {
        private string Name;

        [PrimaryKey]
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }


        public Reports() { }

    }
}