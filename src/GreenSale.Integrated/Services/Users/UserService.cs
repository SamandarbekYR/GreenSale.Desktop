using GreenSale.Integrated.API.Auth;
using GreenSale.Integrated.Interfaces.Users;
using GreenSale.Integrated.Security;
using GreenSale.ViewModels.Models.Auth;
using GreenSale.ViewModels.Models.Storages;
using GreenSale.ViewModels.Models.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.Integrated.Services.Users
{
    public class UserService : IUserService
    {
        public async Task<long> CountAsync()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri($"{AuthAPI.BASE_URL}" + "/api/common/users/count");
                HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);
                var response = long.Parse(await responseMessage.Content.ReadAsStringAsync());

                return response;
            }
            catch
            {
                return 0;
            }
        }

        public Task<bool> DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> GetAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(AuthAPI.BASE_URL + "/api/account");

                var token = IdentitySingelton.GetInstance().Token;
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                var result = client.GetAsync(client.BaseAddress);
                string response = await result.Result.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserModel>(response);
                return user!;
            }
            catch
            {
                return new UserModel();
            }
            
        }

        public async Task<bool> UpdateAsync(UserDto dto)
        {
            try
            {
                var token = IdentitySingelton.GetInstance().Token;
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Put, AuthAPI.BASE_URL + $"/api/account/information");
                request.Headers.Add("Authorization", $"Bearer {token}");

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(dto.FirstName), "FirstName");
                content.Add(new StringContent(dto.LastName), "LastName");
                content.Add(new StringContent(dto.PhoneNumber), "PhoneNumber");
                content.Add(new StringContent(dto.Region), "Region");
                content.Add(new StringContent(dto.District), "District");
                content.Add(new StringContent(dto.Address), "Address");

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

        public async Task<bool> UpdateSecurityAsync(UsersecurtyDto dto)
        {
            try
            {
                var token = IdentitySingelton.GetInstance().Token;
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Put, AuthAPI.BASE_URL + $"/api/account/security");
                request.Headers.Add("Authorization", $"Bearer {token}");
                var content = new StringContent(JsonConvert.SerializeObject(dto), null, "application/json");

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
    }
}
