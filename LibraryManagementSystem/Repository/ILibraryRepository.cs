using LibraryManagementSystem.Dto.Update;
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
        Task UpdateAuthorAsync(int authorId ,NewAuthorDto updatedAuthor);
        Task<IEnumerable<Book>> GetBooksAsync();
        Task AddBookAsync(Book newBook);
        Task DeleteBookAsync(int bookId);
        Task UpdateBookAsync(int bookId, NewBookDto updatedBook);
    }
}
