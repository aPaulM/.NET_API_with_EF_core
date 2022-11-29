﻿using RpgAPI.Dto;

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
    }
}
