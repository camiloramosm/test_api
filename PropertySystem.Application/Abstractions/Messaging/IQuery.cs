using PropertySystem.Domain.Abstractions;
using MediatR;

namespace PropertySystem.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}
