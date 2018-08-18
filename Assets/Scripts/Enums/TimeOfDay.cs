public enum TimeOfDay
{
    None = 0,
    Any = ~0,
    Morning = 1 << 0,
    Afternoon = 1 << 1,
    Evening = 1 << 2,
}