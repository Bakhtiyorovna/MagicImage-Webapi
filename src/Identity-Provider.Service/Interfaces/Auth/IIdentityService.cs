using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity_Provider.Service.Interfaces.Auth
{
    public interface IIdentityService
    {
        public long Id { get; }
        public string Role { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string PhoneNumber { get; }

    }
}
