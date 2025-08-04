using Microsoft.ML.OnnxRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Onnx;

namespace SearchEngine
{
    public class OnnxEmbedder
    {
        private readonly BertOnnxTextEmbeddingGenerationService _generator;

        public OnnxEmbedder(InferenceSession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session), "Inference session cannot be null.");
            }
            _generator = BertOnnxTextEmbeddingGenerationService.Create(Constants.ModelPath, Constants.VocabPath);
        }

        public async Task<float[]> GenerateEmbeddingAsync(string text, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Text cannot be null or empty.", nameof(text));
            }
            var embeddings = await _generator.GenerateEmbeddingsAsync(new[] { text },kernel: null, cancellationToken: ct).ConfigureAwait(false); 


            return embeddings[0].ToArray();
        }

    }
}
