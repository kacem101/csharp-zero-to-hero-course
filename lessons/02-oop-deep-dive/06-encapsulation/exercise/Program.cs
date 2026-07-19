// TODO: Design a `class Playlist` that protects these invariants:
//   1. A playlist can never have more than 100 songs.
//   2. The same song title can't be added twice.
//   3. Songs can only be removed by exact title match, and removing a
//      title that isn't there should not throw — it should just do nothing.
// The internal song list must not be exposed as a mutable public field.
// Provide: AddSong(string title), RemoveSong(string title), Count,
// and a read-only view of the songs.
