using EventRegistrationApp.Data;
using EventRegistrationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationApp.Services;

public class EventService
{
    private readonly ApplicationDbContext _context;
    private readonly Dictionary<string, HashSet<int>> userEventRegistrations = new Dictionary<string, HashSet<int>>();


    public EventService(ApplicationDbContext context)
    {
        _context = context;
    }
    private bool HasUserAlreadyRegistered(string userId, int eventId)
    {
        // If the user is not in the dictionary, they haven't registered for any events
        if (!userEventRegistrations.ContainsKey(userId))
        {
            return false;
        }

        // Check if the user has already registered for the specific event
        return userEventRegistrations[userId].Contains(eventId);
    }
    
    
    public bool RegisterUserForEvent(string userId, int eventId)
    {
        if (HasUserAlreadyRegistered(userId, eventId))
        {
            // User has already registered for this event
            return false;
        }

        // Add the user to the dictionary and mark them as registered for the event
        if (!userEventRegistrations.ContainsKey(userId))
        {
            userEventRegistrations[userId] = new HashSet<int>();
        }

        userEventRegistrations[userId].Add(eventId);
        return true; // User successfully registered for the event
    }
    
    public async Task<List<Event>> GetAvailableEventsAsync()
    {
        return await _context.Events
            .Where(ev => ev.Date >= DateTime.Today && ev.RegisteredAttendees < ev.Capacity)
            .ToListAsync();
    }
    public async Task<Event> GetEventByIdAsync(int eventId)
    {
        return await _context.Events.FindAsync(eventId);
    }
    public async Task UpdateEventAsync(Event updatedEvent)
    {
        _context.Entry(updatedEvent).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEventAsync(int eventId)
    {
        var eventToDelete = await _context.Events.FindAsync(eventId);
        if (eventToDelete != null)
        {
            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task CreateEventAsync(Event newEvent)
    {
        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();
    }

    public async Task RegisterForEventAsync(int eventId, string referenceNumber)
    {
        var eventToUpdate = await _context.Events.FindAsync(eventId);

        if (eventToUpdate != null && eventToUpdate.AvailableSeats > 0)
        {
            eventToUpdate.RegisteredAttendees++;
            await _context.SaveChangesAsync();

            // Create a new EventRegistration record with the reference number
            var registration = new EventRegistration
            {
                EventId = eventId,
                UserId = GetCurrentUserId(), // Implement a method to get the user's ID
                ReferenceNumber = referenceNumber,
                RegistrationDate = DateTime.Now
            };
            
            await _context.SaveChangesAsync();
        }
    }

    private string GetCurrentUserId()
    {
        throw new NotImplementedException();
    }
}