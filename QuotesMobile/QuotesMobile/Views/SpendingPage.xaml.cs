using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuotesMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpendingPage : ContentPage
    {
        public SpendingPage()
        {
            InitializeComponent();
            var startDate = DateTime.ParseExact("01/01/2022", "MM/dd/yyyy", new CultureInfo("en-IE"));
            var endDate = DateTime.Now;
            var monthsTill = MonthsBetween(startDate, endDate);
            List<Months> months = new List<Months>();
            foreach (var item in monthsTill)
            {
                months.Add(item);
            }
            monthPicker.ItemsSource = months;
            var ttt = Convert.ToInt32(monthsTill.LongCount()) - 1;
            monthPicker.SelectedIndex = ttt;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }

        public static IEnumerable<Months> MonthsBetween(
        DateTime startDate,
        DateTime endDate)
        {
            DateTime iterator;
            DateTime limit;


            if (endDate > startDate)
            {
                iterator = new DateTime(startDate.Year, startDate.Month, 1);
                limit = endDate;
            }
            else
            {
                iterator = new DateTime(endDate.Year, endDate.Month, 1);
                limit = startDate;
            }

            var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            while (iterator <= limit)
            {
                Months mm = new Months();
                mm.Monthh = iterator.Month; // 
                mm.Yearr = iterator.Year;
                mm.MonthName = dateTimeFormat.GetMonthName(iterator.Month);
                yield return (
                    mm
                );
                iterator = iterator.AddMonths(1);
            }
        }

        private async void monthPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Months m = new Months();
            m = (Months)monthPicker.SelectedItem;
            double? tot = 0;
            var gg = await App.Database.GetSpentByMonthAsync(m);
            var ghg = gg.Where(x => x.dateBought.Value.Month == m.Monthh);
            foreach (var rec in ghg)
            {
                if (rec.spent != null)
                {
                    tot += rec.spent;
                } 
            }
            spent.Text = tot.ToString();
            double? made = 0;
            var jobsDone = await App.Database.GetJobsDoneAsync();
            foreach (var job in jobsDone)
            {
                if (!job.finishDate.HasValue)
                {
                    await DisplayAlert("Alert", "You need to update " + job.Name + " - " + job.Address + " they are marked as done but have no finish date", "OK");
                    //await Shell.Current.GoToAsync($"{nameof(UpdatePage)}?{nameof(UpdatePage.ID)}={job.ID.ToString()}");
                    continue;
                }
                if (job.finishDate.Value.Month == m.Monthh)
                {
                    made += job.Price;
                }               
            }
            doshIn.Text = made.ToString();
            //jobsDoneByMonth.Items.Clear();
            //getFinishedJobsByMonth(m.Monthh);
        }
    }

    public class Months
    {
        private int nameValue;
        public int Monthh
        {
            get
            {
                return nameValue;
            }
            set
            {
                nameValue = value;
            }
        }

        private string monthNmaeameValue;
        public string MonthName
        {
            get
            {
                return monthNmaeameValue;
            }
            set
            {
                monthNmaeameValue = value;
            }
        }
        private int yearValue;
        private object selectedItem;

        public Months(object selectedItem)
        {
            this.selectedItem = selectedItem;
        }

        public Months()
        {
        }

        public int Yearr
        {
            get
            {
                return yearValue;
            }
            set
            {
                if (value != yearValue)
                {
                    yearValue = value;
                }
            }
        }
    }
}