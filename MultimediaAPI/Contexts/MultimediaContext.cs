using Microsoft.EntityFrameworkCore;
using MultimediaAPI.Models;

namespace MultimediaAPI.Contexts
{
    public class MultimediaContext : DbContext
    {
        public MultimediaContext(DbContextOptions options) : base(options) { }
        public DbSet<Media> MediaSet { get; set; }
        public DbSet<Album> AlbumSet { get; set; }
        public DbSet<Relationship> RelationSet { get; set; }
        public DbSet<User> UserSet { get; set; }
    }
}
