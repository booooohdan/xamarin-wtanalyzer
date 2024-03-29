﻿using System.Collections.Generic;
using System.Xml.Serialization;
using WTAnalyzer.Models;

namespace WTAnalyzer.XmlHandler
{
    [XmlRoot(ElementName = "ArrayOfShips", Namespace = "http://schemas.datacontract.org/2004/07/VehicleDataAccess")]
    public class ArrayOfShips
    {
        [XmlElement(ElementName = "Ships", Namespace = "http://schemas.datacontract.org/2004/07/VehicleDataAccess")]
        public List<Ship> ShipsListApi { get; set; }
    }
}
