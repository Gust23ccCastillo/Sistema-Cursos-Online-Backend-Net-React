using Domain.Entities;

namespace Application.Contracts
{
    public interface IJwtGenerateContract
    {
        string CreateTokenSecurityApplication(UserApplication userApplication);
    }
}
