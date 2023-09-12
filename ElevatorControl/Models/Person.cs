namespace ElevatorControl.Models
{
    public class Person
    {
        public long Id { get; set; }
        public int TargetFloor {get;set;}
        public bool Waiting { get; set; }
    }
}
