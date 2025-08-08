using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class SearchResult
    {
        public SnippetDocument Document { get; set; }
        public double SimilarityScore { get; set; }
    }
}
