using AutoMapper;
using Infrastructure.Entities;
using Schemes.Dtos;

namespace Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
    
        CreateMap<Connection, ConnectionResponse>();
        
        // CreateMap<CreateUrlConnectionRequest, Connection>();
        // CreateMap<CreateHostConnectionRequest, Connection>();
        // CreateMap<UpdateUrlConnectionRequest, Connection>();
        // CreateMap<UpdateHostConnectionRequest, Connection>();
        
        

    }
}