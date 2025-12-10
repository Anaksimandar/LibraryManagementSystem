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
                Console.WriteLine($"Author: {author.Name}, Year of Birth: {author.YearOfBirth}");
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

            PrintAuthors(authors);

            Author newA

        }
    }
}
