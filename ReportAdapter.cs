using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SQLite;

namespace work___Calculator
{
    class ReportAdapter:BaseAdapter<Reports>
    {
        private List<Reports> reps;
        private Context cntxt;

        public ReportAdapter(List<Reports> rep, Context cnt)
        {
            this.reps = rep;
            this.cntxt = cnt;
        }

        public override Reports this[int position]
        {
            get { return reps[position]; }
        }

        public override int Count
        {
            get { return reps.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(cntxt).Inflate(Resource.Layout.list_reports, null, false);
            }
            TextView name = row.FindViewById<TextView>(Resource.Id.repName);
            name.Text = this.reps[position].name;


            return row;
        }
    }
}