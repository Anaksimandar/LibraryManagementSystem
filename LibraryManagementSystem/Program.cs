using LibraryManagementSystem.Dto.Update;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repository;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void PrintAuthors(IEnumerable<Author> authors)
        {
            foreach (var author in authors)
            {
                Console.WriteLine($"Author:{author.Id} {author.Name}, Year of Birth: {author.YearOfBirth}");
                foreach (var book in author.Books)
                {
                    Console.WriteLine($"\tBookid: {book.Id} Book: {book.Title}, Publication Year: {book.PublicationYear}");
                }
            }
        }
        static async Task Main(string[] args)
        {
            //using var context = new LibraryManagementSystemContext();

            ///**
            // * Seeding authors and books
            //var authors = new List<Author>
            //{
            //    new Author { Name = "J.K. Rowling", YearOfBirth = 1965 },
            //    new Author { Name = "J.R.R. Tolkien", YearOfBirth = 1892 },
            //    new Author { Name = "Agatha Christie", YearOfBirth = 1890 }
            //};

            //context.Authors.AddRange(authors);
            //context.SaveChanges();
            //Console.WriteLine("Authors added successfully!");

            //var books = new List<Book>
            //{
            //    new Book { Title = "1984", PublicationYear = 1949, AuthorId = 2},
            //    new Book { Title = "Harry Potter and the Philosopher's Stone", PublicationYear = 1997, AuthorId = 2 },
            //    new Book { Title = "The Hobbit", PublicationYear = 1937, AuthorId = 1 },
            //};

            //context.Books.AddRange(books);
            //context.SaveChanges();
            //**/

            //var efContext = new EFLibraryRepository(context);

            //Book newBook = new Book { Id = 1, Title = "Nineteen Eighty-Four", PublicationYear = 1949, AuthorId = 2 };

            //await efContext.UpdateBookAsync(newBook);

            //var booksGetIEnumerable = await efContext.GetBooksAsync();

            //foreach (var b in booksGetIEnumerable)
            //{
            //    Console.WriteLine($"Book: {b.Title}, Publication Year: {b.PublicationYear}, AuthorId: {b.AuthorId}");
            //}

            string connectionString = "Server=DESKTOP-MURI7U0\\SQLEXPRESS;Database=LibrarySystem;Trusted_Connection=True;TrustServerCertificate=True;";
            var dapperRepo = new DapperLibraryRepository(connectionString);

            var authors = await dapperRepo.GetAuthorsAsync();
            var books = await dapperRepo.GetBooksAsync();

            foreach (var book in books)
            {
                Console.WriteLine($"Book: {book.Title}, Publication Year: {book.PublicationYear}, AuthorId: {book.AuthorId}");
            }

            Console.WriteLine("----- Authors and their Books -----");
            PrintAuthors(authors);

            Author? authorId1 = authors.FirstOrDefault(a => a.Id == 2);
            if (authorId1 != null)
            {
                Console.WriteLine($"Author found: {authorId1.Name}, Year of Birth: {authorId1.YearOfBirth}");
            }
            else
            {
                Console.WriteLine("Author not found.");
                return;
            }
            // Update an author
            var updatedAuthor = new NewAuthorDto
            {
                Name = "Dzordz Orvel",
                YearOfBirth = 1959
            };

            await dapperRepo.UpdateAuthorAsync(authorId1.Id, updatedAuthor);

            var updatedAuthors = await dapperRepo.GetAuthorsAsync();

            // Update an book

            NewBookDto newBook = new NewBookDto { Title = "Hobit u Cacku", PublicationYear = 1949, AuthorId = 2 };

            Book? bookId2 = books.FirstOrDefault(a => a.Id == 2);
            if (bookId2 != null)
            {
                Console.WriteLine("Book found");
            }
            else
            {
                Console.WriteLine("Author not found.");
                return;
            }

            await dapperRepo.UpdateBookAsync(bookId2.Id, newBook);
            updatedAuthors = await dapperRepo.GetAuthorsAsync();
            Console.WriteLine("----- Author (After Dzordz Orvel) and Book (Hobbit ...) update -----");
            PrintAuthors(updatedAuthors);

            // Delete an author

            await dapperRepo.DeleteAuthorAsync(3); // Dzordz Orvel should be deleted
            await dapperRepo.DeleteBookAsync(5); // Hobit u Cacku should be deleted

            updatedAuthors = await dapperRepo.GetAuthorsAsync();
            Console.WriteLine("----- Authors after deleting Tolkin and 1984.");
            PrintAuthors(updatedAuthors);
        }
    }
}
