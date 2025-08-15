using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cli.Options
{
    internal class SearchOptions
    {
        public string? Query { get; init; }
        
        public int? K { get; init; }
    }
}
