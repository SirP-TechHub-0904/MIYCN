using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.IdentityQueries
{
       public class GetUserListByRoleDto : IRequest<IEnumerable<ProfileDto>> {
        public GetUserListByRoleDto(string role)
        {
            Role = role;
        }

        public string Role { get; set; }
    }

    public class GetUserListByRoleDtoHandler : IRequestHandler<GetUserListByRoleDto, IEnumerable<ProfileDto>>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserListByRoleDtoHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<ProfileDto>> Handle(GetUserListByRoleDto request, CancellationToken cancellationToken)
        {
            var UserDatas = _userManager.Users
                .Where(x => x.Email != "universaadmin@miycn.ng" && x.Email != "jinmcever@miycn.ng")
                .Where(x=>x.Role == request.Role)
                .AsEnumerable();

            var UserDatasList = UserDatas.Select(x => new ProfileDto
            {
                Fullname = x.FirstName + " " + x.MiddleName + " " + x.LastName,
                Phone = x.PhoneNumber,
                Email = x.Email,
                Status = x.UserStatus,
                Id = x.Id,
                Category = x.Role
            });
            return UserDatasList;
        }
    }
}
