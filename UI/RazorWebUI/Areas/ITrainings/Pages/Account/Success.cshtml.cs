using Application.Queries.TrainingQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;

namespace RazorWebUI.Areas.ITrainings.Pages.Account
{
    public class SuccessModel : PageModel
    {
        private readonly IMediator _mediator;

        public SuccessModel(IMediator mediator)
        {
            _mediator = mediator;
        }


        public TrainingDto Training { get; set; }
        public string Title { get; set; }
        public async void OnGet(int exp, long id)
        {
            if (exp == 0)
            {
                
                Title = "PRE TEST SUCCESSFUL";
                
            }
            else if (exp == 2)
            {
                Title = "POST TEST SUCCESSFUL";
            }

            GetByIdAndCountTrainingQuery TCommand = new GetByIdAndCountTrainingQuery(id);
            Training = await _mediator.Send(TCommand);
        }
    }
}
