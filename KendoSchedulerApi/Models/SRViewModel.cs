using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace KendoSchedulerApi.Models
{
    [Serializable]
    [XmlRoot("SR"), XmlType("SRViewModel")]
    public class SRViewModel
    {
        //public int SRID
        //{
        //    get;
        //    set;
        //}

        //public string CustomerID { get; set; }

        //public string ContactName
        //{
        //    get;
        //    set;
        //}

        //[Required]
        //public DateTime SRDate
        //{
        //    get;
        //    set;
        //}

        //public DateTime ShippedDate
        //{
        //    get;
        //    set;
        //}
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        private DateTime start;
        public DateTime Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value.ToUniversalTime();
            }
        }

        public string StartTimezone { get; set; }

        private DateTime end;
        public DateTime End
        {
            get
            {
                return end;
            }
            set
            {
                end = value.ToUniversalTime();
            }
        }

        public string EndTimezone { get; set; }

        public string RecurrenceRule { get; set; }
        public string RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public bool IsAllDay { get; set; }
        public int? OwnerID { get; set; }

    }
}
