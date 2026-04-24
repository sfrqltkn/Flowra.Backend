using Europower.EuroScada.Application.Features.Queries.Auth.GetCurrentUser;
using Flowra.Backend.Application.Features.Commands.Auth.ChangePassword;
using Flowra.Backend.Application.Features.Commands.Auth.ConfirmEmail;
using Flowra.Backend.Application.Features.Commands.Auth.ForgotPassword;
using Flowra.Backend.Application.Features.Commands.Auth.Login;
using Flowra.Backend.Application.Features.Commands.Auth.Logout;
using Flowra.Backend.Application.Features.Commands.Auth.RefreshToken;
using Flowra.Backend.Application.Features.Commands.Auth.Register;
using Flowra.Backend.Application.Features.Commands.Auth.ResendConfirmationEmail;
using Flowra.Backend.Application.Features.Commands.Auth.ResetPassword;
using Flowra.Backend.Presentation.Abstractions;
using Flowra.Backend.WebAPI.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flowra.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Authentication")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITokenCookieService _tokenCookieService;

        public AuthController(
            IMediator mediator,
            ITokenCookieService tokenCookieService)
        {
            _mediator = mediator;
            _tokenCookieService = tokenCookieService;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCurrentUserQueryRequest(), cancellationToken);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(TokenCookieFilter))]
        public async Task<IActionResult> Login([FromBody] LoginCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        [ServiceFilter(typeof(TokenCookieFilter))]
        public async Task<IActionResult> RefreshToken()
        {
            var request = new RefreshTokenCommandRequest
            {
                RefreshToken = _tokenCookieService.GetRefreshToken() ?? string.Empty
            };

            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("logout")]
        [Authorize]
        [ServiceFilter(typeof(TokenCookieFilter))]
        public async Task<IActionResult> Logout()
        {
            var result = await _mediator.Send(new LogoutCommandRequest());
            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("reset-password")]
        [ServiceFilter(typeof(TokenCookieFilter))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("change-password")]
        [Authorize]
        [ServiceFilter(typeof(TokenCookieFilter))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
