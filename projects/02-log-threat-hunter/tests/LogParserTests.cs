using System;
using System.IO;
using Xunit;

public class LogParserTests
{
    [Fact]
    public void ParseFile_SkipsMalformedLines_ReturnsOnlyValidEntries()
    {
        string tempPath = Path.GetTempFileName();
        File.WriteAllLines(tempPath, new[]
        {
            "2026-07-18 10:00:01 LOGIN_SUCCESS user=bob ip=198.51.100.9",
            "THIS LINE IS MALFORMED",
            "2026-07-18 10:00:05 LOGIN_FAILED user=alice ip=203.0.113.5"
        });

        var parser = new LogParser();
        var entries = parser.ParseFile(tempPath);

        Assert.Equal(2, entries.Count);
        File.Delete(tempPath);
    }
}

public class ThreatDetectorTests
{
    [Fact]
    public void DetectBruteForce_FiveFailuresInTwoMinutes_RaisesAlertForThatIp()
    {
        var baseTime = new DateTime(2026, 7, 18, 10, 0, 0);
        var entries = new System.Collections.Generic.List<LogEntry>();
        for (int i = 0; i < 5; i++)
            entries.Add(new LogEntry(baseTime.AddSeconds(i * 15), "LOGIN_FAILED", "alice", "203.0.113.5"));

        var detector = new ThreatDetector();
        var alerts = detector.DetectBruteForce(entries);

        Assert.Contains(alerts, a => a.Ip == "203.0.113.5");
    }

    [Fact]
    public void DetectBruteForce_FailuresSpreadOverLongPeriod_DoesNotRaiseAlert()
    {
        var baseTime = new DateTime(2026, 7, 18, 10, 0, 0);
        var entries = new System.Collections.Generic.List<LogEntry>();
        for (int i = 0; i < 5; i++)
            entries.Add(new LogEntry(baseTime.AddMinutes(i * 30), "LOGIN_FAILED", "alice", "203.0.113.5"));

        var detector = new ThreatDetector();
        var alerts = detector.DetectBruteForce(entries);

        Assert.Empty(alerts);
    }

    [Fact]
    public void DetectImpossibleTravel_SameUserDifferentIpsWithinAMinute_RaisesAlert()
    {
        var baseTime = new DateTime(2026, 7, 18, 10, 0, 0);
        var entries = new System.Collections.Generic.List<LogEntry>
        {
            new LogEntry(baseTime, "LOGIN_SUCCESS", "carol", "192.0.2.44"),
            new LogEntry(baseTime.AddSeconds(30), "LOGIN_SUCCESS", "carol", "45.33.10.7"),
        };

        var detector = new ThreatDetector();
        var alerts = detector.DetectImpossibleTravel(entries);

        Assert.Contains(alerts, a => a.User == "carol");
    }
}
