using Newtonsoft.Json;
using QuotesMobile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuotesMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllQuotesPage : ContentPage
    {
        public AllQuotesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = await App.Database.GetQuotesAsync();
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                Quote qt = (Quote)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(UpdatePage)}?{nameof(UpdatePage.ID)}={qt.ID.ToString()}");
            }
        }

        private async void BackupClicked(object sender, EventArgs e)
        {
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "backup.json");
                var alal = await App.Database.GetQuotesAsync();
                string json1 = JsonConvert.SerializeObject(alal);
                File.WriteAllText(fileName, json1);
                
                List<string> toAddress = new List<string>();
                toAddress.Add("elennon@outlook.ie");
                await SendEmail("database backup", "db backup", toAddress, fileName);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", "Exception: " + ex.InnerException.ToString(), "OK");
            }
        }

        public async Task SendEmail(string subject, string body, List<string> recipients, string fileName)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                };
                
                message.Attachments.Add(new EmailAttachment(fileName));
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await DisplayAlert("Alert", "Exception: " + fbsEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", "Exception: " + ex.Message, "OK");
            }
        }
    }
}