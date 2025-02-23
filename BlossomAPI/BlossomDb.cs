using Microsoft.EntityFrameworkCore;

class BlossomDb : DbContext
{
    public BlossomDb(DbContextOptions<BlossomDb> options)
        : base(options) { }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<ChatSession> ChatSessions => Set<ChatSession>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
    public DbSet<ChatSessionReport> ChatSessionReports => Set<ChatSessionReport>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
    // The following three options help with debugging, but should
    // be changed or removed for production.
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors()
        .UseSeeding((context, _) =>
        {
            // create test doctor
            var doctor = new Doctor
            {
                Id = 111,
                Name = "Dr. Test",
                Email = "drtest@test.com"
            };
            if (context.Set<Doctor>().Find(doctor.Id) is null)
            {
                context.Set<Doctor>().Add(doctor);
                context.SaveChanges();
            }

            var patient = new Patient
            {
                Id = 333,
                Name = "Test Patient",
                Email = "test@test.com",
                Doctor = doctor
            };

            if (context.Set<Patient>().Find(patient.Id) is null)
            {
                context.Set<Patient>().Add(patient);
                context.SaveChanges();
            }

            var chatSession = new ChatSession
            {
                Id = 999,
                Patient = patient
            };
            if (context.Set<ChatSession>().Find(chatSession.Id) is null)
            {
                context.Set<ChatSession>().Add(chatSession);
                context.SaveChanges();

                ChatMessage[] messages =
                [
                    new ChatMessage
                    {
                        Id = 1,
                        Content = "Hi there! I'm Blossom, your health assistant. How can I help you today?",
                        ChatSession = chatSession,
                        Sender = "Blossom"
                    },
                    new ChatMessage
                    {
                        Id = 2,
                        Content = "Hi, I've been having a headache for the past 3 days",
                        ChatSession = chatSession,
                        Sender = "User"
                    },
                    new ChatMessage
                    {
                        Id = 35,
                        Content = "Sorry to hear that, describe it to me.",
                        ChatSession = chatSession,
                        Sender = "Blossom"
                    },
                    new ChatMessage
                    {
                        Id = 4,
                        Content = "mostly on one side of my head, sometimes I feel nauseous.",
                        ChatSession = chatSession,
                        Sender = "User"
                    },
                    new ChatMessage
                    {
                        Id = 3,
                        Content = "could you tell me if you're sensitive to light or sound during these headaches?",
                        ChatSession = chatSession,
                        Sender = "Blossom"
                    },
                    new ChatMessage
                    {
                        Id = 98,
                        Content = "Yes, actually. Bright lights really bother me when I have the headache, so been trying to stay in darker rooms",
                        ChatSession = chatSession,
                        Sender = "User"
                    },
                    new ChatMessage
                    {
                        Id = 5,
                        Content = "Have you experienced any visual changes before or during the headache?",
                        ChatSession = chatSession,
                        Sender = "Blossom"
                    },
                    new ChatMessage
                    {
                        Id = 6,
                        Content = "Sometimes I see these weird sparkly spots about 30 minutes before the headache gets bad.",
                        ChatSession = chatSession,
                        Sender = "User"
                    },
                    new ChatMessage
                    {
                        Id = 7,
                        Content = "Have you noticed any patterns with when these headaches occur?",
                        ChatSession = chatSession,
                        Sender = "Blossom"
                    },
                    new ChatMessage
                    {
                        Id = 8,
                        Content = "Nothing in particular, but work's been rough and so has my sleep schedule especially lately",
                        ChatSession = chatSession,
                        Sender = "User"
                    },
                    new ChatMessage
                    {
                        Id = 9,
                        Content = $"Thanks for sharing. I've put this on {doctor.Name}'s radar, so expect a call soon.",
                        ChatSession = chatSession,
                        Sender = "Blossom"
                    }
                ];
                foreach (var message in messages)
                {
                    context.Set<ChatMessage>().Add(message);
                    context.SaveChanges();
                }
            }
        });
}

// dotnet tool install --global dotnet-ef
// dotnet ef migrations add MigrationNameHere
// dotnet ef database update