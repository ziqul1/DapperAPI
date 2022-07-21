using DapperAPI.Entities.DTO;

namespace DapperAPI.Data.Services
{
    public interface IBookService
    {
        public Task<IEnumerable<BookDTO>> GetBooksAsync();
        public Task<BookDTO> GetSingleBookAsync(int id);
        public Task<BookDTO> CreateBookAsync(BookDTO bookDTO);
        public Task UpdateBookAsync(int id, BookDTO bookDTO);
        public Task DeleteBookAsync(int id);
    }
}
