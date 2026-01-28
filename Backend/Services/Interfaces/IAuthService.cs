using Backend.Data.DTOs;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequest);
    }
}
