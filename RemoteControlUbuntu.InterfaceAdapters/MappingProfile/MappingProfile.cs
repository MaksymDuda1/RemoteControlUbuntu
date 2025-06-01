using AutoMapper;
using RemoteControlUbuntu.Domain.DTOs;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Infrastructure.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Connection, ConnectionDto>().ReverseMap();
        CreateMap<AddConnectionDto, Connection>().ReverseMap();
        CreateMap<AddCommandDto, Command>();
    }
}