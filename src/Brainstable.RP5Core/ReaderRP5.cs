using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Brainstable.RP5Core
{
    /// <summary>
    /// Класс для чтения csv-файлов RP5 и их архивов
    /// </summary>
    public class ReaderRP5 : IReaderRP5, IDisposable
    {
        private MetaDataRP5 metaDataRp5;
        private SchemaRP5 schema;
        private Encoding encoding;
        private TypeLoadFileRP5 typeLoadFileRp5 = TypeLoadFileRP5.Unknown;
        private string pathSource;
        private bool isArchive = false;
        private bool isCleanDecompress = true;

        /// <summary>
        /// Метаданные
        /// </summary>
        public MetaDataRP5 MetaData => metaDataRp5;

        /// <summary>
        /// Схема
        /// </summary>
        public SchemaRP5 Schema => schema;

        public List<string> ReadToListString(string fileName)
        {
            ReadWithoutData(fileName);
            var list = FastReaderRP5.ReadListStringFromCsv(pathSource);
            Dispose();
            return list;
        }

        public Dictionary<string, string> ReadToDictionaryString(string fileName)
        {
            ReadWithoutData(fileName);
            var dict = FastReaderRP5.ReadDictionaryStringFromCsv(pathSource);
            Dispose();
            return dict;
        }

        public List<ObservationPoint> ReadToListObservationPoints(string fileName)
        {
            ReadWithoutData(fileName);
            var list = FastReaderRP5.ReadListObservationPointsFromCsv(pathSource);
            Dispose();
            return list;
        }

        public Dictionary<string, ObservationPoint> ReadToDictionaryObservationPoints(string fileName)
        {
            ReadWithoutData(fileName);
            var dict = FastReaderRP5.ReadDictionaryObservationPointsFromCsv(pathSource);
            Dispose();
            return dict;
        }

        public SortedSet<ObservationPoint> ReadToSortedSetObservationPoints(string fileName, IComparer<ObservationPoint> comparer)
        {
            ReadWithoutData(fileName);
            var set = FastReaderRP5.ReadSortedSetObservationPointsFromCsv(pathSource, comparer);
            return set;
        }

        public void ReadWithoutData(string fileName)
        {
            this.pathSource = fileName;
            isArchive = false;
            encoding = HelpMethods.CreateEncoding(fileName);
            string extension = Path.GetExtension(fileName);

            if (extension.Contains("gz"))
            {
                pathSource = GZ.DecompressTempFolder(fileName);
                isArchive = true;
            }

            if (pathSource.EndsWith("csv"))
            {
                typeLoadFileRp5 = isArchive ? TypeLoadFileRP5.ArchCsv : TypeLoadFileRP5.Csv;
            }
            if (pathSource.EndsWith("xls"))
            {
                typeLoadFileRp5 = isArchive ? TypeLoadFileRP5.ArchXls : TypeLoadFileRP5.Xls;
            }

            metaDataRp5 = FastReaderRP5.ReadMetaDataFromCsv(pathSource);
            schema = FastReaderRP5.ReadSchemaFromCsv(pathSource);
        }

        public void Dispose()
        {
            if (isCleanDecompress && isArchive)
            {
                if (File.Exists(this.pathSource))
                    File.Delete(this.pathSource);
            }
        }
    }
}