using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace VisionA
{
    [Serializable]
    public class StructuredLog
    {
        public List<StructuredLogEntry> Entries { get; set; }

        public static StructuredLog Load()
        {
            if(!File.Exists("structedlog.xml")) return new StructuredLog(){Entries = new List<StructuredLogEntry>()};

            using (FileStream fs = new FileStream("structedlog.xml", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(StructuredLog));

                return (StructuredLog)serializer.Deserialize(fs);
            }
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(StructuredLog));
            Stream fs = new FileStream("structedlog.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
            serializer.Serialize(fs, this);
        }
    }
}