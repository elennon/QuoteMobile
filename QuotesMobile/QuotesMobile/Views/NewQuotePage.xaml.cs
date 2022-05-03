using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QuotesMobile.Models;
using System.Collections.Generic;

namespace QuotesMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewQuotePage : ContentPage
    {
        public NewQuotePage()
        {
            InitializeComponent();
            BindingContext = new Quote();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            JobType j = new JobType();
            var jjtpss = j.GetJobTypes();
            quoteDate.Date = DateTime.Today;
            jobTypePicker.ItemsSource = (System.Collections.IList)jjtpss;
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var quote = (Quote)BindingContext;
            quote.Name = custName.Text;
            quote.Email = custEmail.Text;
            quote.Number = custNumber.Text;
            quote.Address = custAddress.Text;
            quote.Time = Convert.ToDouble(jobTime.Text);
            quote.Price = Convert.ToInt16(price.Text);
            quote.QuoteDate = quoteDate.Date;
            quote.JobType = jobTypePicker.SelectedItem.ToString();
            await App.Database.SaveQuoteAsync(quote);
            await DisplayAlert("Alert", "Saved quote", "OK");
            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}