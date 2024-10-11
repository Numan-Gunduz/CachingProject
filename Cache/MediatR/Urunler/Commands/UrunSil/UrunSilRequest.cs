using MediatR;

namespace Cache.MediatR.Urunler.Commands.UrunSil
{
    public class UrunSilRequest : IRequest
    {
        public int UrunId { get; set; }
    }
}
