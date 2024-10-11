using MediatR;

namespace Cache.MediatR.Urunler.Queries.UrunGetir
{
    public class UrunGetirRequest : IRequest<UrunGetirResponse>
    {
        public int UrunId { get; set; }
    }
}
