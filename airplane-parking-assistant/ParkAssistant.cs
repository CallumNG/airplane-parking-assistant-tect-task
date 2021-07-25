namespace airplane_parking_assistant
{
    public class ParkAssistant
    {
        private Parking _parking = new Parking();

        public bool Park(Plane plane)
        {
            switch (plane.PlaneType)
            {
                case "JUMBO":
                    return _parking.ParkJumbo();
                case "JET":
                    return _parking.ParkJet();
                case "PROP":
                    return _parking.ParkProp();
                default:
                    return false;
            }
        }
    }
}