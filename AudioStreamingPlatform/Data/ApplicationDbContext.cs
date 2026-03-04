using Microsoft.EntityFrameworkCore;
using AudioStreamingPlatform.Models;

namespace AudioStreamingPlatform.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<Song> Songs { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<ArtistGenre> ArtistGenres { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArtistGenre>()
                .HasKey(ag => new { ag.ArtistId, ag.GenreId });

            modelBuilder.Entity<ArtistGenre>()
                .HasOne(ag => ag.Artist)
                .WithMany(a => a.ArtistGenres)
                .HasForeignKey(ag => ag.ArtistId);

            modelBuilder.Entity<ArtistGenre>()
                .HasOne(ag => ag.Genre)
                .WithMany(g => g.ArtistGenres)
                .HasForeignKey(ag => ag.GenreId);
        }
    }
}