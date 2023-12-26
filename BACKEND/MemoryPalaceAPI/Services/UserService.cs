using AutoMapper;
using MemoryPalaceAPI.Authorization;
using MemoryPalaceAPI.Entities;
using MemoryPalaceAPI.Exceptions;
using MemoryPalaceAPI.Mappings;
using MemoryPalaceAPI.Models;
using MemoryPalaceAPI.Models.TwoDigitSystemModels;
using MemoryPalaceAPI.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MemoryPalaceAPI.Services
{
    public interface IUserService
    {
        UserDto GetById(int id);
        PagedResult<UserDto> GetAll(UserQuery userQuery);



    }
    public class UserService : IUserService
    {
        private readonly MemoryPalaceDbContext _dbContext;
        private readonly MemoryPalaceMappingService _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public UserService(MemoryPalaceDbContext dbContext, MemoryPalaceMappingService mapper, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public PagedResult<UserDto> GetAll(UserQuery userQuery)
        {
            var baseQuery = _dbContext
                .Users
                .Include(r => r.Role)
                .Where(r => userQuery.SearchPhrase == null ||
                   r.Email.ToLower().Contains(userQuery.SearchPhrase.ToLower()));

            if (!string.IsNullOrEmpty(userQuery.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<User, object>>>
                {
                    { nameof(User.Id), r => r.Id },
                    { "Role.Name", r => r.Role.Name},
                    { nameof(User.Email), r => r.Email }
                };
                var selectedColumn = columnsSelectors[userQuery.SortBy];

                baseQuery = userQuery.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }
            var users = baseQuery
                .Skip(userQuery.PageSize * (userQuery.PageNumber - 1))
                .Take(userQuery.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            var usersDtos = users.Select(_mapper.MapToUserDto).ToList();
            var result = new PagedResult<UserDto>(usersDtos, totalItemsCount, userQuery.PageSize, userQuery.PageNumber);
            return result;
        }

        public UserDto GetById(int id)
        {
            var user = _dbContext.
               Users
               .Include(r => r.Role)
               .FirstOrDefault(r => r.Id == id);
            if (user is null)
                throw new NotFoundException("User not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, user,
                new UserRequirement(ResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            var userDto = _mapper.MapToUserDto(user);
            return userDto;
        }

    }
}
