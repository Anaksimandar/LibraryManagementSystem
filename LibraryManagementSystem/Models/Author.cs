using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfBirth { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
