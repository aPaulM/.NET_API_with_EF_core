using AutoMapper;
using RpgAPI.Dtos.Character;
using RpgAPI.Dtos.Skill;
using RpgAPI.Dtos.Weapon;

namespace RpgAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
        }

    }
}
