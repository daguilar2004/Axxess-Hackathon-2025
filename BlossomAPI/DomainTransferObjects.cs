public class DoctorDTO {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public DoctorDTO() { }
    public DoctorDTO(Doctor doctor) =>
    (Id, Name, Email) = (doctor.Id, doctor.Name, doctor.Email);
}

public class PatientDTO {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int? DoctorId { get; set; }

    public PatientDTO() { }
    public PatientDTO(Patient patient) =>
    (Id, Name, DoctorId, Email) = (patient.Id, patient.Name, patient.DoctorId, patient.Email);
}

public class ChatSessionDTO {
    public int Id { get; set; }
    public int PatientId { get; set; }

    public ChatSessionDTO() { }
    public ChatSessionDTO(ChatSession chatSession) =>
    (Id, PatientId) = (chatSession.Id, chatSession.PatientId);
}

public class ChatMessageDTO {
    public int Id { get; set; }
    public string Content { get; set; }
    public string Sender { get; set; }

    public ChatMessageDTO() { }
    public ChatMessageDTO(ChatMessage chatMessage) =>
    (Id, Content, Sender) = (chatMessage.Id, chatMessage.Content, chatMessage.Sender);
}