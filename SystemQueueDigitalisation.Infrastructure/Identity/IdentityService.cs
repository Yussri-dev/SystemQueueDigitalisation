using Microsoft.AspNetCore.Identity;
using SystemQueueDigitalisation.Application.Dtos;
using SystemQueueDigitalisation.Application.Interfaces.Services;
using SystemQueueDigitalisation.Web.Requests;

namespace SystemQueueDigitalisation.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> AuthenticateAsync(LoginDto request)
        {
            var result = await _signInManager.PasswordSignInAsync(
                request.Email,
                request.Password,
                isPersistent: false,
                lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                return new AuthResponseDto { IsSuccess = false, ErrorMessage = "Login failed" };
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            var roles = await _userManager.GetRolesAsync(user);

            // Générer le token JWT
            var token = _tokenService.GenerateJwtToken(user.Id, user.UserName, roles);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = token,
                UserName = user.UserName,
                Roles = roles.ToList()
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            return new AuthResponseDto
            {
                IsSuccess = true,
                UserName = user.UserName,
                Token = "mock_token_or_generate_one",
                Roles = new List<string> { "User" }
            };
        }

    }
}
