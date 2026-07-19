// TODO 1: You have 100,000 (userId, username) pairs, and need to look up
// a username by userId very frequently. Which collection? Write a small
// method `string? LookupUsername(Dictionary<int,string> users, int id)`
// that demonstrates it.

// TODO 2: You're processing a stream of customer support tickets in the
// order they arrive, always handling the OLDEST unresolved ticket first.
// Which collection? Write `Ticket ProcessNext(Queue<Ticket> tickets)`
// that dequeues and returns the next ticket.

// TODO 3: You need to detect if a web crawler has already visited a URL
// (potentially millions of URLs, need fast "have I seen this?" checks,
// order doesn't matter). Which collection? Write `bool VisitIfNew(
// HashSet<string> visited, string url)` that returns true only if the
// URL hadn't been seen before, and marks it visited either way.

// TODO 4: Implement an "undo" feature for a text editor — each edit
// pushes a snapshot, undo pops the most recent one. Which collection?
