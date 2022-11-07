using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultimediaAPI.Models
{
    [Table("TableRelationship", Schema = "dbo")]
    public class Relationship
    {
        [Key, Column("Id")]
        public int Id { get; set; }
        [Column("MediaId"), Range(1, int.MaxValue)]
        public int MediaId { get; set; }
        [Column("AlbumId"), Range(1, int.MaxValue)]
        public int AlbumId { get; set; }
    }
}
