using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Brainstable.RP5Core
{
    /// <summary>
    /// Класс для объединения файлов RP5
    /// </summary>
    public class Merger
    {
        private Encoding encoding = Encoding.GetEncoding(1251); // ANSI Кодировка по умолчанию для корректного отображения в Excel
        private string tail = "1.0.0.ru.ansi.00000000.csv";
        private MetaDataRP5 metaData;
        private SchemaRP5 schema;

        #region Properties

        public Encoding Encoding => encoding;

        #endregion

        public SortedSet<ObservationPoint> ToSet(string fileName1)
        {
            IReaderRP5 reader1 = new ReaderRP5();
            var set1 = reader1.ReadToSortedSetObservationPoints(fileName1, new ObservationPointComparerUpInDown());
            metaData = reader1.MetaData;
            schema = reader1.Schema;
            return set1;
        }

        public SortedSet<ObservationPoint> JoinToSet(string fileName1, string fileName2)
        {
            IReaderRP5 reader1 = new ReaderRP5();
            var set1 = reader1.ReadToSortedSetObservationPoints(fileName1, new ObservationPointComparerUpInDown());
            metaData = reader1.MetaData;
            schema = reader1.Schema;

            IReaderRP5 reader2 = new ReaderRP5();
            var set2 = reader2.ReadToSortedSetObservationPoints(fileName2, new ObservationPointComparerDownInUp());

            set1.UnionWith(set2);

            return set1;
        }

        public SortedSet<ObservationPoint> JoinToSet(string fileName1, params string[] fileNames)
        {
            IReaderRP5 reader1 = new ReaderRP5();
            var set1 = reader1.ReadToSortedSetObservationPoints(fileName1, new ObservationPointComparerUpInDown());
            metaData = reader1.MetaData;
            schema = reader1.Schema;

            IReaderRP5 reader2 = new ReaderRP5();
            for (int i = 0; i < fileNames.Length; i++)
            {
                var set2 = reader2.ReadToSortedSetObservationPoints(fileNames[i], new ObservationPointComparerDownInUp());
                set1.UnionWith(set2);
            }

            return set1;
        }

        public string ToFile(string outDirectory, string fileName1)
        {
            var set1 = ToSet(fileName1);

            string outFilename = Path.Combine(outDirectory, CreateFileName(metaData.Synoptic.Identificator, set1.Max.DateTime, set1.Min.DateTime));

            StreamWriter writer = new StreamWriter(outFilename, false, encoding);
            WriteMetaData(writer, metaData, set1.Max.DateTime, set1.Min.DateTime);
            WriteSchema(writer, schema.Schema);
            WriteSet(writer, set1, schema.Schema);

            writer.Close();
            return outFilename;
        }

        public string ToFile(string fileName1)
        {
            return ToFile(Path.GetDirectoryName(fileName1), fileName1);
        }

        public string JoinToFile(string outDirectory, string fileName1, string fileName2)
        {
            var set1 = JoinToSet(fileName1, fileName2);

            string outFilename = Path.Combine(outDirectory, CreateFileName(metaData.Synoptic.Identificator, set1.Max.DateTime, set1.Min.DateTime));

            StreamWriter writer = new StreamWriter(outFilename, false, encoding);
            WriteMetaData(writer, metaData, set1.Max.DateTime, set1.Min.DateTime);
            WriteSchema(writer, schema.Schema);
            WriteSet(writer, set1, schema.Schema);

            writer.Close();
            return outFilename;
        }

        public string JoinToFile(string fileName1, string fileName2)
        {
            return JoinToFile(Path.GetDirectoryName(fileName1), fileName1, fileName2);
        }

        public string JoinToFile(string outDirectory, string fileName1, params string[] fileNames)
        {
            var set1 = JoinToSet(fileName1, fileNames);

            string outFilename = Path.Combine(outDirectory, CreateFileName(metaData.Synoptic.Identificator, set1.Max.DateTime, set1.Min.DateTime));

            StreamWriter writer = new StreamWriter(outFilename, false, encoding);
            WriteMetaData(writer, metaData, set1.Max.DateTime, set1.Min.DateTime);
            WriteSchema(writer, schema.Schema);
            WriteSet(writer, set1, schema.Schema);

            writer.Close();
            return outFilename;
        }

        public string JoinToFile(string fileName1, params string[] fileNames)
        {
            return JoinToFile(Path.GetDirectoryName(fileName1), fileName1, fileNames);
        }

        private void WriteMetaData(StreamWriter writer, MetaDataRP5 meta, DateTime @from, DateTime to)
        {
            writer.WriteLine($"# Метеостанция {meta.Station}, {meta.Country}, {meta.StringTypeSynoptic}={meta.Identificator}, " +
                             $"выборка с {@from.Day:D2}.{@from.Month:D2}.{@from.Year} по {to.Day:D2}.{to.Month:D2}.{to.Year}, все дни");
            writer.WriteLine("# Кодировка: ANSI");
            writer.WriteLine("# Информация предоставлена сайтом \"Расписание Погоды\", rp5.ru");
            writer.WriteLine("# Пожалуйста, при использовании данных, любезно указывайте названный сайт.");
            writer.WriteLine($"# Обозначения метеопараметров см. по адресу {meta.Link}");
            writer.WriteLine("#");
        }

        private void WriteSchema(StreamWriter writer, string[] schema)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < schema.Length; i++)
            {
                sb.Append($"\"{schema[i]}\"");
                sb.Append(";");
            }
            writer.WriteLine(sb);
        }

        private void WriteSet(StreamWriter writer, SortedSet<ObservationPoint> points, string[] schema)
        {
            HashSet<string> hash = new HashSet<string>();
            for (int i = 0; i < schema.Length; i++)
            {
                hash.Add(schema[i].ToUpper());
            }
            foreach (var p in points)
            {
                StringBuilder br = new StringBuilder();
                writer.Write($"\"{p.DateTime.Day:D2}.{p.DateTime.Month:D2}.{p.DateTime.Year} {p.DateTime.Hour:D2}:{p.DateTime.Minute:D2}\";");
                if (hash.Contains(nameof(p.T).ToUpper()))
                {
                    writer.Write($"\"{p.T}\";");
                }
                if (hash.Contains("P0") | hash.Contains("PO"))
                {
                    writer.Write($"\"{p.Po}\";");
                }
                if (hash.Contains(nameof(p.P).ToUpper()))
                {
                    writer.Write($"\"{p.P}\";");
                }
                if (hash.Contains(nameof(p.PA).ToUpper()))
                {
                    writer.Write($"\"{p.PA}\";");
                }
                if (hash.Contains(nameof(p.U).ToUpper()))
                {
                    writer.Write($"\"{p.U}\";");
                }
                if (hash.Contains(nameof(p.DD).ToUpper()))
                {
                    writer.Write($"\"{p.DD}\";");
                }
                if (hash.Contains(nameof(p.FF).ToUpper()))
                {
                    writer.Write($"\"{p.FF}\";");
                }
                if (hash.Contains(nameof(p.FF10).ToUpper()))
                {
                    writer.Write($"\"{p.FF10}\";");
                }
                if (hash.Contains(nameof(p.FF3).ToUpper()))
                {
                    writer.Write($"\"{p.FF3}\";");
                }
                if (hash.Contains(nameof(p.N).ToUpper()))
                {
                    writer.Write($"\"{p.N}\";");
                }
                if (hash.Contains(nameof(p.WW).ToUpper()))
                {
                    writer.Write($"\"{p.WW}\";");
                }
                if (hash.Contains("W'W'"))
                {
                    writer.Write($"\"{p.W_W_}\";");
                }
                if (hash.Contains(nameof(p.W1).ToUpper()))
                {
                    writer.Write($"\"{p.W1}\";");
                }
                if (hash.Contains(nameof(p.W2).ToUpper()))
                {
                    writer.Write($"\"{p.W2}\";");
                }
                if (hash.Contains(nameof(p.Tn).ToUpper()))
                {
                    writer.Write($"\"{p.Tn}\";");
                }
                if (hash.Contains(nameof(p.Tx).ToUpper()))
                {
                    writer.Write($"\"{p.Tx}\";");
                }
                if (hash.Contains(nameof(p.C).ToUpper()))
                {
                    writer.Write($"\"{p.C}\";");
                }
                if (hash.Contains(nameof(p.Cl).ToUpper()))
                {
                    writer.Write($"\"{p.Cl}\";");
                }
                if (hash.Contains(nameof(p.Nh).ToUpper()))
                {
                    writer.Write($"\"{p.Nh}\";");
                }
                if (hash.Contains(nameof(p.H).ToUpper()))
                {
                    writer.Write($"\"{p.H}\";");
                }
                if (hash.Contains(nameof(p.Cm).ToUpper()))
                {
                    writer.Write($"\"{p.Cm}\";");
                }
                if (hash.Contains(nameof(p.Ch).ToUpper()))
                {
                    writer.Write($"\"{p.Ch}\";");
                }
                if (hash.Contains(nameof(p.VV).ToUpper()))
                {
                    writer.Write($"\"{p.VV}\";");
                }
                if (hash.Contains(nameof(p.Td).ToUpper()))
                {
                    writer.Write($"\"{p.Td}\";");
                }
                if (hash.Contains(nameof(p.RRR).ToUpper()))
                {
                    writer.Write($"\"{p.RRR}\";");
                }
                if (hash.Contains(nameof(p.TR).ToUpper()))
                {
                    writer.Write($"\"{p.TR}\";");
                }
                if (hash.Contains(nameof(p.E).ToUpper()))
                {
                    writer.Write($"\"{p.E}\";");
                }
                if (hash.Contains(nameof(p.Tg).ToUpper()))
                {
                    writer.Write($"\"{p.Tg}\";");
                }
                if (hash.Contains("E'"))
                {
                    writer.Write($"\"{p.Es}\";");
                }
                if (hash.Contains(nameof(p.SSS).ToUpper()))
                {
                    writer.Write($"\"{p.SSS}\";");
                }
                writer.Write(Environment.NewLine);
            }
        }

        private string CreateFileName(string stationId, DateTime @from, DateTime to)
        {
            return
                $"{stationId}.{@from.Day:D2}.{@from.Month:D2}.{@from.Year}.{to.Day:D2}.{to.Month:D2}.{to.Year}.{tail}";
        }
    }
}