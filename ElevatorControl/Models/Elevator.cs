namespace ElevatorControl.Models
{
    public class Elevator
    {
        public long Id { get; set; }
        public string? Name { get; set; }

        public long LastFloorId { get; set; }
        public string? FloorsToService { get; set; }

    }
}
