using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using System.Web;
using GryfApp_Web.Models;

namespace GryfApp_Web.Controllers
{
    public class RecordController : Controller
    {

        public async Task<IActionResult> Index()
        {

            //Firebase client
            var firebaseClient = new FirebaseClient("https://ehhapp-5467e.firebaseio.com/");
            //List for received records from database
            var recordsList = new List<Record>();


            /*
            var result = await firebaseClient
                .Child("data")
                .Child("records")
                .PostAsync(exampleRecord);
            */


            //Retrieving entries data from Firebase database
            var dbRecord = await firebaseClient
                .Child("data")
                .Child("records")
                .OnceAsync<Record>();

            var yesterday = DateTime.Now;
            yesterday.AddDays(-1);
            foreach (var record in dbRecord)
            {
                if (Convert.ToDateTime(record.Object.userDate).ToLocalTime().CompareTo(yesterday) > (-1))
                {
                    Record newRecord = new Record(record.Object.recordId, record.Object.userDate, record.Object.userId, record.Object.userName, record.Object.userParts);
                    recordsList.Add(newRecord);
                }
            }

            // TODO: Sorting list of records

            //Pasing data to the view
            ViewBag.Records = recordsList;

           
            System.Diagnostics.Debug.WriteLine("Number of records:" + recordsList.Count);

            return View();
        }
    }
}
