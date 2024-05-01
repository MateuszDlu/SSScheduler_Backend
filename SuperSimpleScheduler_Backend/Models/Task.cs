using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperSimpleScheduler_Backend.Models
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set;}

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100)]
        string Title { get; set; }

        [StringLength(500)]
        string? Description { get; set; } = string.Empty;

        DateTime? Deadline { get; set; } = null;

        [Required(ErrorMessage = "Category is required")]
        Category Category { get; set; }
    }
}