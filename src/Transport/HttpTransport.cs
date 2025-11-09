namespace Fisk2.Transport;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using Fisk2.Core.Abstractions;
using Fisk2.Core.Common;

public sealed class HttpTransport : ITransport, IDisposable
{
    private readonly HttpClient _http;

    public HttpTransport(Fisk2.Core.Common.FiskClientOptions opts)
    {
        var handler = new HttpClientHandler
        {
            AutomaticDecompression = opts.EnableCompression ? DecompressionMethods.GZip | DecompressionMethods.Deflate : DecompressionMethods.None,
            SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13
        };
        if (opts.ClientCertificate is not null)
        {
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ClientCertificates.Add(opts.ClientCertificate);
        }
        _http = new HttpClient(handler) { BaseAddress = opts.BaseAddress, Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds) };
        if (opts.EnableCompression)
        {
            _http.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            _http.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
        }
        _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
    }

    public async Task<(int StatusCode, string Body, string? ContentType)> SendAsync(MessageKind kind, string content, string contentType, CancellationToken ct = default)
    {
        throw new NotImplementedException("Use SOAP transport for production Fiskalizacija; HTTP POST kept for extensibility.");
    }

    public void Dispose() => _http.Dispose();
}
