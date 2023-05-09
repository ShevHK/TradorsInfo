using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TradorsInformation.DB.Entities;

namespace TradorsInformation.UI.Data
{
    public class ApiService
    {
        private static ApiService _instance;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        private ApiService()
        {
            _httpClient = new HttpClient();
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            _httpClient.BaseAddress = new Uri("https://localhost:5001/api/"); // Replace with your API base URL
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static ApiService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApiService();
                }
                return _instance;
            }
        }

        public async Task<int> CreateTradorInfoAsync(TradorInfo traderInfo)
        {
            var response = await _httpClient.PostAsync("TradorInfo/Create", GetStringContent(traderInfo));
            response.EnsureSuccessStatusCode();

            var createdId = await response.Content.ReadAsStringAsync();
            return int.Parse(createdId);
        }

        public async Task<bool> CreateTradorInfoListAsync(List<TradorInfo> traderInfos)
        {
            var response = await _httpClient.PostAsync("TradorInfo/CreateList", GetStringContent(traderInfos));
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return bool.Parse(result);
        }

        public async Task<bool> UpdateTradorInfoAsync(int id, TradorInfo traderInfo)
        {
            var response = await _httpClient.PostAsync($"TradorInfo/Update/{id}", GetStringContent(traderInfo));
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return bool.Parse(result);
        }

        public async Task<IList<TradorInfo>> GetAllTradorInfosAsync()
        {
            var response = await _httpClient.GetAsync("TradorInfo/GetAll");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IList<TradorInfo>>(content, _jsonSerializerOptions);
        }

        public async Task<TradorInfo> GetTradorInfoByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"TradorInfo/GetById/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TradorInfo>(content, _jsonSerializerOptions);
        }

        public async Task<bool> DeleteTradorInfoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"TradorInfo/Delete/{id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return bool.Parse(result);
        }

        public async Task<IList<TradorInfo>> SearchTradorInfosAsync(string search)
        {
            var response = await _httpClient.GetAsync($"TradorInfo/Search/{search}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IList<TradorInfo>>(content, _jsonSerializerOptions);
        }

        private StringContent GetStringContent<T>(T data)
        {
            var json = JsonSerializer.Serialize(data, _jsonSerializerOptions);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
