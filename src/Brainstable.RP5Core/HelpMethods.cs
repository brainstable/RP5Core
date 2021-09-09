using System.Collections.Generic;
using System.Text;

namespace Brainstable.RP5Core
{
    internal static class HelpMethods
    {
        internal static Encoding CreateEncoding(string text)
        {
            text = text.ToLower();

            Encoding encoding = Encoding.UTF8;
            if (text.Contains("ansi"))
            {
                encoding = Encoding.GetEncoding(1251);
            }
            if (text.Contains("unic") || text.Contains("unicode"))
            {
                encoding = Encoding.Unicode;
            }
            if (text.Contains("utf8") || text.Contains("utf-8"))
            {
                encoding = Encoding.UTF8;
            }
            return encoding;
        }

        /// <summary>
        /// Получить массив строк для вывода в csv
        /// </summary>
        /// <param name="observationPoints">Массив точек наблюдения</param>
        /// <param name="separator">Разделитель</param>
        /// <param name="isFirstLineTitle">Будет ли первая строка строкой заколовков</param>
        /// <returns>Массив строк</returns>
        public static string[] GetArrayCsv(ObservationPoint[] observationPoints, char separator, bool isFirstLineTitle = true)
        {
            StringBuilder sb = new StringBuilder();

            if (isFirstLineTitle)
            {
                sb.AppendLine(GetLineFirst(separator));
            }
            string[] arr = isFirstLineTitle
                ? new string[observationPoints.Length + 1]
                : new string[observationPoints.Length];
            if (isFirstLineTitle)
                arr[0] = sb.ToString();

            for (int i = isFirstLineTitle ? 1 : 0; i < arr.Length; i++)
            {
                arr[i] = GetLineCsv(observationPoints[isFirstLineTitle ? i - 1 : i], separator);
            }
            return arr;
        }

        /// <summary>
        /// Получить массив строк для вывода в csv
        /// </summary>
        /// <param name="observationPoints">Отсортированный набор точек наблюдения</param>
        /// <param name="separator">Разделитель</param>
        /// <param name="isFirstLineTitle">Будет ли первая строка строкой заколовков</param>
        /// <returns>Массив строк</returns>
        public static string[] GetArrayCsv(SortedSet<ObservationPoint> observationPoints, char separator, bool isFirstLineTitle = true)
        {
            StringBuilder sb = new StringBuilder();

            if (isFirstLineTitle)
            {
                sb.AppendLine(GetLineFirst(separator));
            }
            string[] arr = isFirstLineTitle
                ? new string[observationPoints.Count + 1]
                : new string[observationPoints.Count];
            if (isFirstLineTitle)
                arr[0] = sb.ToString();

            int n = 0;
            if (isFirstLineTitle)
            {
                arr[0] = GetLineFirst(separator);
                n = 1;
            }
            foreach (var point in observationPoints)
            {
                arr[n++] = GetLineCsv(point, separator);
            }
            return arr;
        }

        public static string GetLineFirst(char separator)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("DT" + separator);
            sb.Append("T" + separator);
            sb.Append("P" + separator);
            sb.Append("Po" + separator);
            sb.Append("PA" + separator);
            sb.Append("U" + separator);
            sb.Append("DD" + separator);
            sb.Append("FF" + separator);
            sb.Append("FF10" + separator);
            sb.Append("FF3" + separator);
            sb.Append("N" + separator);
            sb.Append("WW" + separator);
            sb.Append("W1" + separator);
            sb.Append("W2" + separator);
            sb.Append("W'W'" + separator);
            sb.Append("Tn" + separator);
            sb.Append("Tx" + separator);
            sb.Append("Cl" + separator);
            sb.Append("C" + separator);
            sb.Append("Nh" + separator);
            sb.Append("H" + separator);
            sb.Append("Cm" + separator);
            sb.Append("Ch" + separator);
            sb.Append("VV" + separator);
            sb.Append("Td" + separator);
            sb.Append("RRR" + separator);
            sb.Append("TR" + separator);
            sb.Append("E" + separator);
            sb.Append("Tg" + separator);
            sb.Append("E'" + separator);
            sb.Append("SSS");
            return sb.ToString();
        }

        public static string GetLineCsv(ObservationPoint observationPoint, char separator)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(observationPoint.DateTime.ToString() + separator);
            sb.Append(observationPoint.T + separator);
            sb.Append(observationPoint.P + separator);
            sb.Append(observationPoint.Po + separator);
            sb.Append(observationPoint.PA + separator);
            sb.Append(observationPoint.U + separator);
            sb.Append(observationPoint.DD + separator);
            sb.Append(observationPoint.FF + separator);
            sb.Append(observationPoint.FF10 + separator);
            sb.Append(observationPoint.FF3 + separator);
            sb.Append(observationPoint.N + separator);
            sb.Append(observationPoint.WW + separator);
            sb.Append(observationPoint.W1 + separator);
            sb.Append(observationPoint.W2 + separator);
            sb.Append(observationPoint.W_W_ + separator);
            sb.Append(observationPoint.Tn + separator);
            sb.Append(observationPoint.Tx + separator);
            sb.Append(observationPoint.Cl + separator);
            sb.Append(observationPoint.C + separator);
            sb.Append(observationPoint.Nh + separator);
            sb.Append(observationPoint.H + separator);
            sb.Append(observationPoint.Cm + separator);
            sb.Append(observationPoint.Ch + separator);
            sb.Append(observationPoint.VV + separator);
            sb.Append(observationPoint.Td + separator);
            sb.Append(observationPoint.RRR + separator);
            sb.Append(observationPoint.TR + separator);
            sb.Append(observationPoint.E + separator);
            sb.Append(observationPoint.Tg + separator);
            sb.Append(observationPoint.Es + separator);
            sb.Append(observationPoint.SSS);
            return sb.ToString();
        }
    }
}