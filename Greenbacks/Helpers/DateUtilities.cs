using System;
using System.Text;

namespace Greenbacks
{
	public static class DateUtilities
	{
		#region Last date of month

		/// <summary>
		/// Get the last day of the month for any full date.
		/// </summary>
		/// <param name="dtDate">The date to use.</param>
		/// <returns>Last date of the month.</returns>
		public static DateTime GetLastDayOfMonth(DateTime dtDate)
		{
			DateTime dtTo = dtDate;
			dtTo = dtTo.AddMonths(1);
			dtTo = dtTo.AddDays(-(dtTo.Day));
			return dtTo;
		}

		/// <summary>
		/// Get the last day of a month expressed by it's integer value.
		/// </summary>
		/// <param name="iMonth">The month of the year.</param>
		/// <returns>Last date of the month.</returns>
		public static int GetLastDayOfMonth(int iMonth)
		{
			DateTime dtTo = new DateTime(DateTime.Now.Year, iMonth, 1);
			dtTo = dtTo.AddMonths(1);
			dtTo = dtTo.AddDays(-(dtTo.Day));
			return dtTo.Day;
		}

		#endregion

		#region Nth Day

		/// <summary>
		/// Returns the Nth day of a month as a date.
		/// </summary>
		/// <param name="dt">The date.</param>
		/// <param name="nTH">The nTH weekday of the month.</param>
		/// <param name="dayOfWeek">The day of week.</param>
		/// <returns>Adjusted date time.</returns>
		public static DateTime TheDateOfMonth(DateTime dt, int nTH, int dayOfWeek)
		{
			int ND;
			int NQ;
			int d;

			ND = GetLastDayOfMonth(dt.Month);

			if (nTH == 5)
			{
				d = (int)(new DateTime(dt.Year, dt.Month, ND).DayOfWeek);
				NQ = ND - (d + 7 - dayOfWeek) % 7;
			}
			else
			{
				NQ = 7 * nTH - 6 + (dayOfWeek + 7 - (int)(new DateTime(dt.Year, dt.Month, 1).DayOfWeek)) % 7;
			}

			return new DateTime(dt.Year, dt.Month, NQ);
		}
		 
		/// <summary>
		/// Deprecated: Not accurate!!! Returns the Nth day of the month as a date. Not accurate!!!
		/// </summary>
		/// <param name="dt">Date</param>
		/// <param name="Q">The occurance (first through last)</param>
		/// <param name="N">The day of the week (Sunday = 0, Monday = 1, etc)</param>
		/// <returns>DateTime</returns>
		//public static DateTime GetNthDateOfMonth(DateTime dt, int Q, int N)
		//{
		//    int ND;
		//    int NQ;
		//    int d;
		//    DateTime tempDate;

		//    ND = GetLastDayOfMonth(dt.Month);
		//    tempDate = new DateTime(dt.Year, dt.Month, ND);
		//    d = (int)tempDate.DayOfWeek;

		//    if (Q == (int)NthDay.Last)
		//    {
		//        NQ = ND - (d + 7 - N) % 7;
		//    }
		//    else
		//    {
		//        NQ = 7 * (int)Q - 6 + (N + 7 - d) % 7;
		//    }

		//    tempDate = new DateTime(dt.Year, dt.Month, NQ);
		//    return tempDate;
		//}

		/// <summary>
		/// Deprecated: Not accurate!!! Returns the Nth day of the month as a date. Not accurate!!!
		/// </summary>
		/// <param name="dt">The month as an integer</param>
		/// <param name="Q">The occurance (first through last)</param>
		/// <param name="N">The day of the week (Sunday = 0, Monday = 1, etc)</param>
		/// <returns>DateTime</returns>
		//public static DateTime GetNthDateOfMonth(int dt, int Q, int N)
		//{
		//    int ND;
		//    int NQ;
		//    int d;
		//    DateTime tempDate;

		//    ND = GetLastDayOfMonth(dt);
		//    tempDate = new DateTime(DateTime.Now.Year, dt, ND);
		//    d = (int)tempDate.DayOfWeek;

		//    if (Q == (int)NthDay.Last)
		//    {
		//        NQ = ND - (d + 7 - N) % 7;
		//    }
		//    else
		//    {
		//        NQ = 7 * (int)Q - 6 + (N + 7 - d) % 7;
		//    }

		//    tempDate = new DateTime(DateTime.Now.Year, dt, NQ + 1);
		//    return tempDate;
		//}

		/// <summary>
		/// Returns the Nth day of the month as an integer.
		/// </summary>
		/// <param name="dt">The date.</param>
		/// <param name="nTH">The nTH weekday of the month.</param>
		/// <param name="dayOfWeek">The day of week.</param>
		/// <returns>Adjusted day of the month.</returns>
		public static int TheDayOfMonth(DateTime dt, int nTH, int dayOfWeek)
		{
			int ND;
			int NQ;
			int d;

			ND = GetLastDayOfMonth(dt.Month);

			if (nTH == 5)
			{
				d = (int)(new DateTime(dt.Year, dt.Month, ND).DayOfWeek);
				NQ = ND - (d + 7 - dayOfWeek) % 7;
			}
			else
			{
				NQ = 7 * nTH - 6 + (dayOfWeek + 7 - (int)(new DateTime(dt.Year, dt.Month, 1).DayOfWeek)) % 7;
			}

			return NQ;
		}

		/// <summary>
		/// Deprecated: Not accurate!!! Returns the Nth day of the month as an integer. Not accurate!!!
		/// </summary>
		/// <param name="dt">Date</param>
		/// <param name="Q">The occurance (first through last)</param>
		/// <param name="N">The day of the week (Sunday = 0, Monday = 1, etc)</param>
		/// <returns>Integer</returns>
		//public static int GetNthDayOfMonth(DateTime dt, int Q, int N)
		//{
		//    int ND;
		//    int NQ;
		//    int d;
		//    DateTime tempDate;

		//    ND = GetLastDayOfMonth(dt.Month);
		//    tempDate = new DateTime(dt.Year, dt.Month, ND);
		//    d = (int)tempDate.DayOfWeek;

		//    if (Q == (int)NthDay.Last)
		//    {
		//        NQ = ND - (d + 7 - N) % 7;
		//    }
		//    else
		//    {
		//        NQ = 7 * (int)Q - 6 + (N + 7 - d) % 7;
		//    }

		//    return NQ + 1;
		//}

		/// <summary>
		/// Deprecated: Not accurate!!! Returns the Nth day of the month as an integer. Not accurate!!!
		/// </summary>
		/// <param name="dt">The month as an integer</param>
		/// <param name="Q">The occurance (first through last)</param>
		/// <param name="N">The day of the week (Sunday = 0, Monday = 1, etc)</param>
		/// <returns>Integer</returns>
		//public static int GetNthDayOfMonth(int dt, NthDay Q, int N)
		//{
		//    int ND;
		//    int NQ;
		//    int d;
		//    DateTime tempDate;

		//    ND = GetLastDayOfMonth(dt);
		//    tempDate = new DateTime(DateTime.Now.Year, dt, ND);
		//    d = (int)tempDate.DayOfWeek;

		//    if (Q == NthDay.Last)
		//    {
		//        NQ = ND - (d + 7 - N) % 7;
		//    }
		//    else
		//    {
		//        NQ = 7 * (int)Q - 6 + (N + 7 - d) % 7;
		//    }

		//    return NQ;
		//}

		/// <summary>
		/// Takes a date and returns the nTH weekday of the month for the selected date.
		/// </summary>
		/// <param name="dt">The date.</param>
		/// <returns>nTH weekday of the month.</returns>
		public static int NthDayOfMonth(DateTime dt)
		{
			int day = dt.Day / 7;
			int tmp = dt.Day % 7;
			return tmp > 0 ? day += 1 : day;
		}

		#endregion

		#region Date Diff

		/// <summary>
		/// Calculates the difference between two dates.
		/// </summary>
		/// <param name="dateInterval">The interval.</param>
		/// <param name="date1">Date 1</param>
		/// <param name="date2">Date 2</param>
		/// <returns>Number of units between two dates.</returns>
		public static long DateDiff(DateInterval dateInterval, DateTime date1, DateTime date2)
		{
			return DateDiff(dateInterval, date1, date2, System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
		}

		/// <summary>
		/// Calculates the difference between two dates.
		/// </summary>
		/// <param name="dateInterval">The interval.</param>
		/// <param name="date1">Date 1</param>
		/// <param name="date2">Date 2</param>
		/// <param name="firstDayOfWeek">The first day of the week</param>
		/// <returns>Number of units between two dates.</returns>
		public static long DateDiff(DateInterval dateInterval, DateTime date1, DateTime date2, DayOfWeek firstDayOfWeek)
		{
			TimeSpan ts = date2 - date1;

			switch (dateInterval)
			{
				case DateInterval.Second:
					return Round(ts.TotalSeconds);
				case DateInterval.Minute:
					return Round(ts.TotalMinutes);
				case DateInterval.Hour:
					return Round(ts.TotalHours);
				case DateInterval.Day:
				case DateInterval.DayOfYear:
					return Round(ts.TotalDays);
				case DateInterval.Week:
					return Round(ts.TotalDays / 7.0);
				case DateInterval.WeekOfYear:
					while (date2.DayOfWeek != firstDayOfWeek) { date2 = date2.AddDays(-1); }
					while (date1.DayOfWeek != firstDayOfWeek) { date1 = date1.AddDays(-1); }
					ts = date2 - date1;
					return Round(ts.TotalDays / 7.0);
				case DateInterval.Month:
					return (date2.Month - date1.Month) + (12 * (date2.Year - date1.Year));
				case DateInterval.Quarter:
					double d1Quarter = GetQuarter(date1.Month);
					double d2Quarter = GetQuarter(date2.Month);
					double d1 = d2Quarter - d1Quarter;
					double d2 = (4 * (date2.Year - date1.Year));
					return Round(d1 + d2);
				case DateInterval.Year:
					return date2.Year - date1.Year;
				default:
					return 0;
			}
		}

		private static int GetQuarter(int nMonth)
		{
			if (nMonth <= 3) return 1;
			if (nMonth <= 6) return 2;
			if (nMonth <= 9) return 3;
			return 4;
		}

		private static long Round(double dVal)
		{
			return (long)(dVal >= 0 ? Math.Floor(dVal) : Math.Ceiling(dVal));
		}

		#endregion

		#region Easter

		/// <summary>
		/// Returnes the Date of Easter for the given year. Works with years > 1751.
		/// </summary>
		/// <param name="Year">The year.</param>
		/// <returns>Date of easter for the give year.</returns>
		public static DateTime EasterDate(int Year)
		{
			// Gauss Calculation
			int g = Year % 19; // Determine the golden number
			int c = Year / 100; // Determine the century number
			int x = c / 4;
			int y = (8 * c + 13) / 25;
			int z = 19 * g;
			int h = (c - x - y + z + 15) % 30;

			x = h / 28;
			y = 29 / (h + 1);
			z = (21 - g) / 11;
			int i = h - x * (1 - y * z); // The number of days from 21 March to the Paschal full moon

			x = Year / 4;
			y = c / 4;
			int j = (Year + x + i + 2 - c + y) % 7; // The weekday for the Paschal full moon (0=Sunday, 1=Monday, etc.)

			int l = i - j; // The number of days from 21 March to the Sunday on or before the Paschal full moon (a number between -6 and 28)
			x = (l + 40) / 44;

			int Month =  3 + x;
			x = Month / 4;
			int Day = l + 28 - 31 * x;

			return new DateTime(Year, Month, Day);
		}

		#endregion

		/// <summary>
		/// Converts a datetime string to a file friendly format.
		/// </summary>
		/// <param name="dateTime">The datetime as a string</param>
		/// <returns>File friendly datetime string</returns>
		public static string DateTimeToFileFriendly(string dateTime)
		{
			// Non-valid file name characters: \ / : * ? " < > |
			StringBuilder newDateTime = new StringBuilder(dateTime);
			newDateTime.Replace(',', ' ');
			newDateTime.Replace(':', '_');
			newDateTime.Replace('/', '-');

			return newDateTime.ToString();
		}

		// Add appropriate overloads to match TryParse and TryParseExact
		public static DateTime? TryParseNullableDateTime(string text)
		{
			DateTime value;
			return DateTime.TryParse(text, out value) ? value : (DateTime?)null;
		}

		public static string GetDaySuffix(int day)
		{
			switch (day)
			{
				case 1:
				case 21:
				case 31:
					return "st";
				case 2:
				case 22:
					return "nd";
				case 3:
				case 23:
					return "rd";
				default:
					return "th";
			}
		}
	}

	#region Date Enumerators

	public enum NthDay
	{
		None = 0,
		First = 1,
		Second = 2,
		Third = 3,
		Fourth = 4,
		Last = 5
	}

	public enum RecurranceType
	{
		NonRecurring = 1,
		Recurring = 2
	}

	public enum IntervalType
	{
		None = 0,
		Days = 1,
		Weeks = 2,
		Months = 3,
		Years = 4
	}

	public enum DateInterval
	{
		Day,
		DayOfYear,
		Hour,
		Minute,
		Month,
		Quarter,
		Second,
		Week,
		WeekOfYear,
		Year
	}

	#endregion
}
