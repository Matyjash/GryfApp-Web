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
using GryfApp_Web.Helpers;

namespace GryfApp_Web.Controllers
{
    public class RecordController : Controller
    {
        //TODO: delete user id in view bag
        String UserId = "yrOpvYYArfN3iZLTBUKn6ms0Oe13";

        public async Task<IActionResult> Index()
        {
            //TODO: delete user id in view bag
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

        public ActionResult ErrorInput()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(Record newRecord)
        {
            //if any of input data is blank then display error
            if (newRecord.userDate == null || newRecord.userName == null || newRecord.userParts == null || UserId.Length == null || UserId.Length == 0)
            {
                return RedirectToAction(nameof(ErrorInput));
            }

            //posting new record to firebase database
            var firebaseClient = new FirebaseClient("https://ehhapp-5467e.firebaseio.com/");

            //formating date
            String formatedDate = CustomDateFormat.formatDateFromTimePicker(newRecord.userDate);
            newRecord.userDate = formatedDate;

            var result = await firebaseClient
                .Child("data")
                .Child("records")
                .PostAsync(newRecord);

            var id = result.Key;
            newRecord.recordId = id;
            newRecord.userId = UserId;

            await firebaseClient
                .Child("data")
                .Child("records")
                .Child(id)
                .PutAsync(newRecord);

            return RedirectToAction(nameof(ShowRecord),newRecord);
        }

        public ActionResult ShowRecord(Record record)
        {
            return View(record);
        }

        
    }
    
}
