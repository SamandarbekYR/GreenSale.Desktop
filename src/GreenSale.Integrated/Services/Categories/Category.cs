using GreenSale.Integrated.API.Auth;
using GreenSale.Integrated.Interfaces.Categories;
using GreenSale.ViewModels.Models.BuyerPosts;
using GreenSale.ViewModels.Models.Categories;
using Newtonsoft.Json;

namespace GreenSale.Integrated.Services.Categories;

public class Category : ICategoryGetAll
{
    public async Task<List<CategoryViewModel>> GetAllAsync()
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"{AuthAPI.BASE_URL}" + "/api/common/categories");
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
            string response = await message.Content.ReadAsStringAsync();
            List<CategoryViewModel> posts = JsonConvert.DeserializeObject<List<CategoryViewModel>>(response)!;

            return posts;
        }
        catch
        {
            return new List<CategoryViewModel>();
        }
    }
}
