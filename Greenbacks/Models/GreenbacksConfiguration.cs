using System;
using System.Xml.Serialization;

namespace Greenbacks.Models
{
    [Serializable]
    public class GreenbacksConfiguration
    {
        [XmlElement(DataType = "boolean", ElementName = "RememberLastLocation")]
        public bool RememberLastLocation { get; set; }

        [XmlElement(DataType = "boolean", ElementName = "RememberWindowSize")]
        public bool RememberWindowSize { get; set; }

        [XmlElement(DataType = "double", ElementName = "WindowTop")]
        public double WindowTop { get; set; }

        [XmlElement(DataType = "double", ElementName = "WindowLeft")]
        public double WindowLeft { get; set; }

        [XmlElement(DataType = "double", ElementName = "WindowHeight")]
        public double WindowHeight { get; set; }

        [XmlElement(DataType = "double", ElementName = "WindowWidth")]
        public double WindowWidth { get; set; }

        [XmlIgnore]
        public string BasePath { get; set; }
    }
}
