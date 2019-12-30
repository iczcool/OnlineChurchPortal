using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWebPortal.ViewModels
{
    public class CreateEventViewModel
    {
        public DateTime EventDate { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
    }
}
