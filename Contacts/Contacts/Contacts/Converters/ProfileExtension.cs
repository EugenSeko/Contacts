using Contacts.Models;
using Contacts.ViewModels;

namespace Contacts.Converters
{
    public static class ProfileExtension
    {
        public static ProfileModel ToProfile(this ProfileViewModel profile)
        {
            ProfileModel profileModel = new ProfileModel 
            {
                Name = profile.Name,
                NickName = profile.NickName,
                Author = profile.Author,
                Id = profile.Id,
                Description=profile.Description,
                ImageUrl=profile.ImageUrl,
                CreationTime=profile.CreationTime
            };
            return profileModel;
        }
        public static ProfileViewModel ToProfileViewModel(this ProfileModel profile)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel
            {
                Name = profile.Name,
                NickName = profile.NickName,
                Author = profile.Author,
                Id = profile.Id,
                Description = profile.Description,
                ImageUrl = profile.ImageUrl,
                CreationTime = profile.CreationTime
            };
            return profileViewModel;
        }
    }
}
