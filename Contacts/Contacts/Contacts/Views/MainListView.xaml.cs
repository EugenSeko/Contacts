using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainListView : ContentPage
    {
        public string[] Items {get;set;}

        public MainListView()
        {
            InitializeComponent();
         //string[]items = new string[24];
         //   for (int i = 0; i < 24; i++)
         //   {
         //       items[i] = $"Item {i}";
         //   }
         //   Items = items;
         //   this.BindingContext = this;
        }
      
        private void Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
                headLabel.Text = e.SelectedItem.ToString();
            ((ListView)sender).SelectedItem = null;
        }
    }
}