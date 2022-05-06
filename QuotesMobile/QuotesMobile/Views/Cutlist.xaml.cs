using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuotesMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuotesMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cutlist : ContentPage
    {
        public Cutlist()
        {
            InitializeComponent();
            BindingContext = this;
            var b = new List<string>();
            b.Add("3 drawer");
            b.Add("6 drawer");
            b.Add("3 drawer/tall");
            b.Add("6 drawer/tall");
            typePicker.ItemsSource = b;
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {

            Drawers unit = new Drawers(Int32.Parse(fwidth.Text),
                Int32.Parse(fheight.Text),
                Int32.Parse(kicker.Text),
                Int32.Parse(lowSide.Text),
                Int32.Parse(flatTopWidth.Text),
                48.0,
                Int32.Parse(noDrawers.Text),
                Int32.Parse(unitDepth.Text),
                Int32.Parse(cabWidth.Text),
                true,
                typePicker.SelectedItem.ToString());
            var list = PrintOut(unit.getCuttingList(unit));
            var j = getPrice(unit);
            list.AddRange(j);
            //await Shell.Current.GoToAsync($"{nameof(ShowCutList)}?{nameof(ShowCutList.list)}={list}");
            
            App.Cutlust.AddRange(list);
            await Shell.Current.GoToAsync($"{nameof(ShowCutList)}");
        }

        private List<string> getPrice(Drawers understairDrawerUnit)
        {
            double price = 0;
            var noShts = (understairDrawerUnit.eighteenMdfArea + (understairDrawerUnit.eighteenMdfArea / 100 * 10)) / 2880000;
            int i = (int)Math.Ceiling(noShts);
            price += i * 60;
            var notvlvShts = (understairDrawerUnit.twelveMdfArea + (understairDrawerUnit.twelveMdfArea / 100 * 10)) / 2880000;
            int itwlv = (int)Math.Ceiling(notvlvShts);
            price += itwlv * 50;
            var drss = 0;
            if (understairDrawerUnit.drawerNumber == 3)
            {
                price += 45;
                drss = 3;
            }
            else price += 90; drss = 3;
            price += 70; // for 3 by 1.5
            price += 40; // paint
            price += 30; // push to open
            price += 15; // deisel
            price += 8;  // fixings
            //price += understairDrawerUnit.
            List<string> list = new List<string>();
            list.Add(" number of 18mm mdf ... " + i.ToString());
            list.Add(" number of 12mm mdf ... " + itwlv.ToString());
            list.Add(" number drawer runners ... " + drss.ToString());
            list.Add(" full materials cost ... €" + price.ToString());
            return list;
        }

        private List<string> PrintOut(List<cut> cutList)
        {
            List<string> list = new List<string>();
            foreach (cut line in cutList)
            {
                list.Add(line.title);
                if (line.toLong)
                    list.Add(line.length + " * " + line.width + " * " + line.numberOf + " long");
                else
                    list.Add(line.length + " * " + line.width + " * " + line.numberOf);
            }
            return list;
        }

        
    }
}