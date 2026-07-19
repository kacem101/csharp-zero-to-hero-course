// TODO 1: Write async Task<int> GetUserScoreAsync(int id), GetBonusAsync(int id),
// GetPenaltyAsync(int id) — each does `await Task.Delay(200)` then returns
// a fixed int. Write a method that gets all three CONCURRENTLY with
// Task.WhenAll and sums them. Time it with Stopwatch and confirm it's
// close to 200ms, not 600ms.

// TODO 2: Write async Task<string> SlowOperationAsync(CancellationToken ct)
// that loops `await Task.Delay(100, ct)` ten times (checking cancellation
// each iteration via the token passed to Delay). Cancel it after 350ms
// using a CancellationTokenSource, catch OperationCanceledException, and
// print how far it got.

// TODO 3 (bug hunt): identify the deadlock risk and fix it.
public class ReportService
{
    public string GenerateReport() => GenerateReportAsync().Result;
    public async Task<string> GenerateReportAsync()
    {
        await Task.Delay(100);
        return "report";
    }
}

// TODO 4 (bug hunt): convert this to avoid async void outside of an
// actual UI event handler.
public async void SaveUserAsync(User user) { await Task.Delay(50); if (user == null) throw new ArgumentNullException(); }
