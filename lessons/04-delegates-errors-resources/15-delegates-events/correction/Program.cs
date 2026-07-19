using System;
using System.Collections.Generic;

class Thermostat
{
    public double Temperature { get; private set; }
    public event Action<double>? TemperatureChanged;

    public void SetTemperature(double t)
    {
        if (Math.Abs(t - Temperature) < 0.0001) return; // no real change, don't fire
        Temperature = t;
        TemperatureChanged?.Invoke(t);
    }
}

// TODO 3 fix
class AlarmSystem
{
    public event Action? Triggered; // `event` — subscribers can += / -= but never overwrite each other
    public void Trigger() => Triggered?.Invoke();
}
// Bug this prevents: with a plain public `Action? Triggered` field, any
// code with a reference to the AlarmSystem could do
// `alarm.Triggered = someHandler;` and silently WIPE OUT every other
// subscriber that had already registered — e.g. a security module's
// handler could be erased by an unrelated part of the app. `event`
// makes that assignment a compile error; only += and -= are allowed
// from outside the declaring class.

class Program
{
    static List<int> FilterAndTransform(List<int> nums, Predicate<int> filter, Func<int, int> transform)
    {
        var result = new List<int>();
        foreach (var n in nums)
            if (filter(n)) result.Add(transform(n));
        return result;
    }

    static void Main()
    {
        var squaresOfOdds = FilterAndTransform(
            new List<int> { 1, 2, 3, 4, 5 },
            n => n % 2 != 0,
            n => n * n);
        Console.WriteLine(string.Join(", ", squaresOfOdds)); // 1, 9, 25

        var thermostat = new Thermostat();
        thermostat.TemperatureChanged += t => Console.WriteLine($"New temp: {t}");
        thermostat.SetTemperature(22.0); // fires
        thermostat.SetTemperature(22.0); // no change, doesn't fire

        var alarm = new AlarmSystem();
        alarm.Triggered += () => Console.WriteLine("Handler A: calling police");
        alarm.Triggered += () => Console.WriteLine("Handler B: sounding siren");
        alarm.Trigger(); // both handlers run — neither overwrote the other
    }
}
