using GreenSale.Dtos.Dtos.Auth;
using GreenSale.Integrated.API.Auth;
using GreenSale.Integrated.Interfaces.Auth;
using GreenSale.Integrated.Security;
using GreenSale.ViewModels.Models.Auth;
using Newtonsoft.Json;

namespace GreenSale.Integrated.Services.Auth
{
    public class AuthService : IAuthService
    {
        public Task<bool> CheckTokenAsync(AuthorizationDto token)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool Result, string Token)> LoginAsync(UserLoginDto dto)
        {
            try
            {
                HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri(AuthAPI.BASE_URL);
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{AuthAPI.BASE_URL}/api/auth/login");
                var content = new StringContent(JsonConvert.SerializeObject(dto), null, "application/json");
                httpRequestMessage.Content = content;
                var response = await client.SendAsync(httpRequestMessage);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent)!;
                    string token = jsonResponse.token.ToString();
                    //save to identity
                    return (Result: true, Token: token);
                }
                else
                {
                    return (Result: false, Token: "");
                }
            }
            catch
            {
                return (Result: false, Token: "");
            }
        }

        public async Task<bool> RegisterAsync(UserRegisterDto dto)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, AuthAPI.BASE_URL + "/api/auth/register");
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(dto.FirstName), "FirstName");
                    content.Add(new StringContent(dto.LastName), "LastName");
                    content.Add(new StringContent(dto.PhoneNumber), "PhoneNumber");
                    content.Add(new StringContent(dto.Password), "Password");
                    content.Add(new StringContent(dto.District), "District");
                    content.Add(new StringContent(dto.Region), "Region");
                    content.Add(new StringContent(dto.Address), "Address");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<bool> ResetPasswordAsync(ForgotPassword dto)
        {
            try
            {
                HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri(AuthAPI.BASE_URL);
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{AuthAPI.BASE_URL}/api/auth/password/reset");
                var content = new StringContent(JsonConvert.SerializeObject(dto), null, "application/json");
                httpRequestMessage.Content = content;
                var response = await client.SendAsync(httpRequestMessage);
                var res = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendCodeForRegisterAsync(string phoneNumber)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    var request = new HttpRequestMessage(HttpMethod.Post, AuthAPI.BASE_URL + "/api/auth/register/send-code" +
                        $"?phone=%2B{phoneNumber.Substring(1)}");
                    request.Headers.Add("phone", phoneNumber);
                    var collection = new List<KeyValuePair<string, string>>();
                    collection.Add(new("phone", phoneNumber));
                    var content = new FormUrlEncodedContent(collection);
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<(bool Result, string Token)> VerifyRegisterAsync(string phoneNumber, int code)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post,
                        AuthAPI.BASE_URL + "/api/auth/register/verify" + $"?phoneNumber=%2B" +
                        $"\"{phoneNumber.Substring(1)}\"&code={code}");

                    var content = new StringContent($"{{ \"phoneNumber\": \"{phoneNumber}\"," +
                        $" \"code\": {code}}}", null, "application/json");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
                        string token = jsonResponse.token.ToString();

                        return (Result: jsonResponse.result, Token: token);
                    }
                    return (Result: false, Token: "");
                }
            }
            catch
            {
                return (Result: false, Token: "");
            }

        }

        public async Task<(bool Result, string Token)> VerifyResetPasswordAsync(string phoneNumber, int code)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post,
                        AuthAPI.BASE_URL + "/api/auth/password/verify" + $"?phoneNumber=%2B" +
                        $"\"{phoneNumber.Substring(1)}\"&code={code}");

                    var content = new StringContent($"{{ \"phoneNumber\": \"{phoneNumber}\"," +
                        $" \"code\": {code}}}", null, "application/json");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        
                        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent)!;
                        string token = jsonResponse.token.ToString();

                        return (Result: jsonResponse.result, Token: token);
                    }
                    return (Result: false, Token: "");
                }
            }
            catch
            {
                return (Result: false, Token: "");
            }
        }

        public async Task<UserRole> VerifyUserRole()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(AuthAPI.BASE_URL + "/api/auth/check/user/role");

                var token = IdentitySingelton.GetInstance().Token;
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                var result = client.GetAsync(client.BaseAddress);
                string response = await result.Result.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserRole>(response);
                return user!;
            }
            catch
            {
                return new UserRole();
            }
        }
    }
}