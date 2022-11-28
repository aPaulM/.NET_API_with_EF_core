namespace RpgAPI.Service
{
    public interface ICharacterService
    {
        //Get
        List<Character> GetAllCharacters();
        //Get
        Character GetSingleCharacterById(int id);
        //Post
        List<Character> AddNewCharacter(Character newCharacter);
    }
}
