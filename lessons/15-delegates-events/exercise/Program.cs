// TODO 1: Write a method `List<int> FilterAndTransform(List<int> nums,
// Predicate<int> filter, Func<int,int> transform)` that filters then
// maps. Call it to get the squares of only the odd numbers in a list.

// TODO 2: Design a class `Thermostat` with a `double Temperature` and an
// event `event Action<double>? TemperatureChanged`. Setting Temperature
// (via a method `SetTemperature(double t)`) should raise the event with
// the new value, but only if the new value actually differs from the
// old one.

// TODO 3 (bug hunt): rewrite this to use `event` instead of a public
// delegate field, and explain in a comment what bug this prevents.
class AlarmSystem
{
    public Action? Triggered;
}
