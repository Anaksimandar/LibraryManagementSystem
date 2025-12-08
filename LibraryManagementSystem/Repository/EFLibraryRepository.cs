using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repository
{
    public class EFLibraryRepository : ILibraryRepository
    {
        private readonly LibraryManagementSystemContext _context;

        public EFLibraryRepository(LibraryManagementSystemContext context)
        {
            _context = context;
        }

        public Task AddAuthorAsync(Author newAuthor)
        {
            _context.Authors.Add(newAuthor);
            return _context.SaveChangesAsync();
        }

        public Task AddBookAsync(Book newBook)
        {
            _context.Books.Add(newBook);
            return _context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(int authorId)
        {
            Author? author = await _context.Authors.FindAsync(authorId);

            if (author == null) return;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int bookId)
        {
            Book? book = await _context.Books.FindAsync(bookId);

            if (book == null) return;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            var authors = await _context.Authors
                .Include(a => a.Books)
                .ToListAsync();

            return authors;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            var books = await _context.Books
                .Include(a => a.Author)
                .ToListAsync();

            return books;
        }

        public async Task UpdateAuthorAsync(Author updatedAuthor)
        {
            Author? author = await _context.Authors.FindAsync(updatedAuthor.Id);

            if (author == null) return;

            author.Name = updatedAuthor.Name;
            author.YearOfBirth = updatedAuthor.YearOfBirth;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book updatedBook)
        {
            Book? book = await _context.Books.FindAsync(updatedBook.Id);

            if (book == null) return;

            book.Title = updatedBook.Title;
            book.PublicationYear = updatedBook.PublicationYear;
            book.AuthorId = updatedBook.AuthorId;

            await _context.SaveChangesAsync();
        }
    }
}
