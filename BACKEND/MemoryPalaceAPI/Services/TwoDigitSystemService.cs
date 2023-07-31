using AutoMapper;
using MemoryPalaceAPI.Entities;
using MemoryPalaceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MemoryPalaceAPI.Services
{
    public interface ITwoDigitSystemService
    {
        TwoDigitSystemDto GetById(int id);
        IEnumerable<TwoDigitSystemDto> GetAll();
        int Create(CreateTwoDigitSystemDto createTwoDigitSystemDto);
        bool Update(int id, CreateTwoDigitSystemDto createTwoDigitSystemDto);
        bool Delete(int id);


    }
    public class TwoDigitSystemService : ITwoDigitSystemService
    {
        private readonly MemoryPalaceDbContext _dbContext;
        private readonly IMapper _mapper;

        public TwoDigitSystemService(MemoryPalaceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(CreateTwoDigitSystemDto createTwoDigitSystemDto)
        {
            var twoDigitSystem = _mapper.Map<TwoDigitSystem>(createTwoDigitSystemDto);
            _dbContext.TwoDigitSystems.Add(twoDigitSystem);
            _dbContext.SaveChanges();
            return twoDigitSystem.Id;
        }

        public bool Delete(int id)
        {
            var twoDigitSystem = _dbContext.
                TwoDigitSystems
                .Include(r => r.TwoDigitElements)
                .FirstOrDefault(r => r.Id == id);
            if (twoDigitSystem is null) return false;

            _dbContext.Remove(twoDigitSystem);
            _dbContext.SaveChanges();

            return true;
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
            if (twoDigitSystem is null) return null;
            var twoDigitSystemsDto = _mapper.Map<TwoDigitSystemDto>(twoDigitSystem);
            return twoDigitSystemsDto;
        }

        public bool Update(int id, CreateTwoDigitSystemDto createTwoDigitSystemDto)
        {
            var twoDigitSystem = _dbContext.
                TwoDigitSystems
                .Include(r => r.TwoDigitElements)
                .FirstOrDefault(r => r.Id == id);
            if (twoDigitSystem is null) return false;
            twoDigitSystem.TwoDigitElements = _mapper.Map<List<TwoDigitElement>>( createTwoDigitSystemDto.TwoDigitElements);

            _dbContext.SaveChanges();

            return true;

        }
    }
}
