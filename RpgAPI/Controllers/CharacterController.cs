using Microsoft.AspNetCore.Mvc;
using RpgAPI.Service;

namespace RpgAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {

        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }


        [HttpGet("GetAll")]
        public ActionResult<List<Character>> GetAllCharacters()
        {
            return Ok(_characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetSingleCharacterById(int id)
        {
            return Ok(_characterService.GetSingleCharacterById(id));
        }

        [HttpPost]
        public ActionResult<List<Character>> AddNewCharacter(Character newCharacter)
        {
            return Ok(_characterService.AddNewCharacter(newCharacter));
        }

    }
}
