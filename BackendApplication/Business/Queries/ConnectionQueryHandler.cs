using Business.Cqrs;
using Business.Validators;
using AutoMapper;
using Business.Services;
using Infrastructure.DbContext;
using Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Schemes.Dtos;
using Schemes.Mediatr;

namespace Business.Queries;

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