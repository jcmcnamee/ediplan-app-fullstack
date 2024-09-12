namespace Ediplan.Persistence.Helpers;
public static class TruncateToSeconds
{
    public static DateTime Truncate(DateTime dateTime)
    {
        return new DateTime(dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerSecond), DateTimeKind.Utc);
    }
}

