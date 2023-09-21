namespace EventRegistrationApp.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public int Capacity { get; set; }
    public int RegisteredAttendees { get; set; }
    public int AvailableSeats => Capacity - RegisteredAttendees; // checking for avalable seats
}