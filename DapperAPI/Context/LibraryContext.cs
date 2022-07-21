using DapperAPI.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DapperAPI.Context
{
    public class LibraryContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public LibraryContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("BooksDbFirstConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
