using Domain.Models;

namespace Application.Abstract.Services;

public interface IJwtWorker
{
    string? GenerateJwtToken(User user);
}