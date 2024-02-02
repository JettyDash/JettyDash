using AutoMapper;
using Infrastructure.Entities;
using Schemes.DTOs;
using Schemes.Enums;

namespace Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<CreateHostConnectionRequest, Connection>()
            .ForMember(databaseName => databaseName.DatabaseName, opt => opt.MapFrom(src => src.DatabaseName))
            .ForMember(databaseType => databaseType.DatabaseType, opt => opt.MapFrom(src => src.DatabaseType))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ConnectionStatus.Connected))
            .ForMember(dest => dest.ConnectionType, opt => opt.MapFrom(src => ConnectionType.Host));
    
        CreateMap<Connection, ConnectionResponse>();
        
        CreateMap<CreateUserRequest, User>()
            .ForMember(dest => dest.PasswordRetryCount,
                opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.IsActive,
                opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.LastActivityDateTime,
                opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.IsActive,
                opt => opt.MapFrom(src => src.IsActive ? Constants.UserStatus.Active : Constants.UserStatus.Inactive))
            .ForMember(src => src.LastActivityDateTime,
                opt => opt.MapFrom(src => src.LastActivityDateTime.ToString(Constants.DateFormats.DateTimeFormat)));

    }
}