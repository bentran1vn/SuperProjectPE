using SuperProjectPE.REPO.Services.SilverJewelry;

namespace SuperProjectPE.REPO.Abstract;

public interface ISilverJewelryService
{
    Task CreateSJ(Request.Create request);
    Task UpdateSJ(Request.Create request);
    Task DeleteSJ(string id);
    Task<List<Request.Create>> GetAll(string? searchTerm);
    Task<Request.Create> GetById(string id);
}