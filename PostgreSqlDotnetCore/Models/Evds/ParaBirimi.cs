using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreSqlDotnetCore.Models.Evds
{
    public class ParaBirimi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Country { get; set; }

    }
}
