namespace Fisk2.Client;

using Fisk2.Core.Common;
using Fisk2.Core.Abstractions;
using Fisk2.Serialization;
using Fisk2.Validation;
using Fisk2.Messages.eFiskalizacija;
using Fisk2.Messages.eIzvjestavanje;

public sealed class FiskClient : IFiskClient
{
    private readonly FiskClientOptions _opts;
    private readonly ITransport _soap;
    private readonly ISerializer _xml;
    private readonly XmlSchemaValidator? _validator;

    public FiskClient(FiskClientOptions opts)
    {
        _opts = opts;
        _soap = new Fisk2.Transport.SoapTransport(opts);
        _xml = new XmlDotNetSerializer();

        if (opts.EnableSchemaValidation)
        {
            var repo = opts.SchemaRepository ?? new EmbeddedSchemaRepository(typeof(FiskClient).Assembly, "Fisk2.Schemas");
            _validator = new XmlSchemaValidator(repo, "eFiskalizacijaSchema.xsd", "eIzvjestavanjeSchema.xsd");
        }
    }

    private void ValidateIfEnabled(string xml)
    {
        _validator?.ValidateXmlRaw(xml);
    }

    private async Task<FiskResponse<TResp>> Send<TReq, TResp>(MessageKind kind, TReq request, CancellationToken ct)
    {
        var payload = _xml.Serialize(request!);
        ValidateIfEnabled(payload);
        var (code, body, ctHeader) = await _soap.SendAsync(kind, payload, _xml.ContentType, ct);
        if (code is >= 200 and < 300)
        {
            try
            {
                var data = _xml.Deserialize<TResp>(body);
                return FiskResponse<TResp>.Ok(data, body);
            }
            catch (Exception ex)
            {
                return FiskResponse<TResp>.Fail("DESERIALIZATION_ERROR", ex.Message, body);
            }
        }
        return FiskResponse<TResp>.Fail(code.ToString(), body, body);
    }

    public Task<FiskResponse<EvidentirajERacunOdgovor>> EvidentirajERacunAsync(EvidentirajERacunZahtjev request, CancellationToken ct = default)
        => Send<EvidentirajERacunZahtjev, EvidentirajERacunOdgovor>(MessageKind.FiscalizeInvoice, request, ct);

    public Task<FiskResponse<EvidentirajNaplatuOdgovor>> EvidentirajNaplatuAsync(EvidentirajNaplatuZahtjev request, CancellationToken ct = default)
        => Send<EvidentirajNaplatuZahtjev, EvidentirajNaplatuOdgovor>(MessageKind.ReportPaymentStatus, request, ct);

    public Task<FiskResponse<EvidentirajOdbijanjeOdgovor>> EvidentirajOdbijanjeAsync(EvidentirajOdbijanjeZahtjev request, CancellationToken ct = default)
        => Send<EvidentirajOdbijanjeZahtjev, EvidentirajOdbijanjeOdgovor>(MessageKind.ReportRejection, request, ct);

    public Task<FiskResponse<EvidentirajIsporukuZaKojuNijeIzdanERacunOdgovor>> EvidentirajIsporukuAsync(EvidentirajIsporukuZaKojuNijeIzdanERacunZahtjev request, CancellationToken ct = default)
        => Send<EvidentirajIsporukuZaKojuNijeIzdanERacunZahtjev, EvidentirajIsporukuZaKojuNijeIzdanERacunOdgovor>(MessageKind.ReportDelivery, request, ct);
}
