using System;

namespace CrazyFramework.App.Common.Types
{
	public struct Date
	{
		public int Year { get; }
		public int Month { get; }
		public int Day { get; }

		private DateTime AsDate { get; }

		public Date(DateTime dt)
		{
			Year = dt.Year;
			Month = dt.Month;
			Day = dt.Day;

			AsDate = new DateTime(Year, Month, Day);
		}

		public Date(int year, int month, int day)
		{
			var dt = new DateTime(year, month, day);

			Year = dt.Year;
			Month = dt.Month;
			Day = dt.Day;

			AsDate = new DateTime(Year, Month, Day);
		}

		public static bool operator <(Date d1, Date d2)
		{
			return (d1.Year < d2.Year) || (d1.Year == d2.Year && d1.Month < d2.Month) || (d1.Year == d2.Year && d1.Month == d2.Month && d1.Day < d2.Day);
		}

		public static bool operator ==(Date d1, Date d2)
		{
			return (d1.Year, d1.Month, d1.Day) == (d2.Year, d2.Month, d2.Day);
		}

		public static bool operator <=(Date d1, Date d2)
		{
			return d1 < d2 || d1 == d2;
		}

		public static bool operator >(Date d1, Date d2)
		{
			return !(d1 <= d2);
		}

		public static bool operator >=(Date d1, Date d2)
		{
			return !(d1 < d2);
		}

		public static bool operator !=(Date d1, Date d2)
		{
			return !(d1 == d2);
		}

		public static explicit operator DateTime(Date date) => date.AsDate;

		public static explicit operator Date(DateTime dateTime) => new Date(dateTime);

		public override bool Equals(object obj)
		{
			if (obj == null || !GetType().Equals(obj.GetType()))
			{
				return false;
			}
			return this == (Date)obj;
		}

		public override int GetHashCode()
		{
			return AsDate.GetHashCode();
		}

		public override string ToString()
		{
			return $"{Year}-{Month}-{Day}";
		}
	}
}