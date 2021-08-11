using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GryfApp_Web.Helpers
{
    public static class CustomDateFormat
    {
        /// <summary>
        /// Formats date from Time Picker (jquery) to custom date format designed for firebase database storage
        /// </summary>
        /// <returns>Formated date</returns>
        public static String formatDateFromTimePicker(String dateToFormat)
        {
            //format in date picker
            DateTime formatedDate = DateTime.ParseExact(dateToFormat, "MM/dd/yyyy h:mm tt", null);
            //format in firebase
            String formatedDateString = formatedDate.ToString("dd.MM.yyyy HH:mm");

            return formatedDateString;
        }

        /// <summary>
        /// Chcecks if date format is formatable, in case if user made custom changes in date picked in time picker widget
        /// </summary>
        /// <param name="dateToFormat"></param>
        /// <returns>True if date is ok, false if date is not formatable</returns>
        private static bool isDateFormatable(String dateToFormat)
        {
            if (!dateToFormat.Contains('/') || dateToFormat.Length != 18)
            {
                return false;
            }
            return true;
        }
    }
}
