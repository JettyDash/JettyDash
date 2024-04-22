using Business.Cqrs;
using AutoMapper;
using Infrastructure.DbContext;
using Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Schemes.Dto;
using Schemes.Mediatr;

namespace Business.Query;

public class ConnectionQueryHandler(BackendDbContext dbContext, IMapper mapper)
    : IAsyncQueryHandler<GetAllConnectionQuery, ApiResponse<List<ConnectionResponse>>>

{
    public async Task<ApiResponse<List<ConnectionResponse>>> Handle(GetAllConnectionQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Connection>()
        .Where(conn => conn.UserId == request.Context.UserId)
        .ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<Connection>, List<ConnectionResponse>>(list);
        
        return new ApiResponse<List<ConnectionResponse>>(mappedList);
    }
    
}