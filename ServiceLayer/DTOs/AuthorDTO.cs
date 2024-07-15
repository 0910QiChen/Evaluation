using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTOs
{
    public class AuthorDTO
    {
        [Key]
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public virtual ICollection<BookDTO> BookDTOs { get; set; }
    }
}