using SuperProjectPE.WEB.Model;


namespace SuperProjectPE.WEB.API;

public interface IIdentityService
{
    Task LoginAsync(LoginRequest request);
    string? GetTokenFromSession();
}