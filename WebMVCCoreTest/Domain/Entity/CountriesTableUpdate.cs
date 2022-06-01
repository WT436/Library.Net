using Oracle.ManagedDataAccess.Types;

namespace WebMVCCoreTest.Domain.Entity
{
    public class CountriesTableUpdate
    {
        [OracleObjectMappingAttribute("COUNTRY_ID")]
        public string COUNTRYID { get; set; }
        [OracleObjectMappingAttribute("COUNTRY_NAME")]
        public string COUNTRYNAME { get; set; }
        [OracleObjectMappingAttribute("REGION_ID")]
        public double REGIONID { get; set; }
    }
}
