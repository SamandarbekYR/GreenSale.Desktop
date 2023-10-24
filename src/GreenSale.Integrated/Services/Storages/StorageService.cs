using GreenSale.Dtos.Dtos.Storages;
using GreenSale.Integrated.API.Auth;
using GreenSale.Integrated.Interfaces.Storages;
using GreenSale.Integrated.Security;
using GreenSale.ViewModels.Models;
using GreenSale.ViewModels.Models.Storages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GreenSale.Integrated.Services.Storages;


public class StorageService : IStorageService
{
    public async Task<long> CountAsync()
    {
        try
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri($"{AuthAPI.BASE_URL}" + "/api/common/storage/count");
            HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);
            var response = long.Parse(await responseMessage.Content.ReadAsStringAsync());

            return response;
        }
        catch
        {
            return 0;
        }
    }

    public async Task<bool> CreateAsync(StorageDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, AuthAPI.BASE_URL + "/api/client/storages");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(dto.Name), "Name");
            content.Add(new StringContent(dto.Description), "Description");
            content.Add(new StringContent(dto.Region), "Region");
            content.Add(new StringContent(dto.District), "District");
            content.Add(new StringContent(dto.AddressLatitude.ToString()), "AddressLatitude");
            content.Add(new StringContent(dto.AddressLongitude.ToString()), "AddressLongitude");
            content.Add(new StringContent(dto.Address), "Address");
            content.Add(new StringContent(dto.Info), "Info");
            content.Add(new StreamContent(File.OpenRead(dto.ImagePath)), "ImagePath", dto.ImagePath);

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

    public async Task<bool> DeleteAsync(long storageId)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(AuthAPI.BASE_URL + $"/api/client/storages/{storageId}");

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

    public async Task<List<Storage>> GetAllAsync()
    {
        try
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"{AuthAPI.BASE_URL}" + "/api/common/storage");
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
            string response = await message.Content.ReadAsStringAsync();
            List<Storage> posts = JsonConvert.DeserializeObject<List<Storage>>(response)!;

            return posts;
        }
        catch
        {
            return new List<Storage> { };
        }
    }

    public async Task<List<Storage>> GetAllUserId(long userId)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"{AuthAPI.BASE_URL}" + $"/api/common/storage/all/{userId}");
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            if (message.StatusCode.ToString() != "NotFound")
            {
                string response = await message.Content.ReadAsStringAsync();
                List<Storage> posts = JsonConvert.DeserializeObject<List<Storage>>(response)!;
                return posts;
            }
            return new List<Storage>();
        }
        catch
        {
            return new List<Storage>();
        }
    }

    public async Task<StorageGetById> GetByIdAsync(long storageId)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;
            client.BaseAddress = new Uri($"{AuthAPI.BASE_URL}" + $"/api/common/storage/{storageId}");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
            string response = await message.Content.ReadAsStringAsync();
            StorageGetById posts = JsonConvert.DeserializeObject<StorageGetById>(response)!;

            return posts;
        }
        catch
        {
            return new StorageGetById();
        }
    }

    public async Task<StorageSearchViewModel> SearchAsync(string info)
    {
        try
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri($"{AuthAPI.BASE_URL}" + $"/api/common/storage/search/info?search={info}");
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            if (message.StatusCode.ToString() != "NotFound")
            {
                var respons = await message.Content.ReadAsStringAsync();
                StorageSearchViewModel StoragePost = JsonConvert.DeserializeObject<StorageSearchViewModel>(respons)!;
                return StoragePost;
            }
            return new StorageSearchViewModel { };
        }
        catch
        {
            return new StorageSearchViewModel { };
        }
    }

    public async Task<bool> UpdateAsync(long storageId, StorageUpdateDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, AuthAPI.BASE_URL + $"/api/client/storages/{storageId}");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(dto.Name), "Name");
            content.Add(new StringContent(dto.Description), "Description");
            content.Add(new StringContent(dto.Region), "Region");
            content.Add(new StringContent(dto.District), "District");
            content.Add(new StringContent(dto.AddressLatitude.ToString()), "AddressLatitude");
            content.Add(new StringContent(dto.AddressLongitude.ToString()), "AddressLongitude");
            content.Add(new StringContent(dto.Address), "Address");
            content.Add(new StringContent(dto.Info), "Info");
            //content.Add(new StreamContent(File.OpenRead(dto.ImagePath)), "ImagePath", dto.ImagePath);

            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                return true;
            }
            var res1 = await response.Content.ReadAsStringAsync();
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateImageStorageAsync(StorageImageDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, AuthAPI.BASE_URL + $"/api/client/storages/image/{dto.StorageId}");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(File.OpenRead(dto.StorageImagePath)), "StorageImage", dto.StorageImagePath);


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
    public async Task<List<PostCreatedAt>> StorageDaylilyCreatedAsync(int day)
    {
        try
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri($"{AuthAPI.BASE_URL}" + $"/api/common/storage/created/daylily/count?day={day}");
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

    public async Task<List<PostCreatedAt>> StorageMonthlyCreatedAsync(int month)
    {
        try
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri($"{AuthAPI.BASE_URL}" + $"/api/common/storage/created/monthly/count?month={month}");
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

    public async Task<bool> UpdateStartAsync(long postId, int start)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, AuthAPI.BASE_URL + $"/api/client/storage/post/star");
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
}
