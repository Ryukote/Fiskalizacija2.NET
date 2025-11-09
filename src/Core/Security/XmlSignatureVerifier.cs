namespace Fisk2.Core.Security;

using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

public static class XmlSignatureVerifier
{
    public static bool TryVerify(string xml, Func<X509Certificate2, bool>? certificateValidator = null)
    {
        var doc = new XmlDocument { PreserveWhitespace = true };
        doc.LoadXml(xml);
        var nodeList = doc.GetElementsByTagName("Signature");
        if (nodeList.Count == 0) return false;

        var signedXml = new SignedXml(doc);
        signedXml.LoadXml((XmlElement)nodeList[0]);
        var ok = signedXml.CheckSignature();
        if (!ok) return false;

        var ki = signedXml.KeyInfo;
        if (ki is not null)
        {
            foreach (var clause in ki)
            {
                if (clause is KeyInfoX509Data x509)
                {
                    foreach (var c in x509.Certificates)
                    {
                        if (c is X509Certificate2 c2 && certificateValidator is not null)
                        {
                            if (!certificateValidator(c2)) return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}
