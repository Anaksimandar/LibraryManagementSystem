using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem.Dto.Update
{
    public record NewAuthorDto
    {
        public string Name { get; init; }
        public int YearOfBirth { get; init; }
    }
}
