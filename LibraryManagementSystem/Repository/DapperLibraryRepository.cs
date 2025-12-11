using Dapper;
using LibraryManagementSystem.Dto.Update;
using LibraryManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryManagementSystem.Repository
{
    public class DapperLibraryRepository : ILibraryRepository
    {
        private readonly string _dbConnectionString;
        private IDbConnection CreateConnection() => new SqlConnection(_dbConnectionString);

        public DapperLibraryRepository(string dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
        }
        public async Task AddAuthorAsync(Author newAutor)
        {
            var sql = @"INSERT INTO Authors (Name, YearOfBirth) VALUES (@Name, @YearOfBirth);";
            using var connection = CreateConnection();
            connection.Open();

            await connection.ExecuteAsync(sql, new { Name = newAutor.Name, YearOfBirth = newAutor.YearOfBirth });
        }

        public async Task AddBookAsync(Book newBook)
        {
            var sql = @"INSERT INTO Books (Title, PublicationYear, AuthorId) VALUES (@Title, @PublicationYear. @AuthorId);";
            using var connection = CreateConnection();
            connection.Open();

            await connection.ExecuteAsync(sql, new { Title = newBook.Title, PublicationYear = newBook.PublicationYear, AuthorId = newBook.AuthorId });
        }

        public async Task DeleteAuthorAsync(int authorId)
        {
            var sql = @"DELETE FROM Authors WHERE Id = @Id;";
            using var connection = CreateConnection();
            connection.Open();

            await connection.ExecuteAsync(sql, new { Id = authorId });
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var sql = @"DELETE FROM Books WHERE Id = @Id;";
            using var connection = CreateConnection();
            connection.Open();

            await connection.ExecuteAsync(sql, new { Id = bookId });
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            const string sql = @"
            SELECT a.Id, a.Name, a.YearOfBirth,
                   b.Id, b.Title, b.PublicationYear, b.AuthorId
            FROM Authors a
            LEFT JOIN Books b ON b.AuthorId = a.Id;";

            using var conn = CreateConnection();
            conn.Open();

            // Use a dictionary to collapse duplicate authors produced by the JOIN;
            // each row may repeat the same author for different books. We keep one
            // Author instance per Id and accumulate its books.
            var lookup = new Dictionary<int, Author>();

            var result = await conn.QueryAsync<Author, Book, Author>(
                sql,
                (author, book) =>
                {   
                    if (lookup.TryGetValue(author.Id, out var existing))
                    {
                        author = existing;
                    }
                    else
                    {
                        author.Books = new List<Book>();
                        lookup.Add(author.Id, author);
                    }

                    if (book != null && book.Id > 0)
                    {
                        author.Books.Add(book);
                    }

                    return author;
                },
                splitOn: "Id");

            return lookup.Values;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            const string sql = @"
            SELECT b.Id, b.Title, b.AuthorId, b.PublicationYear,
                   a.Id, a.Name
            FROM Books b
            INNER JOIN Authors a ON b.AuthorId = a.Id;";

            using var conn = CreateConnection();
            conn.Open();

            // Dapper multi-mapping to map Book + Author
            var result = await conn.QueryAsync<Book, Author, Book>(
                sql,
                (book, author) => { book.Author = author; return book; },
                splitOn: "Id"
            );

            return result;
        }

        public async Task UpdateAuthorAsync(int authorId, NewAuthorDto newAuthor)
        {
            var sql = @"UPDATE Authors SET Name = @Name, YearOfBirth = @YearOfBirth WHERE Id = @Id;";
            using var connection = CreateConnection();
            connection.Open();

            await connection.ExecuteAsync(sql,
                new { Name = newAuthor.Name, YearOfBirth = newAuthor.YearOfBirth, Id = authorId }
            );
        }

        public async Task UpdateBookAsync(int bookId, NewBookDto newBook)
        {
            var sql = @"UPDATE Books SET Title = @Title, PublicationYear = @PublicationYear, AuthorId = @AuthorId WHERE Id = @Id;";
            using var connection = CreateConnection();
            connection.Open();

            await connection.ExecuteAsync(sql,
                new { Title = newBook.Title, PublicationYear = newBook.PublicationYear, AuthorId = newBook.AuthorId, Id = bookId }
            );
        }
    }
}
