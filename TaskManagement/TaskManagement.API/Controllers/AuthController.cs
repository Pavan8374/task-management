using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Models;
using TaskManagement.Application.DTOs.Auth;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.API.Controllers
{
    /// <summary>
    /// Auth controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Auth controller constructor
        /// </summary>
        /// <param name="authService">Auth service</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Sign up account
        /// </summary>
        /// <param name="signupRequest">Sign up request model</param>
        /// <returns></returns>
        [HttpPost("signup")]
        [ProducesResponseType(typeof(SignInResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpRequest signupRequest)
        {
            var response = await _authService.SignUpAsync(signupRequest);
            return Ok(ApiResponse<SignInResponseModel>.SuccessResponse(response));
        }

        /// <summary>
        /// Sign in account 
        /// </summary>
        /// <param name="signInRequest">Sign in request model</param>
        /// <returns></returns>
        [HttpPost("signin")]
        [ProducesResponseType(typeof(SignInResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest signInRequest)
        {
            var response = await _authService.SignInAsync(signInRequest);
            return Ok(ApiResponse<SignInResponseModel>.SuccessResponse(response));
        }

    }
}
