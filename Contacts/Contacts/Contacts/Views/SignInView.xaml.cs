using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace Contacts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInView : ContentPage
    {
        public SignInView()
        {
            InitializeComponent();
            if (Converters.Global.ThemeStyle == "dark")
            {
                this.Resources.Add(StyleSheet.FromResource
                     ("Views/dark.css", IntrospectionExtensions.GetTypeInfo(typeof(TestPage)).Assembly));
            }
        }
    }
}