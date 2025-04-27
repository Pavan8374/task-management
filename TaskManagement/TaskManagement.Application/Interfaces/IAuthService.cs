using TaskManagement.Application.DTOs.Auth;

namespace TaskManagement.Application.Interfaces;

public interface IAuthService   
{
    public Task<SignInResponseModel> SignUpAsync(SignUpRequest signupRequest);
    public Task<SignInResponseModel> SignInAsync(SignInRequest request);
}
