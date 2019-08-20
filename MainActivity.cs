using System;
using System.IO;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;

using SQLite;

namespace work___Calculator
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private List<Reports> Reports;
        private ListView repListView;
        private EditText IpReportName;
        private Button btnAddReport;
        private readonly static string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbCalculator.db3");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.reports);

            var db1 = new SQLiteConnection(dbPath);

            //db1.DropTable<Reports>();
            //db1.DropTable<Calculations>();

            db1.BeginTransaction();
            
            db1.CreateTable<Reports>();
            db1.CreateTable<Calculations>();

            var repdata = db1.Table<Reports>().ToList();
            Reports = repdata;

            db1.Commit();
            db1.Close();

            repListView = FindViewById<ListView>(Resource.Id.reportlist);
            IpReportName = FindViewById<EditText>(Resource.Id.ReportName);
            btnAddReport = FindViewById<Button>(Resource.Id.btnAddReport);

            ReportAdapter repadap = new ReportAdapter(Reports, this);
            repListView.Adapter = repadap;

            repListView.ItemClick += delegate(Object sender, AdapterView.ItemClickEventArgs e)
            {
                var activity = new Intent(this, typeof(CalcActivity));
                activity.PutExtra("ReportName", repadap[e.Position].name); 
                Console.WriteLine(repadap.GetItem(e.Position).ToString());
                StartActivity(activity);
            };

            repListView.ItemLongClick += delegate (Object sender, AdapterView.ItemLongClickEventArgs e)
            {

                var db2 = new SQLiteConnection(dbPath);
                db2.BeginTransaction();
                var reppname = repadap[e.Position].name;

                try
                {
                    db2.Delete<Reports>(reppname);
                    repadap = new ReportAdapter(db2.Table<Reports>().ToList(), this);
                    repListView.Adapter = repadap;
                    

                    var caldata = db2.Table<Calculations>().Where(x => x.report == reppname).ToList() ?? new List<Calculations>();

                    foreach (var items in caldata)
                    {
                        db2.Delete<Calculations>(items.id);
                    }
                }
                catch(Exception exx)
                {
                    Console.WriteLine(exx);
                }
                finally
                {
                    db2.Commit();
                    db2.Close();
                }
            };

            btnAddReport.Click += delegate
            {
                if(IpReportName.Text != "")
                {
                    var db = new SQLiteConnection(dbPath);
                    Reports reps = new Reports { name = IpReportName.Text};

                    try
                    {
                        db.BeginTransaction();
                        db.CreateTable<Reports>();
                        db.Insert(reps);
                        db.Commit();
                        db.Close();
                    }
                    catch(Exception excep)
                    {
                        Console.WriteLine(excep);
                    }
                    finally
                    {
                        Reports.Add(new Reports { name = IpReportName.Text});
                        repadap = new ReportAdapter(Reports, this);
                        repListView.Adapter = repadap;
                        IpReportName.Text = "";
                    }

                }
                else { }

            };
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}

