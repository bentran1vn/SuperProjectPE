namespace SuperProjectPE.REPO.Abstract;

public interface IIdentityServices
{ 
    Task<string> Login(string email, string password);
}