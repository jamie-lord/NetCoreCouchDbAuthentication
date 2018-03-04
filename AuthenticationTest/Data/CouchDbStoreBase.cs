using MyCouch;
using System;

namespace AuthenticationTest.Data
{
    public class CouchDbStoreBase : IDisposable
    {
        private const string USERNAME = "CommentService";
        private const string PASSWORD = "CommentService";
        private const string SERVER = "http://" + USERNAME + ":" + PASSWORD + "@localhost:5984";

        protected readonly MyCouchStore Store;

        public CouchDbStoreBase(string database)
        {
            using (var client = new MyCouchServerClient(SERVER))
            {
                var d = client.Databases.PutAsync(database).Result;

                if (!client.Databases.HeadAsync(database).Result.IsSuccess)
                {
                    throw new Exception($"Database '{database}' not found!");
                }
            }

            Store = new MyCouchStore(SERVER, database);
        }

        public void Dispose()
        {
            Store.Dispose();
        }
    }
}
