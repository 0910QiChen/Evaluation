using System;
using System.ComponentModel.DataAnnotations;

namespace Evaluation.Models
{
    public class BookVM
    {
        [Key]
        public int BookID { get; set; }
        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Genre is Required")]
        public string Genre { get; set; }
        [Display(Name = "Publish Date")]
        [Required(ErrorMessage = "Publish Date is Required")]
        public DateTime PublishDate { get; set; }
        [Required]
        [Display(Name = "Author ID")]
        public int AuthorID { get; set; }
    }
}