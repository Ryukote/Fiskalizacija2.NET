/// <summary>
/// Strongly-typed configuration for the Fiskalizacija 2.0 client (XML-only).
/// </summary>
namespace Fisk2.Core.Common;

using System.Security.Cryptography.X509Certificates;
using Fisk2.Validation;

public sealed class FiskClientOptions
{
    /// <summary>Base address of the API.</summary>
    public required Uri BaseAddress { get; init; }

    /// <summary>Per-message endpoints.</summary>
    public Dictionary<MessageKind, string> Endpoints { get; } = new()
    {
        { MessageKind.FiscalizeInvoice, "/api/fiscalize" },
        { MessageKind.ReportPaymentStatus, "/api/report/payment" },
        { MessageKind.ReportRejection, "/api/report/rejection" },
        { MessageKind.ReportDelivery, "/api/report/delivery" }
    };

    /// <summary>Per-message transport mode.</summary>
    public Dictionary<MessageKind, TransportMode> TransportModes { get; } = new()
    {
        { MessageKind.FiscalizeInvoice, TransportMode.Soap11 },
        { MessageKind.ReportPaymentStatus, TransportMode.Soap11 },
        { MessageKind.ReportRejection, TransportMode.Soap11 },
        { MessageKind.ReportDelivery, TransportMode.Soap11 }
    };

    /// <summary>Client certificate for mTLS and signing.</summary>
    public X509Certificate2? ClientCertificate { get; init; }

    /// <summary>HTTP timeout (seconds).</summary>
    public int TimeoutSeconds { get; init; } = 30;

    /// <summary>Enable HTTP compression (gzip/deflate).</summary>
    public bool EnableCompression { get; init; } = true;

    /// <summary>Strict XML Schema validation (embedded XSDs by default).</summary>
    public bool EnableSchemaValidation { get; init; } = true;

    /// <summary>Override schema source (defaults to embedded resources).</summary>
    public ISchemaRepository? SchemaRepository { get; init; }

    /// <summary>SOAPAction header per message (optional).</summary>
    public Dictionary<MessageKind, string> SoapActions { get; } = new();

    /// <summary>XML-DSig signing behavior.</summary>
    public SigningOptions Signing { get; init; } = new();
}
