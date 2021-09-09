using System;
using System.Collections.Generic;
using System.IO;

namespace Brainstable.RP5Core
{
    /// <summary>
    /// Быстрое чтение метаданных и схемы из файлов
    /// </summary>
    public static class FastReaderRP5
    {
        /// <summary>
        /// Прочитать метаданные
        /// </summary>
        /// <param name="fileName">Путь к файлу</param>
        /// <returns></returns>
        public static MetaDataRP5 ReadMetaDataFromCsv(string fileName)
        {
            MetaDataRP5 meta = null;
            try
            {
                int counter = 0;
                string line;
                string[] arr = new string[5];

                StreamReader file = new StreamReader(fileName, HelpMethods.CreateEncoding(fileName));
                while ((line = file.ReadLine()) != null)
                {
                    arr[counter++] = line;
                    if (counter == arr.Length)
                        break;
                }
                file.Close();
                meta = MetaDataRP5.CreateFromArrayString(arr);
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return meta;
        }

        /// <summary>
        /// Прочитать схему
        /// </summary>
        /// /// <param name="fileName">Путь к файлу</param>
        /// <returns>Схема</returns>
        public static SchemaRP5 ReadSchemaFromCsv(string fileName)
        {
            SchemaRP5 schema = null;
            try
            {
                int counter = 0;
                string line;
                StreamReader file = new StreamReader(fileName, HelpMethods.CreateEncoding(fileName));
                while ((line = file.ReadLine()) != null)
                {
                    if (counter == 6)
                        break;
                    counter++;
                }
                file.Close();
                schema = CreateSchemaRp5(line);
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return schema;
        }

        private static SchemaRP5 CreateSchemaRp5(string line)
        {
            List<string> list = new List<string>();
            SchemaRP5 schema;
            string[] arr = line.Trim().Replace("\"", "").Split(';');
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == arr.Length - 1)
                {
                    if (string.IsNullOrEmpty(arr[i]))
                    {
                        continue;
                    }
                }

                list.Add(arr[i]);
            }

            schema = SchemaRP5.CreateFromArraySchema(list.ToArray());
            return schema;
        }

        public static List<string> ReadListStringFromCsv(string fileName)
        {
            List<string> list = new List<string>();
            try
            {
                int counter = 0;
                string line;
                StreamReader file = new StreamReader(fileName, HelpMethods.CreateEncoding(fileName));
                while ((line = file.ReadLine()) != null)
                {
                    if (counter > 6)
                    {
                        list.Add(line);
                    }

                    counter++;
                }

                file.Close();
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return list;
        }

        public static Dictionary<string, string> ReadDictionaryStringFromCsv(string fileName)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            try
            {
                int counter = 0;
                string line;
                StreamReader file = new StreamReader(fileName, HelpMethods.CreateEncoding(fileName));
                while ((line = file.ReadLine()) != null)
                {
                    if (counter > 6)
                    {
                        string key = line.Substring(1, 16);
                        dictionary[key] = line;
                    }
                    counter++;
                }
                file.Close();
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return dictionary;
        }

        public static List<ObservationPoint> ReadListObservationPointsFromCsv(string fileName)
        {
            List<ObservationPoint> list = new List<ObservationPoint>();
            try
            {
                int counter = 0;
                string line;
                StreamReader file = new StreamReader(fileName, HelpMethods.CreateEncoding(fileName));
                SchemaRP5 schema = null;
                while ((line = file.ReadLine()) != null)
                {
                    if (counter == 6)
                    {
                        schema = CreateSchemaRp5(line);
                    }
                    if (counter > 6)
                    {
                        list.Add(ObservationPoint.CreateFromLine(line, schema));
                    }
                    counter++;
                }
                file.Close();
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return list;
        }

        public static Dictionary<string, ObservationPoint> ReadDictionaryObservationPointsFromCsv(string fileName)
        {
            Dictionary<string, ObservationPoint> dictionary = new Dictionary<string, ObservationPoint>();
            try
            {
                int counter = 0;
                string line;
                StreamReader file = new StreamReader(fileName, HelpMethods.CreateEncoding(fileName));
                SchemaRP5 schema = null;
                while ((line = file.ReadLine()) != null)
                {
                    if (counter == 6)
                    {
                        schema = CreateSchemaRp5(line);
                    }
                    if (counter > 6)
                    {
                        ObservationPoint observationPoint = ObservationPoint.CreateFromLine(line, schema);
                        dictionary[observationPoint.ToString()] = observationPoint;
                    }
                    counter++;
                }
                file.Close();
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return dictionary;
        }

        public static SortedSet<ObservationPoint> ReadSortedSetObservationPointsFromCsv(string fileName, 
            IComparer<ObservationPoint> comparer)
        {
            SortedSet<ObservationPoint> set = new SortedSet<ObservationPoint>(comparer);
            try
            {
                int counter = 0;
                string line;
                StreamReader file = new StreamReader(fileName, HelpMethods.CreateEncoding(fileName));
                SchemaRP5 schema = null;
                while ((line = file.ReadLine()) != null)
                {
                    if (counter == 6)
                    {
                        schema = CreateSchemaRp5(line);
                    }
                    if (counter > 6)
                    {
                        set.Add(ObservationPoint.CreateFromLine(line, schema));
                    }
                    counter++;
                }
                file.Close();
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return set;
        }

        public static SortedSet<ObservationPoint> ReadSortedSetObservationPointsFromCsv(string fileName)
        {
            return ReadSortedSetObservationPointsFromCsv(fileName, new ObservationPointComparerUpInDown());
        }

        public static List<SimpleObservationPoint> ReadListSimpleObservationPointsFromCsv(string fileName)
        {
            List<SimpleObservationPoint> list = new List<SimpleObservationPoint>();
            try
            {
                int counter = 0;
                string line;
                StreamReader file = new StreamReader(fileName, HelpMethods.CreateEncoding(fileName));
                SchemaRP5 schema = null;
                while ((line = file.ReadLine()) != null)
                {
                    if (counter == 6)
                    {
                        schema = CreateSchemaRp5(line);
                    }
                    if (counter > 6)
                    {
                        list.Add(SimpleObservationPoint.CreateFromLine(line, schema));
                    }
                    counter++;
                }
                file.Close();
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return list;
        }
    }
}