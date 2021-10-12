using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.Profiles
{
    public interface IProfileManager
    {
        ProfileModel Profile { get; set; }

        ObservableCollection<ProfileModel> ProfileList { get; set; }

        Task<ObservableCollection<ProfileModel>> GetAllProfilesAsync();

    }
}
