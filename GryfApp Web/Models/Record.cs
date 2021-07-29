using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GryfApp_Web.Models
{
    public class Record
    {
        public Record() { }
        public Record(String recordId, String userDate, String userId, String userName, String userParts)
        {
            this.recordId = recordId;
            this.userDate = userDate;
            this.userId = userId;
            this.userName = userName;
            this.userParts = userParts;
        }
        public String recordId { get; set; }

        public String userDate { get; set; }

        public String userId { get; set; }

        public String userName { get; set; }

        public String userParts { get; set; }

    }
}
