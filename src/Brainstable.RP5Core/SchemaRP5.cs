using System;
using System.Collections.Generic;

namespace Brainstable.RP5Core
{
    /// <summary>
    /// Схема данных
    /// </summary>
    public sealed class SchemaRP5
    {
        #region Fields

        private string[] schema;
        private int countFields;

        #endregion

        #region Indexators

        public int this[string name] => GetIndexByName(name);

        public string this[int index] => GetNameByIndex(index);

        #endregion

        #region Properties

        /// <summary>
        /// Структура схемы в верхнем регистре
        /// </summary>
        public Dictionary<string, int> Structure { get; private set; }
        /// <summary>
        /// Структура схемы без изменения регистра
        /// </summary>
        public Dictionary<string, int> OriginalStructure { get; private set; }

        /// <summary>
        /// Описание полей структуры
        /// </summary>
        public static Dictionary<string, string> DescriptionFields { get; }
        /// <summary>
        /// Схема
        /// </summary>
        public string[] Schema => schema;
        /// <summary>
        /// Количество полей
        /// </summary>
        public int CountFields => countFields;
        /// <summary>
        /// Имя первого поля
        /// </summary>
        public string NameFirstField
        {
            get
            {
                if (CountFields > 0)
                    return GetOriginalNameByIndex(0);
                return String.Empty;
            }
        }

        #endregion

        #region Ctors

        private SchemaRP5(string lineSchema) :
            this(CreateSchemaByLine(lineSchema))
        { }

        private SchemaRP5(string[] schema)
        {
            this.schema = schema;
            countFields = this.schema.Length;
            CreateStructureBySchema(this.schema);
        }

        private SchemaRP5() { }

        static SchemaRP5()
        {
            DescriptionFields = new Dictionary<string, string>();
            DescriptionFields["T"] = "Температура воздуха (градусы Цельсия) на высоте 2 метра над поверхностью земли";
            DescriptionFields["P"] = "Атмосферное давление на уровне станции (миллиметры ртутного столба)";
            DescriptionFields["Po"] = "Атмосферное давление, приведенное к среднему уровню моря (миллиметры ртутного столба)";
            DescriptionFields["PO"] = DescriptionFields["Po"];
            DescriptionFields["PA"] = "Барическая тенденция: изменение атмосферного давления за последние три часа (миллиметры ртутного столба)";
            DescriptionFields["U"] = "Относительная влажность (%) на высоте 2 метра над поверхностью земли";
            DescriptionFields["DD"] = "Направление ветра (румбы) на высоте 10-12 метров над земной поверхностью, осредненное за 10-минутный период, непосредственно предшествовавший сроку наблюдения";
            DescriptionFields["FF"] = "Cкорость ветра на высоте 10-12 метров над земной поверхностью, осредненная за 10-минутный период, непосредственно предшествовавший сроку наблюдения (метры в секунду)";
            DescriptionFields["FF10"] = "Максимальное значение порыва ветра на высоте 10-12 метров над земной поверхностью за 10-минутный период, непосредственно предшествующий сроку наблюдения (метры в секунду)";
            DescriptionFields["FF3"] = "Максимальное значение порыва ветра на высоте 10-12 метров над земной поверхностью за период между сроками (метры в секунду)";
            DescriptionFields["N"] = "Общая облачность";
            DescriptionFields["WW"] = "Текущая погода, сообщаемая с метеорологической станции";
            DescriptionFields["W1"] = "Прошедшая погода между сроками наблюдения 1";
            DescriptionFields["W2"] = "Прошедшая погода между сроками наблюдения 2";
            DescriptionFields["W'W'"] = "Явления недавней погоды, имеющие оперативное значение";
            DescriptionFields["W_W_"] = DescriptionFields["W'W'"];
            DescriptionFields["Tn"] = "Минимальная температура воздуха (градусы Цельсия) за прошедший период (не более 12 часов)";
            DescriptionFields["TN"] = DescriptionFields["Tn"];
            DescriptionFields["Tx"] = "Максимальная температура воздуха (градусы Цельсия) за прошедший период (не более 12 часов)";
            DescriptionFields["TX"] = DescriptionFields["Tx"];
            DescriptionFields["Cl"] = "Слоисто-кучевые, слоистые, кучевые и кучево-дождевые облака";
            DescriptionFields["CL"] = DescriptionFields["Cl"];
            DescriptionFields["C"] = "Общая облачность";
            DescriptionFields["Nh"] = "Количество всех наблюдающихся облаков Cl или, при отсутствии облаков Cl, количество всех наблюдающихся облаков Cm";
            DescriptionFields["NH"] = DescriptionFields["Nh"];
            DescriptionFields["H"] = "Высота основания самых низких облаков (м)";
            DescriptionFields["Cm"] = "Высококучевые, высокослоистые и слоисто-дождевые облака";
            DescriptionFields["CM"] = DescriptionFields["Cm"];
            DescriptionFields["Ch"] = "Перистые, перисто-кучевые и перисто-слоистые облака";
            DescriptionFields["CH"] = DescriptionFields["Ch"];
            DescriptionFields["VV"] = "Горизонтальная дальность видимости (км)";
            DescriptionFields["Td"] = "Температура точки росы на высоте 2 метра над поверхностью земли (градусы Цельсия)";
            DescriptionFields["TD"] = DescriptionFields["Td"];
            DescriptionFields["RRR"] = "Количество выпавших осадков (миллиметры)";
            DescriptionFields["TR"] = "Период времени, за который накоплено указанное количество осадков (часы)";
            DescriptionFields["E"] = "Состояние поверхности почвы без снега или измеримого ледяного покрова";
            DescriptionFields["Tg"] = "Минимальная температура поверхности почвы за ночь (градусы Цельсия)";
            DescriptionFields["TG"] = DescriptionFields["Tg"];
            DescriptionFields["E'"] = "Состояние поверхности почвы со снегом или измеримым ледяным покровом";
            DescriptionFields["E_"] = DescriptionFields["E'"];
            DescriptionFields["SSS"] = "Высота снежного покрова (см)";
            DescriptionFields["sss"] = DescriptionFields["SSS"];
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Получить имя поля по индексу
        /// </summary>
        /// <param name="index">Индекс структуры</param>
        /// <returns>Имя поля</returns>
        public string GetNameByIndex(int index)
        {
            foreach (var str in Structure)
            {
                if (str.Value == index)
                    return str.Key;
            }
            return String.Empty;
        }

        /// <summary>
        /// Получить оригинальное (без изменения регистра) имя поля
        /// </summary>
        /// <param name="index">Индекс структуры</param>
        /// <returns>Имя поля</returns>
        public string GetOriginalNameByIndex(int index)
        {
            foreach (var str in OriginalStructure)
            {
                if (str.Value == index)
                    return str.Key;
            }
            return String.Empty;
        }

        /// <summary>
        /// Получить индекс структуры по имени поля
        /// </summary>
        /// <param name="name">Имя поля</param>
        /// <returns>Индекс структуры</returns>
        public int GetIndexByName(string name)
        {
            if (Structure.ContainsKey(name.ToUpper()))
                return Structure[name.ToUpper()];
            return -1;
        }

        /// <summary>
        /// Получить описание поля по его имени
        /// </summary>
        /// <param name="name">Имя поля</param>
        /// <returns>Описание поля</returns>
        public string GetDescriptionByName(string name)
        {
            if (DescriptionFields.ContainsKey(name.ToUpper()))
                return DescriptionFields[name.ToUpper()];
            return String.Empty;
        }

        /// <summary>
        /// Получить описание поля по его индексу структуры
        /// </summary>
        /// <param name="index">Индекс структуры</param>
        /// <returns>Описание поля</returns>
        public string GetDescriptionByIndex(int index)
        {
            string name = GetNameByIndex(index);
            return GetDescriptionByName(name);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Получить структуру схемы по схеме
        /// </summary>
        /// <param name="schema">Схема</param>
        /// <returns>Структура схемы</returns>
        private void CreateStructureBySchema(string[] schema)
        {
            Structure = new Dictionary<string, int>();
            OriginalStructure = new Dictionary<string, int>();
            for (int i = 0; i < schema.Length; i++)
            {
                Structure[schema[i].ToUpper()] = i;
                OriginalStructure[schema[i]] = i;
            }
        }

        #endregion

        #region Static private methods

        /// <summary>
        /// Создать схему из строки
        /// </summary>
        /// <param name="lineSchema"></param>
        /// <returns></returns>
        private static string[] CreateSchemaByLine(string lineSchema)
        {
            List<string> list = new List<string>();
            string[] strings = lineSchema.Trim().Replace("\"", "").Split(';');
            for (int i = 0; i < strings.Length; i++)
            {
                if (i == strings.Length - 1)
                {
                    if (string.IsNullOrEmpty(strings[i]))
                    {
                        continue;
                    }
                }
                list.Add(strings[i]);
            }
            return list.ToArray();
        }

        #endregion

        #region Static public methods

        /// <summary>
        /// Создание схемы из строки
        /// </summary>
        /// <param name="lineSchema"></param>
        /// <returns></returns>
        public static SchemaRP5 CreateFromLineSchema(string lineSchema)
        {
            SchemaRP5 schema = null;
            try
            {
                schema = new SchemaRP5(lineSchema);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return schema;
        }

        /// <summary>
        /// Создание схемы из массива значений полей
        /// </summary>
        /// <param name="arraySchema">Массив значений полей</param>
        /// <returns></returns>
        public static SchemaRP5 CreateFromArraySchema(string[] arraySchema)
        {
            SchemaRP5 schema = null;
            try
            {
                schema = new SchemaRP5(arraySchema);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return schema;
        }

        /// <summary>
        /// Создание схемы из файла Csv
        /// </summary>
        /// <param name="fileName">Путь к файлу</param>
        /// <returns>Схема</returns>
        public static SchemaRP5 CreateFromFileCsv(string fileName)
        {
            return FastReaderRP5.ReadSchemaFromCsv(fileName);
        }

        #endregion
    }
}