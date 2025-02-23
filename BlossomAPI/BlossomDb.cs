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
}