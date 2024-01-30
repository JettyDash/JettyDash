using AutoMapper;
using Infrastructure.Entities;
using Schemes.Dtos;
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
        
        // CreateMap<CreateUrlConnectionRequest, Connection>();
        // CreateMap<UpdateUrlConnectionRequest, Connection>();
        // CreateMap<UpdateHostConnectionRequest, Connection>();
        
        

    }
}