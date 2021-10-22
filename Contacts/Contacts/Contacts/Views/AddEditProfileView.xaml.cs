using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace Contacts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditProfileView : ContentPage
    {
        public AddEditProfileView()
        {
            InitializeComponent();
            if (Converters.Global.ThemeStyle == "dark")
            {
                this.Resources.Add(StyleSheet.FromResource
                     ("Views/dark.css", IntrospectionExtensions.GetTypeInfo(typeof(TestPage)).Assembly));
            }
            else
            {
                this.Resources.Add(StyleSheet.FromResource
                     ("Views/light.css", IntrospectionExtensions.GetTypeInfo(typeof(TestPage)).Assembly));
            }
        }
    }
}