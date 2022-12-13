using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultimediaAPI.Models
{
    [Table("TableMedia", Schema = "dbo")]
    public class Media
    {
        [Key, Column("Id")]
        public int Id { get; set; }
        [Column("TypeId"), Required, Range(1, int.MaxValue)]
        public int TypeId { get; set; }
        [Column("Title"), Required]
        public string Title { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("FileName"), Required]
        public string FileName { get; set; }
        [Column("Datetime"), Required]
        public DateTime DateCreated { get; set; }
        [NotMapped]
        public List<int> AlbumId { get; set; }
    }
}
