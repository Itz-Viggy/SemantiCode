using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class SearchEngine: IndexingRepo
    {
        private readonly FileReader _crawler;
        private readonly LiteDbStorage _storage;
        private readonly chunker _chunker;
        private readonly OnnxEmbedder _embedder;

        public SearchEngine(FileReader crawler,chunker chunker, OnnxEmbedder embedder, LiteDbStorage storage )
        {
            _crawler = crawler ?? throw new ArgumentNullException(nameof(crawler));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _chunker = chunker ?? throw new ArgumentNullException(nameof(chunker));
            _embedder = embedder ?? throw new ArgumentNullException(nameof(embedder));
        }

        public async Task<int> IndexRepoAsync(string repoPath)
        {
            if (string.IsNullOrWhiteSpace(repoPath))
            {
                throw new ArgumentException("Repository path cannot be null or empty.", nameof(repoPath));
            }
          
            var docs = new List<SnippetDocument>();
            
            foreach (var file in _crawler.ReadFiles(repoPath))
            {
                var code = await File.ReadAllTextAsync(file).ConfigureAwait(false);

                var chunks = _chunker.Chunk(code).ToList();
                for (int i = 0; i<chunks.Count; i++)
                {
                    var embedding = await _embedder.GenerateEmbeddingAsync(chunks[i]).ConfigureAwait(false);
                    docs.Add(new SnippetDocument
                    {
                        Text = chunks[i],
                        Vector = embedding,
                        FilePath = file,
                        Order = i
                    });
                }
            }
            await _storage.InsertManyAsync(docs);
            return docs.Count;
        }

    }
}
