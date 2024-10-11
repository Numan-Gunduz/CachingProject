using ApiConsumer.Models;
using Cache.MediatR.Urunler.Commands.UrunEkle;
using Cache.MediatR.Urunler.Commands.UrunGuncelle;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5190/api/"); // Cache API'sinin URL'si
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    
    public async Task<List<Urun>> GetUrunlerAsync()
    {
        var response = await _httpClient.GetAsync("Urun/UrunleriGetirOData");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        try
        {
            var urunler = JsonSerializer.Deserialize<List<Urun>>(content, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase // JSON anahtar adlarını camel case olarak maplemesini istediğim için yazdım unutma!
            });
            return urunler;
        }
        catch (JsonException jsonException)
        {
            Debug.WriteLine($"JSON Hatası: {jsonException.Message}");
            return new List<Urun>(); // Hata durumunda boş bir liste döndür
        }
    }


    public async Task<Urun> GetUrunByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"Urun/UrunGetir?urunId={id}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Urun>(content);
        //Buradaki Updatedurun kısmı yerin Urun yazacak unutma!'
    }



    public async Task CreateUrunAsync(string urunIsim, int kategoriId)
    {
        var request = new UrunEkleRequest
        {
            UrunIsim = urunIsim,
            KategoriId = kategoriId
        };

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("Urun/UrunEkle", content);

        if (!response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"Ekleme hatası: {response.StatusCode}, İçerik: {responseContent}");
            throw new Exception($"Hata: {response.StatusCode}, İçerik: {responseContent}");
        }


    }


        public async Task UpdateUrunAsync(UrunGuncelleRequest updatedUrun)
    {
        var json = JsonSerializer.Serialize(updatedUrun);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync("Urun/UrunGuncelle", content);

        if (!response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"Güncelleme hatası: {response.StatusCode}, İçerik: {responseContent}");
            throw new Exception($"Hata: {response.StatusCode}, İçerik: {responseContent}");
        }
    }


    public async Task DeleteUrunAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"Urun/UrunSil?urunId={id}");

        if (!response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"Silme hatası: {response.StatusCode}, İçerik: {responseContent}");
            throw new Exception($"Hata: {response.StatusCode}, İçerik: {responseContent}");
        }
    }







}
