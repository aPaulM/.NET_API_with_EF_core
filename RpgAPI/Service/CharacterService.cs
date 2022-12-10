using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RpgAPI.Data;
using RpgAPI.Dtos.Character;
using RpgAPI.Dtos.Skill;
using System.Security.Claims;

namespace RpgAPI.Service
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

      //-----------------------------------------------------------------------------------------------------------
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));

      //-----------------------------------------------------------------------------------------------------------

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddNewCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.User = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            _dataContext.Characters.Add(character);
            await _dataContext.SaveChangesAsync();
            serviceResponse.Data = await _dataContext.Characters
                .Where(c => c.User.Id == GetUserId())
                .Select(c => _mapper.Map<GetCharacterDto>(c))
                .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {

            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _dataContext.Characters
                .Where(c => c.User.Id == GetUserId())
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            return serviceResponse;
           
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetSingleCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var dbCharacter = await _dataContext.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstAsync(c => c.Id == id && c.User.Id == GetUserId());

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            }
            catch(Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {

                var dbCharacter = await _dataContext.Characters
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

                if (dbCharacter.User.Id == GetUserId())
                {
                    // _mapper.Map(updatedCharacter, character);
                    // The line above REPLACES all the code below... With a single line of code using an AutoMapper!!!

                    dbCharacter.Name = updatedCharacter.Name;
                    dbCharacter.HitPoints = updatedCharacter.HitPoints;
                    dbCharacter.Strength = updatedCharacter.Strength;
                    dbCharacter.Defense = updatedCharacter.Defense;
                    dbCharacter.Intelligence = updatedCharacter.Intelligence;
                    dbCharacter.Class = updatedCharacter.Class;

                    await _dataContext.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
                    serviceResponse.Message = "Character has been updated.";
                }
            }
            catch(Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                var dbCharacter = await _dataContext.Characters
                    .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());

                if (dbCharacter != null)
                {
                    _dataContext.Characters.Remove(dbCharacter);
                    await _dataContext.SaveChangesAsync();
                    serviceResponse.Data = await _dataContext.Characters
                        .Where(c => c.User.Id == GetUserId())
                        .Select(c => _mapper.Map<GetCharacterDto>(c))
                        .ToListAsync();

                    serviceResponse.Message = "Character with id: " + "(" + id + ")" + " has been deleted.";
                } 
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character Not Found!";
                }
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto addCharacterSkill)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var dbCharacter = await _dataContext.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills) //.ThenInclude(s => s.blah blah)
                    .FirstOrDefaultAsync(c => c.Id == addCharacterSkill.CharacterId &&
                    c.User.Id == GetUserId());

                if (dbCharacter == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character Not Found.";
                    return serviceResponse;
                }
                var skill = await _dataContext.Skills
                    .FirstOrDefaultAsync(s => s.Id == addCharacterSkill.SkillId);
                if (skill == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Skill Doesn't Exist.";
                    return serviceResponse;
                }
                dbCharacter.Skills.Add(skill);
                await _dataContext.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
                serviceResponse.Message = "Skill had been equipped.";

            }
            catch (Exception e) 
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }
            return serviceResponse;
        }
    }
} 