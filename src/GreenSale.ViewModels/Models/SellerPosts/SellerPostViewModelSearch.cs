using GreenSale.ViewModels.Enums.SellerPost;

namespace GreenSale.ViewModels.Models.SellerPosts;

public class SellerPostViewModelSearch
{
    public long Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public long UserId { get; set; }
    public string UserPhoneNumber { get; set; } = string.Empty;
    public string PostPhoneNumber { get; set; } = string.Empty;
    public string UserRegion { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Capacity { get; set; }
    public string CapacityMeasure { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public SellerPostEnum Status { get; set; }
    public List<SellerPostImage> PostImages { get; set; }
    public string MainImage { get; set; } = string.Empty;
    public double AverageStars { get; set; }
}
