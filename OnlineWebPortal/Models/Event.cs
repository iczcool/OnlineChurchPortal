using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWebPortal.Models
{
    public class Event
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime EventDate { get; set; }
        [Display(Name = "Name")]
        public string EventName { get; set; }
        public string Description { get; set; }
    }
}
