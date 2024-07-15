using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Evaluation.Models
{
    public class AuthorVM
    {
        [Key]
        [Display(Name = "Author ID")]
        public int AuthorID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Country { get; set; }
        public virtual ICollection<BookVM> BookVMs { get; set; }
    }
}