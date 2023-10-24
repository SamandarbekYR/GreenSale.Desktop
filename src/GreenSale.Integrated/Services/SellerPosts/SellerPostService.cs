using GreenSale.Dtos.Dtos.SellerPost;
using GreenSale.Integrated.API.Auth;
using GreenSale.Integrated.Interfaces.SellerPosts;
using GreenSale.Integrated.Security;
using GreenSale.ViewModels.Models;
using GreenSale.ViewModels.Models.SellerPosts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GreenSale.Integrated.Services.SellerPosts;

public class SellerPostService : ISellerPost
{
    public async Task<long> CountAgreedAsync()
    {
        try
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{AuthAPI.BASE_URL}" + "/api/common/seller/post/agreed/count");
            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            return long.Parse(result);
        }
        catch
        {
            return 0;
        }
    }

    public async Task<long> CountAsync()
    {
        try
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{AuthAPI.BASE_URL}" + "/api/common/seller/post/count");
            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            return long.Parse(result);
        }
        catch
        {
            return 0;
        }
    }

    public async Task<long> CountNewAsync()
    {
        try
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{AuthAPI.BASE_URL}" + "/api/common/seller/post/new/count");
            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            return long.Parse(result);
        }
        catch
        {
            return 0;
        }
    }

    public async Task<bool> CreateAsync(SellerPostCreate dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, AuthAPI.BASE_URL + "/api/client/seller/post");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(dto.PhoneNumber), "PhoneNumber");
            content.Add(new StringContent(dto.Title), "Title");
            content.Add(new StringContent(dto.Description), "Description");
            content.Add(new StringContent(dto.Price.ToString()), "Price");
            content.Add(new StringContent(dto.Capacity.ToString()), "Capacity");
            content.Add(new StringContent(dto.CapacityMeasure.ToString()), "CapacityMeasure");
            content.Add(new StringContent(dto.Type), "Type");
            content.Add(new StringContent(dto.Region), "Region");
            content.Add(new StringContent(dto.District), "District");
            content.Add(new StringContent(dto.CategoryId.ToString()), "CategoryID");
            //content.Add(new StreamContent(File.OpenRead(dto.ImagePath)), "ImagePath", dto.ImagePath);

            foreach (var item in dto.ImagePath)
            {
                content.Add(new StreamContent(File.OpenRead(item)), "ImagePath", item);
            }


            request.Content = content;
            var response = await client.SendAsync(request);

            var res = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(long postId)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(AuthAPI.BASE_URL + $"/api/client/seller/post/{postId}");

            var token = IdentitySingelton.GetInstance().Token;
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var result = await client.DeleteAsync(client.BaseAddress);
            string response = await result.Content.ReadAsStringAsync();
            return response == "true";
        }
        catch
        {
            return false;
        }

    }

    public async Task<bool> DeleteImageAsync(long imageId)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(AuthAPI.BASE_URL + $"/api/client/seller/post/image/{imageId}");

            var token = IdentitySingelton.GetInstance().Token;
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var result = await client.DeleteAsync(client.BaseAddress);
            string response = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<SellerPost>> GetAllAsync()
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"{AuthAPI.BASE_URL}" + "/api/common/seller/post");
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
            string response = await message.Content.ReadAsStringAsync();
            List<SellerPost> posts = JsonConvert.DeserializeObject<List<SellerPost>>(response)!;

            return posts;
        }
        catch
        {
            return new List<SellerPost>();
        }
    }

    public async Task<List<SellerPost>> GetAllUserId(long userId)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"{AuthAPI.BASE_URL}" + $"/api/common/seller/post/all/{userId}");
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            if (message.StatusCode.ToString() != "NotFound")
            {
                string response = await message.Content.ReadAsStringAsync();
                List<SellerPost> posts = JsonConvert.DeserializeObject<List<SellerPost>>(response)!;
                return posts;
            }

            return new List<SellerPost>();
        }
        catch
        {
            return new List<SellerPost>();
        }
    }


    public async Task<SellerGetById> GetByIdAsync(long postId)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;
            client.BaseAddress = new Uri($"{AuthAPI.BASE_URL}" + $"/api/common/seller/post/{postId}");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
            string response = await message.Content.ReadAsStringAsync();
            SellerGetById posts = JsonConvert.DeserializeObject<SellerGetById>(response)!;

            return posts;
        }
        catch
        {
            return new SellerGetById();
        }

    }

    public async Task<bool> ImageUpdateAsync(long imageId, string dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, AuthAPI.BASE_URL + $"/api/client/seller/post/image/{imageId}");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(File.OpenRead(dto)), "ImagePath", dto);

            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<SellerPostSearch> SearchAsync(string title)
    {
        try
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri($"{AuthAPI.BASE_URL}" + $"/api/common/seller/post/search/title?search={title}");
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            if (message.StatusCode.ToString() != "NotFound")
            {
                var respons = await message.Content.ReadAsStringAsync();
                SellerPostSearch SellerPost = JsonConvert.DeserializeObject<SellerPostSearch>(respons)!;
                return SellerPost;
            }
            return new SellerPostSearch { };
        }
        catch
        {
            return new SellerPostSearch { };
        }
    }

    public async Task<List<PostCreatedAt>> SellerDaylilyCreatedAsync(int day)
    {
        try
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri($"{AuthAPI.BASE_URL}" + $"/api/common/seller/post/created/daylily/count?day={day}");
            HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);
            string response = await responseMessage.Content.ReadAsStringAsync();

            List<PostCreatedAt> posts = JsonConvert.DeserializeObject<List<PostCreatedAt>>(response)!;

            return posts;
        }
        catch
        {
            return new List<PostCreatedAt>();
        }
    }

    public async Task<List<PostCreatedAt>> SellerMonthlyCreatedAsync(int month)
    {
        try
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri($"{AuthAPI.BASE_URL}" + $"/api/common/seller/post/created/monthly/count?month={month}");
            HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);
            string response = await responseMessage.Content.ReadAsStringAsync();

            List<PostCreatedAt> posts = JsonConvert.DeserializeObject<List<PostCreatedAt>>(response)!;

            return posts;
        }
        catch
        {
            return new List<PostCreatedAt>();
        }
    }

    public async Task<bool> UpdateAsync(long postId, SellerPostUpdateDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, AuthAPI.BASE_URL + $"/api/client/seller/post/{postId}");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(dto.PhoneNumber), "PhoneNumber");
            content.Add(new StringContent(dto.Title), "Title");
            content.Add(new StringContent(dto.Description), "Description");
            content.Add(new StringContent(dto.Price.ToString()), "Price");
            content.Add(new StringContent(dto.Capacity.ToString()), "Capacity");
            content.Add(new StringContent(dto.CapacityMeasure.ToString()), "CapacityMeasure");
            content.Add(new StringContent(dto.Type), "Type");
            content.Add(new StringContent(dto.Region), "Region");
            content.Add(new StringContent(dto.District), "District");

            request.Content = content;
            var response = await client.SendAsync(request);

            var res1 = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateStartAsync(long postId, int start)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, AuthAPI.BASE_URL + $"/api/client/seller/post/star");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(postId.ToString()), "PostId");
            content.Add(new StringContent(start.ToString()), "Stars");

            request.Content = content;
            var response = await client.SendAsync(request);

            await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateStatusAsync(long postId, int status)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, AuthAPI.BASE_URL + $"/api/client/seller/post/status/{postId}");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var content = new MultipartFormDataContent();

            content.Add(new StringContent(status.ToString()), "PostStatus");
            request.Content = content;

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        catch { return false; }
    }
}
