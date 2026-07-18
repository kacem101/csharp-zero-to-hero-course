using System;
using System.Collections.Generic;
using System.Linq;

public record BruteForceAlert(string Ip, DateTime WindowStart, int FailedAttempts);
public record ImpossibleTravelAlert(string User, string FirstIp, string SecondIp, TimeSpan Gap);

public class ThreatDetector
{
    private static readonly TimeSpan BruteForceWindow = TimeSpan.FromMinutes(2);
    private const int BruteForceThreshold = 5;
    private static readonly TimeSpan ImpossibleTravelWindow = TimeSpan.FromSeconds(60);

    public List<BruteForceAlert> DetectBruteForce(List<LogEntry> entries)
    {
        var alerts = new List<BruteForceAlert>();

        var failuresByIp = entries
            .Where(e => e.EventType == "LOGIN_FAILED")
            .GroupBy(e => e.Ip);

        foreach (var group in failuresByIp)
        {
            var timestamps = group.Select(e => e.Timestamp).OrderBy(t => t).ToList();

            // Sliding window: for each failure, count how many other
            // failures from this IP fall within the next 2 minutes.
            for (int i = 0; i < timestamps.Count; i++)
            {
                var windowEnd = timestamps[i] + BruteForceWindow;
                int countInWindow = timestamps.Count(t => t >= timestamps[i] && t <= windowEnd);
                if (countInWindow >= BruteForceThreshold)
                {
                    alerts.Add(new BruteForceAlert(group.Key, timestamps[i], countInWindow));
                    break; // one alert per IP is enough for this report
                }
            }
        }

        return alerts;
    }

    public List<ImpossibleTravelAlert> DetectImpossibleTravel(List<LogEntry> entries)
    {
        var alerts = new List<ImpossibleTravelAlert>();

        var successesByUser = entries
            .Where(e => e.EventType == "LOGIN_SUCCESS")
            .GroupBy(e => e.User);

        foreach (var group in successesByUser)
        {
            var events = group.OrderBy(e => e.Timestamp).ToList();
            for (int i = 1; i < events.Count; i++)
            {
                var gap = events[i].Timestamp - events[i - 1].Timestamp;
                if (events[i].Ip != events[i - 1].Ip && gap <= ImpossibleTravelWindow)
                {
                    alerts.Add(new ImpossibleTravelAlert(group.Key, events[i - 1].Ip, events[i].Ip, gap));
                }
            }
        }

        return alerts;
    }
}
