
using Newtonsoft.Json;
using QuotesMobile.Data;
using QuotesMobile.Models;
using QuotesMobile.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuotesMobile
{
    public partial class App : Application
    {
        static QuotesDatabase database;
        static List<string> cutlust;

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
        public static List<string> Cutlust
        {
            get
            {
                if (cutlust == null)
                {
                    cutlust = new List<string>();
                }
                return cutlust;
            }
            set { }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();            
        }

        protected async override void OnStart()
        {
            if (Settings.FirstRun)
            {
                // Perform an action such as a "Pop-Up".
                Settings.FirstRun = false;
                await seedDb();
                await seedReceipts();
            }
        }
        private async Task seedReceipts()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(QuotesMobilePage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("QuotesMobile.recpts.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            var custs = JsonConvert.DeserializeObject<List<Receipt>>(text);
            var rcss = await App.Database.GetReceiptsAsync();
            foreach (var item in rcss)
            {
                await App.Database.DeleteReceiptAsync(item);
            }
            foreach (var rc in custs)
            {
                rc.id = 0;
                await App.Database.SaveReceiptAsync(rc);
            }           
        }

        private async Task seedDb()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(QuotesMobilePage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("QuotesMobile.nn.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            var custs = JsonConvert.DeserializeObject<List<Quote>>(text);
            var csts = await App.Database.GetQuotesAsync();
            foreach (var item in csts)
            {
                await App.Database.DeleteQuoteAsync(item);
            }
            foreach (var item in custs)
            {
                item.ID = 0;
                await App.Database.SaveQuoteAsync(item);
            }
        }


        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

    public static class Settings
    {
        public static bool FirstRun
        {
            get => Preferences.Get(nameof(FirstRun), true);
            set => Preferences.Set(nameof(FirstRun), value);
        }
    }
}
