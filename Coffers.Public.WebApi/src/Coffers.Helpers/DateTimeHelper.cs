using System;

namespace Coffers.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime Trunc(this DateTime date, DateTruncType type)
        {
            switch (type)
            {
                case DateTruncType.Day:
                    return new DateTime(date.Year, date.Month, date.Day);
                case DateTruncType.Month:
                    return new DateTime(date.Year, date.Month, 1);
                case DateTruncType.Year:
                    return new DateTime(date.Year, 1, 1);
                case DateTruncType.Hour:
                    return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);
                case DateTruncType.Minute:
                    return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
                case DateTruncType.Second:
                    return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
