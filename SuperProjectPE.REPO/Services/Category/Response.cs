namespace SuperProjectPE.REPO.Services.Category;

public static class Response
{
    public record Category(string CategoryId, string CategoryName, string CategoryDescription, string FromCountry);
}