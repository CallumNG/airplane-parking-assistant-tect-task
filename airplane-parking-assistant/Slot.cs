using System;

namespace airplane_parking_assistant
{
    public class Slot
    {
        public Slot()
        {
            
        }

        public Slot(int id, string type, DateTime arrived, bool available = false)
        {
            Id = id;
            Type = type;
            Arrived = arrived;
            Available = available;
        }
        
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Arrived { get; set; }
        public DateTime LastDeparture { get; set; }
        public bool Available { get; set; } = true;
        
        public bool IsJumbo() => Type.ToUpper() == "JUMBO";
        public bool IsJet() => Type.ToUpper() == "JET";
        public bool IsProp() => Type.ToUpper() == "PROP";
    }
}