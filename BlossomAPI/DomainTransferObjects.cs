public class DoctorDTO {
    public int Id { get; set; }
    public string Name { get; set; }

    public DoctorDTO() { }
    public DoctorDTO(Doctor doctor) =>
    (Id, Name) = (doctor.Id, doctor.Name);
}

public class PatientDTO {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? DoctorId { get; set; }

    public PatientDTO() { }
    public PatientDTO(Patient patient) =>
    (Id, Name, DoctorId) = (patient.Id, patient.Name, patient.DoctorId);
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
    public int ChatSessionId { get; set; }

    public ChatMessageDTO() { }
    public ChatMessageDTO(ChatMessage chatMessage) =>
    (Id, Content, ChatSessionId) = (chatMessage.Id, chatMessage.Content, chatMessage.ChatSessionId);
}