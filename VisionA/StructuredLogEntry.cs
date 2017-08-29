using System;

namespace VisionA
{
    [Serializable]
    public class StructuredLogEntry
    {
        public DateTime Time { get; set; }
        public bool IsOk { get; set; }
        public double FontSize { get; set; }
        public DateTime DateId { get; set; }
        public string EysExt { get; set; }
        public double PrimaryScreenHeight { get; set; }
        public double PrimaryScreennWidth { get; set; }
        public string MachineName { get; set; }
        public string UserName { get; set; }
    }
}