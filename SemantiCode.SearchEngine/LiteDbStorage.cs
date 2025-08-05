using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace SearchEngine
{
    public class LiteDbStorage : IDisposable
    {
        private readonly LiteDatabase _database;
        private readonly ILiteCollection<SnippetDocument> _collection;

        public LiteDbStorage(string databasePath = "codeindex.db")
        {
            if (string.IsNullOrWhiteSpace(databasePath))
            {
                throw new ArgumentException("Database path cannot be null or empty.", nameof(databasePath));
            }
            _database = new LiteDatabase(databasePath);
            _collection = _database.GetCollection<SnippetDocument>("snippets");
        }

        public Task InsertManyAsync(IEnumerable<SnippetDocument> documents)
        {
            if (documents == null)
            {
                throw new ArgumentNullException(nameof(documents));
            }
            return Task.Run(() =>
            {
                _collection.InsertBulk(documents);
            });

        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
