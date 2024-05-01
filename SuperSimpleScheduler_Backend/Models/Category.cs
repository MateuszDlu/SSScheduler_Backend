using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperSimpleScheduler_Backend.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set;}

        [Required(ErrorMessage = "Name is required")]
        string Name { get; set; }
        
        List<Task> Tasks { get; set; } = new List<Task>();

        [Required(ErrorMessage = "User is required")]
        User User { get; set; }
    }
}