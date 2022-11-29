namespace RpgAPI.Service
{
    public class CharacterService : ICharacterService
    {

        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character() {Id = 1, Name = "Mahald"}
        };

        public async Task<ServiceResponse<List<Character>>> AddNewCharacter(Character newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<Character>>();

            characters.Add(newCharacter);
            serviceResponse.Data = characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            return new ServiceResponse<List<Character>> { Data = characters };
        }

        public async Task<ServiceResponse<Character>> GetSingleCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<Character>();
            var character = characters.FirstOrDefault(c => c.Id == id);

            serviceResponse.Data = character;
            return serviceResponse;
        }
    }
}
