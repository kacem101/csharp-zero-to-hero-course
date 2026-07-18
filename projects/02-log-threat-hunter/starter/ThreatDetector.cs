using System;
using System.Collections.Generic;

public record BruteForceAlert(string Ip, DateTime WindowStart, int FailedAttempts);
public record ImpossibleTravelAlert(string User, string FirstIp, string SecondIp, TimeSpan Gap);

public class ThreatDetector
{
    /// <summary>
    /// Detect IPs with 5+ LOGIN_FAILED events inside any 2-minute window.
    /// </summary>
    public List<BruteForceAlert> DetectBruteForce(List<LogEntry> entries)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Detect the same user with LOGIN_SUCCESS from two different IPs
    /// within 60 seconds of each other.
    /// </summary>
    public List<ImpossibleTravelAlert> DetectImpossibleTravel(List<LogEntry> entries)
    {
        throw new NotImplementedException();
    }
}
