using PropertySystem.Application.Users.GetLoggedInUser;
using PropertySystem.Application.Users.LogInUser;
using PropertySystem.Application.Users.RegisterUser;
using PropertySystem.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PropertySystem.Api.Controllers.User
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("me")]
        [Authorize(Roles = Roles.Registered)]
        public async Task<IActionResult> GetLoggedInUser(CancellationToken cancellationToken)
        {
            var query = new GetLoggedInUserQuery();

            Result<UserResponse> result = await _sender.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(request.Email, request.FirstName, request.SecondName, request.LastName, request.FullName, request.Password, request.Phone, request.Address, request.BusinessUnit, request.Role, request.Identification);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LogIn(
       LogInUserRequest request,
       CancellationToken cancellationToken)
        {
            var command = new LogInUserCommand(request.Email, request.Password);

            Result<AccessTokenResponse> result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return Unauthorized(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
