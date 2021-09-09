using System;

namespace Brainstable.RP5Core
{
    /// <summary>
    /// Синоптик
    /// </summary>
    public class SynopticRP5
    {
        #region Consts

        private const string WMO_ID = "WMO_ID";
        private const string METAR = "METAR";

        #endregion

        #region Properties

        /// <summary>
        /// Идентификатор метеостанции
        /// </summary>
        public string Identificator { get; set; }
        /// <summary>
        /// Метеостанция
        /// </summary>
        public string Station { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Тип идентификатора метеостанции
        /// </summary>
        public TypeSynopticRP5 TypeSynopticRp5 { get; set; }

        /// <summary>
        /// Строковое представление метеостанции
        /// </summary>
        public string StringTypeSynoptic => TypeSynopticRp5.ToString();

        #endregion

        #region Ctors

        public SynopticRP5()
        {
            TypeSynopticRp5 = TypeSynopticRP5.Unknown;
            Identificator = Country = Station = String.Empty;
        }

        public SynopticRP5(string identificator, string station, string country, string stringTypeSynoptic) : this()
        {
            Identificator = identificator;
            Station = station;
            Country = country;
            TypeSynopticRp5 = CreateTypeSynoptic(stringTypeSynoptic);
        }

        public SynopticRP5(string identificator, string station, string country, TypeSynopticRP5 typeSynopticRp5) :
            this(identificator, station, country, typeSynopticRp5.ToString())
        { }

        #endregion

        private static TypeSynopticRP5 CreateTypeSynoptic(string stringTypeSynoptic)
        {
            if (stringTypeSynoptic.Contains(WMO_ID))
                return TypeSynopticRP5.WMO_ID;
            if (stringTypeSynoptic.Contains(METAR))
                return TypeSynopticRP5.METAR;
            return TypeSynopticRP5.Unknown;
        }

        #region Ovverides

        public override string ToString()
        {
            return $"{Identificator} - {Station}";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        protected bool Equals(SynopticRP5 other)
        {
            return Identificator == other.Identificator;
        }

        public override int GetHashCode()
        {
            return (Identificator != null ? Identificator.GetHashCode() : 0);
        }

        #endregion

        #region Static public methods

        public static SynopticRP5 CreateFromLine(string line)
        {
            SynopticRP5 synopticRp5 = null;
            try
            {
                synopticRp5 = new SynopticRP5();
                
                string[] s1 = line.Split(',');

                synopticRp5.Station = s1[0].Replace("#", "").Replace("Метеостанция", "").Trim();
                synopticRp5.Country = s1[1].Trim();
                synopticRp5.TypeSynopticRp5 = CreateTypeSynoptic(s1[2].Split('=')[0]);
                synopticRp5.Identificator = s1[2].Split('=')[1];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            return synopticRp5;
        }

        #endregion
    }
}