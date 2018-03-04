using MyCouch;
using System;
using System.IO;
using System.Reflection;

namespace AuthenticationTest.Data
{
    public class CouchDbStoreBase : IDisposable
    {
        private const string USERNAME = "CommentService";
        private const string PASSWORD = "CommentService";
        private const string SERVER = "http://" + USERNAME + ":" + PASSWORD + "@localhost:5984";

        private readonly string _database;

        protected readonly MyCouchStore Store;

        public CouchDbStoreBase(string database)
        {
            _database = database;

            using (var client = new MyCouchServerClient(SERVER))
            {
                var d = client.Databases.PutAsync(_database).Result;

                if (!client.Databases.HeadAsync(_database).Result.IsSuccess)
                {
                    throw new Exception($"Database '{_database}' not found!");
                }
            }

            Store = new MyCouchStore(SERVER, _database);
        }

        protected void InsertDesignDocument(string designDocumentName)
        {
            using (var client = new MyCouchClient(SERVER, _database))
            {
                if (!client.Documents.HeadAsync($"_design/{_database}").Result.IsSuccess)
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    using (Stream stream = assembly.GetManifestResourceStream(designDocumentName))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string docString = reader.ReadToEnd();

                        if (string.IsNullOrWhiteSpace(docString))
                        {
                            throw new Exception($"Failed to load document from file '{designDocumentName}'");
                        }

                        var doc = client.Documents.PostAsync(docString).Result;

                        if (!doc.IsSuccess)
                        {
                            throw new Exception($"Failed to setup document '{designDocumentName}'");
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            Store.Dispose();
        }
    }
}
