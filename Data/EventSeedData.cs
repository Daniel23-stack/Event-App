// Data/EventSeedData.cs

using System;
using System.Linq;
using EventRegistrationApp.Data;
using EventRegistrationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationApp.SeedData
{
    public static class EventSeedData
    {
        public static void SeedData(ApplicationDbContext context)
        {
            context.Database.Migrate(); // Ensure the database is created and up-to-date

            if (!context.Events.Any())
            {
                // Seed initial events
                var events = new[]
                {
                    new Event
                    {
                        Name = "Event 1",
                        Date = DateTime.Now.AddDays(30),
                        Capacity = 100,
                        RegisteredAttendees = 0
                    },
                    new Event
                    {
                        Name = "Event 2",
                        Date = DateTime.Now.AddDays(60),
                        Capacity = 150,
                        RegisteredAttendees = 0
                    },
                    // Add more events as needed
                };

                context.Events.AddRange(events);
                context.SaveChanges();
            }
        }
    }
}