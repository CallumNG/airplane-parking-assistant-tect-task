using System.Collections.Generic;

namespace airplane_parking_assistant
{
    public class Plane
    {
        private readonly string _type;

        private static Dictionary<string, string> _planeType = new()
        {
            {"AS80", "JUMBO"},
            {"B747", "JUMBO"},
            {"A330", "JET"},
            {"B777", "JET"},
            {"E195", "PROP"}
        };

        public Plane(string planeDescription) => _planeType.TryGetValue(planeDescription.ToUpper(), out _type);

        public string PlaneType => _type;
    }
}