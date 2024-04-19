﻿using Domain.DTOs;
using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITrainingFacilitatorRepository : IRepository<TrainingFacilitator>
    { 
        Task<FacilitatorInTrainingDTo> FacilitatorInTraining(long trainingId, string userId);
        Task<List<FacilitatorInTrainingDTo>> FacilitatorInTraining(long trainingId);

        Task<bool> AddFacilitator(TrainingFacilitator model);

    }
}
