using System;
using System.Collections.Generic;

namespace Brainstable.RP5Core
{
    /// <summary>
    /// Точка наблюдения
    /// </summary>
    public class ObservationPoint
    {
        #region Properties

        /// <summary>
        /// Дата и время наблюдения
        /// </summary>
        public DateTime DateTime { get; private set; }

        /// <summary>
        /// Строковое представление даты и времени наблюдения
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Температура воздуха (градусы Цельсия) на высоте 2 метра над поверхностью земли
        /// </summary>
        public string T { get; private set; }

        /// <summary>
        /// Атмосферное давление на уровне станции (миллиметры ртутного столба)
        /// </summary>
        public string P { get; private set; }

        /// <summary>
        /// Атмосферное давление, приведенное к среднему уровню моря (миллиметры ртутного столба)
        /// </summary>
        public string Po { get; private set; }

        /// <summary>
        /// Барическая тенденция: изменение атмосферного давления за последние три часа (миллиметры ртутного столба)
        /// </summary>
        public string PA { get; private set; }

        /// <summary>
        /// Относительная влажность (%) на высоте 2 метра над поверхностью земли
        /// </summary>
        public string U { get; private set; }

        /// <summary>
        /// Направление ветра (румбы) на высоте 10-12 метров над земной поверхностью, осредненное за 10-минутный период, непосредственно предшествовавший сроку наблюдения
        /// </summary>
        public string DD { get; private set; }

        /// <summary>
        /// Cкорость ветра на высоте 10-12 метров над земной поверхностью, осредненная за 10-минутный период, непосредственно предшествовавший сроку наблюдения (метры в секунду)
        /// </summary>
        public string FF { get; private set; }

        /// <summary>
        /// Максимальное значение порыва ветра на высоте 10-12 метров над земной поверхностью за 10-минутный период, непосредственно предшествующий сроку наблюдения (метры в секунду)
        /// </summary>
        // TODO: double?
        public string FF10 { get; private set; }

        /// <summary>
        /// Максимальное значение порыва ветра на высоте 10-12 метров над земной поверхностью за период между сроками (метры в секунду)
        /// </summary>
        // TODO: double?
        public string FF3 { get; private set; }

        /// <summary>
        /// Общая облачность
        /// </summary>
        public string N { get; private set; }

        /// <summary>
        /// Текущая погода, сообщаемая с метеорологической станции
        /// </summary>
        public string WW { get; private set; }

        /// <summary>
        /// Прошедшая погода между сроками наблюдения 1
        /// </summary>
        public string W1 { get; private set; }

        /// <summary>
        /// Прошедшая погода между сроками наблюдения 2
        /// </summary>
        public string W2 { get; private set; }

        /// <summary>
        /// Явления недавней погоды, имеющие оперативное значение
        /// </summary>
        public string W_W_ { get; private set; }

        /// <summary>
        /// Минимальная температура воздуха (градусы Цельсия) за прошедший период (не более 12 часов)
        /// </summary>
        public string Tn { get; private set; }

        /// <summary>
        /// Максимальная температура воздуха (градусы Цельсия) за прошедший период (не более 12 часов)
        /// </summary>
        public string Tx { get; private set; }

        /// <summary>
        /// Слоисто-кучевые, слоистые, кучевые и кучево-дождевые облака
        /// </summary>
        public string Cl { get; private set; }

        /// <summary>
        /// Общая облачность
        /// </summary>
        public string C { get; private set; }

        /// <summary>
        /// Количество всех наблюдающихся облаков Cl или, при отсутствии облаков Cl, количество всех наблюдающихся облаков Cm
        /// </summary>
        public string Nh { get; private set; }

        /// <summary>
        /// Высота основания самых низких облаков (м)
        /// </summary>
        public string H { get; private set; }

        /// <summary>
        /// Высококучевые, высокослоистые и слоисто-дождевые облака
        /// </summary>
        public string Cm { get; private set; }

        /// <summary>
        /// Перистые, перисто-кучевые и перисто-слоистые облака
        /// </summary>
        public string Ch { get; private set; }

        /// <summary>
        /// Горизонтальная дальность видимости (км)
        /// </summary>
        public string VV { get; private set; }

        /// <summary>
        /// Температура точки росы на высоте 2 метра над поверхностью земли (градусы Цельсия)
        /// </summary>
        public string Td { get; private set; }

        /// <summary>
        /// Количество выпавших осадков (миллиметры)
        /// </summary>
        public string RRR { get; private set; }

        /// <summary>
        /// Период времени, за который накоплено указанное количество осадков (часы)
        /// </summary>
        public string TR { get; private set; }

        /// <summary>
        /// Состояние поверхности почвы без снега или измеримого ледяного покрова
        /// </summary>
        public string E { get; private set; }

        /// <summary>
        /// Минимальная температура поверхности почвы за ночь (градусы Цельсия)
        /// </summary>
        public string Tg { get; private set; }

        /// <summary>
        /// Состояние поверхности почвы со снегом или измеримым ледяным покровом
        /// </summary>
        public string Es { get; private set; }

        /// <summary>
        /// Высота снежного покрова (см)
        /// </summary>
        public string SSS { get; private set; }

        #endregion

        #region Ctors

        private ObservationPoint(string stringDateTime)
        {
            DateTime = DateTime.Parse(stringDateTime);
            Id = stringDateTime;
            T = string.Empty;
            P = string.Empty;
            Po = string.Empty;
            PA = string.Empty;
            U = string.Empty;
            DD = string.Empty;
            FF = string.Empty;
            FF10 = string.Empty;
            FF3 = string.Empty;
            N = string.Empty;
            WW = string.Empty;
            W1 = string.Empty;
            W2 = string.Empty;
            W_W_ = string.Empty;
            Tn = string.Empty;
            Tx = string.Empty;
            Cl = string.Empty;
            C = string.Empty;
            Nh = string.Empty;
            H = string.Empty;
            Cm = string.Empty;
            Ch = string.Empty;
            VV = string.Empty;
            Td = string.Empty;
            RRR = string.Empty;
            TR = string.Empty;
            E = string.Empty;
            Tg = string.Empty;
            Es = string.Empty;
            SSS = string.Empty;
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        protected bool Equals(ObservationPoint other)
        {
            return DateTime.Equals(other.DateTime);
        }

        public override int GetHashCode()
        {
            return DateTime.GetHashCode();
        }

        #endregion

        #region Static public methods

        public static ObservationPoint CreateFromArrayValues(string[] stringValues, SchemaRP5 schema)
        {
            return CreateFromArrayValues(stringValues, schema.Structure);
        }

        public static ObservationPoint CreateFromArrayValues(string[] stringValues, Dictionary<string, int> structure)
        {
            ObservationPoint p = null;
            try
            {
                p = new ObservationPoint(stringValues[0].Replace("\"", ""));
                if (structure.ContainsKey("T"))
                    p.T = stringValues[structure["T"]].Replace("\"", "");
                if (structure.ContainsKey("PO"))
                    p.Po = stringValues[structure["PO"]].Replace("\"", "");
                if (structure.ContainsKey("P0"))
                    p.Po = stringValues[structure["P0"]].Replace("\"", "");
                if (structure.ContainsKey("P"))
                    p.P = stringValues[structure["P"]].Replace("\"", "");
                if (structure.ContainsKey("PA"))
                    p.PA = stringValues[structure["PA"]].Replace("\"", "");
                if (structure.ContainsKey("U"))
                    p.U = stringValues[structure["U"]].Replace("\"", "");
                if (structure.ContainsKey("DD"))
                    p.DD = stringValues[structure["DD"]].Replace("\"", "");
                if (structure.ContainsKey("FF"))
                    p.FF = stringValues[structure["FF"]].Replace("\"", "");
                if (structure.ContainsKey("FF10"))
                    p.FF10 = stringValues[structure["FF10"]].Replace("\"", "");
                if (structure.ContainsKey("FF3"))
                    p.FF3 = stringValues[structure["FF3"]].Replace("\"", "");
                if (structure.ContainsKey("N"))
                    p.N = stringValues[structure["N"]].Replace("\"", "");
                if (structure.ContainsKey("WW"))
                    p.WW = stringValues[structure["WW"]].Replace("\"", "");
                if (structure.ContainsKey("W'W'"))
                    p.W_W_ = stringValues[structure["W'W'"]].Replace("\"", "");
                if (structure.ContainsKey("W1"))
                    p.W1 = stringValues[structure["W1"]].Replace("\"", "");
                if (structure.ContainsKey("W2"))
                    p.W2 = stringValues[structure["W2"]].Replace("\"", "");
                if (structure.ContainsKey("TN"))
                    p.Tn = stringValues[structure["TN"]].Replace("\"", "");
                if (structure.ContainsKey("TX"))
                    p.Tx = stringValues[structure["TX"]].Replace("\"", "");
                if (structure.ContainsKey("CL"))
                    p.Cl = stringValues[structure["CL"]].Replace("\"", "");
                if (structure.ContainsKey("C"))
                    p.C = stringValues[structure["C"]].Replace("\"", "");
                if (structure.ContainsKey("NH"))
                    p.Nh = stringValues[structure["NH"]].Replace("\"", "");
                if (structure.ContainsKey("H"))
                    p.H = stringValues[structure["H"]].Replace("\"", "");
                if (structure.ContainsKey("CM"))
                    p.Cm = stringValues[structure["CM"]].Replace("\"", "");
                if (structure.ContainsKey("CH"))
                    p.Ch = stringValues[structure["CH"]].Replace("\"", "");
                if (structure.ContainsKey("VV"))
                    p.VV = stringValues[structure["VV"]].Replace("\"", "");
                if (structure.ContainsKey("TD"))
                    p.Td = stringValues[structure["TD"]].Replace("\"", "");
                if (structure.ContainsKey("RRR"))
                    p.RRR = stringValues[structure["RRR"]].Replace("\"", "");
                if (structure.ContainsKey("TR"))
                    p.TR = stringValues[structure["TR"]].Replace("\"", "");
                if (structure.ContainsKey("E"))
                    p.E = stringValues[structure["E"]].Replace("\"", "");
                if (structure.ContainsKey("TG"))
                    p.Tg = stringValues[structure["TG"]].Replace("\"", "");
                if (structure.ContainsKey("E'"))
                    p.Es = stringValues[structure["E'"]].Replace("\"", "");
                if (structure.ContainsKey("SSS"))
                    p.SSS = stringValues[structure["SSS"]].Replace("\"", "");
            }
            catch (Exception)
            {

            }
            return p;
        }


        public static ObservationPoint CreateFromLine(string stringLine, Dictionary<string, int> structure)
        {
            string[] arrValues = GetValuesFromLine(stringLine, structure);
            return CreateFromArrayValues(arrValues, structure);
        }

        public static ObservationPoint CreateFromLine(string stringLine, SchemaRP5 schema)
        {
            string[] arrValues = GetValuesFromLine(stringLine, schema);
            return CreateFromArrayValues(arrValues, schema);
        }

        #endregion

        #region Static private methods  

        /// <summary>
        /// Получить массив значений из строки
        /// </summary>
        /// <param name="stringLine">Строка из файла</param>
        /// <param name="countField">Количество полей</param>
        /// <returns>Массив значений</returns>
        internal static string[] GetValuesFromLine(string stringLine, int countField)
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