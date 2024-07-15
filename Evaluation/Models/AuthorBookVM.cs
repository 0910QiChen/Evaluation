using System;
using System.ComponentModel.DataAnnotations;

namespace Evaluation.Models
{
    public class AuthorBookVM
    {
        [Display(Name = "Book ID")]
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }
        [Display(Name = "Author ID")]
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
    }
}