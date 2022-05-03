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
            var agreedQuotes = await App.Database.GetQuotesAgreedAsync();           
            collectionView.ItemsSource = getUpComingJobs(agreedQuotes);
        }

        private List<string> getUpComingJobs(List<Quote> qts)
        {
            double days = 0;
            List<string> strungs = new List<string>();
            foreach (Quote job in qts)
            {
                strungs.Add(job.Name + " ... " + job.Address + "  ... " + getDaysSinceAgreed(job.AgreedDate));
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
            return days.ToString() + " days since";
        }
    }
}