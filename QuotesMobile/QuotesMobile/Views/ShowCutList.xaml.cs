using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuotesMobile.Views
{
    
    public partial class ShowCutList : ContentPage
    {
        

        public ShowCutList()
        {
            InitializeComponent();
            shwList.ItemsSource = App.Cutlust;
        }
        
    }
}