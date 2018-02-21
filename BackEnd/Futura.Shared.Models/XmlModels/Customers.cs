using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlModels
{
    [Serializable()]
    [XmlRoot(ElementName = "Customers")]
    public class Customers
    {
        [XmlElement(ElementName = "Customer")]
        public List<Customer> Customer { get; set; }
    }
}
