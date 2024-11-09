using Microsoft.EntityFrameworkCore;
using SuperProjectPE.DAO;
using SuperProjectPE.REPO.Abstract;

namespace SuperProjectPE.REPO.Services.SilverJewelry;

public class SilverJewelryService : ISilverJewelryService
{
    private readonly IBaseDAO<BO.SilverJewelry> _silverJewelryDAO;
    private readonly IBaseDAO<BO.Category> _categoryDAO;

    public SilverJewelryService(IBaseDAO<BO.SilverJewelry> silverJewelryDao, IBaseDAO<BO.Category> categoryDao)
    {
        _silverJewelryDAO = silverJewelryDao;
        _categoryDAO = categoryDao;
    }

    public async Task CreateSJ(Request.Create request)
    {
        var sJE = await _silverJewelryDAO.FindSingleAsync(x =>
            x.SilverJewelryId.Equals(request.SilverJewelryId));

        if (sJE is not null) throw new Exception("Siv Je Exist !");
        
        var cate = await _categoryDAO.FindSingleAsync(x =>
            x.CategoryId.Equals(request.CategoryId));

        if (cate is null) throw new Exception("cate is not Exist !");

        var sv = new BO.SilverJewelry()
        {
            SilverJewelryId = request.SilverJewelryId,
            CategoryId = request.CategoryId,
            Price = request.Price,
            MetalWeight = request.MetalWeight,
            SilverJewelryDescription = request.SilverJewelryDescription,
            ProductionYear = request.ProductionYear,
            SilverJewelryName = request.SilverJewelryName,
            CreatedDate = request.CreatedDate
        };
        
        _silverJewelryDAO.Add(sv);
        await _silverJewelryDAO.SaveChangesAsync();
    }

    public async Task DeleteSJ(string id)
    {
        var sJE = await _silverJewelryDAO.FindSingleAsync(x =>
            x.SilverJewelryId.Equals(id));

        if (sJE is null) throw new Exception("Siv Je is not Exist !");
        
        _silverJewelryDAO.Remove(sJE);
        await _silverJewelryDAO.SaveChangesAsync();
    }

    public async Task<List<BO.SilverJewelry>> GetAll(string? searchTerm)
    {
        var query = string.IsNullOrWhiteSpace(searchTerm)
            ? _silverJewelryDAO.FindAll(null, x => x.Category)
            : _silverJewelryDAO.FindAll(
                x => x.SilverJewelryName.ToLower().Contains(searchTerm.ToLower()) || 
                     x.MetalWeight.ToString().Contains(searchTerm)
                , x => x.Category);

        var list = await query.ToListAsync();
        
        return list;

        // var result = list.Select(x => new Request.Create()
        // {
        //     SilverJewelryId = x.SilverJewelryId,
        //     CategoryId = x.Category.CategoryId,
        //     Price = x.Price ?? 0,
        //     MetalWeight = x.MetalWeight ?? 0,
        //     CreatedDate = x.CreatedDate ?? DateTime.Now,
        //     ProductionYear = x.ProductionYear ?? 0,
        //     SilverJewelryName = x.SilverJewelryName,
        //     SilverJewelryDescription = x.SilverJewelryDescription
        // }).ToList();
        //
        // return result;
    }

    public async Task<Request.Create> GetById(string id)
    {
        var sJE = await _silverJewelryDAO.FindSingleAsync(x =>
            x.SilverJewelryId.Equals(id));

        if (sJE is null) throw new Exception("Siv Je is not Exist !");

        return new Request.Create()
        {
            SilverJewelryId = sJE.SilverJewelryId,
            SilverJewelryDescription = sJE.SilverJewelryDescription,
            SilverJewelryName = sJE.SilverJewelryName,
            CategoryId = sJE.CategoryId,
            Price = sJE.Price ?? 0,
            CreatedDate = sJE.CreatedDate ?? DateTime.Now,
            ProductionYear = sJE.ProductionYear ?? 0,
            MetalWeight = sJE.MetalWeight
        };
    }

    public async Task UpdateSJ(Request.Create request)
    {
        var sJE = await _silverJewelryDAO.FindSingleAsync(x =>
            x.SilverJewelryId.Equals(request.SilverJewelryId));

        if (sJE is null) throw new Exception("Siv Je is not Exist !");
        
        var cate = await _categoryDAO.FindSingleAsync(x =>
            x.CategoryId.Equals(request.CategoryId));

        if (cate is null) throw new Exception("cate is not Exist !");

        sJE.CategoryId = request.CategoryId;
        sJE.Price = request.Price;
        sJE.MetalWeight = request.MetalWeight;
        sJE.SilverJewelryDescription = request.SilverJewelryDescription;
        sJE.ProductionYear = request.ProductionYear;
        sJE.SilverJewelryName = request.SilverJewelryName;
        sJE.CreatedDate = request.CreatedDate;
        
        await _silverJewelryDAO.SaveChangesAsync();
    }
}