using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BlossomDb>(opt => opt.UseInMemoryDatabase("BlossomDB"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "BlossomAPI";
    config.Title = "BlossomAPI v1";
    config.Version = "v1";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "BlossomAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

RouteGroupBuilder doctors = app.MapGroup("/doctors");

doctors.MapPost("/", CreateDoctor);
// TODO: these should require doctor auth
doctors.MapGet("/{doctorId}", GetDoctor);
doctors.MapPut("/{doctorId}", UpdateDoctor);
doctors.MapDelete("/{doctorId}", DeleteDoctor);

if (app.Environment.IsDevelopment())
doctors.MapGet("/", GetAllDoctors);

RouteGroupBuilder patients = app.MapGroup("/patients");


patients.MapPost("/", CreatePatient);
// TODO: these should require patient auth
patients.MapGet("/{patientId}", GetPatient);
patients.MapPut("/{patientId}", UpdatePatient);
patients.MapDelete("/{patientId}", DeletePatient);
patients.MapGet("/{patientId}/chatsessions", GetPatientChatSessions);


if (app.Environment.IsDevelopment())
patients.MapGet("/", GetAllPatients);



RouteGroupBuilder chat = app.MapGroup("/chatsessions");

chat.MapPost("/", CreateChatSession);
// TODO: these should require patient auth
chat.MapGet("/{chatSessionId}", GetChatSession);
chat.MapGet("/{chatSessionId}/messages", GetChatSessionMessages);
chat.MapPost("/{chatSessionId}", AddMessageToChatSession);

app.Run();


static async Task<IResult> GetAllDoctors(BlossomDb db)
{
    return TypedResults.Ok(await db.Doctors.Select(x => new DoctorDTO(x)).ToArrayAsync());
}

static async Task<IResult> GetDoctor(int doctorId, BlossomDb db)
{
    return await db.Doctors.FindAsync(doctorId)
        is Doctor doctor
            ? TypedResults.Ok(new DoctorDTO(doctor))
            : TypedResults.NotFound();
}

static async Task<IResult> CreateDoctor(DoctorDTO doctorDTO, BlossomDb db)
{
    var doctor = new Doctor
    {
        Name = doctorDTO.Name
    };

    db.Doctors.Add(doctor);
    await db.SaveChangesAsync();

    doctorDTO = new DoctorDTO(doctor);

    return TypedResults.Created($"/doctors/{doctor.Id}", doctorDTO);
}

static async Task<IResult> UpdateDoctor(int doctorId, DoctorDTO doctorDTO, BlossomDb db)
{
    var doctor = await db.Doctors.FindAsync(doctorId);

    if (doctor is null) return TypedResults.NotFound();

    doctor.Name = doctorDTO.Name;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteDoctor(int doctorId, BlossomDb db)
{
    if (await db.Doctors.FindAsync(doctorId) is Doctor doctor)
    {
        db.Doctors.Remove(doctor);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}

static async Task<IResult> CreatePatient(PatientDTO patientDTO, BlossomDb db)
{
    var patient = new Patient
    {
        Name = patientDTO.Name
    };

    if (patientDTO.DoctorId is not null)
    {
        var doctor = await db.Doctors.FindAsync(patientDTO.DoctorId.Value);
        if (doctor is null) return TypedResults.NotFound();
        patient.Doctor = doctor;
    }

    db.Patients.Add(patient);
    await db.SaveChangesAsync();

    patientDTO = new PatientDTO(patient);

    return TypedResults.Created($"/patients/{patient.Id}", patientDTO);
}

static async Task<IResult> UpdatePatient(int patientId, PatientDTO patientDTO, BlossomDb db)
{
    var patient = await db.Patients.FindAsync(patientId);

    if (patient is null) return TypedResults.NotFound();

    patient.Name = patientDTO.Name;
    if (patientDTO.DoctorId is not null)
    {
        var doctor = await db.Doctors.FindAsync(patientDTO.DoctorId.Value);
        if (doctor is null) return TypedResults.NotFound();
        patient.Doctor = doctor;
    }

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeletePatient(int patientId, BlossomDb db)
{
    if (await db.Patients.FindAsync(patientId) is Patient patient)
    {
        db.Patients.Remove(patient);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}

static async Task<IResult> GetPatient(int patientId, BlossomDb db)
{
    return await db.Patients.FindAsync(patientId)
        is Patient patient
            ? TypedResults.Ok(new PatientDTO(patient))
            : TypedResults.NotFound();
}

static async Task<IResult> GetAllPatients(BlossomDb db)
{
    return TypedResults.Ok(await db.Patients.Select(x => new PatientDTO(x)).ToArrayAsync());
}


static async Task<IResult> GetPatientChatSessions(int patientId, BlossomDb db)
{
    return await db.Patients.FindAsync(patientId)
        is Patient patient
            ? TypedResults.Ok(await db.ChatSessions.Where(x => x.PatientId == patient.Id).Select(x => new ChatSessionDTO(x)).ToArrayAsync())
            : TypedResults.NotFound();
}

static async Task<IResult> CreateChatSession(ChatSessionDTO chatSessionDTO, BlossomDb db)
{
    var chatSession = new ChatSession
    {
        PatientId = chatSessionDTO.PatientId
    };

    db.ChatSessions.Add(chatSession);
    await db.SaveChangesAsync();

    chatSessionDTO = new ChatSessionDTO(chatSession);

    return TypedResults.Created($"/chatsessions/{chatSession.Id}", chatSessionDTO);
}

static async Task<IResult> GetChatSession(int chatSessionId, BlossomDb db)
{
    return await db.ChatSessions.FindAsync(chatSessionId)
        is ChatSession chatSession
            ? TypedResults.Ok(new ChatSessionDTO(chatSession))
            : TypedResults.NotFound();
}

static async Task<IResult> GetChatSessionMessages(int chatSessionId, BlossomDb db)
{
    var messages = await db.ChatMessages.Where(x => x.ChatSessionId == chatSessionId).Select(x => new ChatMessageDTO(x)).ToArrayAsync();
    return TypedResults.Ok(messages);
}

static async Task<IResult> AddMessageToChatSession(int chatSessionId, ChatMessageDTO chatMessageDTO, BlossomDb db)
{
    var chatMessage = new ChatMessage
    {
        ChatSessionId = chatSessionId,
        Content = chatMessageDTO.Content
    };

    db.ChatMessages.Add(chatMessage);
    await db.SaveChangesAsync();

    chatMessageDTO = new ChatMessageDTO(chatMessage);

    return TypedResults.Created($"/chatsessions/{chatMessage.ChatSessionId}/messages/{chatMessage.Id}", chatMessageDTO);
}