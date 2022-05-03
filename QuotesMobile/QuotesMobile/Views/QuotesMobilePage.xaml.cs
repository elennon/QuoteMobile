using Newtonsoft.Json;
using QuotesMobile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuotesMobile.Views
{
    
    public partial class QuotesMobilePage : ContentPage
    {
        public QuotesMobilePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing() 
        {
            base.OnAppearing();
            await seedDb();
            await seedReceipts();

            var agreedQuotes = await App.Database.GetQuotesAgreedAsync();
            //var f = new Months("April");
            //var agreedQuote = await App.Database.GetReceiptsAsync();
            collectionView.ItemsSource = getUpComingJobs(agreedQuotes);
            var ff = "";
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
            foreach (var item in custs)
            {
                await App.Database.SaveReceiptAsync(item);
            }
            var agreedQuote = await App.Database.GetReceiptsAsync();
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
            foreach (var item in custs)
            {
                await App.Database.SaveQuoteAsync(item);
            }
        }

        private List<string> getUpComingJobs(List<Quote> qts)
        {
            double days = 0;
            List<string> strungs = new List<string>();
            foreach (Quote job in qts)
            {
                strungs.Add(job.Name + " ... " + job.Address + "  ..... " + getDaysSinceAgreed(job.AgreedDate));
                days += job.Time;
            }
            strungs.Add("");
            var weeknumber = Math.Round(days / 7);
            days += weeknumber * 2;
            var availDate = DateTime.Now.AddDays(days);
            strungs.Add("Next available date ..... " + availDate.ToShortDateString() + " ( " + (weeknumber + 1) + " weeks )");
            return strungs;
        
        }
        private string getDaysSinceAgreed(DateTime? agreedDate)
        {
            DateTime endDate = DateTime.Today;
            int days = (endDate - agreedDate).Value.Days;
            return days.ToString() + " days since agreed";
        }

        private async void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                //Quote qt = (Quote)e.CurrentSelection.FirstOrDefault();
                //await App.Database.DeleteQuoteAsync(qt);
            }
        }
    }
}