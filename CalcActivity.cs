using System;
using System.Collections.Generic;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

using SQLite;

namespace work___Calculator
{
    [Activity( Theme = "@style/AppTheme")]
    public class CalcActivity : AppCompatActivity
    {
        private List<Calculations> data;
        private ListView calListView;
        private Button mbtnAdd;
        private Button mbtnSave;
        private TextView viewtotal;
        CalculationsAdapter caladap;
        private readonly string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbCalculator.db3");
        private float reptotal;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.calculation);

            var ReportName = Intent.GetStringExtra("ReportName") ?? string.Empty;
            this.Title = ReportName;
            var head = FindViewById<TextView>(Resource.Id.calheading);
            head.Text = ReportName;

            var db = new SQLiteConnection(dbPath);

            var caldata = db.Table<Calculations>().Where(CalObj => CalObj.report == ReportName).ToList() ?? new List<Calculations>();
            db.Close();

            foreach(var dat in caldata)
            {
                reptotal += dat.total;
            }

            viewtotal = FindViewById<TextView>(Resource.Id.total);
            viewtotal.Text = reptotal.ToString("0.00");

            calListView = FindViewById<ListView>(Resource.Id.calculationlist);
            mbtnAdd = FindViewById<Button>(Resource.Id.calcAdd);
            mbtnSave = FindViewById<Button>(Resource.Id.calcSave);

            data = caldata;
            caladap = new CalculationsAdapter(data, this);
            calListView.Adapter = caladap;


            mbtnAdd.Click += (Object o, EventArgs e) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();

                dialog_cal caldialog = new dialog_cal(this);

                caldialog.Show(transaction, "dialog fragment");

                caldialog.mOnCalComplete += (object sender, OnCal cale) =>
                {
                    var first = cale.Name ?? "";
                    var second = cale.Subname ?? "";

                    if((first != "" || second != "") && cale.calc != "")
                    {
                        var fullname = first + ' ' + second;
                        var temp = cale.calc.Replace("-","*");
                        float total = 1;

                        foreach(var num in temp.Split('*'))
                        {
                            total *= float.Parse(num);
                        }

                        reptotal += total;
                        viewtotal = FindViewById<TextView>(Resource.Id.total);
                        viewtotal.Text = reptotal.ToString("0.00");

                        try
                        {
                            db = new SQLiteConnection(dbPath);
                            var x = new Calculations { total = total, calculations = temp, name = fullname, report = ReportName };
                            db.BeginTransaction();
                            db.Insert(x);
                            db.Commit();
                            db.Close();
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("-----------------some error--------");
                            Console.WriteLine(ex);
                        }
                        finally
                        {
                            db.Close();
                        }

                        data.Add(new Calculations { name = fullname, calculations = temp, total = total });

                        caldialog.Dismiss();
                        caladap = new CalculationsAdapter(data, this);
                        calListView.Adapter = caladap;
                    }
                    else { }
                };

            };

            mbtnSave.Click += delegate
            {
                StartActivity(typeof(MainActivity));
            };

        }

    }
}