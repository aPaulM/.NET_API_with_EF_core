using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RpgAPI.Data;
using RpgAPI.Dtos.Character;
using RpgAPI.Dtos.Weapon;
using System.Security.Claims;

namespace RpgAPI.Service
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext dataContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _dataContext.Characters
                    .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId &&
                    c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if (character == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character Not Found.";
                    return serviceResponse;
                }
                Weapon weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character = character
                };

                _dataContext.Weapons.Add(weapon);
                await _dataContext.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                serviceResponse.Message = "Weapon has been equipped.";

            } 
            catch(Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }
    }
}
