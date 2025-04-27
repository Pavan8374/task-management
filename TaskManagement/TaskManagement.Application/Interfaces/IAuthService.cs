using TaskManagement.Application.DTOs.Auth;

namespace TaskManagement.Application.Interfaces
{
    /// <summary>
    /// Auth service interface
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Signup account
        /// </summary>
        /// <param name="signupRequest">Signup request</param>
        /// <returns>Signin response model</returns>
        public Task<SignInResponseModel> SignUpAsync(SignUpRequest signupRequest);

        /// <summary>
        /// Signin account
        /// </summary>
        /// <param name="request">Signin request</param>
        /// <returns>Sigin response model</returns>
        public Task<SignInResponseModel> SignInAsync(SignInRequest request);
    }
}


