using Dapper;
using DapperAPI.Context;
using DapperAPI.Entities;
using DapperAPI.Entities.DTO;
using System.Data;

namespace DapperAPI.Data.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _libraryContext;

        public BookService(LibraryContext libraryContext)
            => _libraryContext = libraryContext;

        public async Task<IEnumerable<BookDTO>> GetBooksAsync()
        {
            var query = "SELECT * FROM Books";

            using (var connection = _libraryContext.CreateConnection())
            {
                var books = await connection.QueryAsync<BookDTO>(query);

                return books.ToList();
            }
        }

        public async Task<BookDTO> GetSingleBookAsync(int id)
        {
            var query = "SELECT * FROM Books WHERE Id = @id";

            using (var connection = _libraryContext.CreateConnection())
            {
                var book = await connection.QuerySingleOrDefaultAsync<BookDTO>(query, new { id });

                return book;
            }
        }

        public async Task<BookDTO> CreateBookAsync(BookDTO bookDTO)
        {
            var query = @"INSERT INTO Books (Title, Price) VALUES (@Title, @Price)
                          SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();
            parameters.Add("Title", bookDTO.Title, DbType.String);
            parameters.Add("Price", bookDTO.Price, DbType.Int32);

            using (var connection = _libraryContext.CreateConnection())
            {
                var bookIdCreatedInDB = await connection.ExecuteAsync(query, parameters);

                var createdBook = new BookDTO
                {
                    Id = bookIdCreatedInDB,
                    Title = bookDTO.Title,
                    Price = bookDTO.Price,
                };

                return createdBook;
            }
        }

        public async Task UpdateBookAsync(int id, BookDTO bookDTO)
        {
            var query = "UPDATE Books SET Title = @Title, Price = @Price WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Title", bookDTO.Title, DbType.String);
            parameters.Add("Price", bookDTO.Price, DbType.Int32);

            using (var connection = _libraryContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var query = "DELETE FROM Books WHERE Id = @id";

            using (var connection = _libraryContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
