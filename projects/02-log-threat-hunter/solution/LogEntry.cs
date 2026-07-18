using System;

public record LogEntry(DateTime Timestamp, string EventType, string User, string Ip);
