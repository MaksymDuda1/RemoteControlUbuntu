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
        CreateMap<Command, CommandDto>().ReverseMap()
            .ForMember(c => c.User,
                opt => opt.Ignore());
        CreateMap<AddCommandDto, Command>();
    }
}