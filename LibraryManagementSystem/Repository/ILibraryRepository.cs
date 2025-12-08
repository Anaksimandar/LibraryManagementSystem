using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem.Repository
{
    public interface ILibraryRepository
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task AddAuthorAsync(Author newAuthor);
        Task DeleteAuthorAsync(int authorId);
        Task UpdateAuthorAsync(Author updatedAuthor);
        Task<IEnumerable<Book>> GetBooksAsync();
        Task AddBookAsync(Book newBook);
        Task DeleteBookAsync(int bookId);
        Task UpdateBookAsync(Book updatedBook);
    }
}
