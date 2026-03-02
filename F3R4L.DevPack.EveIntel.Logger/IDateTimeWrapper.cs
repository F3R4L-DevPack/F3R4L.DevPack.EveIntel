
namespace F3R4L.DevPack.EveIntel.Logger
{
    public interface IDateTimeWrapper
    {
        DateOnly DateOnlyNow { get; }
        DateTime Now { get; }
        TimeOnly TimeOnlyNow { get; }
        DateOnly UtcDateOnlyNow { get; }
        DateTime UtcNow { get; }
        TimeOnly UtcTimeOnlyNow { get; }
    }
}