using CommentService.DTOs;
using CommentService.Models;
using AutoMapper;

namespace CommentService.Profiles
{
    public class CommentMapper : Profile
    {
        public CommentMapper()
        {
            CreateMap<Comment, CommentReadDto>();
            CreateMap<CommentCreateDto, Comment>();
            CreateMap<CommentUpdateDto, Comment>();
        }
    }
}
