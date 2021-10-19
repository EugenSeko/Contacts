using Contacts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.Services.Profiles
{
    public interface IProfileManager
    {
        ProfileModel Profile { get; set; }
        List<ProfileModel> ProfileList { get; set; }
        Task<List<ProfileModel>> GetAllProfilesAsync();
        Task<int> DeleteAsync(ProfileModel profile);
        Task<ProfileModel> GetProfileById(int id);
        Task<int> CreateAsync(ProfileModel profile);
        Task<int> UpdateAsync(ProfileModel profile);
    }
}
