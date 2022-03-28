using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace newsportal.Models
{
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string PostDate { get; set; }
        
        [Required]
        public string PostTime { get; set; }
    
        [Required]
        [StringLength(400)]
        public string Title { get; set; }
        
        public int? LikeAmount { get; set; }
    
        public int? DislikeAmount { get; set; }
        
        public int? VisitedAmount { get; set; }

        [Required]
        public string FileName { get; set; }

        [NotMapped]
        public IFormFile FormFile { get; set; }
    }
}
