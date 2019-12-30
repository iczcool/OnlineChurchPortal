using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWebPortal.Models
{
    public class Enquiry
    {
        [Key]
        //public int ID { get; set; }
        //public DateTime EnquiryDate { get; set; }
        //public string EnquiryType { get; set; }
        //public string EnquiryBody { get; set; }
        //public string RegUser { get; set; }




        public int ID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime EnquiryDate { get; set; }
        [Required]
        [Display(Name = "Type"), StringLength(60, MinimumLength = 3)]
        public string EnquiryType { get; set; }
        [Display(Name = "Enquiry")]
        public string EnquiryBody { get; set; }
        [Display(Name = "Sender")]
        public string RegUser { get; set; }
    }
}
