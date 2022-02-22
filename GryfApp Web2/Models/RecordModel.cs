using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GryfApp_Web2.Models
{
    public class RecordModel
    {
        public RecordModel() { }
        public RecordModel(String recordId, String userDate, String userId, String userName, String userParts)
        {
            this.recordId = recordId;
            this.userDate = userDate;
            this.userId = userId;
            this.userName = userName;
            this.userParts = userParts;
        }
        public String recordId { get; set; }

        [DataType(DataType.Date)]
        public String userDate { get; set; }

        [DataType(DataType.Time)]
        public String userTime { get; set; }
        public String userId { get; set; }

        public String userName { get; set; }

        public String userParts { get; set; }

    }
}