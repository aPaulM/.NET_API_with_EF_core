namespace RpgAPI.Service
{
    public class CharacterService : ICharacterService
    {

        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character() {Id = 1, Name = "Mahald"}
        };

        public List<Character> AddNewCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return characters;
        }

        public List<Character> GetAllCharacters()
        {
            return characters;
        }

        public Character GetSingleCharacterById(int id)
        {
            return characters.FirstOrDefault(c => c.Id == id);
        }
    }
}
