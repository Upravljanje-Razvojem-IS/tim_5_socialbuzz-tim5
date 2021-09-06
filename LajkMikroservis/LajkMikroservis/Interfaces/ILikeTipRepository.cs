using LajkMikroservis.DTOs.LikeTipDTO;
using System;
using System.Collections.Generic;

namespace LajkMikroservis.Interfaces
{
    public interface ILikeTipRepository
    {
        List<LikeTipReadDto> Get();
        LikeTipReadDto Get(Guid id);
        LikeTipConfirmationDto Create(LikeTipCreateDto dto);
        LikeTipConfirmationDto Update(Guid id, LikeTipCreateDto dto);
        void Delete(Guid id);
    }
}
