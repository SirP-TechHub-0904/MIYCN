using Domain.DTOs;
using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITrainingParticipantRepository : IRepository<TrainingParticipant>
    {
        Task<ParticipantInTrainingDTo> ParticipantInTraining(long trainingId, string userId);
        Task<bool> CheckParticipantInTraining(long trainingId, string userId);
        Task<List<ParticipantInTrainingDTo>> ParticipantInTraining(long trainingId);

        Task<bool> AddParticipant(TrainingParticipant model);
    }
}
