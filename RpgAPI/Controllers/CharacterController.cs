using Microsoft.AspNetCore.Mvc;

namespace RpgAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private static Character Knight = new Character();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Knight);
        }


    }
}
