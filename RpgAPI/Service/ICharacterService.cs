namespace RpgAPI.Service
{
    public interface ICharacterService
    {
        //Get
        Task<List<Character>> GetAllCharacters();
        //Get
        Task<Character> GetSingleCharacterById(int id);
        //Post
        Task<List<Character>> AddNewCharacter(Character newCharacter);
    }
}
