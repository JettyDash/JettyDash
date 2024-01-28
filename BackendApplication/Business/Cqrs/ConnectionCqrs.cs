using MediatR;

namespace Business.Cqrs;



public record DeleteAllConnectionCommand(int ExpenseRequestId) : IRequest<ExpenseResponse>;


public record GetAllConnectionQuery() : IRequest<List<ExpenseResponse>>;

// public record GetAllConnectionByParameterQuery(GetExpenseByParameterRequest Model) : IRequest<List<ExpenseResponse>>;
