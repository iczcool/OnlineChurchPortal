using OnlineWebPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWebPortal.ViewModels
{
    public class CreatePaymentViewModel
    {
        public string PaymentType { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }
        public int RegUserID { get; set; }
    }
}
