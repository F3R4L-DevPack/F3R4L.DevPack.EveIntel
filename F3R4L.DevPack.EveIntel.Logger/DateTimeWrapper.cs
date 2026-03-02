using System.Diagnostics.CodeAnalysis;

namespace F3R4L.DevPack.EveIntel.Logger
{
    [ExcludeFromCodeCoverage]
    public class DateTimeWrapper : IDateTimeWrapper
    {
        public DateTime Now => DateTime.Now;

        public TimeOnly TimeOnlyNow => TimeOnly.FromDateTime(Now);
        public DateOnly DateOnlyNow => DateOnly.FromDateTime(Now);
        public DateTime UtcNow => DateTime.UtcNow;
        public TimeOnly UtcTimeOnlyNow => TimeOnly.FromDateTime(UtcNow);
        public DateOnly UtcDateOnlyNow => DateOnly.FromDateTime(UtcNow);
    }
}
