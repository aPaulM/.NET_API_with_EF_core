﻿using Microsoft.AspNetCore.Mvc;
using RpgAPI.Data;
using RpgAPI.Dtos.User;

namespace RpgAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var serviceResponse = await _authRepo.Register(
                new User { Username = request.Username }, request.Password );

            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse);
            }
            return Ok(serviceResponse);

        }
    }
}
