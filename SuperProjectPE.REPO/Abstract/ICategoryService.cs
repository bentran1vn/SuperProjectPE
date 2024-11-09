using SuperProjectPE.REPO.Services.Category;

namespace SuperProjectPE.REPO.Abstract;

public interface ICategoryService
{
    Task<List<Response.Category>> GetAllCate();
}