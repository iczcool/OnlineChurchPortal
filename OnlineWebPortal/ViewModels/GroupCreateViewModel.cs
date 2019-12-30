using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWebPortal.ViewModels
{
    public class GroupCreateViewModel
    {
        public int ID { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string MeetingDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public string LeaderName { get; set; }
        public string LeaderProfile { get; set; }
    }
}
