using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using SuperProjectPE.BO;

namespace SuperProjectPE.API.Extensions;

public static class OdataExtension
{
    public static void AddOdataServices(this IServiceCollection services)
    {
        services.AddControllers()
            .AddOData(options => options
                .AddRouteComponents("odata", GetEdmModel())
                .Select()
                .Filter()
                .OrderBy()
                .SetMaxTop(100)
                .Count()
                .Expand())
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });
    }
    
    public static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();
        builder.EntitySet<BranchAccount>("BranchAccounts")
            .EntityType.HasKey(b => b.AccountId);  // Explicitly set key
        builder.EntitySet<Category>("Categories")
            .EntityType.HasKey(c => c.CategoryId); // Explicitly set key
        builder.EntitySet<SilverJewelry>("SilverJewelries")
            .EntityType.HasKey(s => s.SilverJewelryId); // Explicitly set key
        return builder.GetEdmModel();
    }
}