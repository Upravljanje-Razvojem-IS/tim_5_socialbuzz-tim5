using LajkMikroservis.DTOs.LikeDTO;
using System;
using System.Collections.Generic;

namespace LajkMikroservis.Interfaces
{
    public interface ILikeRepository
    {
        List<LikeReadDto> Get();
        LikeReadDto Get(Guid id);
        LikeConfirmationDto Create(LikeCreateDto dto);
        LikeConfirmationDto Update(Guid id, LikeCreateDto dto);
        void Delete(Guid id);
        List<LikeReadDto> GetAllByUserId(int userId);
        List<LikeReadDto> GetAllByPostId(int postId);
    }
}
