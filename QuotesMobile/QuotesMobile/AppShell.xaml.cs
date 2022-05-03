
using QuotesMobile.Views;
using Xamarin.Forms;

namespace QuotesMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(UpdatePage), typeof(UpdatePage));
        }
    }
}
