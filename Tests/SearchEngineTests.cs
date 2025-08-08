using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.ML.OnnxRuntime;
using SearchEngine;

namespace Tests
{
    public class SearchEngineTests
    {
        [Fact]
        public async Task SearchAsyncForKnownWord()
        {
            var root = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(root);

            
            var file1 = Path.Combine(root, "file1.cs");
            var file2 = Path.Combine(root, "file2.cs");
            File.WriteAllText(file1, "hello world // public class Test1 { }");
            File.WriteAllText(file2, "goodbye world");

            var dbPath = Path.Combine(root, "testdb.db");

            try
            {
                using var session = new ModelLoader().GetSession();
                using var storage = new LiteDbStorage(dbPath);

                var engine = new SearchEngine.SearchEngine(
                    new FileReader(),
                    new chunker(),
                    new OnnxEmbedder(session),
                    storage
                );

                int totalIndexed = await engine.IndexRepoAsync(root);
                var results = (await engine.SearchAsync("hello world", 1)).ToList();

                Assert.True(totalIndexed >= 1);
                Assert.Single(results);
                Assert.True(results[0].SimilarityScore > 0.8, $"Expected high similarity, got {results[0].SimilarityScore}");
            }
            finally
            {
                
                await Task.Delay(50);
                try { Directory.Delete(root, recursive: true); }
                catch (IOException)
                {
                    await Task.Delay(200);
                    Directory.Delete(root, recursive: true);
                }
            }
        }
    }
}
