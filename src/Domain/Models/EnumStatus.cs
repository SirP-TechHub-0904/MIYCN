using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class EnumStatus
    {
        public enum UserStatus
        {
            [Description("Pending")]
            Pending = 0,

            [Description("Active")]
            Active = 2,
            [Description("Suspended")]
            Suspended = 3,

            [Description("Leave")]
            Leave = 4,
            [Description("Deleted")]
            Deleted = 6,
        }

        public enum GenderStatus
        {
            Nill = 0,
            Female = 1,
            Male = 2,
        }
        public enum AttendanceStatus
        { 
            Present = 1,
            Absent = 2,
            Excused = 3,
            NILL = 0
        }
        public enum TestStatus
        {
            Nill = 0,
            Present = 1,
            Absent = 2,
            Excused = 3,
        }
        public enum TrainingStatus
        {
            Nill = 0,
            Active = 1,
            Completed = 2,
            Ongoing = 3,
        }
    }
}
