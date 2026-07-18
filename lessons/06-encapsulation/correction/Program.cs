using System;
using System.Collections.Generic;
using System.Linq;

class Playlist
{
    private const int MaxSongs = 100;
    private readonly List<string> _songs = new();

    public IReadOnlyList<string> Songs => _songs;
    public int Count => _songs.Count;

    public void AddSong(string title)
    {
        if (_songs.Count >= MaxSongs)
            throw new InvalidOperationException("Playlist is full.");
        if (_songs.Contains(title))
            throw new ArgumentException($"'{title}' is already in the playlist.");
        _songs.Add(title);
    }

    public void RemoveSong(string title) => _songs.Remove(title); // no-op if absent
}

class Program
{
    static void Main()
    {
        var playlist = new Playlist();
        playlist.AddSong("Bohemian Rhapsody");
        playlist.AddSong("Hotel California");

        try { playlist.AddSong("Bohemian Rhapsody"); }
        catch (ArgumentException ex) { Console.WriteLine(ex.Message); }

        playlist.RemoveSong("Not There"); // safe no-op
        Console.WriteLine(playlist.Count); // 2

        // playlist.Songs.Add("x"); // would NOT compile — IReadOnlyList has no Add
    }
}
