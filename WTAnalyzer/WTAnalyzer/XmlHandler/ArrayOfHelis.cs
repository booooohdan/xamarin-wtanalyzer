﻿using System.Collections.Generic;
using System.Xml.Serialization;
using WTAnalyzer.Models;

namespace WTAnalyzer.XmlHandler
{
    [XmlRoot(ElementName = "ArrayOfHelis", Namespace = "http://schemas.datacontract.org/2004/07/VehicleDataAccess")]
    public class ArrayOfHelis
    {
        [XmlElement(ElementName = "Helis", Namespace = "http://schemas.datacontract.org/2004/07/VehicleDataAccess")]
        public List<Heli> HelisListApi { get; set; }
    }
}
