using PorukaService.DTOs;
using System;
using System.Collections.Generic;

namespace PorukaService.Interfaces
{
    public interface IMessageRepository
    {
        List<MessageReadDto> Get();
        MessageReadDto Get(Guid id);
        MessageConfirmationDto Create(MessageCreateDto dto);
        MessageConfirmationDto Update(Guid id, MessageCreateDto dto);
        void Delete(Guid id);
        List<MessageReadDto> MessagesBetweenTwoUsers(int userOne, int userTwo);
        List<MessageReadDto> AllSentByUser(int userId);
    }
}
