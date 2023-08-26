using AutoMapper;
using MemoryPalaceAPI.Authorization;
using MemoryPalaceAPI.Entities;
using MemoryPalaceAPI.Exceptions;
using MemoryPalaceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MemoryPalaceAPI.Services
{
    public interface IUserService
    {
        UserDto GetById(int id);
        IEnumerable<UserDto> GetAll();



    }
    public class UserService : IUserService
    {
        private readonly MemoryPalaceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public UserService(MemoryPalaceDbContext dbContext, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _dbContext.
                Users
                .Include(r => r.Role)
                .ToList();
            var usersDtos = _mapper.Map<List<UserDto>>(users);
            return usersDtos;
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

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

    }
}
