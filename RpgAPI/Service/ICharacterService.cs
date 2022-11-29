namespace RpgAPI.Service
{
    public interface ICharacterService
    {
        //Get
        Task<ServiceResponse<List<Character>>> GetAllCharacters();
        //Get
        Task<ServiceResponse<Character>> GetSingleCharacterById(int id);
        //Post
        Task<ServiceResponse<List<Character>>> AddNewCharacter(Character newCharacter);
    }
}
