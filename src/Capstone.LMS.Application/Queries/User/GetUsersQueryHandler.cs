using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Application.Dtos.User;
using Capstone.LMS.Domain.Extensions;
using Capstone.LMS.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.User
{
    public sealed class GetUsersQueryHandler(
        IUserRepository userRepository,
        ILogger<GetUsersQueryHandler> logger) : IRequestHandler<GetUsersQuery, ListResponseDto<GetUserResponseDto>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<GetUsersQueryHandler> _logger = logger;

        public async Task<ListResponseDto<GetUserResponseDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            // get queryable
            var query = _userRepository.GetQueryable();

            // filter
            var searchTermLowerCase = request.SearchTerm?.ToLower();
            
            query = request.SearchTerm.IsEmpty() ? query : 
                query.Where(p => 
                p.FirstName.ToLower().Contains(searchTermLowerCase) ||
                p.LastName.ToLower().Contains(searchTermLowerCase) ||
                p.UserName.ToLower().Contains(searchTermLowerCase));

            // get total count
            var total = await query.CountAsync(cancellationToken);

            // sort
            if (request.SortBy.IsNotEmpty())
            {
                Expression<Func<Domain.Entities.User, object>> GetSortBy()
                {
                    return request.SortBy.ToLower() switch
                    {
                        "id"        => p => p.Id,
                        "firstname" => p => p.FirstName,
                        "lastname"  => p => p.LastName,
                        "username"  => p => p.UserName,
                        _           => null
                    };
                }

                var sortBy = GetSortBy();

                if (sortBy is not null)
                {
                    query = request.SortDirection == Domain.Enums.SortDirection.Descending ?
                        query.OrderByDescending(sortBy) :
                        query.OrderBy(sortBy);
                }
            }

            // pagination
            var skip = (request.Page.Value - 1) * request.PageSize.Value;
            var take = request.PageSize.Value;

            query = query
                .Skip(skip)
                .Take(take);

            // retrieve
            var items = await query
                .Select(user => new GetUserResponseDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender.Value
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);


            var result = new ListResponseDto<GetUserResponseDto>
            {
                Page = request.Page.Value,
                PageSize = request.PageSize.Value,
                Total = total,
                Items = items
            };

            return result;

        }
    }
}
