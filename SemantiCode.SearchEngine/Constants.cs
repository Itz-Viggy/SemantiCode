using System.Reflection;

namespace SearchEngine
{
    internal static class Constants
    {
        private static string GetModelPath(string fileName)
        {
            // Start from the current directory and look for the models folder
            var currentDir = Directory.GetCurrentDirectory();
            
            // Try different possible paths
            var possiblePaths = new[]
            {
                Path.Combine(currentDir, "models", fileName),
                Path.Combine(currentDir, "..", "SemantiCode.SearchEngine", "models", fileName),
                Path.Combine(currentDir, "..", "..", "SemantiCode.SearchEngine", "models", fileName),
                Path.Combine(currentDir, "..", "..", "..", "SemantiCode.SearchEngine", "models", fileName),
                Path.Combine(currentDir, "..", "..", "..", "..", "SemantiCode.SearchEngine", "models", fileName)
            };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }

            // If file not found, throw a helpful exception
            throw new FileNotFoundException($"Could not find {fileName} in any of the expected locations. Current directory: {currentDir}");
        }

        public static string ModelPath => GetModelPath("bge-base.onnx");
        public static string VocabPath => GetModelPath("vocab.txt");
    }
}
