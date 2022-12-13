using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultimediaAPI.Models
{
    [Table("TableUser", Schema = "dbo")]
    public class User
    {
        [Key, Column("UserName")]
        public string UserName { get; set; }
        [Required, Column("Password")]
        public string Password { get; set; }
        [Column("EmailAddress")]
        public string EmailAddress { get; set; }
        [Required, Column("IsRoot"), Range(0, 1)]
        public int IsRoot { get; set; }
    }
}
