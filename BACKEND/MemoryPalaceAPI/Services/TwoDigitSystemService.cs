using AutoMapper;
using MemoryPalaceAPI.Authorization;
using MemoryPalaceAPI.Entities;
using MemoryPalaceAPI.Exceptions;
using MemoryPalaceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MemoryPalaceAPI.Services
{
    public interface ITwoDigitSystemService
    {
        TwoDigitSystemDto GetById(int id);
        IEnumerable<TwoDigitSystemDto> GetAll();
        int Create(CreateTwoDigitSystemDto createTwoDigitSystemDto);
        bool Update(int id, CreateTwoDigitSystemDto createTwoDigitSystemDto);
        void Delete(int id);


    }
    public class TwoDigitSystemService : ITwoDigitSystemService
    {
        private readonly MemoryPalaceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public TwoDigitSystemService(MemoryPalaceDbContext dbContext, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public int Create(CreateTwoDigitSystemDto createTwoDigitSystemDto)
        {
            var twoDigitSystem = _mapper.Map<TwoDigitSystem>(createTwoDigitSystemDto);
            twoDigitSystem.CreatedById = _userContextService.GetUserId;
            _dbContext.TwoDigitSystems.Add(twoDigitSystem);
            _dbContext.SaveChanges();
            return twoDigitSystem.Id;
        }

        public void Delete(int id)
        {
            var twoDigitSystem = _dbContext.
                TwoDigitSystems
                .Include(r => r.TwoDigitElements)
                .FirstOrDefault(r => r.Id == id);

            if (twoDigitSystem is null)
                throw new NotFoundException("Restaurant not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, twoDigitSystem,
                new TwoDigitSystemRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _dbContext.Remove(twoDigitSystem);
            _dbContext.SaveChanges();
        }

        public IEnumerable<TwoDigitSystemDto> GetAll()
        {
            var twoDigitSystems = _dbContext.
                TwoDigitSystems
                .Include(r => r.TwoDigitElements)
                .ToList();
            var twoDigitSystemsDtos = _mapper.Map<List<TwoDigitSystemDto>>(twoDigitSystems);
            return twoDigitSystemsDtos;
        }

        public TwoDigitSystemDto GetById(int id)
        {
            var twoDigitSystem = _dbContext.
                TwoDigitSystems
                .Include(r => r.TwoDigitElements)
                .FirstOrDefault(r => r.Id == id);
            if (twoDigitSystem is null)
                throw new NotFoundException("Restaurant not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, twoDigitSystem,
                new TwoDigitSystemRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            var twoDigitSystemsDto = _mapper.Map<TwoDigitSystemDto>(twoDigitSystem);
            return twoDigitSystemsDto;
        }

        public bool Update(int id, CreateTwoDigitSystemDto createTwoDigitSystemDto)
        {
            var twoDigitSystem = _dbContext.
                TwoDigitSystems
                .Include(r => r.TwoDigitElements)
                .FirstOrDefault(r => r.Id == id);

            if (twoDigitSystem is null)
                throw new NotFoundException("Restaurant not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, twoDigitSystem,
                new TwoDigitSystemRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            twoDigitSystem.TwoDigitElements = _mapper.Map<List<TwoDigitElement>>( createTwoDigitSystemDto.TwoDigitElements);

            _dbContext.SaveChanges();

            return true;

        }
    }
}
