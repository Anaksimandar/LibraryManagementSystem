using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem.Dto.Update
{
    public record NewBookDto
    {
        public string Title { get; init; }
        public int PublicationYear { get; init; }
        public int AuthorId { get; init; }
    }
}
