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
    public interface ITrainingRepository : IRepository<Training>
    {
        Task<TrainingDto> GetTrainingByIdAndCounts(long id);
        Task<List<TrainingByUserDto>> GetAllTrainingsByUserId(string userId);
        Task<List<Training>> GetAll(string? state);

        Task RemoveTraining(long id);
    }
}
