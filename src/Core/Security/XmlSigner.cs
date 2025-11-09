namespace Fisk2.Core.Security;

using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using Fisk2.Core.Common;

public static class XmlSigner
{
    public static string SignEnveloped(string xml, X509Certificate2 cert, SigningOptions opts)
    {
        var doc = new XmlDocument { PreserveWhitespace = true };
        doc.LoadXml(xml);

        var signedXml = new SignedXml(doc) { SigningKey = cert.GetRSAPrivateKey() };
        var reference = new Reference { Uri = "" };
        reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
        reference.DigestMethod = opts.DigestMethod;
        signedXml.AddReference(reference);
        signedXml.SignedInfo.SignatureMethod = opts.SignatureMethod;

        if (opts.IncludeCertificateInKeyInfo)
        {
            var ki = new KeyInfo();
            ki.AddClause(new KeyInfoX509Data(cert));
            signedXml.KeyInfo = ki;
        }

        signedXml.ComputeSignature();
        var sig = signedXml.GetXml();
        doc.DocumentElement!.AppendChild(doc.ImportNode(sig, true));
        return doc.OuterXml;
    }
}

public sealed class SigningOptions
{
    public bool EnableXmlSigning { get; init; } = false;
    public string SignatureMethod { get; init; } = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
    public string DigestMethod { get; init; } = "http://www.w3.org/2001/04/xmlenc#sha256";
    public bool IncludeCertificateInKeyInfo { get; init; } = true;
}
