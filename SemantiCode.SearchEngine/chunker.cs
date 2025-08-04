using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class chunker
    {
        public IEnumerable<string> Chunk(string code, int maxChars = 600)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (maxChars <= 0) throw new ArgumentOutOfRangeException(nameof(maxChars));

            if (code.Length == 0)
                yield break;

            for (int i = 0; i < code.Length; i+=maxChars) 
            {
                int len = Math.Min(maxChars, code.Length - i);
                yield return code.Substring(i, len);
            
            
            }
        }
    }
}
