namespace Fisk2.Client;

using Fisk2.Core.Common;
using Fisk2.Messages.eFiskalizacija;
using Fisk2.Messages.eIzvjestavanje;

public interface IFiskClient
{
    Task<FiskResponse<EvidentirajERacunOdgovor>> EvidentirajERacunAsync(EvidentirajERacunZahtjev request, CancellationToken ct = default);
    Task<FiskResponse<EvidentirajNaplatuOdgovor>> EvidentirajNaplatuAsync(EvidentirajNaplatuZahtjev request, CancellationToken ct = default);
    Task<FiskResponse<EvidentirajOdbijanjeOdgovor>> EvidentirajOdbijanjeAsync(EvidentirajOdbijanjeZahtjev request, CancellationToken ct = default);
    Task<FiskResponse<EvidentirajIsporukuZaKojuNijeIzdanERacunOdgovor>> EvidentirajIsporukuAsync(EvidentirajIsporukuZaKojuNijeIzdanERacunZahtjev request, CancellationToken ct = default);
}
