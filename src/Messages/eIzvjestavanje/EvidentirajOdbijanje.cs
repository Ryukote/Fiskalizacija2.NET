namespace Fisk2.Messages.eIzvjestavanje;

using System.Xml.Serialization;

[XmlRoot("EvidentirajOdbijanjeZahtjev", Namespace="http://www.porezna-uprava.gov.hr/fin/2024/types/eIzvjestavanje")]
public sealed class EvidentirajOdbijanjeZahtjev
{
    [XmlAnyElement] public System.Xml.XmlElement[]? Any { get; set; }
}

[XmlRoot("EvidentirajOdbijanjeOdgovor", Namespace="http://www.porezna-uprava.gov.hr/fin/2024/types/eIzvjestavanje")]
public sealed class EvidentirajOdbijanjeOdgovor
{
    [XmlAnyElement] public System.Xml.XmlElement[]? Any { get; set; }
}
