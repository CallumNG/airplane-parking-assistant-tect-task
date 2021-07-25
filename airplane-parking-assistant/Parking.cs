using System;
using System.Collections.Generic;
using System.Linq;

namespace airplane_parking_assistant
{
    public class Parking
    {
        private const int MAX_JUMBOS = 25;
        private const int MAX_JETS = 50;
        private const int MAX_PROP = 25;

        private List<(string type, string slot)> _jetsParked = new ();

        public bool ParkJumbo()
        {
            IsParkingFull();

            if (CanParkJumbo())
                return LogParked("JUMBO", "JUMBO");

            return false;
        }

        public bool ParkJet()
        {
            IsParkingFull();

            if (CanParkJet())
                return LogParked("JET", "JET");

            if (CanParkJumbo())
                return LogParked("JET", "JUMBO");

            return false;
        }

        public bool ParkProp()
        {
            IsParkingFull();

            if (CanParkProp())
                return LogParked("PROP", "PROP");
            if (CanParkJet())
                return LogParked("PROP", "JET");

            if (CanParkJumbo())
                return LogParked("PROP", "JUMBO");

            return false;
        }

        private bool CanParkJumbo() => _jetsParked.Count(x => x.slot == "JUMBO") < MAX_JUMBOS;
        private bool CanParkJet() => _jetsParked.Count(x => x.slot == "JET") < MAX_JETS;
        private bool CanParkProp() => _jetsParked.Count(x => x.slot == "PROP") < MAX_PROP;
        private bool IsParkingFull() => _jetsParked.Count == 100 ? throw new Exception("Parking Full") : false;

        private bool LogParked(string type, string slot)
        {
            _jetsParked.Add((type, slot));
            return true;
        }
    }
}