using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MultimediaAPI.Models
{
    [Table("TableAlbum", Schema = "dbo")]
    public class Album
    {
        [Key, Column("Id")]
        public int Id { get; set; }
        [Column("Title"), Required]
        public string Title { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("Datetime"), Required]
        public DateTime DateCreated { get; set; }
        [NotMapped]
        public List<int> MediaId { get; set; }
    }
}
