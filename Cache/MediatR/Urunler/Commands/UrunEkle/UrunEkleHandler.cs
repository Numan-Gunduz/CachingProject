using Cache.Models;
using MediatR;

namespace Cache.MediatR.Urunler.Commands.UrunEkle
{
    public class UrunEkleHandler(Context context) : IRequestHandler<UrunEkleRequest>
    {
        public async Task Handle(UrunEkleRequest request, CancellationToken cancellationToken)
        {
            if (context.Kategoris.Any(a => a.Id == request.KategoriId))
            {
                var yeniUrun = new Urun { Isim = request.UrunIsim, KategoriId = request.KategoriId, IsActive = true
            };
                
                await context.Uruns.AddAsync(yeniUrun, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
