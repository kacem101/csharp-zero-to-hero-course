// TODO 1: Reproduce the race condition above: 4 concurrent Task.Run
// increments of a shared `int counter`, 100,000 iterations each, no
// synchronization. Print the final count and confirm it's usually NOT
// 400,000 (run it a few times if needed).

// TODO 2: Fix it with `lock`. Confirm the count is now always exactly
// 400,000.

// TODO 3: Fix the ORIGINAL version instead using Interlocked.Increment
// on a `long` (avoids lock overhead). Confirm the count is correct
// here too.

// TODO 4 (bug hunt / design fix): given the lock-ordering deadlock
// example above, rewrite MethodTwo so it can never deadlock with
// MethodOne, without changing MethodOne.
