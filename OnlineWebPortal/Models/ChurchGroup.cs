using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWebPortal.Models
{
    public class ChurchGroup
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Group Name"), StringLength(60, MinimumLength = 3)]
        public string GroupName { get; set; }
        [Display(Name = "Group Description")]
        public string GroupDescription { get; set; }
        [Display(Name = "Meeting Day")]
        public string MeetingDay { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Start Time"), DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "End Time"), DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string Location { get; set; }
        [Display(Name = "Leader Name"), StringLength(60, MinimumLength = 3)]
        public string LeaderName { get; set; }
        [Display(Name = "Leader Profile"), StringLength(100, MinimumLength = 3)]
        public string LeaderProfile { get; set; }
        public virtual ICollection<RegUserChurchGroup> RegUserChurchGroups { get; set; }

    }
}
