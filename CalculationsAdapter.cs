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

namespace work___Calculator
{
    class CalculationsAdapter : BaseAdapter<Calculations>
    {
        private List<Calculations> cals;
        private Context cntxt;

        public CalculationsAdapter(List<Calculations> cal, Context cnt)
        {
            this.cals = cal;
            this.cntxt = cnt;
        }

        public override Calculations this[int position]
        {
            get { return cals[position]; }
        }

        public override int Count
        {
            get { return cals.Count; }
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
                row = LayoutInflater.From(cntxt).Inflate(Resource.Layout.list_cal, null, false);
            }

            TextView name = row.FindViewById<TextView>(Resource.Id.calname);
            name.Text = this.cals[position].name;

            TextView cals = row.FindViewById<TextView>(Resource.Id.calcal);
            cals.Text = this.cals[position].calculations;

            TextView total = row.FindViewById<TextView>(Resource.Id.caltotal);
            total.Text = this.cals[position].total.ToString();
            return row;
        }
    }
}