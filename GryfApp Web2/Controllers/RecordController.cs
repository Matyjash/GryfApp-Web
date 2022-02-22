using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;
using GryfApp_Web2.Helpers;
using GryfApp_Web2.Models;

namespace GryfApp_Web2.Controllers
{
    [Authorize]
    public class RecordController : Controller
    {
        private static List<RecordModel> records = new List<RecordModel>();
        public async Task<ActionResult> Index()
        {
            records = new List<RecordModel>();
            //Firebase client
            var firebaseClient = new FirebaseClient("https://ehhapp-5467e.firebaseio.com/");

            //Retrieving entries data from Firebase database
            var dbRecord = await firebaseClient
                .Child("data")
                .Child("records")
                .OnceAsync<RecordModel>();
            //Getting yesterday date to compare with
            var yesterday = DateTime.Now;
            yesterday.AddDays(-1);
            //Filling recordsList
            foreach (var record in dbRecord)
            {
                if (Convert.ToDateTime(record.Object.userDate).ToLocalTime().CompareTo(yesterday) > (-1))
                {
                    RecordModel newRecord = new RecordModel(record.Object.recordId, record.Object.userDate, record.Object.userId, record.Object.userName, record.Object.userParts);
                    records.Add(newRecord);
                }
            }

            //Sorting records by date
            records.Sort((x, y) => DateTime.Compare(DateTime.Parse(x.userDate), DateTime.Parse(y.userDate)));

            return View(records);
        }


        public ActionResult ErrorInput()
        {
            return View();
        }

        [Route("Record/Delete/{recordId:string}")]
        public ActionResult Delete(string recordId)
        {
            RecordModel record = new RecordModel();


            foreach(var rec in records)
            {
                if (rec.recordId.Equals(recordId))
                {
                    record = rec;
                }
            }

            return View(record);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(RecordModel record)
        {

            //Firebase client
            var firebaseClient = new FirebaseClient("https://ehhapp-5467e.firebaseio.com/");

            //Delete record
             await firebaseClient
                .Child("data")
                .Child("records")
                .Child(record.recordId).DeleteAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Create(RecordModel newRecord)
        {
            //if any of input data is blank then display error
            if (newRecord.userDate == null || newRecord.userName == null || newRecord.userParts == null || CurrentUser.userId == null)
            {
                return RedirectToAction(nameof(ErrorInput));
            }

            //posting new record to firebase database
            var firebaseClient = new FirebaseClient("https://ehhapp-5467e.firebaseio.com/");

            //formating date
            String formatedDate = CustomDateFormat.formatDateFromTimePicker(newRecord.userDate, newRecord.userTime);
            newRecord.userDate = formatedDate;
            newRecord.userTime = null;

            var result = await firebaseClient
                .Child("data")
                .Child("records")
                .PostAsync(newRecord);

            var id = result.Key;
            newRecord.recordId = id;
            newRecord.userId = CurrentUser.userId;

            await firebaseClient
                .Child("data")
                .Child("records")
                .Child(id)
                .PutAsync(newRecord);

            return RedirectToAction(nameof(ShowRecord), newRecord);
        }

        public ActionResult ShowRecord(RecordModel record)
        {
            return View(record);
        }


    }
}