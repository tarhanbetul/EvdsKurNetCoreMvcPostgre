using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreSqlDotnetCore.Models.Evds
{
    public class ExchangeRateInformation
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string RateDate { get; set; }
        public double CurrencyValue { get; set; }
    }
}
