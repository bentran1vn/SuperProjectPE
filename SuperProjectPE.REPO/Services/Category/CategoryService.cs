using Microsoft.EntityFrameworkCore;
using SuperProjectPE.DAO;
using SuperProjectPE.REPO.Abstract;

namespace SuperProjectPE.REPO.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly IBaseDAO<SuperProjectPE.BO.Category> _categoryDAO;

    public CategoryService(IBaseDAO<BO.Category> categoryDao)
    {
        _categoryDAO = categoryDao;
    }

    public async Task<List<Response.Category>> GetAllCate()
    {
        var list = await _categoryDAO.FindAll(null).ToListAsync();
        return list.Select(x => new Response.Category(x.CategoryId, x.CategoryName, x.CategoryDescription, x.FromCountry ?? "VietNam")).ToList();
    }
}