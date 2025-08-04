using SearchEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchEngine;
using Microsoft.ML.OnnxRuntime;

namespace Tests
{
    public class OnnxEmbedderTests
    {
        [Fact]
        public async Task GenerateEmbeddingAsync_ShouldReturnValidEmbedding()
        {
            // Arrange
            var session = new ModelLoader().GetSession();
            
            var embedder = new SearchEngine.OnnxEmbedder(session);
            string testText = "This is a test sentence.";
            // Act
            var embedding = await embedder.GenerateEmbeddingAsync(testText, CancellationToken.None);
            // Assert
            Assert.NotNull(embedding);
            Assert.NotEmpty(embedding);
            Assert.Equal(768, embedding.Length); // Assuming the embedding size is 768
        }
    }
}
