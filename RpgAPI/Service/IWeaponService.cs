using RpgAPI.Dtos.Character;
using RpgAPI.Dtos.Weapon;

namespace RpgAPI.Service
{
    public interface IWeaponService
    {

        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);

    }
}
