using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RpgAPI.Dtos.Character;
using RpgAPI.Dtos.Skill;
using RpgAPI.Service;
using System.Security.Claims;

namespace RpgAPI.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAllCharacters()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("GetSingle/{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingleCharacterById(int id)
        {
            var serviceResponse = await _characterService.GetSingleCharacterById(id);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return serviceResponse;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddNewCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.AddNewCharacter(newCharacter));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = await _characterService.UpdateCharacter(updatedCharacter);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id)
        {
            var serviceResponse = await _characterService.DeleteCharacter(id);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpPost("AddSkill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            return Ok(await _characterService.AddCharacterSkill(newCharacterSkill));
        }
    }
}
