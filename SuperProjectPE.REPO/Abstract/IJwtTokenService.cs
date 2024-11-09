using System.Security.Claims;

namespace SuperProjectPE.REPO.Abstract;

public interface IJwtTokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
}