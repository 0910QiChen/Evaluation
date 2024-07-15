using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTOs
{
    public class BookDTO
    {
        [Key]
        public int BookID { get; set; }
        public int AuthorID { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
    }
} 