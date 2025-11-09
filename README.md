# Fisk2 — Fiskalizacija 2.0 .NET Client (XML‑only, SOAP, XSD, XML‑DSig)

XML‑only, schema‑first C#/.NET 8 library for Croatia’s **Fiskalizacija 2.0**:
- **XML** requests & responses (SOAP 1.1).
- **Embedded XSD** validation (no runtime ZIPs).
- **mTLS** client certificate.
- **XML‑DSig** signing (enveloped) + verification helper.
- Root DTO‑ovi s točnim imenima i namespace‑ovima; sadržaj je agnostičan i validira se preko XSD.

## Installation
Add the project to your solution or pack as a NuGet:
```bash
dotnet build Fisk2.csproj
dotnet pack Fisk2.csproj -c Release
```

## Quick start
```csharp
var opts = new Fisk2.Core.Common.FiskClientOptions
{
    BaseAddress = new Uri("https://your-endpoint"),
    // ClientCertificate = Fisk2.Core.Security.CertificateLoader.LoadFromPfx("cert.pfx","pass"),
    // Signing = new Fisk2.Core.Security.SigningOptions { EnableXmlSigning = true }
};
var client = new Fisk2.Client.FiskClient(opts);
var req = new Fisk2.Messages.eFiskalizacija.EvidentirajERacunZahtjev{ /* fill Any */ };
var rsp = await client.EvidentirajERacunAsync(req);
```

## SOAP 1.1
Per‑operation transport is SOAP 1.1 by default; optional `SOAPAction` via `FiskClientOptions.SoapActions[kind]`.

## XSD validation
By default uses **embedded** `eFiskalizacijaSchema.xsd` and `eIzvjestavanjeSchema.xsd`.  
Override with `FolderSchemaRepository` if needed.

## XML‑DSig signing & verify
Enable signing in options; verification helper is available for incoming XML.

## DTOs (agnostic, schema‑first)
Exact roots + namespaces:
- eFiskalizacija: `EvidentirajERacunZahtjev` / `EvidentirajERacunOdgovor`
- eIzvjestavanje: `EvidentirajNaplatuZahtjev` / `EvidentirajNaplatuOdgovor`, `EvidentirajOdbijanjeZahtjev` / `…Odgovor`, `EvidentirajIsporukuZaKojuNijeIzdanERacunZahtjev` / `…Odgovor`

## Environments
Use separate `FiskClientOptions` for test/prod (BaseAddress, endpoints, certs).

## Error handling
All calls return `FiskResponse<T>` with `Success`, `Data` or `ErrorCode`/`ErrorMessage`, and `RawPayload`.

---

## 💖 Support the Project
Ako vam je ovaj library koristan i želite podržati daljnji razvoj, možete poslati malu donaciju:

- **Aircash:** +385959180338  
- **Revolut:** http://revolut.me/antoniqml

Hvala! 🙏
