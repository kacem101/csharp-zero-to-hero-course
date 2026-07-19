# Lesson 19a — Multithreading & Locks

## Why this matters
`async`/`await` (Lessons 18–19) is about not blocking a thread during I/O waits — it does **not**, by itself, run your code on multiple CPU cores at once. Real parallel execution (and the shared-state bugs that come with it — race conditions) is a separate topic, and one directly relevant to you as a security student: race conditions are a genuine, exploitable vulnerability class (time-of-check-to-time-of-use bugs are a classic example).

## The concept

### `Task.Run` — actual parallel CPU work
```csharp
Task<int> t1 = Task.Run(() => ExpensiveComputation(1));
Task<int> t2 = Task.Run(() => ExpensiveComputation(2));
int[] results = await Task.WhenAll(t1, t2); // both run on separate thread-pool threads, genuinely concurrently
```
Use `Task.Run` for CPU-bound work you want to parallelize. Use plain `await someIoCall()` (Lesson 18) for I/O-bound waits — mixing these up (wrapping an I/O call in `Task.Run` "to make it async") wastes a thread pool thread for no benefit.

### The race condition
```csharp
int counter = 0;
void Increment() { for (int i = 0; i < 100_000; i++) counter++; } // NOT atomic!

var tasks = new List<Task>();
for (int i = 0; i < 4; i++) tasks.Add(Task.Run(Increment));
await Task.WhenAll(tasks);
Console.WriteLine(counter); // NOT 400,000 — some increments get lost
```
`counter++` is actually three steps: read the value, add 1, write it back. If two threads read the same value before either writes back, one increment is silently lost. This is a **race condition** — the bug depends on timing, so it may not show up every run, which makes it especially dangerous (and especially relevant to security: an attacker deliberately timing operations to win a race is a real attack technique — TOCTOU bugs).

### `lock` — mutual exclusion
```csharp
readonly object _sync = new();
void Increment() { for (int i = 0; i < 100_000; i++) { lock (_sync) { counter++; } } }
```
`lock` ensures only one thread at a time can execute the code inside the block — other threads wait their turn. This fixes correctness at the cost of some performance (threads now serialize on that one operation).

### `Interlocked` — lighter-weight atomics for simple operations
```csharp
using System.Threading;
long counter = 0;
void Increment() { for (int i = 0; i < 100_000; i++) Interlocked.Increment(ref counter); }
```
For simple operations like increment/decrement/add/compare-and-swap, `Interlocked` avoids the overhead of a full lock by using hardware-level atomic instructions directly.

### The lock-ordering deadlock (different from the async deadlock in Lesson 19)
```csharp
object lockA = new(), lockB = new();
void MethodOne() { lock (lockA) { lock (lockB) { /* ... */ } } }
void MethodTwo() { lock (lockB) { lock (lockA) { /* ... */ } } } // opposite order!
```
If `MethodOne` and `MethodTwo` run concurrently, `MethodOne` can grab `lockA` while `MethodTwo` grabs `lockB` — then each waits forever for the lock the other is holding. **Fix: always acquire shared locks in the same, consistent order everywhere in your codebase.**
