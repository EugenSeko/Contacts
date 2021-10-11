using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Contacts.Services.Profiles
{
    public interface IProfileManager
    {
         int Id { get; set; }

         string Author { get; set; } // ключ


         string NicktName { get; set; }

         string FirstName { get; set; }

         string LastName { get; set; }

         string ImageUrl { get; set; }

         DateTime CreationTime { get; set; }

        ObservableCollection<ProfileModel> ProfileList { get; set; }

        void GetAllProfiles();

    }
}
