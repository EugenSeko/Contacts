using SQLite;

namespace Contacts.Models
{
    [Table("Users")]
    public class UserModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [Unique]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Sortby { get; set; }
        public string Descending { get; set; }
        public string ThemeStyle { get; set; }
    }
}
