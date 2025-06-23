using AutoMapper;
using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Application.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Connection, ConnectionDto>().ReverseMap();
        CreateMap<AddConnectionDto, Connection>().ReverseMap();
        CreateMap<Command, CommandDto>().ReverseMap().ForMember(c => c.User, opt => opt.Ignore());
        CreateMap<AddCommandDto, Command>();
        CreateMap<AddCommandDto, UserCommandsWhiteList>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        CreateMap<AddCommandDto, UserCommandsWhiteList>().ForMember(dest => dest.Command, opt => opt.MapFrom(src => src.TerminalCommand));
        CreateMap<CommandSetDto, CommandSet>();
    }
}