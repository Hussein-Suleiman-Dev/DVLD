using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public static DataTable GetAllCountries()
        { 
        return clsCountryDataAccess.GetAllCountries();
        }
        public clsCountry(int countryID, string countryName)
        {
            this.CountryID= countryID;
            this.CountryName = countryName;
        }

        public static clsCountry FindCountry(string CountryName)
        { 
       int CountryID= clsCountryDataAccess.GetCountryID(CountryName);
          return new clsCountry(CountryID, CountryName);
        }
        public static clsCountry FindCountry(int CountryID)
        { 
            string CountryName = clsCountryDataAccess.GetCountryName(CountryID);
            if(CountryName==null)return null;
            return new clsCountry(CountryID,CountryName);
        }

    }
}
