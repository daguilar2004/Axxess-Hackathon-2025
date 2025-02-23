public class Patient {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Doctor Doctor { get; set; }
    public int? DoctorId { get; set; }
    public List<ChatSession> ChatSessions { get; set; }

}


public class Doctor {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class ChatSession {
    public int Id { get; set; }
    public Patient Patient { get; set; }
    public int PatientId { get; set; }
}

public class ChatMessage {
    public int Id { get; set; }
    public string Content { get; set; }

    public string Sender { get; set; }
    public int ChatSessionId { get; set; }
    public ChatSession ChatSession { get; set; }
}

public class ChatSessionReport {
    public int Id { get; set; }
    public int ChatSessionId { get; set; }
    public ChatSession ChatSession { get; set; }
    
    // TODO: add pdf binary field
}