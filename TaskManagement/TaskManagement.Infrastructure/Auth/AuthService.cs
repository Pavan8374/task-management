using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using TaskManagement.Application.Common;
using TaskManagement.Application.DTOs.Auth;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Infrastructure.Auth
{
    /// <summary>
    /// Auth service
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Auth service constructor
        /// </summary>
        /// <param name="userService">User service</param>
        /// <param name="configuration">Configuration</param>
        public AuthService(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        /// <summary>
        /// Sign up account
        /// </summary>
        /// <param name="signupRequest">Signup request</param>
        /// <returns></returns>
        /// <exception cref="BusinessException">Business exception</exception>
        public async Task<SignInResponseModel> SignUpAsync(SignUpRequest signupRequest)
        {
            if (signupRequest is null)
                throw new BusinessException("Request body empty");

            var existingUser = await _userService.GetUserByEmailAsync(signupRequest.Email);
            if (existingUser is not null)
                throw new BusinessException("Already an existing account with this email");

            string salt = SharedUtils.GenerateSalt();
            string passwordHash = SharedUtils.HashPassword(signupRequest.Password, salt);
            var user = new User()
            {
                Email = signupRequest.Email,
                FullName = signupRequest.FullName,
                RoleId = signupRequest.IsAdmin ? (int)RoleEnum.Admin : (int)RoleEnum.User,
                Salt = salt,
                PasswordHash = passwordHash,
            };
            await _userService.AddAsync(user);
            return LoginResponse(user);
        }

        /// <summary>
        /// Sign in account
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException">Not found exception</exception>
        /// <exception cref="ValidationException">Validation exception</exception>
        public async Task<SignInResponseModel> SignInAsync(SignInRequest request)
        {
            var user = await _userService.GetUserByEmailAsync(request.Email);

            if (user is null)
                throw new NotFoundException("User not found with this email.");

            var isPasswordValid = SharedUtils.VerifyPassword(request.Password, user.PasswordHash, user.Salt);

            if (!isPasswordValid)
                throw new ValidationException("Invalid email or password.");

            return LoginResponse(user);
        }

        private SignInResponseModel LoginResponse(User user)
        {
            var claims = SharedUtils.GetTokenClaims(user.Email, user.Id.ToString(), user.FullName, GetRoleNameFromId(user.RoleId));

            var token = SharedUtils.GetJWTToken(
                    claims,
                    _configuration["JWT:Secret"],
                    _configuration["JWT:ValidIssuer"],
                    _configuration["JWT:ValidAudience"],
                    _configuration["JWT:JWTExpiryDays"]
                );

            return new SignInResponseModel
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                RoleName = GetRoleNameFromId(user.RoleId),
                Expiration = token.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };
        }

        private string GetRoleNameFromId(int roleId)
        {
            if (Enum.IsDefined(typeof(RoleEnum), roleId))
            {
                return ((RoleEnum)roleId).ToString();
            }

            return "Unknown";
        }

    }
}
