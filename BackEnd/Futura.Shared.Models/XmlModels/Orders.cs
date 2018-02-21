using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlModels
{
    [Serializable()]
    [XmlRoot(ElementName = "Orders")]
    public class Orders
    {
        [XmlElement(ElementName = "Order")]
        public List<Order> Order { get; set; }
    }
}
