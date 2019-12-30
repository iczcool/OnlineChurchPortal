using OnlineWebPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWebPortal.ViewModels
{
    public class CreateEnquiryViewModel
    {
        //[DataType(DataType.Date)]
        //[Display(Name = "Date"), DisplayFormat(DataFormatString = "{dd-MM-yyyy:0}", ApplyFormatInEditMode = true)]
        public DateTime EnquiryDate { get; set; }
        //[Required]
        //[Display(Name = "Type"), StringLength(60, MinimumLength = 3)]
        public string EnquiryType { get; set; }
        //[Display(Name = "Enquiry")]
        public string EnquiryBody { get; set; }
        //[Display(Name = "Sender")]
        public string RegUser { get; set; }
    }
}
