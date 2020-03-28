using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// to have   [Required] and [StringLength(255)] 
using System.ComponentModel.DataAnnotations;


namespace Vidly.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

    }
}
