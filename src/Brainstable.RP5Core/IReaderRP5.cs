using System.Collections.Generic;

namespace Brainstable.RP5Core
{
    public interface IReaderRP5
    {
        MetaDataRP5 MetaData { get; }
        SchemaRP5 Schema { get; }
        void ReadWithoutData(string fileName);
        List<string> ReadToListString(string fileName);
        Dictionary<string, string> ReadToDictionaryString(string fileName);
        List<ObservationPoint> ReadToListObservationPoints(string fileName);
        Dictionary<string, ObservationPoint> ReadToDictionaryObservationPoints(string fileName);
        SortedSet<ObservationPoint> ReadToSortedSetObservationPoints(string fileName, IComparer<ObservationPoint> comparer);
    }
}