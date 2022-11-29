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
        public async Task<ActionResult<List<Character>>> GetAllCharacters()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetSingleCharacterById(int id)
        {
            return Ok(await _characterService.GetSingleCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> AddNewCharacter(Character newCharacter)
        {
            return Ok(await _characterService.AddNewCharacter(newCharacter));
        }

    }
}
