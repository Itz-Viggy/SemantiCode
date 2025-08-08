using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class SnippetDocument
    {
        
        public ObjectId Id { get; set; }
        
        public string Text { get; set; }

   
        public float[] Vector { get; set; }

    
        public string FilePath { get; set; }

      
        public int Order { get; set; }
    }
}
