namespace Fisk2.Transport;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Fisk2.Core.Abstractions;
using Fisk2.Core.Common;
using Fisk2.Core.Security;

public sealed class SoapTransport : ITransport
{
    private readonly HttpClient _http;
    private readonly FiskClientOptions _opts;

    public SoapTransport(FiskClientOptions opts)
    {
        _opts = opts;
        _http = new HttpClient { BaseAddress = opts.BaseAddress, Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds) };
        _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
    }

    public async Task<(int StatusCode, string Body, string? ContentType)> SendAsync(MessageKind kind, string content, string contentType, CancellationToken ct = default)
    {
        var endpoint = _opts.Endpoints.TryGetValue(kind, out var ep) ? ep : throw new InvalidOperationException($"No endpoint for {kind}.");
        var uri = Uri.TryCreate(endpoint, UriKind.Absolute, out var abs) ? abs : new Uri(_http.BaseAddress!, endpoint);

        // Optional signing
        if (_opts.Signing.EnableXmlSigning && _opts.ClientCertificate is not null && content.TrimStart().StartsWith("<"))
        {
            content = XmlSigner.SignEnveloped(content, _opts.ClientCertificate, _opts.Signing);
        }

        var sb = new StringBuilder();
        sb.Append("<?xml version="1.0" encoding="utf-8"?>");
        sb.Append("<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">");
        sb.Append("<soap:Body>");
        sb.Append(content);
        sb.Append("</soap:Body></soap:Envelope>");

        using var req = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new StringContent(sb.ToString(), Encoding.UTF8, "text/xml")
        };

        if (_opts.SoapActions.TryGetValue(kind, out var action) && !string.IsNullOrWhiteSpace(action))
        {
            req.Headers.Add("SOAPAction", action);
        }

        using var rsp = await _http.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, ct);
        var body = await rsp.Content.ReadAsStringAsync(ct);
        var ctHeader = rsp.Content.Headers.ContentType?.MediaType;
        return ((int)rsp.StatusCode, body, ctHeader);
    }
}
