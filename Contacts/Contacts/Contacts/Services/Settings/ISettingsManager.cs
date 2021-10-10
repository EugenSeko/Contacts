using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Services.Settings
{
   public interface ISettingsManager
    {
        string UserName { get; set; }
    }
}
