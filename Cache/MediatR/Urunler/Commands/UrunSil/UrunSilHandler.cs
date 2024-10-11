using Cache.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cache.MediatR.Urunler.Commands.UrunSil
{
    public class UrunSilHandler(Context context) : IRequestHandler<UrunSilRequest>
    {
        public async Task Handle(UrunSilRequest request, CancellationToken cancellationToken)
        {
            var urun = await context.Uruns.Where(u => u.Id == request.UrunId).FirstAsync(cancellationToken);

            if (urun != null)
            {
                context.Uruns.Remove(urun);
                await context.SaveChangesAsync(cancellationToken);

            }
        }
    }
}
