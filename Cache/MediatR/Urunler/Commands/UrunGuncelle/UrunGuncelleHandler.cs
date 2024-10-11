using Cache.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cache.MediatR.Urunler.Commands.UrunGuncelle
{
    public class UrunGuncelleHandler(Context context) : IRequestHandler<UrunGuncelleRequest>
    {
        public async Task Handle(UrunGuncelleRequest request, CancellationToken cancellationToken)
        {
            if (context.Kategoris.Any(k => k.Id == request.KategoriId))
            {
                var urun = await context.Uruns.Where(u => u.Id == request.UrunId).FirstAsync(cancellationToken);
                if (urun != null)
                {
                    urun.Isim = request.Isim;
                    urun.KategoriId = request.KategoriId;
                    context.Uruns.Update(urun);
                    await context.SaveChangesAsync(cancellationToken);

                }
            }
        }
    }
}

