// TODO 1: Write `async Task<int> ComputeAfterDelayAsync(int value, int
// delayMs)` that awaits Task.Delay(delayMs), then returns value * 2.
// Call it from Main (which itself needs to be async — use
// `static async Task Main()`), await it, and print the result.

// TODO 2: Write `async Task<string[]> DownloadAllAsync(HttpClient
// client, string[] urls)` that awaits each GetStringAsync call
// SEQUENTIALLY, in a loop. Time it with Stopwatch.

// TODO 3: Explain in a comment (you don't need Task.WhenAll here — that's
// Lesson 19) why the sequential version in TODO 2 is slower than it
// needs to be for independent downloads, even though no thread is
// "blocked" in the traditional sense.

// TODO 4 (bug hunt): what's wrong with this signature and call?
async Task<int> Square(int x) { await Task.Delay(10); return x * x; }
void Main() { int result = Square(5); } // won't compile — why?
