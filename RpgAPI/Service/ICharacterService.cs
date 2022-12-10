﻿using RpgAPI.Dtos.Character;
using RpgAPI.Dtos.Skill;

namespace RpgAPI.Service
{
    public interface ICharacterService
    {
        //Get
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        //Get
        Task<ServiceResponse<GetCharacterDto>> GetSingleCharacterById(int id);
        //Post
        Task<ServiceResponse<List<GetCharacterDto>>> AddNewCharacter(AddCharacterDto newCharacter);
        //Put
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter);
        //Delete
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
        //Post
        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto addCharacterSkill);
    }
}
