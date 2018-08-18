public enum Weather
{
    None = 0,
    Any = ~0,
    Clear = 1 << 0,
    Sunny = 1 << 1,
    Cloudy = 1 << 2,
    Rainy = 1 << 3,
}
