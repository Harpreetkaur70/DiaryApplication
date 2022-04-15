using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryApplication.Models
{
    [Table("DiaryPosts")]
     public class DiaryPostEntity
     {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public string ImageUrl { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual DiaryApplicationUser DiaryApplicationUser { get; set; }
    }
}
