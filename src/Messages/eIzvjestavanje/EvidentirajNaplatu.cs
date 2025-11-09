namespace Fisk2.Messages.eIzvjestavanje;

using System.Xml.Serialization;

[XmlRoot("EvidentirajNaplatuZahtjev", Namespace="http://www.porezna-uprava.gov.hr/fin/2024/types/eIzvjestavanje")]
public sealed class EvidentirajNaplatuZahtjev
{
    [XmlAnyElement] public System.Xml.XmlElement[]? Any { get; set; }
}

[XmlRoot("EvidentirajNaplatuOdgovor", Namespace="http://www.porezna-uprava.gov.hr/fin/2024/types/eIzvjestavanje")]
public sealed class EvidentirajNaplatuOdgovor
{
    [XmlAnyElement] public System.Xml.XmlElement[]? Any { get; set; }
}
