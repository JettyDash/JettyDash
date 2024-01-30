//
// using MediatR;
// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using MediatR.Pipeline;
// using Schemes.Dtos;
// using Schemes.Enums;
//
// namespace Business.Preprocessor;
//
// public class CreateUrlConnectionRequestPreProcessor : IRequestPreProcessor<CreateUrlConnectionRequest>
// {
//     public Task Process(CreateUrlConnectionRequest request, CancellationToken cancellationToken)
//     {
//         // Convert string DatabaseType to Enum
//         if (!string.IsNullOrEmpty(request.DatabaseType))
//         {
//             if (Enum.TryParse(request.DatabaseType, out DatabaseType databaseType))
//             {
//                 request.DatabaseType = databaseType;
//             }
//             else
//             {
//                 // Handle invalid DatabaseType here if needed
//                 throw new InvalidOperationException("Invalid DatabaseType");
//             }
//         }
//
//         return Task.CompletedTask;
//     }
// }