using LibraryManagementSystem.Models;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new LibraryManagementSystemContext();

            /**
             * Seeding authors and books
            var authors = new List<Author>
            {
                new Author { Name = "J.K. Rowling", YearOfBirth = 1965 },
                new Author { Name = "J.R.R. Tolkien", YearOfBirth = 1892 },
                new Author { Name = "Agatha Christie", YearOfBirth = 1890 }
            };

            context.Authors.AddRange(authors);
            context.SaveChanges();
            Console.WriteLine("Authors added successfully!");

            var books = new List<Book>
            {
                new Book { Title = "1984", PublicationYear = 1949, AuthorId = 2},
                new Book { Title = "Harry Potter and the Philosopher's Stone", PublicationYear = 1997, AuthorId = 2 },
                new Book { Title = "The Hobbit", PublicationYear = 1937, AuthorId = 1 },
            };

            context.Books.AddRange(books);
            context.SaveChanges();
            **/

            var authorsGet = context.Authors.ToList();
            var booksGet = context.Books.ToList();

            foreach (var a in authorsGet)
            {
                Console.WriteLine($"Author: {a.Name}, Year of Birth: {a.YearOfBirth}");
            }

            foreach (var b in booksGet)
            {
                Console.WriteLine($"Book: {b.Title}, Publication Year: {b.PublicationYear}, AuthorId: {b.AuthorId}");
            }


        }
    }
}
