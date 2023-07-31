#nullable disable
namespace Nanotec.UsbMotor;
public static class MyExtensions
{
    public static string ToBinaryString(this long number, int length = -1)
	{
		const int mask = 1;
		var binary = string.Empty;
		while (number > 0)
		{
			binary = (number & mask) + binary;
			number >>= 1;
		}

		if (length > 0)
			return binary.PadLeft(length, '0');

		return binary;
	}
	public static string ToSimpleString<T>(this IEnumerable<T> values, char separator = ',') => values.Aggregate(string.Empty, (current, val) => current + (val + separator.ToString())).Trim(separator);
}

