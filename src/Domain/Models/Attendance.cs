﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.Models
{
    public class Attendance
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public AppUser User { get; set; }
        public AttendanceStatus AttendanceStatus { get; set; }

        public long DialyActivityId { get; set; }
        public DialyActivity DialyActivity { get; set; }

    }
}
