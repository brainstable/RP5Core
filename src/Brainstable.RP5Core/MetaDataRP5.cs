using System;
using System.Collections.Generic;

namespace Brainstable.RP5Core
{
    public class MetaDataRP5
    {

        #region Fields

        private static Dictionary<string, int> months = new Dictionary<string, int>();

        #endregion

        #region Properties

        /// <summary>
        /// Синоптик (локация)
        /// </summary>
        public SynopticRP5 Synoptic { get; private set; }

        /// <summary>
        /// Идентификатор метеостанции
        /// </summary>
        public string Identificator => Synoptic.Identificator;
        /// <summary>
        /// Метеостанция
        /// </summary>
        public string Station => Synoptic.Station;
        /// <summary>
        /// Страна
        /// </summary>
        public string Country => Synoptic.Country;
        /// <summary>
        /// Строковое представление типа синоптика
        /// </summary>
        public string StringTypeSynoptic => Synoptic.StringTypeSynoptic;
        /// <summary>
        /// Тип синоптика
        /// </summary>
        public TypeSynopticRP5 TypeSynopticRp5 => Synoptic.TypeSynopticRp5;

        /// <summary>
        /// Внутренне представление строки с названием метеостанции
        /// </summary>
        public string InnerStation { get; private set; }

        /// <summary>
        /// Внутренне представление строки с названием страны
        /// </summary>
        public string InnerCountry { get; private set; }

        /// <summary>
        /// Внутренне представление строки с идентификатором метеостанции и его типом
        /// </summary>
        public string InnerSynoptic { get; private set; }

        /// <summary>
        /// Внутренне представление строки с периодом выборки
        /// </summary>
        public string InnerFetch { get; private set; }

        /// <summary>
        /// Внутреннее представление строки с типом выборки
        /// </summary>
        public string InnerTypeFetch { get; private set; }

        /// <summary>
        /// Тип выборки (пока реализована ВСЕ ДНИ)
        /// </summary>
        public TypeFetchRP5 TypeFetchRp5
        {
            get
            {
                if (InnerTypeFetch.ToLower().Contains("все дни")) return TypeFetchRP5.AllDays;

                //TODO
                return TypeFetchRP5.Unknown;
            }
        }

        /// <summary>
        /// Внутреннее представление строки с кодировкой
        /// </summary>
        public string InnerEncoding { get; private set; }

        /// <summary>
        /// Кодировка
        /// </summary>
        public string Encoding { get; private set; }


        /// <summary>
        /// Ссылка на архив погоды
        /// </summary>
        public string Link { get; private set; }
        
        /// <summary>
        /// Сайт
        /// </summary>
        public string Site { get; private set; }

        
        /// <summary>
        /// Начальная дата выборки
        /// </summary>
        public DateTime StartFetch
        {
            get
            {
                string start = InnerFetch.Substring(10, 10);
                DateTime dt = Convert.ToDateTime(start);
                return dt;
            }
        }

        /// <summary>
        /// Конечная дата выборки
        /// </summary>
        public DateTime EndFetch
        {
            get
            {
                string end = InnerFetch.Substring(24, 10);
                DateTime dt = Convert.ToDateTime(end);
                return dt;
            }
        }

        /// <summary>
        /// Массив строк, инициализирующий метаданные
        /// </summary>
        public string[] InitializeArray { get; private set; }

        #endregion

        #region Static constructor

        static MetaDataRP5()
        {
            months.Add("Январь", 1);
            months.Add("Февраль", 2);
            months.Add("Март", 3); 
            months.Add("Апрель", 4);
            months.Add("Май", 5);
            months.Add("Июнь", 6);
            months.Add("Июль", 7);
            months.Add("Август", 8);
            months.Add("Сентябрь", 9);
            months.Add("Октябрь", 10);
            months.Add("Ноябрь", 11);
            months.Add("Декабрь", 12);
        }

        private MetaDataRP5()
        {
        }

        #endregion

        #region Static public methods

        /// <summary>
        /// Создать метаданные из массива строк
        /// </summary>
        /// <param name="arr">Массив строк</param>
        /// <returns>Метаданные</returns>
        public static MetaDataRP5 CreateFromArrayString(string[] arr)
        {
            MetaDataRP5 meta = null;
            if (ValidateArrayForMetaData(arr))
            {
                try
                {
                    meta = new MetaDataRP5();
                    meta.InitializeArray = arr;
                    
                    string[] s1 = arr[0].Split(',');

                    meta.Synoptic = SynopticRP5.CreateFromLine(arr[0]);
                    meta.InnerStation = s1[0].Replace("#", "").Trim();
                    meta.InnerCountry = s1[1].Trim();
                    meta.InnerSynoptic = s1[2].Trim();
                    meta.InnerFetch = s1[3].Trim();

                    if (s1.Length == 6)
                        meta.InnerTypeFetch = s1[4].Trim() + ", " + s1[5].Trim();
                    else if (s1.Length == 5)
                        meta.InnerTypeFetch = s1[4].Trim();

                    meta.InnerEncoding = arr[1].Replace("#", "").Trim();
                    meta.Encoding = meta.InnerEncoding.Replace("Кодировка:", "").Trim();

                    //meta.GettingInfo = arr[2].Split(',')[0].Replace("#", "").Trim();

                    meta.Site = arr[2].Split(',')[1].Trim();

                    string link = arr[4].Trim();
                    int index = link.IndexOf("http");
                    meta.Link = link.Substring(index, link.Length - index);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return meta;
        }

        /// <summary>
        /// Создать метаданные из файла Csv
        /// </summary>
        /// <param name="fileName">Путь к файлу</param>
        /// <returns>Метаданные</returns>
        public static MetaDataRP5 CreateFromFileCsv(string fileName)
        {
            return FastReaderRP5.ReadMetaDataFromCsv(fileName);
        }

        /// <summary>
        /// Получить URL по идентификатору метеостанции
        /// </summary>
        /// <param name="numberStation">Идентификатор метеостанции</param>
        /// <returns>URL</returns>
        public static string GetUrlByIdentificatorSynoptic(string numberStation)
        {
            string url = "http://rp5.ru";
            if (char.IsUpper(numberStation, 0))
            {
                url += $"/metar.php?metar={numberStation}&lang=ru";
            }
            else if (char.IsDigit(numberStation, 0))
            {
                url += $"/archive.php?wmo_id={numberStation}&lang=ru";
            }
            return url;
        }

        #endregion

        #region Static private methods

        /// <summary>
        /// Получить номер месяца по названию
        /// </summary>
        /// <param name="nameMonth">Название месяца</param>
        /// <returns>Номер месяца</returns>
        private static int GetNumberMonth(string nameMonth)
        {
            if (months.ContainsKey(nameMonth))
                return months[nameMonth];
            return -1;
        }

        /// <summary>
        /// Валидация массива для создания метеоданных
        /// </summary>
        /// <param name="arr">Массив</param>
        /// <returns>True - метаданные можно создать</returns>
        private static bool ValidateArrayForMetaData(string[] arr)
        {
            bool isValid = arr.Length == 5 && arr[0].StartsWith("# Метеостанция") &&
                           arr[1].StartsWith("# Кодировка:")
                           && arr[2].StartsWith("# Информация предоставлена сайтом") &&
                           arr[3].StartsWith("# Пожалуйста, при использовании данных") &&
                           arr[4].StartsWith("# Обозначения метеопараметров");
            return isValid;
        }

        #endregion
    }
}