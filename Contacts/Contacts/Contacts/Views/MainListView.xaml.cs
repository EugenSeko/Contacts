using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace Contacts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainListView : ContentPage
    {
        public MainListView()
        {
            InitializeComponent();
            if (Converters.Global.ThemeStyle == "dark")
            {
                this.Resources.Add(StyleSheet.FromResource
                     ("Views/dark.css", IntrospectionExtensions.GetTypeInfo(typeof(MainListView)).Assembly));
            }
            else
            {
                this.Resources.Add(StyleSheet.FromResource
                     ("Views/light.css", IntrospectionExtensions.GetTypeInfo(typeof(MainListView)).Assembly));
            }
            
        }
        private void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            ((ListView)sender).SelectedItem = null;
        }
    }
}