using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task<int> GetUserScoreAsync(int id) { await Task.Delay(200); return 100; }
    static async Task<int> GetBonusAsync(int id) { await Task.Delay(200); return 20; }
    static async Task<int> GetPenaltyAsync(int id) { await Task.Delay(200); return -10; }

    static async Task<int> GetTotalAsync(int id)
    {
        var sw = Stopwatch.StartNew();
        Task<int> scoreTask = GetUserScoreAsync(id);
        Task<int> bonusTask = GetBonusAsync(id);
        Task<int> penaltyTask = GetPenaltyAsync(id);
        await Task.WhenAll(scoreTask, bonusTask, penaltyTask);
        sw.Stop();
        Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms"); // ~200ms, not ~600ms
        return scoreTask.Result + bonusTask.Result + penaltyTask.Result;
    }

    static async Task<string> SlowOperationAsync(CancellationToken ct)
    {
        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(100, ct); // throws OperationCanceledException when cancelled
            Console.WriteLine($"Step {i + 1} done");
        }
        return "completed";
    }

    // TODO 3 fix: expose only the async version publicly; if a sync
    // entry point is unavoidable, document that it must not be called
    // from a context with a synchronization context (or better: make the
    // whole call chain async instead of bridging with .Result).
    public class ReportService
    {
        public async Task<string> GenerateReportAsync()
        {
            await Task.Delay(100);
            return "report";
        }
        // No sync-over-async GenerateReport() wrapper — callers must go async too.
    }

    // TODO 4 fix
    public class User { }
    static async Task SaveUserAsync(User? user)
    {
        await Task.Delay(50);
        if (user == null) throw new ArgumentNullException(nameof(user)); // caller CAN catch this now
    }

    static async Task Main()
    {
        Console.WriteLine(await GetTotalAsync(1)); // 110, elapsed ~200ms

        using var cts = new CancellationTokenSource();
        cts.CancelAfter(350);
        try { await SlowOperationAsync(cts.Token); }
        catch (OperationCanceledException) { Console.WriteLine("Cancelled after ~3 steps"); }

        try { await SaveUserAsync(null); }
        catch (ArgumentNullException) { Console.WriteLine("Caught cleanly — no crash"); }
    }
}
