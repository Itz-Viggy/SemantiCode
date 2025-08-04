using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SearchEngine;

namespace Tests
{
    public class ChunkTests
    {
        [Fact]
        public void DivideChunk()
        {
            var sb = new StringBuilder();
            sb.Append('x', 1200);
            string code = sb.ToString();

            var chunker = new chunker();

            var chunks = chunker.Chunk(code).ToList();


            Assert.Equal(2, chunks.Count);
            Assert.Equal(600, chunks[0].Length);
            Assert.Equal(600, chunks[1].Length);
        
        }
    }
}
