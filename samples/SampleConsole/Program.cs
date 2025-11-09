using Fisk2.Client;
using Fisk2.Core.Common;
using Fisk2.Messages.eFiskalizacija;

var client = new FiskClient(new FiskClientOptions
{
    BaseAddress = new Uri("https://change-me"),
    // ClientCertificate = Fisk2.Core.Security.CertificateLoader.LoadFromPfx("cert.pfx","pass"),
});

var req = new EvidentirajERacunZahtjev
{
    // Fill with Any (XML elements) or use strongly typed child DTOs if added later
};

var rsp = await client.EvidentirajERacunAsync(req);
Console.WriteLine(rsp.Success ? "OK" : $"ERR {rsp.ErrorCode}: {rsp.ErrorMessage}");
