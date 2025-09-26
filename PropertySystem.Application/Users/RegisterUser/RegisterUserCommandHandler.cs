using PropertySystem.Application.Abstractions.Authentication;
using PropertySystem.Application.Abstractions.Messaging;
using PropertySystem.Domain.Abstractions;
using PropertySystem.Domain.Users;

namespace PropertySystem.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(
        IAuthenticationService authenticationService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _authenticationService = authenticationService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(new FirstName(request.FirstName),
            new SecondName(request.SecondName),
            new LastName(request.LastName),
            new FullName(request.FullName),
            new UserContact(request.Phone, request.Address),
            new BusinessUnit(request.BusinessUnit),
            new Password(request.Password),
            new Role(request.Role),
            new Identification(request.Identification),
            new Email(request.Email));

        var identityId = await _authenticationService.RegisterAsync(user, cancellationToken);

        user.SetIdentityId(identityId);

        _userRepository.Add(user);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
