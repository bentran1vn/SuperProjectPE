using System.Text.Json.Serialization;

namespace SuperProjectPE.BO;

public partial class Category
{
    public string CategoryId { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string CategoryDescription { get; set; } = null!;

    public string? FromCountry { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<SilverJewelry> SilverJewelries { get; set; } = new List<SilverJewelry>();
}
