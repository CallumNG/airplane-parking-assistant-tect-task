using System;
using airplane_parking_assistant;

namespace parking_assistant
{
    class Program
    {
        private static ParkAssistant _assistant = new ParkAssistant();

        static void Main(string[] args)
        {
            bool open = true;
            while (open)
            {
                Console.WriteLine("Enter plane landing: ");
                var planeDesc = Console.ReadLine();

                if (_assistant.CanPark(planeDesc))
                {
                    var plane = new Plane(planeDesc);
                    var slot = _assistant.RecommendSlot(plane);
                    if (slot.slotFound)
                    {
                        Console.WriteLine("Slots are available!");
                        Console.WriteLine($"Slot recommended: {slot.slotRec.Type}-{slot.slotRec.Id} ");
                        Console.WriteLine("Do you want to park in the slot? (Y/N)");

                        var parkInRecSlot = Console.ReadLine().ToUpper() == "Y";
                        if (parkInRecSlot)
                            ParkPlane(plane, slot.slotRec);
                        else
                            ParkPlane(plane);
                    }
                }
                else
                {
                    Console.WriteLine("NO SPACE AVAILABLE!");
                }

                Console.WriteLine("Has another plane landed (Y/N)?");
                open = Console.ReadLine()?.ToUpper() == "Y";
            }
            Console.WriteLine("Done!");

        }

        private static void ParkPlane(Plane plane, Slot slot = null)
        {
            Console.WriteLine(_assistant.Park(plane, slot) ? "Plane parked" : "Unable to park plane");
        }
    }
}