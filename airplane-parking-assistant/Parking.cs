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

        private List<(string type, string slot)> _jetsParked = new();

        public ParkingSlot ParkingSlots;

        public Parking()
        {
            ParkingSlots = new ParkingSlot(this);
        }

        public bool ParkJumbo(Slot slot = null)
        {
            IsParkingFull();

            if (!CanParkJumbo()) return false;

            ParkingSlots.ParkJumbo(slot);
            return LogParked("JUMBO", "JUMBO");
        }

        public bool ParkJet(Slot slot = null)
        {
            IsParkingFull();

            if (CanParkJet())
            {
                ParkingSlots.ParkJet(slot);
                return LogParked("JET", "JET");
            }

            if (CanParkJumbo())
            {
                ParkingSlots.ParkJumbo(slot);
                return LogParked("JET", "JUMBO");
            }

            return false;
        }

        public bool ParkProp(Slot slot = null)
        {
            IsParkingFull();

            if (CanParkProp())
            {
                ParkingSlots.ParkProp(slot);
                return LogParked("PROP", "PROP");
            }

            if (CanParkJet())
            {
                ParkingSlots.ParkJet(slot);
                return LogParked("PROP", "JET");
            }

            if (CanParkJumbo())
            {
                ParkingSlots.ParkJumbo(slot);
                return LogParked("PROP", "JUMBO");
            }

            return false;
        }

        public bool CanPark(string planeDesc)
            => new Plane(planeDesc).PlaneType switch
            {
                "JUMBO" => CanParkJumbo(),
                "JET" => CanParkJet() || CanParkJumbo(),
                "PROP" => CanParkProp() || CanParkJet() || CanParkJumbo(),
                _ => false
            };

        public bool CanParkJumbo() => _jetsParked.Count(x => x.slot == "JUMBO") < MAX_JUMBOS;
        public bool CanParkJet() => _jetsParked.Count(x => x.slot == "JET") < MAX_JETS;
        public bool CanParkProp() => _jetsParked.Count(x => x.slot == "PROP") < MAX_PROP;
        private bool IsParkingFull() => _jetsParked.Count == 100 ? throw new Exception("Parking Full") : false;

        private bool LogParked(string type, string slot)
        {
            _jetsParked.Add((type, slot));
            return true;
        }
    }
}