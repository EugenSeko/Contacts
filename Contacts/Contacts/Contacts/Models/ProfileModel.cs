using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Models
{
    [Table("Profiles")]
    public class ProfileModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement,Column("_id")]
        public int Id { get; set; }

        public string Author { get; set; } // ключ


        public string NickName { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreationTime { get; set; }

    }
}
