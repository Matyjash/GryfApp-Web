using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using System.Web;
using GryfApp_Web.Models;
using Firebase.Auth;

namespace GryfApp_Web.Controllers
{
    public class RecordController : Controller
    {

        public async Task<IActionResult> Index()
        {
            ViewBag.UserId = "yrOpvYYArfN3iZLTBUKn6ms0Oe13";
            //Firebase client
            var firebaseClient = new FirebaseClient("https://ehhapp-5467e.firebaseio.com/");
            //List for received records from database
            var recordsList = new List<Record>();

            //Retrieving entries data from Firebase database
            var dbRecord = await firebaseClient
                .Child("data")
                .Child("records")
                .OnceAsync<Record>();
            //Getting yesterday date to compare with
            var yesterday = DateTime.Now;
            yesterday.AddDays(-1);
            //Filling recordsList
            foreach (var record in dbRecord)
            {
                if (Convert.ToDateTime(record.Object.userDate).ToLocalTime().CompareTo(yesterday) > (-1))
                {
                    Record newRecord = new Record(record.Object.recordId, record.Object.userDate, record.Object.userId, record.Object.userName, record.Object.userParts);
                    recordsList.Add(newRecord);
                }
            }

            //Sorting records by date
            recordsList.Sort((x, y) => DateTime.Compare(DateTime.Parse(x.userDate), DateTime.Parse(y.userDate)));

            //Pasing data to the view
            ViewBag.Records = recordsList;        

            return View(recordsList);
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}
