using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.Authentication
{
    public interface IAuthenticationService
    {

         void ExitAuthorisation();
        

         Task<string> AuthorisatonAsync(string username, string password);


         Task<bool> RegistrationAsync(string username, string password);

    }
}
