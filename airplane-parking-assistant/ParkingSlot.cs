using System;
using System.Collections.Generic;
using System.Linq;

namespace airplane_parking_assistant
{
    public class ParkingSlot
    {
        private List<Slot> _jmboSlot { get; set; }
        private List<Slot> _jtSlot { get; set; }
        private List<Slot> _popSlot { get; set; }

        private readonly Parking _parking;

        public ParkingSlot(Parking parking)
        {
            _parking = parking;

            _jmboSlot = new List<Slot>();
            _jtSlot = new List<Slot>();
            _popSlot = new List<Slot>();
        }

        public bool FindSlot(Plane plane, out Slot slot)
        {
            slot = new Slot();
            switch (plane.PlaneType)
            {
                case "JUMBO":
                    if (!_parking.CanParkJumbo()) return false;
                    slot = FindJumboParking();
                    return true;

                case "JET":
                    if (_parking.CanParkJet())
                    {
                        slot = FindJetParking();
                        return true;
                    }

                    if (_parking.CanParkJumbo())
                    {
                        slot = FindJumboParking();
                        return true;
                    }

                    return false;
                case "PROP":
                    if (_parking.CanParkProp())
                    {
                        slot = FindPropParking();
                        return true;
                    }

                    if (_parking.CanParkJet())
                    {
                        slot = FindJetParking();
                        return true;
                    }

                    if (_parking.CanParkJumbo())
                    {
                        slot = FindJumboParking();
                        return true;
                    }

                    return false;
                default:
                    return false;
            }
        }

        // Flaw with logic here - should take into account the departure date e.g. if there are 5 and 3 leaves the next recommended slot should be 3
        private Slot FindPropParking()
            => (!_jmboSlot.Any()
                ? new Slot(1, "PROP", DateTime.Now)
                : new Slot(_popSlot.Last().Id + 1, "PROP", DateTime.Now));

        private Slot FindJumboParking()
            => (!_jmboSlot.Any()
                ? new Slot(1, "JUMBO", DateTime.Now)
                : new Slot(_jmboSlot.Last().Id + 1, "JUMBO", DateTime.Now));

        private Slot FindJetParking()
            => (!_jtSlot.Any()
                ? new Slot(1, "JET", DateTime.Now)
                : new Slot(_jtSlot.Last().Id + 1, "JET", DateTime.Now));

        public void ParkJumbo(Slot slot)
        {
            if (slot == null)
                _jmboSlot.Add(new Slot(_jmboSlot.Any() ? _jmboSlot.Last().Id : 1, "JUMBOO", DateTime.Now));

            _jmboSlot.Add(slot);
        }

        public void ParkJet(Slot slot) => _jmboSlot.Add(slot);


        public void ParkProp(Slot slot) => _jmboSlot.Add(slot);
    }
}