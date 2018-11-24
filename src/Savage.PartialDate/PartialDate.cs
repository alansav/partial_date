using System;

namespace Savage
{
    public class PartialDate
    {
        public readonly short? Year;
        public readonly byte? Month;
        public readonly byte? Day;

        public PartialDate(short? year, byte? month, byte? day)
        {
            if (!year.HasValue && !month.HasValue && !day.HasValue)
            {
                throw new ArgumentException("At least one of the elements must have a value");
            }

            if (!YearIsValid(year))
            {
                throw new ArgumentException(message: $"Year value must be a value between 1 and 9999");
            }

            if (!MonthIsValid(month))
            {
                throw new ArgumentException(message: $"Month value must be a value between 1 and 12.");
            }

            if (!DayIsValid(year, month, day))
            {
                throw new ArgumentException(message: $"Month cannot have {day} days.");
            }

            Year = year;
            Month = month;
            Day = day;
        }

        private bool YearIsValid(short? year)
        {
            return !year.HasValue || year >= 1 && year <= 9999;
        }

        private bool MonthIsValid(byte? month)
        {
            return !month.HasValue || month >= 1 && month <= 12;
        }

        private bool DayIsValid(short? year, byte? month, byte? day)
        {
            int maxDaysInMonth = DateTime.DaysInMonth(year ?? 2000, month ?? 1);
            return !day.HasValue || day >= 1 && day <= maxDaysInMonth;
        }

        public bool IsCompleteDate => Year.HasValue && Month.HasValue && Day.HasValue;

        public DateTime ToDateTime()
        {
            if (!Year.HasValue || !Month.HasValue || !Day.HasValue)
            {
                throw new InvalidOperationException("Cannot convert To DateTime object when elements are null.");
            }

            return new DateTime(Year.Value, Month.Value, Day.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            PartialDate other = (PartialDate)obj;
            return Year == other.Year && Month == other.Month && Day == other.Day;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Year.GetHashCode();
                hashCode = (hashCode * 397) ^ Month.GetHashCode();
                hashCode = (hashCode * 397) ^ Day.GetHashCode();
                return hashCode;
            }
        }
    }
}
