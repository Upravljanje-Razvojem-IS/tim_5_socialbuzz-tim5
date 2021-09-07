using AutoMapper;
using PorukaService.Data;
using PorukaService.DTOs;
using PorukaService.Entities;
using PorukaService.Interfaces;
using PorukaService.Logger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PorukaService.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly FakeLogger _logger;

        public MessageRepository(FakeLogger logger, IMapper mapper, DatabaseContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public MessageConfirmationDto Create(MessageCreateDto dto)
        {
            User sender = UserData.Users.FirstOrDefault(e => e.Id == dto.SenderId);

            if (sender == null)
                throw new Exception("User does not exit");

            User reciver = UserData.Users.FirstOrDefault(e => e.Id == dto.ReciverId);

            if (reciver == null)
                throw new Exception("User does not exit");

            Message message = new Message()
            {
                Id = Guid.NewGuid(),
                Content = dto.Content,
                IsSeen = dto.IsSeen,
                ReciverId = dto.ReciverId,
                SenderId = dto.SenderId
            };

            _context.Messages.Add(message);
            _context.SaveChanges();

            _logger.Log("Message created");

            return _mapper.Map<MessageConfirmationDto>(message);
        }

        public void Delete(Guid id)
        {
            var messsages = _context.Messages.FirstOrDefault(e => e.Id == id);

            if (messsages == null)
                throw new Exception("Message with provided id does not exist");

            _context.Messages.Remove(messsages);
            _context.SaveChanges();

            _logger.Log("Message deleted");
        }

        public List<MessageReadDto> Get()
        {
            var list = _context.Messages.ToList();

            _logger.Log("Message list fetched");

            return _mapper.Map<List<MessageReadDto>>(list);
        }

        public MessageReadDto Get(Guid id)
        {
            var message = _context.Messages.FirstOrDefault(e => e.Id == id);

            _logger.Log("Message fetched");

            return _mapper.Map<MessageReadDto>(message);
        }

        public List<MessageReadDto> MessagesBetweenTwoUsers(int userOne, int userTwo)
        {
            User firstUser = UserData.Users.FirstOrDefault(e => e.Id == userOne);

            if (firstUser == null)
                throw new Exception("User does not exit");

            User secondUser = UserData.Users.FirstOrDefault(e => e.Id == userTwo);

            if (secondUser == null)
                throw new Exception("User does not exit");

            var list = _context.Messages.Where(e => (e.SenderId == userOne && e.ReciverId == userTwo) || (e.SenderId == userTwo && e.ReciverId == userOne));

            return _mapper.Map<List<MessageReadDto>>(list);
        }

        public List<MessageReadDto> AllSentByUser(int userId)
        {
            User user = UserData.Users.FirstOrDefault(e => e.Id == userId);

            if (user == null)
                throw new Exception("User does not exit");

            var list = _context.Messages.Where(e => e.SenderId == userId);

            return _mapper.Map<List<MessageReadDto>>(list);
        }

        public MessageConfirmationDto Update(Guid id, MessageCreateDto dto)
        {
            var message = _context.Messages.FirstOrDefault(e => e.Id == id);

            if (message == null)
                throw new Exception("Message with provided id does not exist");

            User sender = UserData.Users.FirstOrDefault(e => e.Id == dto.SenderId);

            if (sender == null)
                throw new Exception("User does not exit");

            User reciver = UserData.Users.FirstOrDefault(e => e.Id == dto.ReciverId);

            if (reciver == null)
                throw new Exception("User does not exit");

            message.Content = dto.Content;
            message.IsSeen = dto.IsSeen;
            message.ReciverId = dto.ReciverId;
            message.SenderId = dto.SenderId;

            _context.SaveChanges();

            _logger.Log("Message  updated");

            return _mapper.Map<MessageConfirmationDto>(message);
        }
    }
}
