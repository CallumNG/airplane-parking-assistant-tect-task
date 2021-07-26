namespace airplane_parking_assistant
{
    public class ParkAssistant
    {
        private readonly Parking _parking = new Parking();
        public (bool slotFound,Slot slotRec) RecommendSlot(Plane plane)
            =>  (_parking.ParkingSlots.FindSlot(plane, out Slot slot), slot);

        public bool CanPark(string planeDesc) => _parking.CanPark(planeDesc);

        public bool Park(Plane plane, Slot slot = null)
        {
            return plane.PlaneType switch
            {
                "JUMBO" => _parking.ParkJumbo(slot),
                "JET" => _parking.ParkJet(),
                "PROP" => _parking.ParkProp(),
                _ => false
            };
        }
    }
}