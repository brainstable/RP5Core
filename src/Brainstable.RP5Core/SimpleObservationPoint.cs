using System;
using System.Collections.Generic;

namespace Brainstable.RP5Core
{
    public class SimpleObservationPoint
    {
        public DateTime DateTime { get; set; }
        public double? Temperature { get; set; }
        public double? MinTemperature { get; set; }
        public double? MaxTemperature { get; set; }
        public double? Rainfall { get; set; }
        public double? SnowHight { get; set; }

        public SimpleObservationPoint()
        {
            Temperature = null;
            MinTemperature = null;
            MaxTemperature = null;
            Rainfall = null;
            SnowHight = null;
        }

        public override string ToString()
        {
            return DateTime.ToString();
        }

        public static SimpleObservationPoint CreateFromArrayValues(string[] stringValues, SchemaRP5 schema)
        {
            return CreateFromArrayValues(stringValues, schema.Structure);
        }

        public static SimpleObservationPoint CreateFromArrayValues(string[] stringValues, Dictionary<string, int> structure)
        {
            SimpleObservationPoint p = null;
            double t;
            try
            {
                p = new SimpleObservationPoint();
                p.DateTime = Convert.ToDateTime(stringValues[0].Replace("\"", "").Replace('.', ','));
                if (structure.ContainsKey("T"))
                {
                    if (Double.TryParse(stringValues[structure["T"]].Replace("\"", "").Replace('.', ','), out t))
                    {
                        p.Temperature = t;
                    }
                }

                if (structure.ContainsKey("TN"))
                {
                    if (Double.TryParse(stringValues[structure["TN"]].Replace("\"", "").Replace('.', ','), out t))
                    {
                        p.MinTemperature = t;
                    }
                }

                if (structure.ContainsKey("TX"))
                {
                    if (Double.TryParse(stringValues[structure["TX"]].Replace("\"", "").Replace('.', ','), out t))
                    {
                        p.MaxTemperature = t;
                    }
                }

                if (structure.ContainsKey("RRR"))
                {
                    if (Double.TryParse(stringValues[structure["RRR"]].Replace("\"", "").Replace('.', ','), out t))
                    {
                        p.Rainfall = t;
                    }
                }

                if (structure.ContainsKey("SSS"))
                {
                    if (Double.TryParse(stringValues[structure["SSS"]].Replace("\"", "").Replace('.', ','), out t))
                    {
                        p.SnowHight = t;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Source} - {ex.Message}");
            }
            return p;
        }

        public static SimpleObservationPoint CreateFromLine(string stringLine, Dictionary<string, int> structure)
        {
            string[] arrValues = GetValuesFromLine(stringLine, structure);
            return CreateFromArrayValues(arrValues, structure);
        }

        public static SimpleObservationPoint CreateFromLine(string stringLine, SchemaRP5 schema)
        {
            string[] arrValues = GetValuesFromLine(stringLine, schema);
            return CreateFromArrayValues(arrValues, schema);
        }

        #region Static private methods  

        /// <summary>
        /// Получить массив значений из строки
        /// </summary>
        /// <param name="stringLine">Строка из файла</param>
        /// <param name="countField">Количество полей</param>
        /// <returns>Массив значений</returns>
        private static string[] GetValuesFromLine(string stringLine, int countField)
        {
            string[] arr = new string[countField];
            string[] s = stringLine.Split(new[] { "\";" }, StringSplitOptions.None);
            for (int i = 0; i < countField; i++)
            {
                arr[i] = s[i].TrimStart('"');
            }
            return arr;
        }

        /// <summary>
        /// Получить массив значений из строки
        /// </summary>
        /// <param name="stringLine">Строка из файла</param>
        /// <param name="schema">Схема</param>
        /// <returns>Массив значений</returns>
        private static string[] GetValuesFromLine(string stringLine, string[] schema)
        {
            return GetValuesFromLine(stringLine, schema.Length);
        }

        /// <summary>
        /// Получить массив значений из строки
        /// </summary>
        /// <param name="stringLine">Строка из файла</param>
        /// <param name="dictStructureSchema">Структура схемы</param>
        /// <returns>Массив значений</returns>
        private static string[] GetValuesFromLine(string stringLine, Dictionary<string, int> dictStructureSchema)
        {
            return GetValuesFromLine(stringLine, dictStructureSchema.Count);
        }

        /// <summary>
        /// Получить массив значений из строки
        /// </summary>
        /// <param name="stringLine">Строка из файла</param>
        /// <param name="schema">Схема</param>
        /// <returns>Массив значений</returns>
        private static string[] GetValuesFromLine(string stringLine, SchemaRP5 schema)
        {
            return GetValuesFromLine(stringLine, schema.Structure);
        }

        #endregion  
    }
}