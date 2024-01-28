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

namespace Business.Queries;

public class ConnectionQueryHandler :
    IRequestHandler<GetAllConnectionQuery, ApiResponse<List<ConnectionResponse>>>

{
    private readonly BackendDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IHandlerValidator validate;
    private readonly IUserService userService;
    
    public ConnectionQueryHandler(BackendDbContext dbContext, IMapper mapper, IHandlerValidator validate, IUserService userService)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.validate = validate;
        this.userService = userService;
    }

    public async Task<ApiResponse<List<ConnectionResponse>>> Handle(GetAllConnectionQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Connection>()
        .Where(conn => conn.UserId == userService.GetUserId())
        .ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<Connection>, List<ConnectionResponse>>(list);
        
        return new ApiResponse<List<ConnectionResponse>>(mappedList);
    }
}