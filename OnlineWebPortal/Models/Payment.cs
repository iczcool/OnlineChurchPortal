using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWebPortal.Models
{
    public class Payment
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Payment Type"), StringLength(30), Required]
        public string PaymentType { get; set; }
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
        public int RegUserID { get; set; }
        public RegUser RegUser { get; set; }
    }
}
