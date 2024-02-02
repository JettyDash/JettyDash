using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using MediatR;

namespace Schemes.Dtos;

// public abstract class ContextualRequest
// {
//     [JsonIgnore]
//     [IgnoreDataMember]
//     public IDictionary<string, object> ItemsCache { get; }
//         = new Dictionary<string, object>();
// }

// public abstract record ContextualRequest<TRequest, TResponse> : IRequest<TResponse>
// {
//     [JsonIgnore]
//     [IgnoreDataMember]
//     public IDictionary<string, object> ItemsCache { get; }
//         = new Dictionary<string, object>();
//
// }


// public class ContextualRequest<TRequest, TResponse> : IRequest<TResponse> where TRequest : IRequest<TResponse>
// {
//     public ContextualRequest(TRequest data)
//     {
//         Data = data;
//     }
// 	
//     public TRequest Data { get; }
// 	
//     public string UserName { get; }
// }



// public class ItemsCache : Dictionary<string, object>
// {
//     public ItemsCache()
//     {
//     }
//
//     public ItemsCache(IDictionary<string, object> dictionary) : base(dictionary)
//     {
//     }
// }
