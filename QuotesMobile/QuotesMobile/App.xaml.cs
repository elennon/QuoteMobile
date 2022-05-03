
using QuotesMobile.Data;
using QuotesMobile.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuotesMobile
{
    public partial class App : Application
    {
        static QuotesDatabase database;
        //static ReceiptsDatabase rdb;

        // Create the database connection as a singleton.
        public static QuotesDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new QuotesDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Quotes.db3"));
                }
                return database;
            }
        }

        //public static ReceiptsDatabase Rdb
        //{
        //    get
        //    {
        //        if (rdb == null)
        //        {
        //            rdb = new ReceiptsDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Quotes.db3"));
        //        }
        //        return rdb;
        //    }
        //}

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
