using QuotesMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuotesMobile.Views
{
    [QueryProperty(nameof(ID), nameof(ID))]
    public partial class UpdatePage : ContentPage
    {
        public string ID
        {
            set
            {
                LoadQuote(value);
            }
        }
        public UpdatePage()
        {
            InitializeComponent();
            BindingContext = new Quote();
            
        }

        async void LoadQuote(string ID)
        {
            try
            {
                int id = Convert.ToInt32(ID);
                Quote qt = await App.Database.GetQuoteAsync(id);
                BindingContext = qt;
                custName.Text = qt.Name;
                custAddress.Text = qt.Address;
                custEmail.Text = qt.Email;
                custNumber.Text = qt.Number;
                jobTime.Text = qt.Time.ToString();
                price.Text = qt.Price.ToString();
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load quote.");
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();           
        }


        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var quote = (Quote)BindingContext;
            quote.Name = custName.Text;
            quote.Email = custEmail.Text;
            quote.Number = custNumber.Text;
            quote.Address = custAddress.Text;
            quote.Time = Convert.ToDouble(jobTime.Text.Split(' ')[0]);
            quote.Price = Convert.ToInt16(price.Text.Split(' ')[0]);
            if (ckbAgreed.IsChecked)
            {
                quote.AgreedDate = qfDate.Date;
                quote.Agreed = true;
            }
            if (ckbFinished.IsChecked)
            {
                quote.finishDate = qfDate.Date;
                quote.Finished = true;
            }

            await App.Database.SaveQuoteAsync(quote);
            await DisplayAlert("Alert", "updated..", "OK");
            // Navigate backwards
            //await Shell.Current.GoToAsync("..");
        }
    }
}