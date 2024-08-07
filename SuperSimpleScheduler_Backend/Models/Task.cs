using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SuperSimpleScheduler_Backend.Constants;

namespace SuperSimpleScheduler_Backend.Models
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set;}

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100)]
        public required string Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; } = string.Empty;

        [DateInFuture]
        public DateTime? Deadline { get; set; } = null;

        [Required(ErrorMessage = "Category is required")]
        [JsonIgnore]
        public Category Category { get; set; }
    }
}