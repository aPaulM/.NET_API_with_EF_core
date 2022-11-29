using AutoMapper;
using RpgAPI.Dto;

namespace RpgAPI.Service
{
    public class CharacterService : ICharacterService
    {

        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character() {Id = 1, Name = "Mahald"}
        };

        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddNewCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            return new ServiceResponse<List<GetCharacterDto>> 
            {
                Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()
            };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetSingleCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.Id == id);

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                Character character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

                _mapper.Map(updatedCharacter, character);
                // The line above REPLACES all the code below... With a single line of code using an AutoMapper!!!
                /*
                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;
                */

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch(Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }
    }
}
