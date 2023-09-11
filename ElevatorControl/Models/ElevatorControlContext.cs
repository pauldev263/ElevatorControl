using Microsoft.EntityFrameworkCore;

namespace ElevatorControl.Models;

public class ElevatorControlContext : DbContext
{
    public ElevatorControlContext(DbContextOptions<ElevatorControlContext> options)
        : base(options)
    {
    }

    public DbSet<Elevator> Elevators { get; set; } = null!;
    public DbSet<Person> Persons { get; set; } = null!;
    public DbSet<Floor> Floors { get; set; } = null!;

}
