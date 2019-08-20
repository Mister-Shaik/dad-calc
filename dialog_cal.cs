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
    public class OnCal : EventArgs
    {
        private string name;
        private string subname;
        private string calculation;

        public string Name {
            get { return name; }
            set { name = value; }
        }
        public string Subname {
            get { return subname; }
            set { subname = value; }
        }
        public string calc {
            get { return calculation; }
            set { calculation = calc; }
        }

        public OnCal(string Name, string Subname, string calc) : base()
        {
            name = Name;
            subname = Subname;
            calculation = calc;
        }
    }

    class dialog_cal : DialogFragment
    {
        Context cnt;
        private string mname;
        private string msub;
        private string mcalc;


        public dialog_cal(Context cc)
        {
            this.cnt = cc;
        }

        public event EventHandler<OnCal> mOnCalComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.calculations_popup, container, false);

            var calname = view.FindViewById<Spinner>(Resource.Id.cal_name_input);
            calname.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_nameSelected);
            var nameadap = ArrayAdapter.CreateFromResource(cnt, Resource.Array.calculation_input_list, Android.Resource.Layout.SimpleSpinnerItem);

            nameadap.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            calname.Adapter = nameadap;

            var calsubname = view.FindViewById<Spinner>(Resource.Id.cal_sub_name_input);
            calsubname.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_subSelected);
            var subnameadap = ArrayAdapter.CreateFromResource(cnt, Resource.Array.calculation_sub_input_list, Android.Resource.Layout.SimpleSpinnerItem);

            subnameadap.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            calsubname.Adapter = subnameadap;

            var btnclick = view.FindViewById<Button>(Resource.Id.cal_save);

            btnclick.Click += (Object o, EventArgs e) =>
            {
                mcalc = view.FindViewById<EditText>(Resource.Id.calcalc).Text;
                mOnCalComplete.Invoke(this, new OnCal(mname, msub, mcalc));

            };

            return view;
        }


        public void spinner_nameSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            mname = spinner.GetItemAtPosition(e.Position).ToString();
        }
        public void spinner_subSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            msub = spinner.GetItemAtPosition(e.Position).ToString();
        }
    }
}