namespace GreenSale.ViewModels.Models.Categories;

public class CategoryViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set;}
}