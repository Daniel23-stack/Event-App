namespace EventRegistrationApp.Models;

public class EventRegistration
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string UserId { get; set; }
    public string ReferenceNumber { get; set; } // Unique reference number
    public DateTime RegistrationDate { get; set; }
}