using ElevatorControl.Models;
using Microsoft.EntityFrameworkCore;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ElevatorControlContext(
            serviceProvider.GetRequiredService<DbContextOptions<ElevatorControlContext>>()))
        {
            if (context.Elevators.Any())
            {
                return;   // Data was already seeded
            }

            for (var i = 1; i < 6; i++)
            {
                context.Floors.Add(new Floor { Id = i });
            }

            for (var i = 1; i < 4; i++)
            {
                
            }
            context.Elevators.Add(new Elevator { Id = 1, Name = "A", LastFloorId = 1, FloorsToService = "2,3" });
            context.Elevators.Add(new Elevator { Id = 2, Name = "B", LastFloorId = 1, FloorsToService = "2" });
            context.Elevators.Add(new Elevator { Id = 3, Name = "C", LastFloorId = 1, FloorsToService = "" });
            context.Elevators.Add(new Elevator { Id = 4, Name = "D", LastFloorId = 1, FloorsToService = null });

            context.SaveChanges();
        }
    }
}