using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = SuperProjectPE.WEB.Model.LoginRequest;

namespace SuperProjectPE.WEB.API;

public class IdentityService : IIdentityService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string LoginUrl = "http://localhost:5259/odata/Identity/login";

    public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(LoginUrl, request);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            _httpContextAccessor.HttpContext.Session.SetString("token", result);
        }
        else
        {
            throw new Exception("Login failed.");
        }
    }
    
    public string? GetTokenFromSession()
    {
        return _httpContextAccessor?.HttpContext?.Session?.GetString("token");
    }
}