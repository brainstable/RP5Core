namespace Brainstable.RP5Core
{
    /// <summary>
    /// Тип загружаемого файла
    /// </summary>
    public enum TypeLoadFileRP5
    {
        Unknown,
        /// <summary>
        /// Файл CSV
        /// </summary>
        Csv,
        /// <summary>
        /// Файл Excel
        /// </summary>
        Xls,
        /// <summary>
        /// Архив ZIP файла CSV
        /// </summary>
        ArchCsv,
        /// <summary>
        /// Архив ZIP файла Excel
        /// </summary>
        ArchXls
    }
}