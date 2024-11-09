using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISupervisorSectionQuestionRepository : IRepository<SupervisorSectionQuestion>
    {
        Task<List<SupervisorSectionQuestion>> ListQuestions();
    }
}
