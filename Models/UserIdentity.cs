using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace VinayakSuleyDotCom.Models
{
    public class UserIdentity : IIdentity
    {
        private System.Web.Security.FormsAuthenticationTicket _ticket;

        public UserIdentity(System.Web.Security.FormsAuthenticationTicket ticket)
        {
            this._ticket = ticket;
        }

        public string AuthenticationType
        {
            get { return "Admin"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name
        {
            get { return _ticket.Name; }
        }

        public string FriendlyName
        {
            get { return _ticket.UserData; }
        }

    }
}
