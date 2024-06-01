using DigiTipGreen.ClientApp.Models;
using System.Text;
using System.Text.Json;

namespace DigiTipGreen.ClientApp.Services
{
    public class ApiServices
    {
        //const string apiUrl = "http://localhost:5120/api/";
        const string apiUrl = "http://10.0.2.2:5120/api/";

        public async Task<bool> LoginUser(string username, string password)
        {
            var login = new Login { Username = username, Password = password };
            var httpCliet = new HttpClient();

            var serializationOptions = new JsonSerializerOptions 
            { 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, 
                WriteIndented = true 
            };
            var json = JsonSerializer.Serialize(login, serializationOptions);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var url = $"{apiUrl}account/login";
            var result = await httpCliet.PostAsync(url, content);

            if(result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
