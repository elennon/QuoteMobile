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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddReceiptPage : ContentPage
    {
        public AddReceiptPage()
        {
            InitializeComponent();
            BindingContext = new Receipt();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Supplier j = new Supplier();
            var jjtpss = j.GetSuppliers();
            receiptDate.Date = DateTime.Today;
            supplierPicker.ItemsSource = (System.Collections.IList)jjtpss;
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var sup = (Receipt)BindingContext;
            sup.supplier = supplierPicker.SelectedItem.ToString();
            sup.spent = Convert.ToDouble(spent.Text);
            sup.dateBought = receiptDate.Date;
            

            await App.Database.SaveReceiptAsync(sup);
            await DisplayAlert("Alert", "Saved receipt", "OK");
            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}