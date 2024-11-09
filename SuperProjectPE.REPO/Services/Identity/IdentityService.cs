using System.Security.Claims;
using SuperProjectPE.BO;
using SuperProjectPE.DAO;
using SuperProjectPE.REPO.Abstract;

namespace SuperProjectPE.REPO.Services.Identity;

public class IdentityService : IIdentityServices
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IBaseDAO<BO.BranchAccount> _branchAccountDAO;

    public IdentityService(IJwtTokenService jwtTokenService, IBaseDAO<BranchAccount> branchAccountDao)
    {
        _jwtTokenService = jwtTokenService;
        _branchAccountDAO = branchAccountDao;
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _branchAccountDAO.FindSingleAsync(x => x.EmailAddress.Equals(email));

        if (user is null)
        {
            throw new Exception("Null");
        }

        if (!user.AccountPassword.Equals(password))
        {
            throw new Exception("Forbidden");
        }
        
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.EmailAddress ?? "default"),
            new Claim(ClaimTypes.Role, user.Role.ToString()!),
            new Claim("Role", user.Role.ToString()!),
            new Claim("UserId", user.AccountId.ToString()!),
            new Claim(ClaimTypes.Name, user.EmailAddress ?? "default"),
            new Claim(ClaimTypes.Expired, TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow.AddMinutes(120), vietnamTimeZone).ToString())
        };
        
        var accessToken = _jwtTokenService.GenerateAccessToken(claims);

        return accessToken;
    }
}