using ApiConsumer.Models;
using Cache.MediatR.Urunler.Commands.UrunGuncelle;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


public class UrunController : Controller
{
    private readonly ApiClient _apiClient;



    public UrunController(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    // Ürünleri listeleme işlemini 
    public async Task<IActionResult> Index(string searchQuery)
    {

        var start = DateTime.UtcNow;

        List<Urun> urunler = new List<Urun>();

        try
        {
            urunler = await _apiClient.GetUrunlerAsync();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                // Arama terimine göre filtreleme
                urunler = urunler.Where(u => u.Isim.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (urunler == null || urunler.Count == 0)
            {
                return View("Error");
            }

            var end = DateTime.UtcNow;
            var duration = (end - start).TotalMilliseconds;
            ViewData["Sure"] = duration;

            return View(urunler);
        }
        catch (HttpRequestException httpRequestException)
        {
            Debug.WriteLine($"HTTP Hatası: {httpRequestException.Message}");
            return View("Error");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Beklenmeyen hata: {ex.Message}");
            return View("Error");
        }
    }

    // Yeni ürün ekleme formu
    public IActionResult Create()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        stopwatch.Stop();
        ViewData["Sure"] = stopwatch.ElapsedMilliseconds;
        return View();
    }

    // Yeni ürün ekleme işlemi
    [HttpPost]
    public async Task<IActionResult> Create(UrunEkle newUrun)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        if (ModelState.IsValid)
        {
            try
            {
                await _apiClient.CreateUrunAsync(newUrun.Isim, newUrun.KategoriId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ekleme hatası: {ex.Message}");
            }
        }
        stopwatch.Stop();
        ViewData["Sure"] = stopwatch.ElapsedMilliseconds;
        return View(newUrun);
    }
    // Ürün güncelleme formu
    public async Task<IActionResult> Edit(int id)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var urun = await _apiClient.GetUrunByIdAsync(id);

        if (urun == null)
        {
            return NotFound();
        }
        stopwatch.Stop();
        ViewData["Sure"] = stopwatch.ElapsedMilliseconds;
        return View(urun);
    }

    // Ürün güncelleme işlemi
    [HttpPost]
    public async Task<IActionResult> Edit(Urun  urun)
    {

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        if (ModelState.IsValid)
        {
            try
            {
                // Burada API'ye uygun formatta bir request yapıyoruz
                var urunGuncelleRequest = new UrunGuncelleRequest
                {
                    UrunId = urun.Id,
                    Isim = urun.Isim,
                    KategoriId = urun.KategoriId
                };

                await _apiClient.UpdateUrunAsync(urunGuncelleRequest); // Doğru modelle API'yi çağırıyoruz unutma!'
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Güncelleme hatası: {ex.Message}");
            }
        }
        stopwatch.Stop();
        ViewData["Sure"] = stopwatch.ElapsedMilliseconds;
        return View(urun);
    }


    //Silme onay sayfası(GET)
    public async Task<IActionResult> UrunSıl(int id)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var urun = await _apiClient.GetUrunByIdAsync(id);
        if (urun == null)
        {
            return NotFound();
        }
        stopwatch.Stop();
        ViewData["Sure"] = stopwatch.ElapsedMilliseconds;
        return View(urun);
    }

    // Silme işlemi (POST)
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int Id)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        if (Id <= 0)
        {
            return BadRequest("Geçersiz ürün ID'si.");
        }

        try
        {
            // API istemcisine doğru ID'yi gönderiyoruz
            await _apiClient.DeleteUrunAsync(Id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Hata durumunda ModelState'e hata ekleyip formu tekrar gösteriyoruz
            ModelState.AddModelError(string.Empty, $"Silme hatası: {ex.Message}");
            // Hata durumunda urunId ile birlikte yeni bir Urun nesnesi gönderiyoruz
            return View("UrunSıl", new { Id });
        }
        stopwatch.Stop();
        ViewData["Sure"] = stopwatch.ElapsedMilliseconds;
    }



}
