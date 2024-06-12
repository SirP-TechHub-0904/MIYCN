using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class UserTestResultDto
    {
        public List<UserTest> UserTest { get; set; }
        public double PercentageResult { get; set; }
        public int TotalQuestions { get; set; }
    }
}
