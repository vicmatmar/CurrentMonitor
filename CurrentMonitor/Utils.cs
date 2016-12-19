using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentMonitor
{
    public class Utils
    {
        /// <summary>
        /// converts the value into a string with SI prefix
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>si prefixed string</returns>
        public static string ToSIPrefixedString(double d)
        {
            double exponent = Math.Log10(Math.Abs(d));
            if (Math.Abs(d) >= 1)
            {
                switch ((int)Math.Floor(exponent))
                {
                    case 0:
                    case 1:
                    case 2:
                        return string.Format("{0:00.00}", d);
                    case 3:
                    case 4:
                    case 5:
                        return string.Format("{0:00.00 k}", (d / 1e3));
                    case 6:
                    case 7:
                    case 8:
                        return string.Format("{0:00.00 M}", (d / 1e6));
                    case 9:
                    case 10:
                    case 11:
                        return string.Format("{0:00.00 G}", (d / 1e9));
                    case 12:
                    case 13:
                    case 14:
                        return string.Format("{0:00.00 T}", (d / 1e12));
                    case 15:
                    case 16:
                    case 17:
                        return string.Format("{0:00.00 P}", (d / 1e15));
                    case 18:
                    case 19:
                    case 20:
                        return string.Format("{0:00.00 E}", (d / 1e18));
                    case 21:
                    case 22:
                    case 23:
                        return string.Format("{0:00.00 Z}", (d / 1e21));
                    default:
                        return string.Format("{0:00.00 Y}", (d / 1e24));
                }
            }
            else if (Math.Abs(d) > 0)
            {
                switch ((int)Math.Floor(exponent))
                {
                    case -1:
                    case -2:
                    case -3:
                        return string.Format("{0:00.00 m}", (d * 1e3));
                    case -4:
                    case -5:
                    case -6:
                        return string.Format("{0:00.00 μ}", (d * 1e6));
                    case -7:
                    case -8:
                    case -9:
                        return string.Format("{0:00.00 n}", (d * 1e9));
                    case -10:
                    case -11:
                    case -12:
                        return string.Format("{0:00.00 p}", (d * 1e12));
                    case -13:
                    case -14:
                    case -15:
                        return string.Format("{0:00.00 f}", (d * 1e15));
                    case -16:
                    case -17:
                    case -18:
                        return string.Format("{0:00.00 a}", (d * 1e15));
                    case -19:
                    case -20:
                    case -21:
                        return string.Format("{0:00.00 z}", (d * 1e15));
                    default:
                        return string.Format("{0:00.00 y}", (d * 1e15));
                }
            }
            else
            {
                return "0";
            }
        }

    }
}
