# ElevatorControl

Instructions
It is highly recommended that Visual Studio Code is used to build and run this service.
You can download from here: https://code.visualstudio.com/

# Instructions for building and running the service
- Clone a copy of the main branch 
- Open Visual Studio Code and open the folder into which you cloned the main branch
- Open a new terminal window (CTRL+SHIFT+``)
- execute this command: dotnet run --project .\ElevatorControl\ElevatorControl.csproj --urls=http://localhost:8080

# Testing the endpoints
- To determine if the application has been seeded with default data, open a new terminal window (CTRL+SHIFT+``)
- Run the following command: curl http://localhost:8080/api/Elevators.  You should see a list of four elevators in the response.

# Available endpoints
- Requesting an elevator to the current floor: e.g. curl -X 'POST' 'http://localhost:8080/api/People/RequestElevator/currentfloor=2'
- Requesting to be taken to a specific floor: e.g. curl -X 'POST' 'http://localhost:8080/api/People/PressFloorButton/floor=2'
- Requesting all floors being serviced: e.g. curl http://localhost:8080/api/Elevators/1/ServicingFloors
- Requesting next floor to be serviced: curl http://localhost:8080/api/Elevators/2/NextFloor

