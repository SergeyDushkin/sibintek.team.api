using System.Collections.Generic;
using AutoMapper;
using sibintek.team.dto;

namespace sibintek.team.mapping
{
    public class MappingProfile : Profile {
        public MappingProfile() {
            
            CreateMap<ConversationDto, ConversationHeaderDto>();
            
            CreateMap<Conversation, ConversationHeaderDto>()
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.OuterKey));

            CreateMap<Conversation, ConversationDto>()
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.OuterKey));

            CreateMap<ConversationUser, ConversationUserDto>();
            
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.OuterKey));
            
            //CreateMap<User, CreateUserCommand>()
            //    .ForMember(dest => dest.Id, source => source.MapFrom(src => src.OuterKey))
            //    .ForMember(dest => dest.Thumbnail, source => source.MapFrom(src => src.Links.Thumbnail))
            //    .ForMember(dest => dest.Image, source => source.MapFrom(src => src.Links.Image));

            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.OuterKey, source => source.MapFrom(src => src.Id))
                .ForPath(dest => dest.Links.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail))
                .ForPath(dest => dest.Links.Image, opt => opt.MapFrom(src => src.Image));
                //.IncludeMembers(s => s.Links);
        }
    }

    public static class MeppingExtension
    {
        public static IEnumerable<TDestination> Map<TSource, TDestination>(this IEnumerable<TSource> source, IMapper maper)
        {
            return maper.Map<IEnumerable<TDestination>>(source);
        }
    }
}
