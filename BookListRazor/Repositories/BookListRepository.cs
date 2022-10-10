using BookListRazor.Configs;
using BookListRazor.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookListRazor.Repositories
{
    public interface IBookListRepository<T>
    {
        Task CreateAsync(T entity);
        Task<T> GetAsync(Guid id);
        Task<List<T>> GetAsync();
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, T entity);
    }

    public class BookListRepository : IBookListRepository<BookListModel>
    {
        private readonly IMongoCollection<BookListModel> _books;
        private readonly FilterDefinitionBuilder<BookListModel> filterBuilder = Builders<BookListModel>.Filter;
        public BookListRepository(IOptions<MongoSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);

            _books = mongoDatabase.GetCollection<BookListModel>("BookLists");
        }
        public async Task CreateAsync(BookListModel entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _books.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var book = await _books.Find(filterBuilder.Eq(_ => _.Id, id)).FirstOrDefaultAsync();

            if(book != null)
            {
                await _books.DeleteOneAsync(filterBuilder.Eq(_ => _.Id, id));
            }
        }

        public Task<BookListModel> GetAsync(Guid id)
        {
            return _books.Find(filterBuilder.Eq(_ => _.Id, id)).FirstOrDefaultAsync();
        }

        public async Task<List<BookListModel>> GetAsync()
        {
            return await _books.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task UpdateAsync(Guid id, BookListModel entity)
        {
            var book = await _books.Find(filterBuilder.Eq(_ => _.Id, id)).FirstOrDefaultAsync();
            if(book == null)
            {
                throw new Exception("Book not found");
            }
            entity.Id = book.Id;
            await _books.ReplaceOneAsync(filterBuilder.Eq(_ => _.Id, id), entity);
        }
    }
}