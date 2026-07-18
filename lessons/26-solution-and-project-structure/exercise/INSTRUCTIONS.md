# Exercise — Lesson 26: Solution & Project Structure

This lesson is a hands-on CLI walkthrough rather than a single file to fill in.

Work through these steps in your terminal, in a scratch directory:

1. Create a new solution `TaskTracker` with three projects:
   - `src/TaskTracker.Domain` (classlib) — will hold a `TaskItem` class
   - `src/TaskTracker.Console` (console) — entry point
   - `tests/TaskTracker.Tests` (xunit) — tests for Domain

2. Add project references so that:
   - `TaskTracker.Console` references `TaskTracker.Domain`
   - `TaskTracker.Tests` references `TaskTracker.Domain`
   - `Domain` references nothing else in the solution

3. In `TaskTracker.Domain`, add:
   ```csharp
   public record TaskItem(string Title, bool IsDone);
   ```
   and a method `TaskItem MarkDone(TaskItem task) => task with { IsDone = true };`
   (a static class `TaskOperations` works fine for this).

4. In `TaskTracker.Console`'s `Program.cs`, create a `TaskItem`, mark it
   done using `TaskOperations.MarkDone`, and print both the original and
   the updated task.

5. In `TaskTracker.Tests`, write an xUnit `[Fact]` proving `MarkDone`
   returns a new `TaskItem` with `IsDone = true`, without mutating the original.

6. Run `dotnet build` from the solution root and confirm all three
   projects build. Run `dotnet test` and confirm the test passes.

Check `correction/NOTES.md` for the full command sequence and file contents once you've tried it yourself.
