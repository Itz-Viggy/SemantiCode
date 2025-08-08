using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.ML.OnnxRuntime;
using SearchEngine;
using Xunit.Abstractions;

namespace Tests
{
    public class PerfTests
    {
        private readonly ITestOutputHelper _output;

        public PerfTests(ITestOutputHelper output) => _output = output;

        [Fact]
        [Trait("Category", "Performance")]

        public async Task PerformanceFor100Files()
        {
            var root = Path.Combine(Path.GetTempPath(), "perf-" + Guid.NewGuid());
            Directory.CreateDirectory(root);

            for(int i = 0; i < 100; i++)
            {
                var filePath = Path.Combine(root, $"file{i}.cs");
                File.WriteAllText(filePath, $"// Sample code {i}\npublic class Test{i} {{ /* hello world {i} */ }}");
            }

            var dbPath = Path.Combine(root, "codeindex.db");

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

                var stopwatch = Stopwatch.StartNew();
                var totalIndexed = await engine.IndexRepoAsync(root);
                stopwatch.Stop();

            }
            finally
            {
                Directory.Delete(root, recursive: true);

            }
        }
    }
    
}

