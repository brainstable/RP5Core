using System.Collections.Generic;
using System.IO;

namespace Brainstable.RP5Core
{
    public class DirectoryRP5
    {
        private readonly string directory;
        public string[] Files { get; }
        private Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

        public DirectoryRP5(string directory)
        {
            this.directory = directory;
            Files = GetFiles(this.directory);
        }

        private string[] GetFiles(string directory)
        {
            List<string> listFilesRP5 = new List<string>();
            string[] files = Directory.GetFiles(directory);
            MetaDataRP5 metaData;
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    ReaderRP5 reader = new ReaderRP5();
                    reader.ReadWithoutData(files[i]);
                    metaData = reader.MetaData;
                    if (!dict.ContainsKey(metaData.Identificator))
                    {
                        dict[metaData.Identificator] = new List<string>();
                    }
                    dict[metaData.Identificator].Add(files[i]);
                    listFilesRP5.Add(files[i]);
                }
                catch
                {
                }
            }
            return listFilesRP5.ToArray();
        }

        public void MergeFiles(string outDirectory)
        {
            foreach (var files in dict.Values)
            {
                Merger merger = new Merger();
                if (files.Count > 1)
                {
                    List<string> temp = new List<string>();
                    for (int i = 1; i < files.Count; i++)
                    {
                        temp.Add(files[i]);
                    }
                    merger.JoinToFile(outDirectory, files[0], temp.ToArray());
                }
                else if (files.Count == 1)
                {
                    merger.ToFile(outDirectory, files[0]);
                }
            }
        }
    }
}