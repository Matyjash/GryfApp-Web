using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GryfApp_Web2.Helpers
{
    public class CurrentUser
    {
        public static bool logged = false;
        public static string userId {get; set;}
    }
}