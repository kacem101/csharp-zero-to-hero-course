using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task<int> ComputeAfterDelayAsync(int value, int delayMs)
    {
        await Task.Delay(delayMs);
        return value * 2;
    }

    static async Task<string[]> DownloadAllAsync(HttpClient client, string[] urls)
    {
        var results = new string[urls.Length];
        for (int i = 0; i < urls.Length; i++)
            results[i] = await client.GetStringAsync(urls[i]); // one at a time — see TODO 3
        return results;
    }

    static async Task Main()
    {
        int doubled = await ComputeAfterDelayAsync(21, 200);
        Console.WriteLine(doubled); // 42

        // (Downloading real URLs needs network; the point here is the
        // pattern, not the live call — Lesson 20 covers HttpClient in
        // depth with real requests.)
    }
}

// TODO 3 answer: awaiting sequentially means each download's network
// wait happens one after another — download 2 doesn't even START until
// download 1 has fully finished. No thread is blocked while waiting (the
// thread IS freed during each await), but the total WALL-CLOCK TIME is
// still the sum of every download's duration, because they're not
// happening concurrently. Freeing threads solves scalability (how many
// requests a server can handle at once); it does not, by itself, make
// independent operations run in parallel with each other. That needs
// Task.WhenAll — covered in Lesson 19.

// TODO 4 answer: `Square` returns `Task<int>`, not `int` — you cannot
// assign a Task<int> directly to an int variable; you must `await` it to
// get the actual value. And `await` can only be used inside a method
// marked `async`, so `void Main()` would need to be `static async Task
// Main()` (or `Main` needs to call an async helper via `.Result`, which
// reintroduces the blocking problem from the concept section above).
// Fix:
// static async Task Main() { int result = await Square(5); }
