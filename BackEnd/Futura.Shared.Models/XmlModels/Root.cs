using System;
using System.Xml.Serialization;

namespace XmlModels
{
    [Serializable()]
    [XmlRoot(ElementName = "Root")]
    public class Root
    {
        [XmlElement(ElementName = "Customers")]
        public Customers Customers { get; set; }

        [XmlElement(ElementName = "Orders")]
        public Orders Orders { get; set; }
    }
}
