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

    string connectionString = "";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
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
                Id = 999,
                Name = "Dr. Test",
                Email = "drtest@test.com"
            };
            if (context.Set<Doctor>().Find(doctor.Id) is null) {
                context.Set<Doctor>().Add(doctor);
                context.SaveChanges();
            }

            var patient = new Patient
            {
                Id = 999,
                Name = "Test Patient",
                Email = "test@test.com",
                Doctor = doctor
            };

            if (context.Set<Patient>().Find(patient.Id) is null) {
                context.Set<Patient>().Add(patient);
                context.SaveChanges();
            }

            var chatSession = new ChatSession
            {
                Id = 999,
                Patient = patient
            };
            if (context.Set<ChatSession>().Find(chatSession.Id) is null) {
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
        Content = "Hi, I've been having this terrible headache for the past 3 days. It's mostly on one side of my head and sometimes I feel nauseous.",
        ChatSession = chatSession,
        Sender = "User"
    },
    new ChatMessage
    {
        Id = 3,
        Content = "I understand you're experiencing headaches. To help gather relevant information for a healthcare provider, could you tell me if you're sensitive to light or sound during these headaches?",
        ChatSession = chatSession,
        Sender = "Blossom"
    },
    new ChatMessage
    {
        Id = 4,
        Content = "Yes, actually. Bright lights really bother me when I have the headache, and I've been trying to stay in darker rooms.",
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
        Content = "Now that you mention it, I've been really stressed at work lately and haven't been sleeping well.",
        ChatSession = chatSession,
        Sender = "User"
    },
    new ChatMessage
    {
        Id = 9,
        Content = $"Given the duration and nature of your symptoms, I recommend scheduling an appointment with a healthcare provider as soon as possible. They will be able to properly evaluate your condition and provide appropriate care. I've put this on {doctor.Name}'s radar, so expect a call soon.",
        ChatSession = chatSession,
        Sender = "Blossom"
    }
];
                foreach (var message in messages) {
                    context.Set<ChatMessage>().Add(message);
                    context.SaveChanges();
                }
            }
        });
}

// dotnet tool install --global dotnet-ef
// dotnet ef migrations add MigrationNameHere
// dotnet ef database update