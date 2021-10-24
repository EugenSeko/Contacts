namespace Contacts.Services.Settings
{
   public interface ISettingsManager
    {
        string UserName { get; set; }
        string SortBy { get; set; }
        string Descending { get; set; }
        string ThemeStyle { get; set; }
    }
}
