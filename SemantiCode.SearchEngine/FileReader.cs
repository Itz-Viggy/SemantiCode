using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SearchEngine
{
    public class FileReader
    {
        private static readonly string[] _supportedExtensions = { ".cs", ".ts", ".js", ".py" };

        public IEnumerable<string> ReadFiles(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                throw new ArgumentException("Directory path cannot be null or empty.", nameof(directoryPath));
            }
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"The directory '{directoryPath}' does not exist.");
            }
            return Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories)
                .Where(file => _supportedExtensions.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase));
           
        }
    }
}
